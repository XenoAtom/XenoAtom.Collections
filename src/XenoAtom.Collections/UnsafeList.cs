// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XenoAtom.Collections;


public interface ICollectionBatchOutput<T>
{
    ref T UnsafeGetBatch(int additionalCount, int marginCount);
}


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
public struct UnsafeList<T> : IEnumerable<T>, ICollectionBatchOutput<T>
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

    public UnsafeList(T[] items, int count)
    {
        _items = items;
        _count = count;
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

    public ref T UnsafeFirstRef => ref MemoryMarshal.GetArrayDataReference(_items);

    public readonly Span<T> AsSpan() => new(_items, 0, (int)_count);

    public readonly bool IsReadOnly => false;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        if (_count > 0)
        {
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                Array.Clear(_items, 0, (int)_count);
            }
            _count = 0;
        }
    }

    public readonly UnsafeList<T> Clone()
    {
        var items = (T[])_items.Clone();
        return new UnsafeList<T>() { _count = _count, _items = items };
    }

    public readonly bool Contains(T item)
    {
        return _count > 0 && IndexOf(item) >= 0;
    }

    public readonly void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
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
        var count = _count;
        var items = _items;
        if ((uint)count < (uint)items.Length)
        {
            _count = count + 1;
            items[count] = item;
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
        _items[count] = item;
        _count = count + 1;
    }

    public ref T UnsafeGetOrCreate(int index)
    {
        if (index >= _count)
        {
            var newCount = index + 1;
            EnsureCapacity(newCount);
            _count = newCount;
        }

        return ref Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_items), index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddByRef(in T item)
    {
        var count = _count;
        var items = _items;
        if (count == items.Length)
        {
            EnsureCapacity(count + 1);
            items = _items;
        }
        items[count] = item;
        _count = count + 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Insert(int index, T item)
    {
        if (_count == _items.Length)
        {
            EnsureCapacity(_count + 1);
        }
        if (index < _count)
        {
            Array.Copy(_items, index, _items, index + 1, _count - index);
        }
        _items[index] = item;
        _count++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InsertByRef(int index, in T item)
    {
        if (_count == _items.Length)
        {
            EnsureCapacity(_count + 1);
        }
        if (index < _count)
        {
            Array.Copy(_items, index, _items, index + 1, _count - index);
        }
        _items[index] = item;
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

    public readonly int IndexOf(T element)
    {
        return Array.IndexOf(_items, element, 0, (int)_count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveAt(int index)
    {
        //Throw
        if (index >= _count) ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
        _count--;
        if (index < _count)
        {
            Array.Copy(_items, index + 1, _items, index, _count - index);
        }

        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            _items[_count] = default!;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T RemoveAtAndGet(int index)
    {
        //Throw
        if (index >= _count) ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
        _count--;
        // previous children
        var item = _items[index];
        if (index < _count)
        {
            Array.Copy(_items, index + 1, _items, index, _count - index);
        }
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            _items[_count] = default!;
        }
        return item;
    }

    public T RemoveLast()
    {
        if (_count > 0)
        {
            _count--;
            ref var removed = ref _items[_count];
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                removed = default!;
            }
            return removed;
        }
        return default!;
    }

    public ref T this[nint index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if ((uint)index >= (uint)_count) ThrowHelper.ThrowIndexOutOfRangeException(index);
            return ref Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_items), (IntPtr)index);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ref T UnsafeGetRefAt(int index) => ref Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_items), (IntPtr)index);

    public void Push(T element)
    {
        Add(element);
    }

    public readonly ref T Peek()
    {
        return ref _items[_count - 1];
    }

    public T Pop()
    {
        return RemoveAtAndGet(_count - 1);
    }

    private void EnsureCapacity(int min)
    {
        var items = _items;
        if (items.Length < min)
        {
            int num = (items.Length == 0) ? DefaultCapacity : items.Length << 1;
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
    }

    public Enumerator GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public struct Enumerator : IEnumerator<T>
    {
        private readonly UnsafeList<T> _list;
        private int _index;
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
                _current = _list[_index];
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

    private sealed class DebugListView
    {
        private readonly UnsafeList<T> _collection;

        public DebugListView(in UnsafeList<T> collection)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T UnsafeGetBatch(int addCount, int marginCount)
    {
        var count = _count;
        Capacity = _count + addCount + marginCount;
        _count = count + addCount;
        return ref UnsafeGetRefAt(count);
    }
}