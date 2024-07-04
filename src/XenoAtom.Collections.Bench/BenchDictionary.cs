// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using BenchmarkDotNet.Attributes;

namespace XenoAtom.Collections.Bench;

public partial class BenchDictionary
{
    private UnsafeDictionary<int, int> _unsafeDictionary;
    private readonly Dictionary<int, int> _dictionary;

    public BenchDictionary()
    {
        _unsafeDictionary = new();
        _dictionary = new();
    }

    [Benchmark(Description = "UnsafeDictionary<int, int>")]
    public int UnsafeList()
    {
        ref var dict = ref _unsafeDictionary;
        dict.Clear();
        dict.Add(1, 1);
        dict.Add(2, 2);
        dict.Add(3, 3);
        dict.Add(4, 4);
        dict.Add(5, 5);
        dict.Add(6, 6);
        dict.Add(7, 7);
        dict.Add(8, 8);
        dict.Add(10, 10);
        var result = dict[1] + dict[2] + dict[3] + dict[4] + dict[5] + dict[6] + dict[7] + dict[8] + dict[10];
        return result;
    }

    [Benchmark(Description = "Dictionary<int, int>", Baseline = true)]
    public int List()
    {
        var dict = _dictionary;
        dict.Clear();
        dict.Add(1, 1);
        dict.Add(2, 2);
        dict.Add(3, 3);
        dict.Add(4, 4);
        dict.Add(5, 5);
        dict.Add(6, 6);
        dict.Add(7, 7);
        dict.Add(8, 8);
        dict.Add(10, 10);
        var result = dict[1] + dict[2] + dict[3] + dict[4] + dict[5] + dict[6] + dict[7] + dict[8] + dict[10];
        return result;
    }
}