// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace XenoAtom.Collections.Bench;

[MemoryDiagnoser]
public class BenchSort
{
    private Item[] _data;
    private Item[] _copy;

    public BenchSort()
    {
        _data = Array.Empty<Item>();
        _copy = Array.Empty<Item>();
    }

    public Item[] Data => _data;

    [Params(3, 8, 16, 256)]
    public int ParamSize { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _data = Enumerable.Range(0, ParamSize).Select(i => new Item(1024.0f - i, i)).ToArray();
        _copy = _data.ToArray();
    }

    [Benchmark]
    public void SortByRef()
    {
        _copy.AsSpan().CopyTo(_data);
        _data.AsSpan().SortByRef(new ItemComparerByRef());
    }

    [Benchmark(Baseline = true)]
    public void Sort()
    {
        _copy.AsSpan().CopyTo(_data);
        _data.AsSpan().Sort(ItemComparer.Instance);
    }

    public struct Item
    {
        public Item(float priority, int index)
        {
            Priority = priority;
            Index = index;
        }

        public float Priority;
        public int Index;
        public int NotUsed1;
        public int NotUsed2;
    }

    private class ItemComparer : IComparer<Item>
    {
        public static readonly ItemComparer Instance = new ItemComparer();

        public int Compare(Item x, Item y) => x.Priority.CompareTo(y.Priority);
    }

    private readonly struct ItemComparerByRef : IComparerByRef<Item>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool LessThan(in Item left, in Item right) => left.Priority < right.Priority;
    }
}