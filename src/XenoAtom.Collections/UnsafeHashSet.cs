// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Collections;
using System.Runtime.CompilerServices;

namespace XenoAtom.Collections;

[DebuggerTypeProxy(typeof(UnsafeHashSet<>.DebugHashSetView))]
[DebuggerDisplay("Count = {Count}")]
[Serializable]
public struct UnsafeHashSet<T> : ICollection<T> where T: IEquatable<T>
{
    /// <summary>Cutoff point for stackallocs. This corresponds to the number of ints.</summary>
    private const int StackAllocThreshold = 100;

    /// <summary>
    /// When constructing a hashset from an existing collection, it may contain duplicates,
    /// so this is used as the max acceptable excess ratio of capacity to count. Note that
    /// this is only used on the ctor and not to automatically shrink if the hashset has, e.g,
    /// a lot of adds followed by removes. Users must explicitly shrink by calling TrimExcess.
    /// This is set to 3 because capacity is acceptable as 2x rounded up to nearest prime.
    /// </summary>
    private const int ShrinkThreshold = 3;
    private const int StartOfFreeList = -3;

    private int[]? _buckets;
    private Entry[]? _entries;
    private int _count;
    private int _freeList;
    private int _freeCount;

    #region Constructors

    public UnsafeHashSet() : this(0) { }


    public UnsafeHashSet(int capacity)
    {
        if (capacity < 0)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
        }

