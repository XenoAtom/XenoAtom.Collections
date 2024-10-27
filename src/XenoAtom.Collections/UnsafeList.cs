// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XenoAtom.Collections;

/// <summary>
/// Lightweight struct list with optimized behavior over <see cref="List{T}"/>:
/// - by ref on this[int index]
/// - AddByRef(in T item)
/// - RemoveAt returning remove item T
/// - Settable Count
/// - Underlying array accessible (pinnable...etc.)
/// - Push/Pop methods
/// </summary>
/// <typeparam name="T">Type of an item</typeparam>
[DebuggerTypeProxy(typeof(UnsafeList<>.DebugListView)), DebuggerDisplay("Count = {_count}")]
public partial struct UnsafeList<T> : IEnumerable<T>, IUnsafeListBatch<T>
{
    private const int DefaultCapacity = 4;
    private T[] _items;
    private int _count;

    public UnsafeList() : this(0)
    {
    }

    public UnsafeList(int capacity)
    {
        _count = 0;
        _items = capacity == 0 ? Array.Empty<T>() : new T[capacity];
    }

    public UnsafeList(T[] items)
    {
        _items = items;
        _count = items.Length;
    }
    
    public readonly int Count => _count;
    
    public int Capacity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => _items.Length;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value <= _items.Length) return;
            EnsureCapacity(value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UnsafeSetCount(int count)
    {
        Capacity = count;
        _count = count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UnsafeSetCount(int capacity, int count)
    {
        Capacity = capacity;
        _count = count;
    }

    public readonly ref T UnsafeFirstRef
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref MemoryMarshal.GetArrayDataReference(_items);
    }

    public readonly Span<T> AsSpan() => new(_items, 0, (int)_count);

    public readonly bool IsReadOnly => false;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            Array.Clear(_items, 0, (int)_count);
        }
        _count = 0;
    }

    public readonly UnsafeList<T> Clone()
    {
        var items = (T[])_items.Clone();
        return new UnsafeList<T>() { _count = _count, _items = items };
    }

    public readonly bool Contains(T item) => _count > 0 && IndexOf(item) >= 0;

    public readonly void CopyTo(Span<T> array)
    {
        AsSpan().CopyTo(array);
    }

    public readonly void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (_count > 0)
        {
            System.Array.Copy(_items, 0, array, arrayIndex, _count);
        }
    }

    public readonly T[] ToArray()
    {
        var array = new T[_count];
        CopyTo(array, 0);
        return array;
    }

    public void Reset()
    {
        Clear();
        _count = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(T item)
    {
        var count = (nint)_count;
        var items = _items;
        if ((uint)count < (uint)items.Length)
        {
            Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(items), count) = item;
            _count++; // Don't reuse count as it generates another inc instruction on x86_64 and it performs worse than inc [memory]
        }
        else
        {
            ResizeAndAdd(item);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void ResizeAndAdd(T item)
    {
        var count = _count;
        EnsureCapacity(count + 1);
        UnsafeGetRefAt(count) = item;
        _count = count + 1;
    }

    public ref T UnsafeGetOrCreate(nint index)
    {
        if (index >= _count)
        {
            var newCount = index + 1;
            EnsureCapacity(newCount);
            _count = (int)newCount;
        }

        return ref UnsafeGetRefAt(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddByRef(in T item)
    {
        var count = (nint)_count;
        var items = _items;
        if ((uint)count < (uint)items.Length)
        {
            Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(items), count) = item;
            _count++; // Don't reuse count as it generates another inc instruction on x86_64 and it performs worse than inc [memory]
        }
        else
        {
            ResizeAndAdd(item);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Insert(nint index, T item)
    {
        if ((uint)index > (uint)_count) ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);

        var count = _count;
        if (count == _items.Length)
        {
            EnsureCapacity(count + 1);
        }
        if (index < count)
        {
            Array.Copy(_items, index, _items, index + 1, count - index);
        }
        UnsafeGetRefAt(index) = item;
        _count++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InsertByRef(nint index, in T item)
    {
        if ((uint)index > (uint)_count) ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);

        var count = _count;
        if (count == _items.Length)
        {
            EnsureCapacity(count + 1);
        }
        if (index < count)
        {
            Array.Copy(_items, index, _items, index + 1, count - index);
        }
        UnsafeGetRefAt(index) = item;
        _count++;
    }

    public bool Remove(T element)
    {
        var index = IndexOf(element);
        if (index >= 0)
        {
            RemoveAt(index);
            return true;
        }
        return false;
    }

    public void Sort(Comparison<T> comparison) => AsSpan().Sort(comparison);

    public void Sort<TComparer>() where TComparer : IComparer<T>, new() => Sort(new TComparer());
    
    public void Sort<TComparer>(TComparer comparer) where TComparer : IComparer<T>
    {
        AsSpan().Sort(comparer);
    }

    public void SortByRef<TComparer>(in TComparer comparer) where TComparer : struct, IComparerByRef<T>
    {
        AsSpan().SortByRef(comparer);
    }

    public readonly int IndexOf(T element) => Array.IndexOf(_items, element, 0, (int)_count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveAt(nint index)
    {
        if ((uint)index >= (uint)_count) ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
        _count--;
        if (index < _count)
        {
            Array.Copy(_items, index + 1, _items, index, _count - index);
        }

        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            UnsafeGetRefAt(_count) = default!;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T RemoveAtAndGet(nint index)
    {
        if ((uint)index >= (uint)_count) ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
        _count--;

        // previous children
        var item = UnsafeGetRefAt(index);
        if (index < _count)
        {
            Array.Copy(_items, index + 1, _items, index, _count - index);
        }
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            UnsafeGetRefAt(_count) = default!;
        }
        return item;
    }

    public T RemoveLast()
    {
        if (_count == 0) ThrowHelper.ThrowInvalidOperationRemoveOnEmptyList();

        _count--;
        ref var removed = ref UnsafeGetRefAt(_count);
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            removed = default!;
        }
        return removed;
    }

    public ref T this[nint index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if ((uint)index >= (uint)_count) ThrowHelper.ThrowIndexOutOfRangeException(index);
            return ref UnsafeGetRefAt(index);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ref T UnsafeGetRefAt(nint index) => ref Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_items), index);

    public void Push(T element)
    {
        Add(element);
    }

    public readonly ref T Peek()
    {
        if (_count == 0) ThrowHelper.ThrowInvalidOperationPeekOnEmptyList();
        return ref UnsafeGetRefAt(_count - 1);
    }

    public T Pop() => RemoveAtAndGet(_count - 1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T UnsafeGetBatch(int addCount, int additionalCapacity)
    {
        var count = (nint)_count;
        var items = _items;
        if (items.Length < count + addCount + additionalCapacity)
        {
            return ref SlowUnsafeGetBatch(addCount, additionalCapacity);
        }

        _count = (int)(count + addCount);
        return ref Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(items), count);
    }
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    private ref T SlowUnsafeGetBatch(int addCount, int additionalCapacity)
    {
        var count = _count;
        var newCapacity = count + addCount + additionalCapacity;
        EnsureCapacity(newCapacity);
        _count = count + addCount;
        return ref UnsafeGetRefAt(count);
    }

    private void EnsureCapacity(nint min)
    {
        var items = _items;
        Debug.Assert(min > items.Length);
        nint num = (items.Length == 0) ? DefaultCapacity : items.Length << 1;
        if (num < min)
        {
            num = min;
        }
        var destinationArray = new T[num];
        if (_count > 0)
        {
            Array.Copy(items, 0, destinationArray, 0, _count);
        }
        _items = destinationArray;
    }

    public Enumerator GetEnumerator() => new(this);

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator<T>
    {
        private readonly UnsafeList<T> _list;
        private nint _index;
        private T _current;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Enumerator(UnsafeList<T> list)
        {
            this._list = list;
            _index = 0;
            _current = default!;
        }

        public T Current => _current;

        object IEnumerator.Current => Current!;


        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_index < _list._count)
            {
                _current = _list.UnsafeGetRefAt(_index);
                _index++;
                return true;
            }
            return MoveNextRare();
        }

        private bool MoveNextRare()
        {
            _index = _list._count + 1;
            _current = default!;
            return false;
        }

        void IEnumerator.Reset()
        {
            _index = 0;
            _current = default!;
        }
    }

    [ExcludeFromCodeCoverage]
    private sealed class DebugListView(UnsafeList<T> collection)
    {
        private readonly UnsafeList<T> _collection = collection;

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                var array = new T[this._collection._count];
                _collection.CopyTo(array);
                return array;
            }
        }
    }
}