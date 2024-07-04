// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using BenchmarkDotNet.Attributes;

namespace XenoAtom.Collections.Bench;

public partial class BenchList
{
    private UnsafeList<int> _unsafeList;
    private readonly List<int> _list;

    public BenchList()
    {
        _unsafeList = new();
        _list = new();
    }

    [Benchmark(Description = "UnsafeList<int>.Add")]
    public void UnsafeList()
    {
        ref var list = ref _unsafeList;
        list.Clear();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.Add(5);
        list.Add(6);
        list.Add(7);
        list.Add(8);
        list.Add(10);
    }

    [Benchmark(Description = "List<int>.Add", Baseline = true)]
    public void List()
    {
        var list = _list;
        list.Clear();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.Add(5);
        list.Add(6);
        list.Add(7);
        list.Add(8);
        list.Add(10);
    }
}