        if (capacity > 0)
        {
            Initialize(capacity);
        }
    }

    #endregion

    #region ICollection<T> methods

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void ICollection<T>.Add(T item) => AddIfNotPresent(item);

    /// <summary>Removes all elements from the <see cref="UnsafeHashSet{T}"/> object.</summary>
    public void Clear()
    {
        int count = _count;
        if (count > 0)
        {
            Debug.Assert(_buckets != null, "_buckets should be non-null");
            Debug.Assert(_entries != null, "_entries should be non-null");

            Array.Clear(_buckets);
            _count = 0;
            _freeList = -1;
            _freeCount = 0;
            Array.Clear(_entries, 0, count);
        }
    }

    /// <summary>Determines whether the <see cref="UnsafeHashSet{T}"/> contains the specified element.</summary>
    /// <param name="item">The element to locate in the <see cref="UnsafeHashSet{T}"/> object.</param>
    /// <returns>true if the <see cref="UnsafeHashSet{T}"/> object contains the specified element; otherwise, false.</returns>
    public readonly bool Contains(T item) => FindItemIndex(item) >= 0;

    /// <summary>Gets the index of the item in <see cref="_entries"/>, or -1 if it's not in the set.</summary>
    private readonly int FindItemIndex(T item)
    {
        int[]? buckets = _buckets;
        if (buckets != null)
        {
            Entry[]? entries = _entries;
            Debug.Assert(entries != null, "Expected _entries to be initialized");

            int hashCode = item!.GetHashCode();
            int i = GetBucketRef(hashCode) - 1; // Value in _buckets is 1-based
            while (i >= 0)
            {
                ref Entry entry = ref entries[i];
                if (entry.HashCode == hashCode && EqualityComparer<T>.Default.Equals(entry.Value, item))
                {
                    return i;
                }
                i = entry.Next;
            }
        }

        return -1;
    }

    /// <summary>Gets a reference to the specified hashcode's bucket, containing an index into <see cref="_entries"/>.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly ref int GetBucketRef(int hashCode)
    {
        int[] buckets = _buckets!;
        return ref buckets[(uint)hashCode % (uint)buckets.Length];
    }

    public bool Remove(T item)
    {
        if (_buckets != null)
        {
            Entry[]? entries = _entries;
            Debug.Assert(entries != null, "entries should be non-null");

            int last = -1;

            int hashCode = item?.GetHashCode() ?? 0;

            ref int bucket = ref GetBucketRef(hashCode);
            int i = bucket - 1; // Value in buckets is 1-based

            while (i >= 0)
            {
                ref Entry entry = ref entries[i];

                if (entry.HashCode == hashCode && entry.Value.Equals(item))
                {
                    if (last < 0)
                    {
                        bucket = entry.Next + 1; // Value in buckets is 1-based
                    }
                    else
                    {
                        entries[last].Next = entry.Next;
                    }

                    Debug.Assert((StartOfFreeList - _freeList) < 0, "shouldn't underflow because max hashtable length is MaxPrimeArrayLength = 0x7FEFFFFD(2146435069) _freelist underflow threshold 2147483646");
                    entry.Next = StartOfFreeList - _freeList;

                    if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                    {
                        entry.Value = default!;
                    }

                    _freeList = i;
                    _freeCount++;
                    return true;
                }

                last = i;
                i = entry.Next;
            }
        }

        return false;
    }

    /// <summary>Gets the number of elements that are contained in the set.</summary>
    public readonly int Count => _count - _freeCount;

    readonly bool ICollection<T>.IsReadOnly => false;

    #endregion

    #region IEnumerable methods

    public readonly Enumerator GetEnumerator() => new Enumerator(this);

    readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() =>
        Count == 0 ? Enumerable.Empty<T>().GetEnumerator() :
        GetEnumerator();

    readonly IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

    #endregion

    #region UnsafeHashSet methods

    /// <summary>Adds the specified element to the <see cref="UnsafeHashSet{T}"/>.</summary>
    /// <param name="item">The element to add to the set.</param>
    /// <returns>true if the element is added to the <see cref="UnsafeHashSet{T}"/> object; false if the element is already present.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Add(T item) => AddIfNotPresent(item);

    /// <summary>Searches the set for a given value and returns the equal value it finds, if any.</summary>
    /// <param name="equalValue">The value to search for.</param>
    /// <param name="actualValue">The value from the set that the search found, or the default value of <typeparamref name="T"/> when the search yielded no match.</param>
    /// <returns>A value indicating whether the search was successful.</returns>
    /// <remarks>
    /// This can be useful when you want to reuse a previously stored reference instead of
    /// a newly constructed one (so that more sharing of references can occur) or to look up
    /// a value that has more complete data than the value you currently have, although their
    /// comparer functions indicate they are equal.
    /// </remarks>
    public readonly bool TryGetValue(T equalValue, [MaybeNullWhen(false)] out T actualValue)
    {
        if (_buckets != null)
        {
            int index = FindItemIndex(equalValue);
            if (index >= 0)
            {
                actualValue = _entries![index].Value;
                return true;
            }
        }

        actualValue = default;
        return false;
    }

    public readonly void CopyTo(T[] array) => CopyTo(array, 0, Count);

    /// <summary>Copies the elements of a <see cref="UnsafeHashSet{T}"/> object to an array, starting at the specified array index.</summary>
    /// <param name="array">The destination array.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    public readonly void CopyTo(T[] array, int arrayIndex) => CopyTo(array, arrayIndex, Count);

    public readonly void CopyTo(T[] array, int arrayIndex, int count)
    {
        if (array == null)
        {
            ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
        }

        ArgumentOutOfRangeException.ThrowIfNegative(arrayIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        // Will the array, starting at arrayIndex, be able to hold elements? Note: not
        // checking arrayIndex >= array.Length (consistency with list of allowing
        // count of 0; subsequent check takes care of the rest)
        if (arrayIndex > array!.Length || count > array.Length - arrayIndex)
        {
            ThrowHelper.ThrowArgumentException(ThrowHelper.Arg_ArrayPlusOffTooSmall);
        }

        Entry[]? entries = _entries;
        for (int i = 0; i < _count && count != 0; i++)
        {
            ref Entry entry = ref entries![i];
            if (entry.Next >= -1)
            {
                array[arrayIndex++] = entry.Value;
                count--;
            }
        }
    }

    /// <summary>Removes all elements that match the conditions defined by the specified predicate from a <see cref="UnsafeHashSet{T}"/> collection.</summary>
    public int RemoveWhere(Predicate<T> match)
    {
        if (match == null)
        {
            ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
        }

        Entry[]? entries = _entries;
        int numRemoved = 0;
        for (int i = 0; i < _count; i++)
        {
            ref Entry entry = ref entries![i];
            if (entry.Next >= -1)
            {
                // Cache value in case delegate removes it
                T value = entry.Value;
                if (match!(value))
                {
                    // Check again that remove actually removed it.
                    if (Remove(value))
                    {
                        numRemoved++;
                    }
                }
            }
        }

        return numRemoved;
    }

    /// <summary>Ensures that this hash set can hold the specified number of elements without growing.</summary>
    public int EnsureCapacity(int capacity)
    {
        if (capacity < 0)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
        }

        int currentCapacity = _entries == null ? 0 : _entries.Length;
        if (currentCapacity >= capacity)
        {
            return currentCapacity;
        }

        if (_buckets == null)
        {
            return Initialize(capacity);
        }

        int newSize = HashHelpers.GetPrime(capacity);
        Resize(newSize, forceNewHashCodes: false);
        return newSize;
    }

    private void Resize() => Resize(HashHelpers.ExpandPrime(_count), forceNewHashCodes: false);

    private void Resize(int newSize, bool forceNewHashCodes)
    {
        // Value types never rehash
        Debug.Assert(!forceNewHashCodes || !typeof(T).IsValueType);
        Debug.Assert(_entries != null, "_entries should be non-null");
        Debug.Assert(newSize >= _entries.Length);

        var entries = new Entry[newSize];

        int count = _count;
        Array.Copy(_entries, entries, count);

        if (!typeof(T).IsValueType && forceNewHashCodes)
        {
            for (int i = 0; i < count; i++)
            {
                ref Entry entry = ref entries[i];
                if (entry.Next >= -1)
                {
                    entry.HashCode = entry.Value?.GetHashCode() ?? 0;
                }
            }
        }

        // Assign member variables after both arrays allocated to guard against corruption from OOM if second fails
        _buckets = new int[newSize];
        for (int i = 0; i < count; i++)
        {
            ref Entry entry = ref entries[i];
            if (entry.Next >= -1)
            {
                ref int bucket = ref GetBucketRef(entry.HashCode);
                entry.Next = bucket - 1; // Value in _buckets is 1-based
                bucket = i + 1;
            }
        }

        _entries = entries;
    }

    /// <summary>
    /// Sets the capacity of a <see cref="UnsafeHashSet{T}"/> object to the actual number of elements it contains,
    /// rounded up to a nearby, implementation-specific value.
    /// </summary>
    public void TrimExcess()
    {
        int capacity = Count;

        int newSize = HashHelpers.GetPrime(capacity);
        Entry[]? oldEntries = _entries;
        int currentCapacity = oldEntries == null ? 0 : oldEntries.Length;
        if (newSize >= currentCapacity)
        {
            return;
        }

        int oldCount = _count;
        Initialize(newSize);
        Entry[]? entries = _entries;
        int count = 0;
        for (int i = 0; i < oldCount; i++)
        {
            int hashCode = oldEntries![i].HashCode; // At this point, we know we have entries.
            if (oldEntries[i].Next >= -1)
            {
                ref Entry entry = ref entries![count];
                entry = oldEntries[i];
                ref int bucket = ref GetBucketRef(hashCode);
                entry.Next = bucket - 1; // Value in _buckets is 1-based
                bucket = count + 1;
                count++;
            }
        }

        _count = capacity;
        _freeCount = 0;
    }

    #endregion

    #region Helper methods

    /// <summary>
    /// Initializes buckets and slots arrays. Uses suggested capacity by finding next prime
    /// greater than or equal to capacity.
    /// </summary>
    private int Initialize(int capacity)
    {
        int size = HashHelpers.GetPrime(capacity);
        var buckets = new int[size];
        var entries = new Entry[size];

        // Assign member variables after both arrays are allocated to guard against corruption from OOM if second fails.
        _freeList = -1;
        _buckets = buckets;
        _entries = entries;
        return size;
    }

    /// <summary>Adds the specified element to the set if it's not already contained.</summary>
    /// <param name="value">The element to add to the set.</param>
    /// <param name="location">The index into <see cref="_entries"/> of the element.</param>
    /// <returns>true if the element is added to the <see cref="UnsafeHashSet{T}"/> object; false if the element is already present.</returns>
    private bool AddIfNotPresent(T value)
    {
        if (_buckets == null)
        {
            Initialize(0);
        }
        Debug.Assert(_buckets != null);

        Entry[]? entries = _entries;
        Debug.Assert(entries != null, "expected entries to be non-null");

        int hashCode;

        ref int bucket = ref Unsafe.NullRef<int>();

        hashCode = value!.GetHashCode();
        bucket = ref GetBucketRef(hashCode);
        int i = bucket - 1; // Value in _buckets is 1-based

        // ValueType: Devirtualize with EqualityComparer<TValue>.Default intrinsic
        while (i >= 0)
        {
            ref Entry entry = ref entries[i];
            if (entry.HashCode == hashCode && entry.Value.Equals(value))
            {
                //location = i;
                return false;
            }
            i = entry.Next;
        }

        int index;
        if (_freeCount > 0)
        {
            index = _freeList;
            _freeCount--;
            Debug.Assert((StartOfFreeList - entries![_freeList].Next) >= -1, "shouldn't overflow because `next` cannot underflow");
            _freeList = StartOfFreeList - entries[_freeList].Next;
        }
        else
        {
            int count = _count;
            if (count == entries.Length)
            {
                Resize();
                bucket = ref GetBucketRef(hashCode);
            }
            index = count;
            _count = count + 1;
            entries = _entries;
        }

        {
            ref Entry entry = ref entries![index];
            entry.HashCode = hashCode;
            entry.Next = bucket - 1; // Value in _buckets is 1-based
            entry.Value = value;
            bucket = index + 1;
            //location = index;
        }

        return true;
    }
 
    #endregion

    private struct Entry
    {
        public int HashCode;
        /// <summary>
        /// 0-based index of next entry in chain: -1 means end of chain
        /// also encodes whether this entry _itself_ is part of the free list by changing sign and subtracting 3,
        /// so -2 means end of free list, -3 means index 0 but on free list, -4 means index 1 but on free list, etc.
        /// </summary>
        public int Next;
        public T Value;
    }

    public struct Enumerator : IEnumerator<T>
    {
        private readonly UnsafeHashSet<T> _hashSet;
        private int _index;
        private T _current;

        internal Enumerator(UnsafeHashSet<T> hashSet)
        {
            _hashSet = hashSet;
            _index = 0;
            _current = default!;
        }

        public bool MoveNext()
        {
            // Use unsigned comparison since we set index to dictionary.count+1 when the enumeration ends.
            // dictionary.count+1 could be negative if dictionary.count is int.MaxValue
            while ((uint)_index < (uint)_hashSet._count)
            {
                ref Entry entry = ref _hashSet._entries![_index++];
                if (entry.Next >= -1)
                {
                    _current = entry.Value;
                    return true;
                }
            }

            _index = _hashSet._count + 1;
            _current = default!;
            return false;
        }

        public T Current => _current;

        public void Dispose() { }

        object? IEnumerator.Current
        {
            get
            {
                if (_index == 0 || (_index == _hashSet._count + 1))
                {
                    ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                }

                return _current;
            }
        }

        void IEnumerator.Reset()
        {
            _index = 0;
            _current = default!;
        }
    }

    private sealed class DebugHashSetView
    {
        private readonly UnsafeHashSet<T> _collection;

        public DebugHashSetView(UnsafeHashSet<T> collection)
        {
            this._collection = collection;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                var array = new T[this._collection._count];
                _collection.CopyTo(array, 0);
                return array;
            }
        }
    }
}