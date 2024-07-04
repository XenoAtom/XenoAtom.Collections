// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Runtime.CompilerServices;

namespace XenoAtom.Collections.Tests;


[TestClass]
public class FastSortTests
{
    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(4)]
    [DataRow(8)]
    [DataRow(15)]
    [DataRow(16)]
    [DataRow(17)]
    [DataRow(256)]
    [DataRow(512)]
    [DataRow(64 * 1024)] // Failing!
    public void TestSort(int size)
    {
        // Reverse order
        {
            var data = Enumerable.Range(0, size).Select(i => new Item(1024.0f - i, i)).ToArray();
            var copy = data.ToArray();
            AssertSort(data, copy);
        }

        // Random order with multiple seed
        for (int j = 0; j < 10; j++)
        {
            var random = new Random(j);
            var data = Enumerable.Range(0, size).Select(i => new Item(random.NextSingle(), i)).ToArray();
            var copy = data.ToArray();
            AssertSort(data, copy);
        }

        static void AssertSort(Item[] data, Item[] copy)
        {
            var span = data.AsSpan();
            span.Sort(new ItemComparer());
            var result1 = span.ToArray();

            copy.AsSpan().CopyTo(data);
            span.Sort((IComparer<Item>)new ItemComparer());
            var result2 = span.ToArray();

            if (data.Length < 32)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    Console.WriteLine($"[{i}] {result1[i]} <---> {result2[i]} (Original: {copy[i]})");
                }
            }

            CollectionAssert.AreEqual(result1, result2);
        }
    }

    public struct Item
    {
        public Item(float priority1, int index)
        {
            Priority1 = priority1;
            Index = index;
        }

        public float Priority1;

        public int Index;

        public override string ToString() => $"({Priority1}, {Index})";
    }

    private readonly struct ItemComparer : IComparerByRef<Item>, IComparer<Item>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool LessThan(in Item left, in Item right) => left.Priority1 < right.Priority1;

        public int Compare(Item x, Item y) => x.Priority1.CompareTo(y.Priority1);
    }
}