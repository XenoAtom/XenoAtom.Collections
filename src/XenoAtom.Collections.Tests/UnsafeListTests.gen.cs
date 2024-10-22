// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Collections;

namespace XenoAtom.Collections.Tests;



[TestClass]
public class UnsafeListTests
{
    [TestMethod]
    public void TestSimple()
    {
        var list = new UnsafeList<int>();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(0, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(4, list.Capacity);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
        list[0] = 5;
        Assert.AreEqual(5, list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(4, list.Capacity);
    }

    [TestMethod]
    public void TestSimpleWithString()
    {
        var list = new UnsafeList<string>();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(0, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(4, list.Capacity);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
        list[0] = "5";
        Assert.AreEqual("5", list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestResize()
    {
        var list = new UnsafeList<int>();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(0, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestResizeWithString()
    {
        var list = new UnsafeList<string>();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(0, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        list.Add("5");
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual("5", list[4]);
    }

    [TestMethod]
    public void TestResizeWithZeroCapacity()
    {
        var list = new UnsafeList<int>();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        Assert.AreEqual(1, list.Count);
        list.Add(2);
        Assert.AreEqual(2, list.Count);
        list.Add(3);
        Assert.AreEqual(3, list.Count);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestRemove()
    {
        var list = new UnsafeList<int>();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(0, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        Assert.IsFalse(list.Remove(5));
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(4, list.Capacity);
        Assert.IsTrue(list.Remove(2));
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(3, list[1]);
        Assert.AreEqual(4, list[2]);
        Assert.IsTrue(list.Remove(1));
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(4, list[1]);
        Assert.IsTrue(list.Remove(4));
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.IsTrue(list.Remove(3));
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveString()
    {
        var list = new UnsafeList<string>();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(0, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(4, list.Capacity);
        list.Remove("2");
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("3", list[1]);
        Assert.AreEqual("4", list[2]);
        list.Remove("1");
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("4", list[1]);
        list.Remove("4");
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("3", list[0]);
        list.Remove("3");
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveAt()
    {
        var list = new UnsafeList<int>();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.RemoveAt(2);
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(4, list[2]);
        list.RemoveAt(0);
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(2, list[0]);
        Assert.AreEqual(4, list[1]);
        list.RemoveAt(1);
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(2, list[0]);
        list.RemoveAt(0);
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestInsert()
    {
        var list = new UnsafeList<int>();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(4, list.Capacity);
        list.Insert(2, 5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(8, list.Capacity);
    }

    [TestMethod]
    public void TestIndexOf()
    {
        var list = new UnsafeList<int>();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(0, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        var indexOf2 = list.IndexOf(2);
        Assert.AreEqual(1, indexOf2);
        var indexOf5 = list.IndexOf(5);
        Assert.AreEqual(-1, indexOf5);
    }

    [TestMethod]
    public void TestContains()
    {
        var list = new UnsafeList<int>();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.IsTrue(list.Contains(2));
        Assert.IsFalse(list.Contains(5));
    }

    [TestMethod]
    public void TestCopyTo()
    {
        var list = new UnsafeList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
                var array = new int[4];
        list.CopyTo(array, 0);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, array);
                var array2 = new int[4];
        list.CopyTo(array2);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, array2);
    }

    [TestMethod]
    public void TestEnumerator()
    {
        var list = new UnsafeList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorGeneric()
    {
        var list = new UnsafeList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable<int>)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric()
    {
        var list = new UnsafeList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric2()
    {
        var list = new UnsafeList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        var enumerator = ((IEnumerable)list).GetEnumerator();
        while (enumerator.MoveNext())
        {
            Assert.AreEqual(list[index], enumerator.Current);
            index++;
        }
    }

    [TestMethod]
    public void TestRemoveAtAndGet()
    {
        var list = new UnsafeList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(1, list.RemoveAtAndGet(0));
        Assert.AreEqual(2, list.RemoveAtAndGet(0));
        Assert.AreEqual(4, list.RemoveAtAndGet(1));
        Assert.AreEqual(3, list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestRemoveAtAndGetString()
    {
        var list = new UnsafeList<string>();
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual("1", list.RemoveAtAndGet(0));
        Assert.AreEqual("2", list.RemoveAtAndGet(0));
        Assert.AreEqual("4", list.RemoveAtAndGet(1));
        Assert.AreEqual("3", list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestInsertByRef()
    {
        var list = new UnsafeList<int>();
        int item = 1;
        list.InsertByRef(0, item);
        item = 2;
        list.InsertByRef(0, item);
        item = 3;
        list.InsertByRef(0, item);
        item = 4;
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(1, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestInsertByRefWithString()
    {
        var list = new UnsafeList<string>();
        Assert.AreEqual(0, list.Count);
        string item = "1";
        list.InsertByRef(0, item);
        item = "2";
        list.InsertByRef(0, item);
        item = "3";
        list.InsertByRef(0, item);
        item = "4";
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("1", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestPushPeekPop()
    {
        var list = new UnsafeList<int>();
        list.Push(1);
        list.Push(2);
        list.Push(3);
        list.Push(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(4, list.Capacity);
        Assert.AreEqual(4, list.Peek());
        Assert.AreEqual(4, list.Pop());
        Assert.AreEqual(3, list.Pop());
        Assert.AreEqual(2, list.Pop());
        Assert.AreEqual(1, list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(4, list.Capacity);
    }

    [TestMethod]
    public void TestPushPeekPopString()
    {
        var list = new UnsafeList<string>();
        Assert.AreEqual(0, list.Count);
        list.Push("1");
        list.Push("2");
        list.Push("3");
        list.Push("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(4, list.Capacity);
        Assert.AreEqual("4", list.Peek());
        Assert.AreEqual("4", list.Pop());
        Assert.AreEqual("3", list.Pop());
        Assert.AreEqual("2", list.Pop());
        Assert.AreEqual("1", list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(4, list.Capacity);
    }

    [TestMethod]
    public void TestAddByRef()
    {
        var list = new UnsafeList<int>();
        Assert.AreEqual(0, list.Count);
        int item = 1;
        list.AddByRef(item);
        item = 2;
        list.AddByRef(item);
        item = 3;
        list.AddByRef(item);
        item = 4;
        list.AddByRef(item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestAsSpan()
    {
        var list = new UnsafeList<int>();
        Assert.AreEqual(0, list.AsSpan().Length);
        list.Add(1);
        list.Add(2);

        var span = list.AsSpan();
        Assert.AreEqual(2, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);

        list.Add(3);
        list.Add(4);
        span = list.AsSpan();
        Assert.AreEqual(4, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);
        Assert.AreEqual(3, span[2]);
        Assert.AreEqual(4, span[3]);
    }

    [TestMethod]
    public void TestUnsafeGetRefAt()
    {
        var list = new UnsafeList<int>()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(1, list.UnsafeGetRefAt(0));
        Assert.AreEqual(2, list.UnsafeGetRefAt(1));
        Assert.AreEqual(3, list.UnsafeGetRefAt(2));
        Assert.AreEqual(4, list.UnsafeGetRefAt(3));
    }

    [TestMethod]
    public void TestRemoveLast()
    {
        var list = new UnsafeList<int>()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(1, list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveLastString()
    {
        var list = new UnsafeList<string>()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("1", list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestReset()
    {
        var list = new UnsafeList<int>()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Reset();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClear()
    {
        var list = new UnsafeList<int>()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClearWithString()
    {
        var list = new UnsafeList<string>()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

        [TestMethod]
    public void TestCapacity()
    {
        var list = new UnsafeList<int>(4);
        Assert.AreEqual(4, list.Capacity);
        list.Capacity = 8;
        Assert.AreEqual(8, list.Capacity);
        list.Capacity = 2;
        Assert.AreEqual(8, list.Capacity);
    }
    
    [TestMethod]
    public void TestUnsafeGetOrCreate()
    {
        var list = new UnsafeList<int>();
        Assert.AreEqual(0, list.Count);
        ref var item1 = ref list.UnsafeGetOrCreate(0);
        item1 = 1;
        ref var item2 = ref list.UnsafeGetOrCreate(1);
        item2 = 2;
        ref var item3 = ref list.UnsafeGetOrCreate(2);
        item3 = 3;
        ref var item4 = ref list.UnsafeGetOrCreate(3);
        item4 = 4;
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestUnsafeSetCount()
    {
        var list = new UnsafeList<int>(4);
        list.UnsafeSetCount(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(4 , list.Capacity);

                list.UnsafeSetCount(16, 8);
        Assert.AreEqual(8, list.Count);
        Assert.AreEqual(16, list.Capacity);
            }

    [TestMethod]
    public void TestClone()
    {
        var list = new UnsafeList<int>()
        {
            1,
            2,
            3,
            4
        };
        var clone = list.Clone();
        Assert.AreEqual(4, clone.Count);
        Assert.AreEqual(4, clone.Capacity);
        Assert.AreEqual(1, clone[0]);
        Assert.AreEqual(2, clone[1]);
        Assert.AreEqual(3, clone[2]);
        Assert.AreEqual(4, clone[3]);
        list[0] = 5;
        Assert.AreEqual(1, clone[0]);
    }

    [TestMethod]
    public void TestIsReadOnly()
    {
        var list = new UnsafeList<int>();
        Assert.IsFalse(list.IsReadOnly);
    }

    [TestMethod]
    public void TestUnsafeFirstRef()
    {
        var list = new UnsafeList<int>()
        {
            1,
            2,
            3,
            4
        };
        ref var first = ref list.UnsafeFirstRef;
        Assert.AreEqual(1, first);
        first = 5;
        Assert.AreEqual(5, list[0]);
    }

    [TestMethod]
    public void TestArgumentExceptions()
    {
        var list = new UnsafeList<int>();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(2, 1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[0] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[1] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[4] = 1);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(2, 1));
        Assert.ThrowsException<InvalidOperationException>(() => list.RemoveLast());
        Assert.ThrowsException<InvalidOperationException>(() => list.Peek());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Pop());
    }

        [TestMethod]
    public void TestConstructorWithArray()
    {
        var list = new UnsafeList<int>([1, 2, 3, 4]);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(4, list.Capacity);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestUnsafeGetBatch()
    {
        var list = new UnsafeList<int>();
        ref var firstItem = ref list.UnsafeGetBatch(2, 3);
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(5, list.Capacity);
        firstItem = 1;
        Assert.AreEqual(1, list[0]);
    }
    
    [TestMethod]
    public void TestSort()
    {
        var list = new UnsafeList<int>()
        {
            4,
            3,
            2,
            1
        };
        list.Sort((left, right) => left.CompareTo(right));
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestSortStringWithComparer()
    {
        var list = new UnsafeList<string>()
        {
            "4",
            "3",
            "2",
            "1"
        };
        list.Sort(StringComparer.Ordinal);

        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestSortByRef()
    {
        var list = new UnsafeList<int>()
        {
            4,
            3,
            2,
            1
        };
        list.SortByRef(new IntComparerByRef());
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }
    
    private struct IntComparerByRef : IComparerByRef<int>
    {
        public bool LessThan(in int left, in int right) => left.CompareTo(right) < 0;
    }
}

[TestClass]
public class UnsafeListTestsN8
{
    [TestMethod]
    public void TestSimple()
    {
        var list = new UnsafeList<int>.N8();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(8, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(8, list.Capacity);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
        list[0] = 5;
        Assert.AreEqual(5, list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(8, list.Capacity);
    }

    [TestMethod]
    public void TestSimpleWithString()
    {
        var list = new UnsafeList<string>.N8();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(8, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(8, list.Capacity);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
        list[0] = "5";
        Assert.AreEqual("5", list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestResize()
    {
        var list = new UnsafeList<int>.N8();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(8, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestResizeWithString()
    {
        var list = new UnsafeList<string>.N8();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(8, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        list.Add("5");
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual("5", list[4]);
    }

    [TestMethod]
    public void TestResizeWithZeroCapacity()
    {
        var list = new UnsafeList<int>.N8();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        Assert.AreEqual(1, list.Count);
        list.Add(2);
        Assert.AreEqual(2, list.Count);
        list.Add(3);
        Assert.AreEqual(3, list.Count);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestRemove()
    {
        var list = new UnsafeList<int>.N8();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(8, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        Assert.IsFalse(list.Remove(5));
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(8, list.Capacity);
        Assert.IsTrue(list.Remove(2));
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(3, list[1]);
        Assert.AreEqual(4, list[2]);
        Assert.IsTrue(list.Remove(1));
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(4, list[1]);
        Assert.IsTrue(list.Remove(4));
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.IsTrue(list.Remove(3));
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveString()
    {
        var list = new UnsafeList<string>.N8();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(8, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(8, list.Capacity);
        list.Remove("2");
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("3", list[1]);
        Assert.AreEqual("4", list[2]);
        list.Remove("1");
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("4", list[1]);
        list.Remove("4");
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("3", list[0]);
        list.Remove("3");
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveAt()
    {
        var list = new UnsafeList<int>.N8();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.RemoveAt(2);
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(4, list[2]);
        list.RemoveAt(0);
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(2, list[0]);
        Assert.AreEqual(4, list[1]);
        list.RemoveAt(1);
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(2, list[0]);
        list.RemoveAt(0);
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestInsert()
    {
        var list = new UnsafeList<int>.N8();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(8, list.Capacity);
        list.Insert(2, 5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(8, list.Capacity);
    }

    [TestMethod]
    public void TestIndexOf()
    {
        var list = new UnsafeList<int>.N8();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(8, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        var indexOf2 = list.IndexOf(2);
        Assert.AreEqual(1, indexOf2);
        var indexOf5 = list.IndexOf(5);
        Assert.AreEqual(-1, indexOf5);
    }

    [TestMethod]
    public void TestContains()
    {
        var list = new UnsafeList<int>.N8();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.IsTrue(list.Contains(2));
        Assert.IsFalse(list.Contains(5));
    }

    [TestMethod]
    public void TestCopyTo()
    {
        var list = new UnsafeList<int>.N8();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
                var array2 = new int[4];
        list.CopyTo(array2);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, array2);
    }

    [TestMethod]
    public void TestEnumerator()
    {
        var list = new UnsafeList<int>.N8();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorGeneric()
    {
        var list = new UnsafeList<int>.N8();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable<int>)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric()
    {
        var list = new UnsafeList<int>.N8();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric2()
    {
        var list = new UnsafeList<int>.N8();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        var enumerator = ((IEnumerable)list).GetEnumerator();
        while (enumerator.MoveNext())
        {
            Assert.AreEqual(list[index], enumerator.Current);
            index++;
        }
    }

    [TestMethod]
    public void TestRemoveAtAndGet()
    {
        var list = new UnsafeList<int>.N8();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(1, list.RemoveAtAndGet(0));
        Assert.AreEqual(2, list.RemoveAtAndGet(0));
        Assert.AreEqual(4, list.RemoveAtAndGet(1));
        Assert.AreEqual(3, list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestRemoveAtAndGetString()
    {
        var list = new UnsafeList<string>.N8();
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual("1", list.RemoveAtAndGet(0));
        Assert.AreEqual("2", list.RemoveAtAndGet(0));
        Assert.AreEqual("4", list.RemoveAtAndGet(1));
        Assert.AreEqual("3", list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestInsertByRef()
    {
        var list = new UnsafeList<int>.N8();
        int item = 1;
        list.InsertByRef(0, item);
        item = 2;
        list.InsertByRef(0, item);
        item = 3;
        list.InsertByRef(0, item);
        item = 4;
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(1, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestInsertByRefWithString()
    {
        var list = new UnsafeList<string>.N8();
        Assert.AreEqual(0, list.Count);
        string item = "1";
        list.InsertByRef(0, item);
        item = "2";
        list.InsertByRef(0, item);
        item = "3";
        list.InsertByRef(0, item);
        item = "4";
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("1", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestPushPeekPop()
    {
        var list = new UnsafeList<int>.N8();
        list.Push(1);
        list.Push(2);
        list.Push(3);
        list.Push(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(8, list.Capacity);
        Assert.AreEqual(4, list.Peek());
        Assert.AreEqual(4, list.Pop());
        Assert.AreEqual(3, list.Pop());
        Assert.AreEqual(2, list.Pop());
        Assert.AreEqual(1, list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(8, list.Capacity);
    }

    [TestMethod]
    public void TestPushPeekPopString()
    {
        var list = new UnsafeList<string>.N8();
        Assert.AreEqual(0, list.Count);
        list.Push("1");
        list.Push("2");
        list.Push("3");
        list.Push("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(8, list.Capacity);
        Assert.AreEqual("4", list.Peek());
        Assert.AreEqual("4", list.Pop());
        Assert.AreEqual("3", list.Pop());
        Assert.AreEqual("2", list.Pop());
        Assert.AreEqual("1", list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(8, list.Capacity);
    }

    [TestMethod]
    public void TestAddByRef()
    {
        var list = new UnsafeList<int>.N8();
        Assert.AreEqual(0, list.Count);
        int item = 1;
        list.AddByRef(item);
        item = 2;
        list.AddByRef(item);
        item = 3;
        list.AddByRef(item);
        item = 4;
        list.AddByRef(item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestAsSpan()
    {
        var list = new UnsafeList<int>.N8();
        Assert.AreEqual(0, list.AsSpan().Length);
        list.Add(1);
        list.Add(2);

        var span = list.AsSpan();
        Assert.AreEqual(2, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);

        list.Add(3);
        list.Add(4);
        span = list.AsSpan();
        Assert.AreEqual(4, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);
        Assert.AreEqual(3, span[2]);
        Assert.AreEqual(4, span[3]);
    }

    [TestMethod]
    public void TestUnsafeGetRefAt()
    {
        var list = new UnsafeList<int>.N8()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(1, list.UnsafeGetRefAt(0));
        Assert.AreEqual(2, list.UnsafeGetRefAt(1));
        Assert.AreEqual(3, list.UnsafeGetRefAt(2));
        Assert.AreEqual(4, list.UnsafeGetRefAt(3));
    }

    [TestMethod]
    public void TestRemoveLast()
    {
        var list = new UnsafeList<int>.N8()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(1, list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveLastString()
    {
        var list = new UnsafeList<string>.N8()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("1", list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestReset()
    {
        var list = new UnsafeList<int>.N8()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Reset();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClear()
    {
        var list = new UnsafeList<int>.N8()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClearWithString()
    {
        var list = new UnsafeList<string>.N8()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    
    [TestMethod]
    public void TestUnsafeGetOrCreate()
    {
        var list = new UnsafeList<int>.N8();
        Assert.AreEqual(0, list.Count);
        ref var item1 = ref list.UnsafeGetOrCreate(0);
        item1 = 1;
        ref var item2 = ref list.UnsafeGetOrCreate(1);
        item2 = 2;
        ref var item3 = ref list.UnsafeGetOrCreate(2);
        item3 = 3;
        ref var item4 = ref list.UnsafeGetOrCreate(3);
        item4 = 4;
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestUnsafeSetCount()
    {
        var list = new UnsafeList<int>.N8();
        list.UnsafeSetCount(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(8 , list.Capacity);

            }

    [TestMethod]
    public void TestClone()
    {
        var list = new UnsafeList<int>.N8()
        {
            1,
            2,
            3,
            4
        };
        var clone = list.Clone();
        Assert.AreEqual(4, clone.Count);
        Assert.AreEqual(8, clone.Capacity);
        Assert.AreEqual(1, clone[0]);
        Assert.AreEqual(2, clone[1]);
        Assert.AreEqual(3, clone[2]);
        Assert.AreEqual(4, clone[3]);
        list[0] = 5;
        Assert.AreEqual(1, clone[0]);
    }

    [TestMethod]
    public void TestIsReadOnly()
    {
        var list = new UnsafeList<int>.N8();
        Assert.IsFalse(list.IsReadOnly);
    }

    [TestMethod]
    public void TestUnsafeFirstRef()
    {
        var list = new UnsafeList<int>.N8()
        {
            1,
            2,
            3,
            4
        };
        ref var first = ref list.UnsafeFirstRef;
        Assert.AreEqual(1, first);
        first = 5;
        Assert.AreEqual(5, list[0]);
    }

    [TestMethod]
    public void TestArgumentExceptions()
    {
        var list = new UnsafeList<int>.N8();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(2, 1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[0] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[1] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[4] = 1);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(2, 1));
        Assert.ThrowsException<InvalidOperationException>(() => list.RemoveLast());
        Assert.ThrowsException<InvalidOperationException>(() => list.Peek());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Pop());
    }

    
    [TestMethod]
    public void TestSort()
    {
        var list = new UnsafeList<int>.N8()
        {
            4,
            3,
            2,
            1
        };
        list.Sort((left, right) => left.CompareTo(right));
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestSortStringWithComparer()
    {
        var list = new UnsafeList<string>.N8()
        {
            "4",
            "3",
            "2",
            "1"
        };
        list.Sort(StringComparer.Ordinal);

        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestSortByRef()
    {
        var list = new UnsafeList<int>.N8()
        {
            4,
            3,
            2,
            1
        };
        list.SortByRef(new IntComparerByRef());
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }
    
    private struct IntComparerByRef : IComparerByRef<int>
    {
        public bool LessThan(in int left, in int right) => left.CompareTo(right) < 0;
    }
}

[TestClass]
public class UnsafeListTestsN16
{
    [TestMethod]
    public void TestSimple()
    {
        var list = new UnsafeList<int>.N16();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(16, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(16, list.Capacity);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
        list[0] = 5;
        Assert.AreEqual(5, list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(16, list.Capacity);
    }

    [TestMethod]
    public void TestSimpleWithString()
    {
        var list = new UnsafeList<string>.N16();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(16, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(16, list.Capacity);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
        list[0] = "5";
        Assert.AreEqual("5", list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestResize()
    {
        var list = new UnsafeList<int>.N16();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(16, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestResizeWithString()
    {
        var list = new UnsafeList<string>.N16();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(16, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        list.Add("5");
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual("5", list[4]);
    }

    [TestMethod]
    public void TestResizeWithZeroCapacity()
    {
        var list = new UnsafeList<int>.N16();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        Assert.AreEqual(1, list.Count);
        list.Add(2);
        Assert.AreEqual(2, list.Count);
        list.Add(3);
        Assert.AreEqual(3, list.Count);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestRemove()
    {
        var list = new UnsafeList<int>.N16();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(16, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        Assert.IsFalse(list.Remove(5));
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(16, list.Capacity);
        Assert.IsTrue(list.Remove(2));
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(3, list[1]);
        Assert.AreEqual(4, list[2]);
        Assert.IsTrue(list.Remove(1));
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(4, list[1]);
        Assert.IsTrue(list.Remove(4));
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.IsTrue(list.Remove(3));
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveString()
    {
        var list = new UnsafeList<string>.N16();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(16, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(16, list.Capacity);
        list.Remove("2");
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("3", list[1]);
        Assert.AreEqual("4", list[2]);
        list.Remove("1");
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("4", list[1]);
        list.Remove("4");
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("3", list[0]);
        list.Remove("3");
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveAt()
    {
        var list = new UnsafeList<int>.N16();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.RemoveAt(2);
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(4, list[2]);
        list.RemoveAt(0);
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(2, list[0]);
        Assert.AreEqual(4, list[1]);
        list.RemoveAt(1);
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(2, list[0]);
        list.RemoveAt(0);
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestInsert()
    {
        var list = new UnsafeList<int>.N16();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(16, list.Capacity);
        list.Insert(2, 5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(16, list.Capacity);
    }

    [TestMethod]
    public void TestIndexOf()
    {
        var list = new UnsafeList<int>.N16();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(16, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        var indexOf2 = list.IndexOf(2);
        Assert.AreEqual(1, indexOf2);
        var indexOf5 = list.IndexOf(5);
        Assert.AreEqual(-1, indexOf5);
    }

    [TestMethod]
    public void TestContains()
    {
        var list = new UnsafeList<int>.N16();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.IsTrue(list.Contains(2));
        Assert.IsFalse(list.Contains(5));
    }

    [TestMethod]
    public void TestCopyTo()
    {
        var list = new UnsafeList<int>.N16();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
                var array2 = new int[4];
        list.CopyTo(array2);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, array2);
    }

    [TestMethod]
    public void TestEnumerator()
    {
        var list = new UnsafeList<int>.N16();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorGeneric()
    {
        var list = new UnsafeList<int>.N16();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable<int>)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric()
    {
        var list = new UnsafeList<int>.N16();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric2()
    {
        var list = new UnsafeList<int>.N16();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        var enumerator = ((IEnumerable)list).GetEnumerator();
        while (enumerator.MoveNext())
        {
            Assert.AreEqual(list[index], enumerator.Current);
            index++;
        }
    }

    [TestMethod]
    public void TestRemoveAtAndGet()
    {
        var list = new UnsafeList<int>.N16();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(1, list.RemoveAtAndGet(0));
        Assert.AreEqual(2, list.RemoveAtAndGet(0));
        Assert.AreEqual(4, list.RemoveAtAndGet(1));
        Assert.AreEqual(3, list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestRemoveAtAndGetString()
    {
        var list = new UnsafeList<string>.N16();
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual("1", list.RemoveAtAndGet(0));
        Assert.AreEqual("2", list.RemoveAtAndGet(0));
        Assert.AreEqual("4", list.RemoveAtAndGet(1));
        Assert.AreEqual("3", list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestInsertByRef()
    {
        var list = new UnsafeList<int>.N16();
        int item = 1;
        list.InsertByRef(0, item);
        item = 2;
        list.InsertByRef(0, item);
        item = 3;
        list.InsertByRef(0, item);
        item = 4;
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(1, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestInsertByRefWithString()
    {
        var list = new UnsafeList<string>.N16();
        Assert.AreEqual(0, list.Count);
        string item = "1";
        list.InsertByRef(0, item);
        item = "2";
        list.InsertByRef(0, item);
        item = "3";
        list.InsertByRef(0, item);
        item = "4";
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("1", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestPushPeekPop()
    {
        var list = new UnsafeList<int>.N16();
        list.Push(1);
        list.Push(2);
        list.Push(3);
        list.Push(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(16, list.Capacity);
        Assert.AreEqual(4, list.Peek());
        Assert.AreEqual(4, list.Pop());
        Assert.AreEqual(3, list.Pop());
        Assert.AreEqual(2, list.Pop());
        Assert.AreEqual(1, list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(16, list.Capacity);
    }

    [TestMethod]
    public void TestPushPeekPopString()
    {
        var list = new UnsafeList<string>.N16();
        Assert.AreEqual(0, list.Count);
        list.Push("1");
        list.Push("2");
        list.Push("3");
        list.Push("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(16, list.Capacity);
        Assert.AreEqual("4", list.Peek());
        Assert.AreEqual("4", list.Pop());
        Assert.AreEqual("3", list.Pop());
        Assert.AreEqual("2", list.Pop());
        Assert.AreEqual("1", list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(16, list.Capacity);
    }

    [TestMethod]
    public void TestAddByRef()
    {
        var list = new UnsafeList<int>.N16();
        Assert.AreEqual(0, list.Count);
        int item = 1;
        list.AddByRef(item);
        item = 2;
        list.AddByRef(item);
        item = 3;
        list.AddByRef(item);
        item = 4;
        list.AddByRef(item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestAsSpan()
    {
        var list = new UnsafeList<int>.N16();
        Assert.AreEqual(0, list.AsSpan().Length);
        list.Add(1);
        list.Add(2);

        var span = list.AsSpan();
        Assert.AreEqual(2, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);

        list.Add(3);
        list.Add(4);
        span = list.AsSpan();
        Assert.AreEqual(4, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);
        Assert.AreEqual(3, span[2]);
        Assert.AreEqual(4, span[3]);
    }

    [TestMethod]
    public void TestUnsafeGetRefAt()
    {
        var list = new UnsafeList<int>.N16()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(1, list.UnsafeGetRefAt(0));
        Assert.AreEqual(2, list.UnsafeGetRefAt(1));
        Assert.AreEqual(3, list.UnsafeGetRefAt(2));
        Assert.AreEqual(4, list.UnsafeGetRefAt(3));
    }

    [TestMethod]
    public void TestRemoveLast()
    {
        var list = new UnsafeList<int>.N16()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(1, list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveLastString()
    {
        var list = new UnsafeList<string>.N16()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("1", list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestReset()
    {
        var list = new UnsafeList<int>.N16()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Reset();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClear()
    {
        var list = new UnsafeList<int>.N16()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClearWithString()
    {
        var list = new UnsafeList<string>.N16()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    
    [TestMethod]
    public void TestUnsafeGetOrCreate()
    {
        var list = new UnsafeList<int>.N16();
        Assert.AreEqual(0, list.Count);
        ref var item1 = ref list.UnsafeGetOrCreate(0);
        item1 = 1;
        ref var item2 = ref list.UnsafeGetOrCreate(1);
        item2 = 2;
        ref var item3 = ref list.UnsafeGetOrCreate(2);
        item3 = 3;
        ref var item4 = ref list.UnsafeGetOrCreate(3);
        item4 = 4;
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestUnsafeSetCount()
    {
        var list = new UnsafeList<int>.N16();
        list.UnsafeSetCount(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(16 , list.Capacity);

            }

    [TestMethod]
    public void TestClone()
    {
        var list = new UnsafeList<int>.N16()
        {
            1,
            2,
            3,
            4
        };
        var clone = list.Clone();
        Assert.AreEqual(4, clone.Count);
        Assert.AreEqual(16, clone.Capacity);
        Assert.AreEqual(1, clone[0]);
        Assert.AreEqual(2, clone[1]);
        Assert.AreEqual(3, clone[2]);
        Assert.AreEqual(4, clone[3]);
        list[0] = 5;
        Assert.AreEqual(1, clone[0]);
    }

    [TestMethod]
    public void TestIsReadOnly()
    {
        var list = new UnsafeList<int>.N16();
        Assert.IsFalse(list.IsReadOnly);
    }

    [TestMethod]
    public void TestUnsafeFirstRef()
    {
        var list = new UnsafeList<int>.N16()
        {
            1,
            2,
            3,
            4
        };
        ref var first = ref list.UnsafeFirstRef;
        Assert.AreEqual(1, first);
        first = 5;
        Assert.AreEqual(5, list[0]);
    }

    [TestMethod]
    public void TestArgumentExceptions()
    {
        var list = new UnsafeList<int>.N16();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(2, 1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[0] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[1] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[4] = 1);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(2, 1));
        Assert.ThrowsException<InvalidOperationException>(() => list.RemoveLast());
        Assert.ThrowsException<InvalidOperationException>(() => list.Peek());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Pop());
    }

    
    [TestMethod]
    public void TestSort()
    {
        var list = new UnsafeList<int>.N16()
        {
            4,
            3,
            2,
            1
        };
        list.Sort((left, right) => left.CompareTo(right));
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestSortStringWithComparer()
    {
        var list = new UnsafeList<string>.N16()
        {
            "4",
            "3",
            "2",
            "1"
        };
        list.Sort(StringComparer.Ordinal);

        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestSortByRef()
    {
        var list = new UnsafeList<int>.N16()
        {
            4,
            3,
            2,
            1
        };
        list.SortByRef(new IntComparerByRef());
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }
    
    private struct IntComparerByRef : IComparerByRef<int>
    {
        public bool LessThan(in int left, in int right) => left.CompareTo(right) < 0;
    }
}

[TestClass]
public class UnsafeListTestsN32
{
    [TestMethod]
    public void TestSimple()
    {
        var list = new UnsafeList<int>.N32();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(32, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(32, list.Capacity);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
        list[0] = 5;
        Assert.AreEqual(5, list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(32, list.Capacity);
    }

    [TestMethod]
    public void TestSimpleWithString()
    {
        var list = new UnsafeList<string>.N32();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(32, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(32, list.Capacity);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
        list[0] = "5";
        Assert.AreEqual("5", list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestResize()
    {
        var list = new UnsafeList<int>.N32();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(32, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestResizeWithString()
    {
        var list = new UnsafeList<string>.N32();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(32, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        list.Add("5");
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual("5", list[4]);
    }

    [TestMethod]
    public void TestResizeWithZeroCapacity()
    {
        var list = new UnsafeList<int>.N32();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        Assert.AreEqual(1, list.Count);
        list.Add(2);
        Assert.AreEqual(2, list.Count);
        list.Add(3);
        Assert.AreEqual(3, list.Count);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestRemove()
    {
        var list = new UnsafeList<int>.N32();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(32, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        Assert.IsFalse(list.Remove(5));
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(32, list.Capacity);
        Assert.IsTrue(list.Remove(2));
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(3, list[1]);
        Assert.AreEqual(4, list[2]);
        Assert.IsTrue(list.Remove(1));
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(4, list[1]);
        Assert.IsTrue(list.Remove(4));
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.IsTrue(list.Remove(3));
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveString()
    {
        var list = new UnsafeList<string>.N32();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(32, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(32, list.Capacity);
        list.Remove("2");
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("3", list[1]);
        Assert.AreEqual("4", list[2]);
        list.Remove("1");
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("4", list[1]);
        list.Remove("4");
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("3", list[0]);
        list.Remove("3");
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveAt()
    {
        var list = new UnsafeList<int>.N32();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.RemoveAt(2);
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(4, list[2]);
        list.RemoveAt(0);
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(2, list[0]);
        Assert.AreEqual(4, list[1]);
        list.RemoveAt(1);
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(2, list[0]);
        list.RemoveAt(0);
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestInsert()
    {
        var list = new UnsafeList<int>.N32();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(32, list.Capacity);
        list.Insert(2, 5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(32, list.Capacity);
    }

    [TestMethod]
    public void TestIndexOf()
    {
        var list = new UnsafeList<int>.N32();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(32, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        var indexOf2 = list.IndexOf(2);
        Assert.AreEqual(1, indexOf2);
        var indexOf5 = list.IndexOf(5);
        Assert.AreEqual(-1, indexOf5);
    }

    [TestMethod]
    public void TestContains()
    {
        var list = new UnsafeList<int>.N32();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.IsTrue(list.Contains(2));
        Assert.IsFalse(list.Contains(5));
    }

    [TestMethod]
    public void TestCopyTo()
    {
        var list = new UnsafeList<int>.N32();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
                var array2 = new int[4];
        list.CopyTo(array2);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, array2);
    }

    [TestMethod]
    public void TestEnumerator()
    {
        var list = new UnsafeList<int>.N32();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorGeneric()
    {
        var list = new UnsafeList<int>.N32();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable<int>)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric()
    {
        var list = new UnsafeList<int>.N32();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric2()
    {
        var list = new UnsafeList<int>.N32();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        var enumerator = ((IEnumerable)list).GetEnumerator();
        while (enumerator.MoveNext())
        {
            Assert.AreEqual(list[index], enumerator.Current);
            index++;
        }
    }

    [TestMethod]
    public void TestRemoveAtAndGet()
    {
        var list = new UnsafeList<int>.N32();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(1, list.RemoveAtAndGet(0));
        Assert.AreEqual(2, list.RemoveAtAndGet(0));
        Assert.AreEqual(4, list.RemoveAtAndGet(1));
        Assert.AreEqual(3, list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestRemoveAtAndGetString()
    {
        var list = new UnsafeList<string>.N32();
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual("1", list.RemoveAtAndGet(0));
        Assert.AreEqual("2", list.RemoveAtAndGet(0));
        Assert.AreEqual("4", list.RemoveAtAndGet(1));
        Assert.AreEqual("3", list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestInsertByRef()
    {
        var list = new UnsafeList<int>.N32();
        int item = 1;
        list.InsertByRef(0, item);
        item = 2;
        list.InsertByRef(0, item);
        item = 3;
        list.InsertByRef(0, item);
        item = 4;
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(1, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestInsertByRefWithString()
    {
        var list = new UnsafeList<string>.N32();
        Assert.AreEqual(0, list.Count);
        string item = "1";
        list.InsertByRef(0, item);
        item = "2";
        list.InsertByRef(0, item);
        item = "3";
        list.InsertByRef(0, item);
        item = "4";
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("1", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestPushPeekPop()
    {
        var list = new UnsafeList<int>.N32();
        list.Push(1);
        list.Push(2);
        list.Push(3);
        list.Push(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(32, list.Capacity);
        Assert.AreEqual(4, list.Peek());
        Assert.AreEqual(4, list.Pop());
        Assert.AreEqual(3, list.Pop());
        Assert.AreEqual(2, list.Pop());
        Assert.AreEqual(1, list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(32, list.Capacity);
    }

    [TestMethod]
    public void TestPushPeekPopString()
    {
        var list = new UnsafeList<string>.N32();
        Assert.AreEqual(0, list.Count);
        list.Push("1");
        list.Push("2");
        list.Push("3");
        list.Push("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(32, list.Capacity);
        Assert.AreEqual("4", list.Peek());
        Assert.AreEqual("4", list.Pop());
        Assert.AreEqual("3", list.Pop());
        Assert.AreEqual("2", list.Pop());
        Assert.AreEqual("1", list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(32, list.Capacity);
    }

    [TestMethod]
    public void TestAddByRef()
    {
        var list = new UnsafeList<int>.N32();
        Assert.AreEqual(0, list.Count);
        int item = 1;
        list.AddByRef(item);
        item = 2;
        list.AddByRef(item);
        item = 3;
        list.AddByRef(item);
        item = 4;
        list.AddByRef(item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestAsSpan()
    {
        var list = new UnsafeList<int>.N32();
        Assert.AreEqual(0, list.AsSpan().Length);
        list.Add(1);
        list.Add(2);

        var span = list.AsSpan();
        Assert.AreEqual(2, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);

        list.Add(3);
        list.Add(4);
        span = list.AsSpan();
        Assert.AreEqual(4, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);
        Assert.AreEqual(3, span[2]);
        Assert.AreEqual(4, span[3]);
    }

    [TestMethod]
    public void TestUnsafeGetRefAt()
    {
        var list = new UnsafeList<int>.N32()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(1, list.UnsafeGetRefAt(0));
        Assert.AreEqual(2, list.UnsafeGetRefAt(1));
        Assert.AreEqual(3, list.UnsafeGetRefAt(2));
        Assert.AreEqual(4, list.UnsafeGetRefAt(3));
    }

    [TestMethod]
    public void TestRemoveLast()
    {
        var list = new UnsafeList<int>.N32()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(1, list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveLastString()
    {
        var list = new UnsafeList<string>.N32()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("1", list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestReset()
    {
        var list = new UnsafeList<int>.N32()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Reset();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClear()
    {
        var list = new UnsafeList<int>.N32()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClearWithString()
    {
        var list = new UnsafeList<string>.N32()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    
    [TestMethod]
    public void TestUnsafeGetOrCreate()
    {
        var list = new UnsafeList<int>.N32();
        Assert.AreEqual(0, list.Count);
        ref var item1 = ref list.UnsafeGetOrCreate(0);
        item1 = 1;
        ref var item2 = ref list.UnsafeGetOrCreate(1);
        item2 = 2;
        ref var item3 = ref list.UnsafeGetOrCreate(2);
        item3 = 3;
        ref var item4 = ref list.UnsafeGetOrCreate(3);
        item4 = 4;
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestUnsafeSetCount()
    {
        var list = new UnsafeList<int>.N32();
        list.UnsafeSetCount(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(32 , list.Capacity);

            }

    [TestMethod]
    public void TestClone()
    {
        var list = new UnsafeList<int>.N32()
        {
            1,
            2,
            3,
            4
        };
        var clone = list.Clone();
        Assert.AreEqual(4, clone.Count);
        Assert.AreEqual(32, clone.Capacity);
        Assert.AreEqual(1, clone[0]);
        Assert.AreEqual(2, clone[1]);
        Assert.AreEqual(3, clone[2]);
        Assert.AreEqual(4, clone[3]);
        list[0] = 5;
        Assert.AreEqual(1, clone[0]);
    }

    [TestMethod]
    public void TestIsReadOnly()
    {
        var list = new UnsafeList<int>.N32();
        Assert.IsFalse(list.IsReadOnly);
    }

    [TestMethod]
    public void TestUnsafeFirstRef()
    {
        var list = new UnsafeList<int>.N32()
        {
            1,
            2,
            3,
            4
        };
        ref var first = ref list.UnsafeFirstRef;
        Assert.AreEqual(1, first);
        first = 5;
        Assert.AreEqual(5, list[0]);
    }

    [TestMethod]
    public void TestArgumentExceptions()
    {
        var list = new UnsafeList<int>.N32();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(2, 1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[0] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[1] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[4] = 1);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(2, 1));
        Assert.ThrowsException<InvalidOperationException>(() => list.RemoveLast());
        Assert.ThrowsException<InvalidOperationException>(() => list.Peek());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Pop());
    }

    
    [TestMethod]
    public void TestSort()
    {
        var list = new UnsafeList<int>.N32()
        {
            4,
            3,
            2,
            1
        };
        list.Sort((left, right) => left.CompareTo(right));
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestSortStringWithComparer()
    {
        var list = new UnsafeList<string>.N32()
        {
            "4",
            "3",
            "2",
            "1"
        };
        list.Sort(StringComparer.Ordinal);

        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestSortByRef()
    {
        var list = new UnsafeList<int>.N32()
        {
            4,
            3,
            2,
            1
        };
        list.SortByRef(new IntComparerByRef());
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }
    
    private struct IntComparerByRef : IComparerByRef<int>
    {
        public bool LessThan(in int left, in int right) => left.CompareTo(right) < 0;
    }
}

[TestClass]
public class UnsafeListTestsN64
{
    [TestMethod]
    public void TestSimple()
    {
        var list = new UnsafeList<int>.N64();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(64, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(64, list.Capacity);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
        list[0] = 5;
        Assert.AreEqual(5, list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(64, list.Capacity);
    }

    [TestMethod]
    public void TestSimpleWithString()
    {
        var list = new UnsafeList<string>.N64();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(64, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(64, list.Capacity);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
        list[0] = "5";
        Assert.AreEqual("5", list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestResize()
    {
        var list = new UnsafeList<int>.N64();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(64, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestResizeWithString()
    {
        var list = new UnsafeList<string>.N64();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(64, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        list.Add("5");
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual("5", list[4]);
    }

    [TestMethod]
    public void TestResizeWithZeroCapacity()
    {
        var list = new UnsafeList<int>.N64();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        Assert.AreEqual(1, list.Count);
        list.Add(2);
        Assert.AreEqual(2, list.Count);
        list.Add(3);
        Assert.AreEqual(3, list.Count);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestRemove()
    {
        var list = new UnsafeList<int>.N64();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(64, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        Assert.IsFalse(list.Remove(5));
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(64, list.Capacity);
        Assert.IsTrue(list.Remove(2));
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(3, list[1]);
        Assert.AreEqual(4, list[2]);
        Assert.IsTrue(list.Remove(1));
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(4, list[1]);
        Assert.IsTrue(list.Remove(4));
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.IsTrue(list.Remove(3));
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveString()
    {
        var list = new UnsafeList<string>.N64();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(64, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(64, list.Capacity);
        list.Remove("2");
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("3", list[1]);
        Assert.AreEqual("4", list[2]);
        list.Remove("1");
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("4", list[1]);
        list.Remove("4");
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("3", list[0]);
        list.Remove("3");
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveAt()
    {
        var list = new UnsafeList<int>.N64();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.RemoveAt(2);
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(4, list[2]);
        list.RemoveAt(0);
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(2, list[0]);
        Assert.AreEqual(4, list[1]);
        list.RemoveAt(1);
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(2, list[0]);
        list.RemoveAt(0);
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestInsert()
    {
        var list = new UnsafeList<int>.N64();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(64, list.Capacity);
        list.Insert(2, 5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(64, list.Capacity);
    }

    [TestMethod]
    public void TestIndexOf()
    {
        var list = new UnsafeList<int>.N64();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(64, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        var indexOf2 = list.IndexOf(2);
        Assert.AreEqual(1, indexOf2);
        var indexOf5 = list.IndexOf(5);
        Assert.AreEqual(-1, indexOf5);
    }

    [TestMethod]
    public void TestContains()
    {
        var list = new UnsafeList<int>.N64();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.IsTrue(list.Contains(2));
        Assert.IsFalse(list.Contains(5));
    }

    [TestMethod]
    public void TestCopyTo()
    {
        var list = new UnsafeList<int>.N64();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
                var array2 = new int[4];
        list.CopyTo(array2);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, array2);
    }

    [TestMethod]
    public void TestEnumerator()
    {
        var list = new UnsafeList<int>.N64();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorGeneric()
    {
        var list = new UnsafeList<int>.N64();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable<int>)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric()
    {
        var list = new UnsafeList<int>.N64();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric2()
    {
        var list = new UnsafeList<int>.N64();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        var enumerator = ((IEnumerable)list).GetEnumerator();
        while (enumerator.MoveNext())
        {
            Assert.AreEqual(list[index], enumerator.Current);
            index++;
        }
    }

    [TestMethod]
    public void TestRemoveAtAndGet()
    {
        var list = new UnsafeList<int>.N64();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(1, list.RemoveAtAndGet(0));
        Assert.AreEqual(2, list.RemoveAtAndGet(0));
        Assert.AreEqual(4, list.RemoveAtAndGet(1));
        Assert.AreEqual(3, list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestRemoveAtAndGetString()
    {
        var list = new UnsafeList<string>.N64();
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual("1", list.RemoveAtAndGet(0));
        Assert.AreEqual("2", list.RemoveAtAndGet(0));
        Assert.AreEqual("4", list.RemoveAtAndGet(1));
        Assert.AreEqual("3", list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestInsertByRef()
    {
        var list = new UnsafeList<int>.N64();
        int item = 1;
        list.InsertByRef(0, item);
        item = 2;
        list.InsertByRef(0, item);
        item = 3;
        list.InsertByRef(0, item);
        item = 4;
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(1, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestInsertByRefWithString()
    {
        var list = new UnsafeList<string>.N64();
        Assert.AreEqual(0, list.Count);
        string item = "1";
        list.InsertByRef(0, item);
        item = "2";
        list.InsertByRef(0, item);
        item = "3";
        list.InsertByRef(0, item);
        item = "4";
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("1", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestPushPeekPop()
    {
        var list = new UnsafeList<int>.N64();
        list.Push(1);
        list.Push(2);
        list.Push(3);
        list.Push(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(64, list.Capacity);
        Assert.AreEqual(4, list.Peek());
        Assert.AreEqual(4, list.Pop());
        Assert.AreEqual(3, list.Pop());
        Assert.AreEqual(2, list.Pop());
        Assert.AreEqual(1, list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(64, list.Capacity);
    }

    [TestMethod]
    public void TestPushPeekPopString()
    {
        var list = new UnsafeList<string>.N64();
        Assert.AreEqual(0, list.Count);
        list.Push("1");
        list.Push("2");
        list.Push("3");
        list.Push("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(64, list.Capacity);
        Assert.AreEqual("4", list.Peek());
        Assert.AreEqual("4", list.Pop());
        Assert.AreEqual("3", list.Pop());
        Assert.AreEqual("2", list.Pop());
        Assert.AreEqual("1", list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(64, list.Capacity);
    }

    [TestMethod]
    public void TestAddByRef()
    {
        var list = new UnsafeList<int>.N64();
        Assert.AreEqual(0, list.Count);
        int item = 1;
        list.AddByRef(item);
        item = 2;
        list.AddByRef(item);
        item = 3;
        list.AddByRef(item);
        item = 4;
        list.AddByRef(item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestAsSpan()
    {
        var list = new UnsafeList<int>.N64();
        Assert.AreEqual(0, list.AsSpan().Length);
        list.Add(1);
        list.Add(2);

        var span = list.AsSpan();
        Assert.AreEqual(2, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);

        list.Add(3);
        list.Add(4);
        span = list.AsSpan();
        Assert.AreEqual(4, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);
        Assert.AreEqual(3, span[2]);
        Assert.AreEqual(4, span[3]);
    }

    [TestMethod]
    public void TestUnsafeGetRefAt()
    {
        var list = new UnsafeList<int>.N64()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(1, list.UnsafeGetRefAt(0));
        Assert.AreEqual(2, list.UnsafeGetRefAt(1));
        Assert.AreEqual(3, list.UnsafeGetRefAt(2));
        Assert.AreEqual(4, list.UnsafeGetRefAt(3));
    }

    [TestMethod]
    public void TestRemoveLast()
    {
        var list = new UnsafeList<int>.N64()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(1, list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveLastString()
    {
        var list = new UnsafeList<string>.N64()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("1", list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestReset()
    {
        var list = new UnsafeList<int>.N64()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Reset();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClear()
    {
        var list = new UnsafeList<int>.N64()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClearWithString()
    {
        var list = new UnsafeList<string>.N64()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    
    [TestMethod]
    public void TestUnsafeGetOrCreate()
    {
        var list = new UnsafeList<int>.N64();
        Assert.AreEqual(0, list.Count);
        ref var item1 = ref list.UnsafeGetOrCreate(0);
        item1 = 1;
        ref var item2 = ref list.UnsafeGetOrCreate(1);
        item2 = 2;
        ref var item3 = ref list.UnsafeGetOrCreate(2);
        item3 = 3;
        ref var item4 = ref list.UnsafeGetOrCreate(3);
        item4 = 4;
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestUnsafeSetCount()
    {
        var list = new UnsafeList<int>.N64();
        list.UnsafeSetCount(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(64 , list.Capacity);

            }

    [TestMethod]
    public void TestClone()
    {
        var list = new UnsafeList<int>.N64()
        {
            1,
            2,
            3,
            4
        };
        var clone = list.Clone();
        Assert.AreEqual(4, clone.Count);
        Assert.AreEqual(64, clone.Capacity);
        Assert.AreEqual(1, clone[0]);
        Assert.AreEqual(2, clone[1]);
        Assert.AreEqual(3, clone[2]);
        Assert.AreEqual(4, clone[3]);
        list[0] = 5;
        Assert.AreEqual(1, clone[0]);
    }

    [TestMethod]
    public void TestIsReadOnly()
    {
        var list = new UnsafeList<int>.N64();
        Assert.IsFalse(list.IsReadOnly);
    }

    [TestMethod]
    public void TestUnsafeFirstRef()
    {
        var list = new UnsafeList<int>.N64()
        {
            1,
            2,
            3,
            4
        };
        ref var first = ref list.UnsafeFirstRef;
        Assert.AreEqual(1, first);
        first = 5;
        Assert.AreEqual(5, list[0]);
    }

    [TestMethod]
    public void TestArgumentExceptions()
    {
        var list = new UnsafeList<int>.N64();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(2, 1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[0] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[1] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[4] = 1);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(2, 1));
        Assert.ThrowsException<InvalidOperationException>(() => list.RemoveLast());
        Assert.ThrowsException<InvalidOperationException>(() => list.Peek());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Pop());
    }

    
    [TestMethod]
    public void TestSort()
    {
        var list = new UnsafeList<int>.N64()
        {
            4,
            3,
            2,
            1
        };
        list.Sort((left, right) => left.CompareTo(right));
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestSortStringWithComparer()
    {
        var list = new UnsafeList<string>.N64()
        {
            "4",
            "3",
            "2",
            "1"
        };
        list.Sort(StringComparer.Ordinal);

        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestSortByRef()
    {
        var list = new UnsafeList<int>.N64()
        {
            4,
            3,
            2,
            1
        };
        list.SortByRef(new IntComparerByRef());
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }
    
    private struct IntComparerByRef : IComparerByRef<int>
    {
        public bool LessThan(in int left, in int right) => left.CompareTo(right) < 0;
    }
}

[TestClass]
public class UnsafeListTestsN128
{
    [TestMethod]
    public void TestSimple()
    {
        var list = new UnsafeList<int>.N128();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(128, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(128, list.Capacity);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
        list[0] = 5;
        Assert.AreEqual(5, list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(128, list.Capacity);
    }

    [TestMethod]
    public void TestSimpleWithString()
    {
        var list = new UnsafeList<string>.N128();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(128, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(128, list.Capacity);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
        list[0] = "5";
        Assert.AreEqual("5", list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestResize()
    {
        var list = new UnsafeList<int>.N128();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(128, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestResizeWithString()
    {
        var list = new UnsafeList<string>.N128();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(128, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        list.Add("5");
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual("5", list[4]);
    }

    [TestMethod]
    public void TestResizeWithZeroCapacity()
    {
        var list = new UnsafeList<int>.N128();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        Assert.AreEqual(1, list.Count);
        list.Add(2);
        Assert.AreEqual(2, list.Count);
        list.Add(3);
        Assert.AreEqual(3, list.Count);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestRemove()
    {
        var list = new UnsafeList<int>.N128();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(128, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        Assert.IsFalse(list.Remove(5));
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(128, list.Capacity);
        Assert.IsTrue(list.Remove(2));
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(3, list[1]);
        Assert.AreEqual(4, list[2]);
        Assert.IsTrue(list.Remove(1));
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(4, list[1]);
        Assert.IsTrue(list.Remove(4));
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.IsTrue(list.Remove(3));
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveString()
    {
        var list = new UnsafeList<string>.N128();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(128, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(128, list.Capacity);
        list.Remove("2");
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("3", list[1]);
        Assert.AreEqual("4", list[2]);
        list.Remove("1");
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("4", list[1]);
        list.Remove("4");
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("3", list[0]);
        list.Remove("3");
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveAt()
    {
        var list = new UnsafeList<int>.N128();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.RemoveAt(2);
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(4, list[2]);
        list.RemoveAt(0);
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(2, list[0]);
        Assert.AreEqual(4, list[1]);
        list.RemoveAt(1);
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(2, list[0]);
        list.RemoveAt(0);
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestInsert()
    {
        var list = new UnsafeList<int>.N128();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(128, list.Capacity);
        list.Insert(2, 5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(128, list.Capacity);
    }

    [TestMethod]
    public void TestIndexOf()
    {
        var list = new UnsafeList<int>.N128();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(128, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        var indexOf2 = list.IndexOf(2);
        Assert.AreEqual(1, indexOf2);
        var indexOf5 = list.IndexOf(5);
        Assert.AreEqual(-1, indexOf5);
    }

    [TestMethod]
    public void TestContains()
    {
        var list = new UnsafeList<int>.N128();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.IsTrue(list.Contains(2));
        Assert.IsFalse(list.Contains(5));
    }

    [TestMethod]
    public void TestCopyTo()
    {
        var list = new UnsafeList<int>.N128();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
                var array2 = new int[4];
        list.CopyTo(array2);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, array2);
    }

    [TestMethod]
    public void TestEnumerator()
    {
        var list = new UnsafeList<int>.N128();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorGeneric()
    {
        var list = new UnsafeList<int>.N128();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable<int>)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric()
    {
        var list = new UnsafeList<int>.N128();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric2()
    {
        var list = new UnsafeList<int>.N128();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        var enumerator = ((IEnumerable)list).GetEnumerator();
        while (enumerator.MoveNext())
        {
            Assert.AreEqual(list[index], enumerator.Current);
            index++;
        }
    }

    [TestMethod]
    public void TestRemoveAtAndGet()
    {
        var list = new UnsafeList<int>.N128();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(1, list.RemoveAtAndGet(0));
        Assert.AreEqual(2, list.RemoveAtAndGet(0));
        Assert.AreEqual(4, list.RemoveAtAndGet(1));
        Assert.AreEqual(3, list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestRemoveAtAndGetString()
    {
        var list = new UnsafeList<string>.N128();
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual("1", list.RemoveAtAndGet(0));
        Assert.AreEqual("2", list.RemoveAtAndGet(0));
        Assert.AreEqual("4", list.RemoveAtAndGet(1));
        Assert.AreEqual("3", list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestInsertByRef()
    {
        var list = new UnsafeList<int>.N128();
        int item = 1;
        list.InsertByRef(0, item);
        item = 2;
        list.InsertByRef(0, item);
        item = 3;
        list.InsertByRef(0, item);
        item = 4;
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(1, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestInsertByRefWithString()
    {
        var list = new UnsafeList<string>.N128();
        Assert.AreEqual(0, list.Count);
        string item = "1";
        list.InsertByRef(0, item);
        item = "2";
        list.InsertByRef(0, item);
        item = "3";
        list.InsertByRef(0, item);
        item = "4";
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("1", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestPushPeekPop()
    {
        var list = new UnsafeList<int>.N128();
        list.Push(1);
        list.Push(2);
        list.Push(3);
        list.Push(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(128, list.Capacity);
        Assert.AreEqual(4, list.Peek());
        Assert.AreEqual(4, list.Pop());
        Assert.AreEqual(3, list.Pop());
        Assert.AreEqual(2, list.Pop());
        Assert.AreEqual(1, list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(128, list.Capacity);
    }

    [TestMethod]
    public void TestPushPeekPopString()
    {
        var list = new UnsafeList<string>.N128();
        Assert.AreEqual(0, list.Count);
        list.Push("1");
        list.Push("2");
        list.Push("3");
        list.Push("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(128, list.Capacity);
        Assert.AreEqual("4", list.Peek());
        Assert.AreEqual("4", list.Pop());
        Assert.AreEqual("3", list.Pop());
        Assert.AreEqual("2", list.Pop());
        Assert.AreEqual("1", list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(128, list.Capacity);
    }

    [TestMethod]
    public void TestAddByRef()
    {
        var list = new UnsafeList<int>.N128();
        Assert.AreEqual(0, list.Count);
        int item = 1;
        list.AddByRef(item);
        item = 2;
        list.AddByRef(item);
        item = 3;
        list.AddByRef(item);
        item = 4;
        list.AddByRef(item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestAsSpan()
    {
        var list = new UnsafeList<int>.N128();
        Assert.AreEqual(0, list.AsSpan().Length);
        list.Add(1);
        list.Add(2);

        var span = list.AsSpan();
        Assert.AreEqual(2, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);

        list.Add(3);
        list.Add(4);
        span = list.AsSpan();
        Assert.AreEqual(4, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);
        Assert.AreEqual(3, span[2]);
        Assert.AreEqual(4, span[3]);
    }

    [TestMethod]
    public void TestUnsafeGetRefAt()
    {
        var list = new UnsafeList<int>.N128()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(1, list.UnsafeGetRefAt(0));
        Assert.AreEqual(2, list.UnsafeGetRefAt(1));
        Assert.AreEqual(3, list.UnsafeGetRefAt(2));
        Assert.AreEqual(4, list.UnsafeGetRefAt(3));
    }

    [TestMethod]
    public void TestRemoveLast()
    {
        var list = new UnsafeList<int>.N128()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(1, list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveLastString()
    {
        var list = new UnsafeList<string>.N128()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("1", list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestReset()
    {
        var list = new UnsafeList<int>.N128()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Reset();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClear()
    {
        var list = new UnsafeList<int>.N128()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClearWithString()
    {
        var list = new UnsafeList<string>.N128()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    
    [TestMethod]
    public void TestUnsafeGetOrCreate()
    {
        var list = new UnsafeList<int>.N128();
        Assert.AreEqual(0, list.Count);
        ref var item1 = ref list.UnsafeGetOrCreate(0);
        item1 = 1;
        ref var item2 = ref list.UnsafeGetOrCreate(1);
        item2 = 2;
        ref var item3 = ref list.UnsafeGetOrCreate(2);
        item3 = 3;
        ref var item4 = ref list.UnsafeGetOrCreate(3);
        item4 = 4;
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestUnsafeSetCount()
    {
        var list = new UnsafeList<int>.N128();
        list.UnsafeSetCount(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(128 , list.Capacity);

            }

    [TestMethod]
    public void TestClone()
    {
        var list = new UnsafeList<int>.N128()
        {
            1,
            2,
            3,
            4
        };
        var clone = list.Clone();
        Assert.AreEqual(4, clone.Count);
        Assert.AreEqual(128, clone.Capacity);
        Assert.AreEqual(1, clone[0]);
        Assert.AreEqual(2, clone[1]);
        Assert.AreEqual(3, clone[2]);
        Assert.AreEqual(4, clone[3]);
        list[0] = 5;
        Assert.AreEqual(1, clone[0]);
    }

    [TestMethod]
    public void TestIsReadOnly()
    {
        var list = new UnsafeList<int>.N128();
        Assert.IsFalse(list.IsReadOnly);
    }

    [TestMethod]
    public void TestUnsafeFirstRef()
    {
        var list = new UnsafeList<int>.N128()
        {
            1,
            2,
            3,
            4
        };
        ref var first = ref list.UnsafeFirstRef;
        Assert.AreEqual(1, first);
        first = 5;
        Assert.AreEqual(5, list[0]);
    }

    [TestMethod]
    public void TestArgumentExceptions()
    {
        var list = new UnsafeList<int>.N128();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(2, 1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[0] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[1] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[4] = 1);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(2, 1));
        Assert.ThrowsException<InvalidOperationException>(() => list.RemoveLast());
        Assert.ThrowsException<InvalidOperationException>(() => list.Peek());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Pop());
    }

    
    [TestMethod]
    public void TestSort()
    {
        var list = new UnsafeList<int>.N128()
        {
            4,
            3,
            2,
            1
        };
        list.Sort((left, right) => left.CompareTo(right));
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestSortStringWithComparer()
    {
        var list = new UnsafeList<string>.N128()
        {
            "4",
            "3",
            "2",
            "1"
        };
        list.Sort(StringComparer.Ordinal);

        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestSortByRef()
    {
        var list = new UnsafeList<int>.N128()
        {
            4,
            3,
            2,
            1
        };
        list.SortByRef(new IntComparerByRef());
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }
    
    private struct IntComparerByRef : IComparerByRef<int>
    {
        public bool LessThan(in int left, in int right) => left.CompareTo(right) < 0;
    }
}

[TestClass]
public class UnsafeListTestsN256
{
    [TestMethod]
    public void TestSimple()
    {
        var list = new UnsafeList<int>.N256();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(256, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(256, list.Capacity);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
        list[0] = 5;
        Assert.AreEqual(5, list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(256, list.Capacity);
    }

    [TestMethod]
    public void TestSimpleWithString()
    {
        var list = new UnsafeList<string>.N256();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(256, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(256, list.Capacity);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
        list[0] = "5";
        Assert.AreEqual("5", list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestResize()
    {
        var list = new UnsafeList<int>.N256();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(256, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestResizeWithString()
    {
        var list = new UnsafeList<string>.N256();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(256, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        list.Add("5");
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual("5", list[4]);
    }

    [TestMethod]
    public void TestResizeWithZeroCapacity()
    {
        var list = new UnsafeList<int>.N256();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        Assert.AreEqual(1, list.Count);
        list.Add(2);
        Assert.AreEqual(2, list.Count);
        list.Add(3);
        Assert.AreEqual(3, list.Count);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestRemove()
    {
        var list = new UnsafeList<int>.N256();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(256, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        Assert.IsFalse(list.Remove(5));
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(256, list.Capacity);
        Assert.IsTrue(list.Remove(2));
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(3, list[1]);
        Assert.AreEqual(4, list[2]);
        Assert.IsTrue(list.Remove(1));
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(4, list[1]);
        Assert.IsTrue(list.Remove(4));
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.IsTrue(list.Remove(3));
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveString()
    {
        var list = new UnsafeList<string>.N256();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(256, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(256, list.Capacity);
        list.Remove("2");
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("3", list[1]);
        Assert.AreEqual("4", list[2]);
        list.Remove("1");
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("4", list[1]);
        list.Remove("4");
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("3", list[0]);
        list.Remove("3");
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveAt()
    {
        var list = new UnsafeList<int>.N256();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.RemoveAt(2);
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(4, list[2]);
        list.RemoveAt(0);
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(2, list[0]);
        Assert.AreEqual(4, list[1]);
        list.RemoveAt(1);
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(2, list[0]);
        list.RemoveAt(0);
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestInsert()
    {
        var list = new UnsafeList<int>.N256();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(256, list.Capacity);
        list.Insert(2, 5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(256, list.Capacity);
    }

    [TestMethod]
    public void TestIndexOf()
    {
        var list = new UnsafeList<int>.N256();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(256, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        var indexOf2 = list.IndexOf(2);
        Assert.AreEqual(1, indexOf2);
        var indexOf5 = list.IndexOf(5);
        Assert.AreEqual(-1, indexOf5);
    }

    [TestMethod]
    public void TestContains()
    {
        var list = new UnsafeList<int>.N256();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.IsTrue(list.Contains(2));
        Assert.IsFalse(list.Contains(5));
    }

    [TestMethod]
    public void TestCopyTo()
    {
        var list = new UnsafeList<int>.N256();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
                var array2 = new int[4];
        list.CopyTo(array2);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, array2);
    }

    [TestMethod]
    public void TestEnumerator()
    {
        var list = new UnsafeList<int>.N256();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorGeneric()
    {
        var list = new UnsafeList<int>.N256();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable<int>)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric()
    {
        var list = new UnsafeList<int>.N256();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric2()
    {
        var list = new UnsafeList<int>.N256();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        var enumerator = ((IEnumerable)list).GetEnumerator();
        while (enumerator.MoveNext())
        {
            Assert.AreEqual(list[index], enumerator.Current);
            index++;
        }
    }

    [TestMethod]
    public void TestRemoveAtAndGet()
    {
        var list = new UnsafeList<int>.N256();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(1, list.RemoveAtAndGet(0));
        Assert.AreEqual(2, list.RemoveAtAndGet(0));
        Assert.AreEqual(4, list.RemoveAtAndGet(1));
        Assert.AreEqual(3, list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestRemoveAtAndGetString()
    {
        var list = new UnsafeList<string>.N256();
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual("1", list.RemoveAtAndGet(0));
        Assert.AreEqual("2", list.RemoveAtAndGet(0));
        Assert.AreEqual("4", list.RemoveAtAndGet(1));
        Assert.AreEqual("3", list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestInsertByRef()
    {
        var list = new UnsafeList<int>.N256();
        int item = 1;
        list.InsertByRef(0, item);
        item = 2;
        list.InsertByRef(0, item);
        item = 3;
        list.InsertByRef(0, item);
        item = 4;
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(1, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestInsertByRefWithString()
    {
        var list = new UnsafeList<string>.N256();
        Assert.AreEqual(0, list.Count);
        string item = "1";
        list.InsertByRef(0, item);
        item = "2";
        list.InsertByRef(0, item);
        item = "3";
        list.InsertByRef(0, item);
        item = "4";
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("1", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestPushPeekPop()
    {
        var list = new UnsafeList<int>.N256();
        list.Push(1);
        list.Push(2);
        list.Push(3);
        list.Push(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(256, list.Capacity);
        Assert.AreEqual(4, list.Peek());
        Assert.AreEqual(4, list.Pop());
        Assert.AreEqual(3, list.Pop());
        Assert.AreEqual(2, list.Pop());
        Assert.AreEqual(1, list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(256, list.Capacity);
    }

    [TestMethod]
    public void TestPushPeekPopString()
    {
        var list = new UnsafeList<string>.N256();
        Assert.AreEqual(0, list.Count);
        list.Push("1");
        list.Push("2");
        list.Push("3");
        list.Push("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(256, list.Capacity);
        Assert.AreEqual("4", list.Peek());
        Assert.AreEqual("4", list.Pop());
        Assert.AreEqual("3", list.Pop());
        Assert.AreEqual("2", list.Pop());
        Assert.AreEqual("1", list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(256, list.Capacity);
    }

    [TestMethod]
    public void TestAddByRef()
    {
        var list = new UnsafeList<int>.N256();
        Assert.AreEqual(0, list.Count);
        int item = 1;
        list.AddByRef(item);
        item = 2;
        list.AddByRef(item);
        item = 3;
        list.AddByRef(item);
        item = 4;
        list.AddByRef(item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestAsSpan()
    {
        var list = new UnsafeList<int>.N256();
        Assert.AreEqual(0, list.AsSpan().Length);
        list.Add(1);
        list.Add(2);

        var span = list.AsSpan();
        Assert.AreEqual(2, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);

        list.Add(3);
        list.Add(4);
        span = list.AsSpan();
        Assert.AreEqual(4, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);
        Assert.AreEqual(3, span[2]);
        Assert.AreEqual(4, span[3]);
    }

    [TestMethod]
    public void TestUnsafeGetRefAt()
    {
        var list = new UnsafeList<int>.N256()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(1, list.UnsafeGetRefAt(0));
        Assert.AreEqual(2, list.UnsafeGetRefAt(1));
        Assert.AreEqual(3, list.UnsafeGetRefAt(2));
        Assert.AreEqual(4, list.UnsafeGetRefAt(3));
    }

    [TestMethod]
    public void TestRemoveLast()
    {
        var list = new UnsafeList<int>.N256()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(1, list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveLastString()
    {
        var list = new UnsafeList<string>.N256()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("1", list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestReset()
    {
        var list = new UnsafeList<int>.N256()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Reset();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClear()
    {
        var list = new UnsafeList<int>.N256()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClearWithString()
    {
        var list = new UnsafeList<string>.N256()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    
    [TestMethod]
    public void TestUnsafeGetOrCreate()
    {
        var list = new UnsafeList<int>.N256();
        Assert.AreEqual(0, list.Count);
        ref var item1 = ref list.UnsafeGetOrCreate(0);
        item1 = 1;
        ref var item2 = ref list.UnsafeGetOrCreate(1);
        item2 = 2;
        ref var item3 = ref list.UnsafeGetOrCreate(2);
        item3 = 3;
        ref var item4 = ref list.UnsafeGetOrCreate(3);
        item4 = 4;
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestUnsafeSetCount()
    {
        var list = new UnsafeList<int>.N256();
        list.UnsafeSetCount(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(256 , list.Capacity);

            }

    [TestMethod]
    public void TestClone()
    {
        var list = new UnsafeList<int>.N256()
        {
            1,
            2,
            3,
            4
        };
        var clone = list.Clone();
        Assert.AreEqual(4, clone.Count);
        Assert.AreEqual(256, clone.Capacity);
        Assert.AreEqual(1, clone[0]);
        Assert.AreEqual(2, clone[1]);
        Assert.AreEqual(3, clone[2]);
        Assert.AreEqual(4, clone[3]);
        list[0] = 5;
        Assert.AreEqual(1, clone[0]);
    }

    [TestMethod]
    public void TestIsReadOnly()
    {
        var list = new UnsafeList<int>.N256();
        Assert.IsFalse(list.IsReadOnly);
    }

    [TestMethod]
    public void TestUnsafeFirstRef()
    {
        var list = new UnsafeList<int>.N256()
        {
            1,
            2,
            3,
            4
        };
        ref var first = ref list.UnsafeFirstRef;
        Assert.AreEqual(1, first);
        first = 5;
        Assert.AreEqual(5, list[0]);
    }

    [TestMethod]
    public void TestArgumentExceptions()
    {
        var list = new UnsafeList<int>.N256();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(2, 1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[0] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[1] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[4] = 1);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(2, 1));
        Assert.ThrowsException<InvalidOperationException>(() => list.RemoveLast());
        Assert.ThrowsException<InvalidOperationException>(() => list.Peek());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Pop());
    }

    
    [TestMethod]
    public void TestSort()
    {
        var list = new UnsafeList<int>.N256()
        {
            4,
            3,
            2,
            1
        };
        list.Sort((left, right) => left.CompareTo(right));
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestSortStringWithComparer()
    {
        var list = new UnsafeList<string>.N256()
        {
            "4",
            "3",
            "2",
            "1"
        };
        list.Sort(StringComparer.Ordinal);

        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestSortByRef()
    {
        var list = new UnsafeList<int>.N256()
        {
            4,
            3,
            2,
            1
        };
        list.SortByRef(new IntComparerByRef());
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }
    
    private struct IntComparerByRef : IComparerByRef<int>
    {
        public bool LessThan(in int left, in int right) => left.CompareTo(right) < 0;
    }
}

[TestClass]
public class UnsafeListTestsN512
{
    [TestMethod]
    public void TestSimple()
    {
        var list = new UnsafeList<int>.N512();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(512, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(512, list.Capacity);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
        list[0] = 5;
        Assert.AreEqual(5, list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(512, list.Capacity);
    }

    [TestMethod]
    public void TestSimpleWithString()
    {
        var list = new UnsafeList<string>.N512();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(512, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(512, list.Capacity);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
        list[0] = "5";
        Assert.AreEqual("5", list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestResize()
    {
        var list = new UnsafeList<int>.N512();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(512, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestResizeWithString()
    {
        var list = new UnsafeList<string>.N512();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(512, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        list.Add("5");
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual("5", list[4]);
    }

    [TestMethod]
    public void TestResizeWithZeroCapacity()
    {
        var list = new UnsafeList<int>.N512();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        Assert.AreEqual(1, list.Count);
        list.Add(2);
        Assert.AreEqual(2, list.Count);
        list.Add(3);
        Assert.AreEqual(3, list.Count);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestRemove()
    {
        var list = new UnsafeList<int>.N512();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(512, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        Assert.IsFalse(list.Remove(5));
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(512, list.Capacity);
        Assert.IsTrue(list.Remove(2));
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(3, list[1]);
        Assert.AreEqual(4, list[2]);
        Assert.IsTrue(list.Remove(1));
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(4, list[1]);
        Assert.IsTrue(list.Remove(4));
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.IsTrue(list.Remove(3));
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveString()
    {
        var list = new UnsafeList<string>.N512();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(512, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(512, list.Capacity);
        list.Remove("2");
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("3", list[1]);
        Assert.AreEqual("4", list[2]);
        list.Remove("1");
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("4", list[1]);
        list.Remove("4");
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("3", list[0]);
        list.Remove("3");
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveAt()
    {
        var list = new UnsafeList<int>.N512();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.RemoveAt(2);
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(4, list[2]);
        list.RemoveAt(0);
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(2, list[0]);
        Assert.AreEqual(4, list[1]);
        list.RemoveAt(1);
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(2, list[0]);
        list.RemoveAt(0);
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestInsert()
    {
        var list = new UnsafeList<int>.N512();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(512, list.Capacity);
        list.Insert(2, 5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(512, list.Capacity);
    }

    [TestMethod]
    public void TestIndexOf()
    {
        var list = new UnsafeList<int>.N512();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(512, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        var indexOf2 = list.IndexOf(2);
        Assert.AreEqual(1, indexOf2);
        var indexOf5 = list.IndexOf(5);
        Assert.AreEqual(-1, indexOf5);
    }

    [TestMethod]
    public void TestContains()
    {
        var list = new UnsafeList<int>.N512();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.IsTrue(list.Contains(2));
        Assert.IsFalse(list.Contains(5));
    }

    [TestMethod]
    public void TestCopyTo()
    {
        var list = new UnsafeList<int>.N512();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
                var array2 = new int[4];
        list.CopyTo(array2);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, array2);
    }

    [TestMethod]
    public void TestEnumerator()
    {
        var list = new UnsafeList<int>.N512();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorGeneric()
    {
        var list = new UnsafeList<int>.N512();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable<int>)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric()
    {
        var list = new UnsafeList<int>.N512();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric2()
    {
        var list = new UnsafeList<int>.N512();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        var enumerator = ((IEnumerable)list).GetEnumerator();
        while (enumerator.MoveNext())
        {
            Assert.AreEqual(list[index], enumerator.Current);
            index++;
        }
    }

    [TestMethod]
    public void TestRemoveAtAndGet()
    {
        var list = new UnsafeList<int>.N512();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(1, list.RemoveAtAndGet(0));
        Assert.AreEqual(2, list.RemoveAtAndGet(0));
        Assert.AreEqual(4, list.RemoveAtAndGet(1));
        Assert.AreEqual(3, list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestRemoveAtAndGetString()
    {
        var list = new UnsafeList<string>.N512();
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual("1", list.RemoveAtAndGet(0));
        Assert.AreEqual("2", list.RemoveAtAndGet(0));
        Assert.AreEqual("4", list.RemoveAtAndGet(1));
        Assert.AreEqual("3", list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestInsertByRef()
    {
        var list = new UnsafeList<int>.N512();
        int item = 1;
        list.InsertByRef(0, item);
        item = 2;
        list.InsertByRef(0, item);
        item = 3;
        list.InsertByRef(0, item);
        item = 4;
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(1, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestInsertByRefWithString()
    {
        var list = new UnsafeList<string>.N512();
        Assert.AreEqual(0, list.Count);
        string item = "1";
        list.InsertByRef(0, item);
        item = "2";
        list.InsertByRef(0, item);
        item = "3";
        list.InsertByRef(0, item);
        item = "4";
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("1", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestPushPeekPop()
    {
        var list = new UnsafeList<int>.N512();
        list.Push(1);
        list.Push(2);
        list.Push(3);
        list.Push(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(512, list.Capacity);
        Assert.AreEqual(4, list.Peek());
        Assert.AreEqual(4, list.Pop());
        Assert.AreEqual(3, list.Pop());
        Assert.AreEqual(2, list.Pop());
        Assert.AreEqual(1, list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(512, list.Capacity);
    }

    [TestMethod]
    public void TestPushPeekPopString()
    {
        var list = new UnsafeList<string>.N512();
        Assert.AreEqual(0, list.Count);
        list.Push("1");
        list.Push("2");
        list.Push("3");
        list.Push("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(512, list.Capacity);
        Assert.AreEqual("4", list.Peek());
        Assert.AreEqual("4", list.Pop());
        Assert.AreEqual("3", list.Pop());
        Assert.AreEqual("2", list.Pop());
        Assert.AreEqual("1", list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(512, list.Capacity);
    }

    [TestMethod]
    public void TestAddByRef()
    {
        var list = new UnsafeList<int>.N512();
        Assert.AreEqual(0, list.Count);
        int item = 1;
        list.AddByRef(item);
        item = 2;
        list.AddByRef(item);
        item = 3;
        list.AddByRef(item);
        item = 4;
        list.AddByRef(item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestAsSpan()
    {
        var list = new UnsafeList<int>.N512();
        Assert.AreEqual(0, list.AsSpan().Length);
        list.Add(1);
        list.Add(2);

        var span = list.AsSpan();
        Assert.AreEqual(2, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);

        list.Add(3);
        list.Add(4);
        span = list.AsSpan();
        Assert.AreEqual(4, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);
        Assert.AreEqual(3, span[2]);
        Assert.AreEqual(4, span[3]);
    }

    [TestMethod]
    public void TestUnsafeGetRefAt()
    {
        var list = new UnsafeList<int>.N512()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(1, list.UnsafeGetRefAt(0));
        Assert.AreEqual(2, list.UnsafeGetRefAt(1));
        Assert.AreEqual(3, list.UnsafeGetRefAt(2));
        Assert.AreEqual(4, list.UnsafeGetRefAt(3));
    }

    [TestMethod]
    public void TestRemoveLast()
    {
        var list = new UnsafeList<int>.N512()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(1, list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveLastString()
    {
        var list = new UnsafeList<string>.N512()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("1", list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestReset()
    {
        var list = new UnsafeList<int>.N512()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Reset();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClear()
    {
        var list = new UnsafeList<int>.N512()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClearWithString()
    {
        var list = new UnsafeList<string>.N512()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    
    [TestMethod]
    public void TestUnsafeGetOrCreate()
    {
        var list = new UnsafeList<int>.N512();
        Assert.AreEqual(0, list.Count);
        ref var item1 = ref list.UnsafeGetOrCreate(0);
        item1 = 1;
        ref var item2 = ref list.UnsafeGetOrCreate(1);
        item2 = 2;
        ref var item3 = ref list.UnsafeGetOrCreate(2);
        item3 = 3;
        ref var item4 = ref list.UnsafeGetOrCreate(3);
        item4 = 4;
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestUnsafeSetCount()
    {
        var list = new UnsafeList<int>.N512();
        list.UnsafeSetCount(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(512 , list.Capacity);

            }

    [TestMethod]
    public void TestClone()
    {
        var list = new UnsafeList<int>.N512()
        {
            1,
            2,
            3,
            4
        };
        var clone = list.Clone();
        Assert.AreEqual(4, clone.Count);
        Assert.AreEqual(512, clone.Capacity);
        Assert.AreEqual(1, clone[0]);
        Assert.AreEqual(2, clone[1]);
        Assert.AreEqual(3, clone[2]);
        Assert.AreEqual(4, clone[3]);
        list[0] = 5;
        Assert.AreEqual(1, clone[0]);
    }

    [TestMethod]
    public void TestIsReadOnly()
    {
        var list = new UnsafeList<int>.N512();
        Assert.IsFalse(list.IsReadOnly);
    }

    [TestMethod]
    public void TestUnsafeFirstRef()
    {
        var list = new UnsafeList<int>.N512()
        {
            1,
            2,
            3,
            4
        };
        ref var first = ref list.UnsafeFirstRef;
        Assert.AreEqual(1, first);
        first = 5;
        Assert.AreEqual(5, list[0]);
    }

    [TestMethod]
    public void TestArgumentExceptions()
    {
        var list = new UnsafeList<int>.N512();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(2, 1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[0] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[1] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[4] = 1);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(2, 1));
        Assert.ThrowsException<InvalidOperationException>(() => list.RemoveLast());
        Assert.ThrowsException<InvalidOperationException>(() => list.Peek());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Pop());
    }

    
    [TestMethod]
    public void TestSort()
    {
        var list = new UnsafeList<int>.N512()
        {
            4,
            3,
            2,
            1
        };
        list.Sort((left, right) => left.CompareTo(right));
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestSortStringWithComparer()
    {
        var list = new UnsafeList<string>.N512()
        {
            "4",
            "3",
            "2",
            "1"
        };
        list.Sort(StringComparer.Ordinal);

        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestSortByRef()
    {
        var list = new UnsafeList<int>.N512()
        {
            4,
            3,
            2,
            1
        };
        list.SortByRef(new IntComparerByRef());
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }
    
    private struct IntComparerByRef : IComparerByRef<int>
    {
        public bool LessThan(in int left, in int right) => left.CompareTo(right) < 0;
    }
}

[TestClass]
public class UnsafeListTestsN1024
{
    [TestMethod]
    public void TestSimple()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
        list[0] = 5;
        Assert.AreEqual(5, list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(1024, list.Capacity);
    }

    [TestMethod]
    public void TestSimpleWithString()
    {
        var list = new UnsafeList<string>.N1024();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
        list[0] = "5";
        Assert.AreEqual("5", list[0]);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestResize()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestResizeWithString()
    {
        var list = new UnsafeList<string>.N1024();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        list.Add("5");
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual("5", list[4]);
    }

    [TestMethod]
    public void TestResizeWithZeroCapacity()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        Assert.AreEqual(1, list.Count);
        list.Add(2);
        Assert.AreEqual(2, list.Count);
        list.Add(3);
        Assert.AreEqual(3, list.Count);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.Add(5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(5, list[4]);
    }

    [TestMethod]
    public void TestRemove()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        Assert.IsFalse(list.Remove(5));
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        Assert.IsTrue(list.Remove(2));
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(3, list[1]);
        Assert.AreEqual(4, list[2]);
        Assert.IsTrue(list.Remove(1));
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(4, list[1]);
        Assert.IsTrue(list.Remove(4));
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.IsTrue(list.Remove(3));
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveString()
    {
        var list = new UnsafeList<string>.N1024();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        list.Remove("2");
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("3", list[1]);
        Assert.AreEqual("4", list[2]);
        list.Remove("1");
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("4", list[1]);
        list.Remove("4");
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("3", list[0]);
        list.Remove("3");
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveAt()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        list.RemoveAt(2);
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(4, list[2]);
        list.RemoveAt(0);
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(2, list[0]);
        Assert.AreEqual(4, list[1]);
        list.RemoveAt(1);
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(2, list[0]);
        list.RemoveAt(0);
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestInsert()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        list.Insert(2, 5);
        Assert.AreEqual(5, list.Count);
        Assert.AreEqual(1024, list.Capacity);
    }

    [TestMethod]
    public void TestIndexOf()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        var indexOf2 = list.IndexOf(2);
        Assert.AreEqual(1, indexOf2);
        var indexOf5 = list.IndexOf(5);
        Assert.AreEqual(-1, indexOf5);
    }

    [TestMethod]
    public void TestContains()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.AreEqual(0, list.Count);
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(4, list.Count);
        Assert.IsTrue(list.Contains(2));
        Assert.IsFalse(list.Contains(5));
    }

    [TestMethod]
    public void TestCopyTo()
    {
        var list = new UnsafeList<int>.N1024();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
                var array2 = new int[4];
        list.CopyTo(array2);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, array2);
    }

    [TestMethod]
    public void TestEnumerator()
    {
        var list = new UnsafeList<int>.N1024();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorGeneric()
    {
        var list = new UnsafeList<int>.N1024();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable<int>)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric()
    {
        var list = new UnsafeList<int>.N1024();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        foreach (var item in (IEnumerable)list)
        {
            Assert.AreEqual(list[index], item);
            index++;
        }
    }

    [TestMethod]
    public void TestEnumeratorNonGeneric2()
    {
        var list = new UnsafeList<int>.N1024();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, list.ToArray());
        var index = 0;
        var enumerator = ((IEnumerable)list).GetEnumerator();
        while (enumerator.MoveNext())
        {
            Assert.AreEqual(list[index], enumerator.Current);
            index++;
        }
    }

    [TestMethod]
    public void TestRemoveAtAndGet()
    {
        var list = new UnsafeList<int>.N1024();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Assert.AreEqual(1, list.RemoveAtAndGet(0));
        Assert.AreEqual(2, list.RemoveAtAndGet(0));
        Assert.AreEqual(4, list.RemoveAtAndGet(1));
        Assert.AreEqual(3, list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestRemoveAtAndGetString()
    {
        var list = new UnsafeList<string>.N1024();
        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        Assert.AreEqual("1", list.RemoveAtAndGet(0));
        Assert.AreEqual("2", list.RemoveAtAndGet(0));
        Assert.AreEqual("4", list.RemoveAtAndGet(1));
        Assert.AreEqual("3", list.RemoveAtAndGet(0));
    }

    [TestMethod]
    public void TestInsertByRef()
    {
        var list = new UnsafeList<int>.N1024();
        int item = 1;
        list.InsertByRef(0, item);
        item = 2;
        list.InsertByRef(0, item);
        item = 3;
        list.InsertByRef(0, item);
        item = 4;
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(3, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(1, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestInsertByRefWithString()
    {
        var list = new UnsafeList<string>.N1024();
        Assert.AreEqual(0, list.Count);
        string item = "1";
        list.InsertByRef(0, item);
        item = "2";
        list.InsertByRef(0, item);
        item = "3";
        list.InsertByRef(0, item);
        item = "4";
        list.InsertByRef(3, item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual("3", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("1", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestPushPeekPop()
    {
        var list = new UnsafeList<int>.N1024();
        list.Push(1);
        list.Push(2);
        list.Push(3);
        list.Push(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        Assert.AreEqual(4, list.Peek());
        Assert.AreEqual(4, list.Pop());
        Assert.AreEqual(3, list.Pop());
        Assert.AreEqual(2, list.Pop());
        Assert.AreEqual(1, list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(1024, list.Capacity);
    }

    [TestMethod]
    public void TestPushPeekPopString()
    {
        var list = new UnsafeList<string>.N1024();
        Assert.AreEqual(0, list.Count);
        list.Push("1");
        list.Push("2");
        list.Push("3");
        list.Push("4");
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1024, list.Capacity);
        Assert.AreEqual("4", list.Peek());
        Assert.AreEqual("4", list.Pop());
        Assert.AreEqual("3", list.Pop());
        Assert.AreEqual("2", list.Pop());
        Assert.AreEqual("1", list.Pop());
        Assert.AreEqual(0, list.Count);
        Assert.AreEqual(1024, list.Capacity);
    }

    [TestMethod]
    public void TestAddByRef()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.AreEqual(0, list.Count);
        int item = 1;
        list.AddByRef(item);
        item = 2;
        list.AddByRef(item);
        item = 3;
        list.AddByRef(item);
        item = 4;
        list.AddByRef(item);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestAsSpan()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.AreEqual(0, list.AsSpan().Length);
        list.Add(1);
        list.Add(2);

        var span = list.AsSpan();
        Assert.AreEqual(2, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);

        list.Add(3);
        list.Add(4);
        span = list.AsSpan();
        Assert.AreEqual(4, span.Length);
        Assert.AreEqual(1, span[0]);
        Assert.AreEqual(2, span[1]);
        Assert.AreEqual(3, span[2]);
        Assert.AreEqual(4, span[3]);
    }

    [TestMethod]
    public void TestUnsafeGetRefAt()
    {
        var list = new UnsafeList<int>.N1024()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(1, list.UnsafeGetRefAt(0));
        Assert.AreEqual(2, list.UnsafeGetRefAt(1));
        Assert.AreEqual(3, list.UnsafeGetRefAt(2));
        Assert.AreEqual(4, list.UnsafeGetRefAt(3));
    }

    [TestMethod]
    public void TestRemoveLast()
    {
        var list = new UnsafeList<int>.N1024()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(1, list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestRemoveLastString()
    {
        var list = new UnsafeList<string>.N1024()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.RemoveLast();
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        list.RemoveLast();
        Assert.AreEqual(2, list.Count);
        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        list.RemoveLast();
        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("1", list[0]);
        list.RemoveLast();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestReset()
    {
        var list = new UnsafeList<int>.N1024()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Reset();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClear()
    {
        var list = new UnsafeList<int>.N1024()
        {
            1,
            2,
            3,
            4
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void TestClearWithString()
    {
        var list = new UnsafeList<string>.N1024()
        {
            "1",
            "2",
            "3",
            "4"
        };
        Assert.AreEqual(4, list.Count);
        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    
    [TestMethod]
    public void TestUnsafeGetOrCreate()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.AreEqual(0, list.Count);
        ref var item1 = ref list.UnsafeGetOrCreate(0);
        item1 = 1;
        ref var item2 = ref list.UnsafeGetOrCreate(1);
        item2 = 2;
        ref var item3 = ref list.UnsafeGetOrCreate(2);
        item3 = 3;
        ref var item4 = ref list.UnsafeGetOrCreate(3);
        item4 = 4;
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestUnsafeSetCount()
    {
        var list = new UnsafeList<int>.N1024();
        list.UnsafeSetCount(4);
        Assert.AreEqual(4, list.Count);
        Assert.AreEqual(1024 , list.Capacity);

            }

    [TestMethod]
    public void TestClone()
    {
        var list = new UnsafeList<int>.N1024()
        {
            1,
            2,
            3,
            4
        };
        var clone = list.Clone();
        Assert.AreEqual(4, clone.Count);
        Assert.AreEqual(1024, clone.Capacity);
        Assert.AreEqual(1, clone[0]);
        Assert.AreEqual(2, clone[1]);
        Assert.AreEqual(3, clone[2]);
        Assert.AreEqual(4, clone[3]);
        list[0] = 5;
        Assert.AreEqual(1, clone[0]);
    }

    [TestMethod]
    public void TestIsReadOnly()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.IsFalse(list.IsReadOnly);
    }

    [TestMethod]
    public void TestUnsafeFirstRef()
    {
        var list = new UnsafeList<int>.N1024()
        {
            1,
            2,
            3,
            4
        };
        ref var first = ref list.UnsafeFirstRef;
        Assert.AreEqual(1, first);
        first = 5;
        Assert.AreEqual(5, list[0]);
    }

    [TestMethod]
    public void TestArgumentExceptions()
    {
        var list = new UnsafeList<int>.N1024();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Insert(2, 1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[0] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[1] = 1);
        Assert.ThrowsException<IndexOutOfRangeException>(() => list[4] = 1);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.RemoveAtAndGet(-1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(-1, 1));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.InsertByRef(2, 1));
        Assert.ThrowsException<InvalidOperationException>(() => list.RemoveLast());
        Assert.ThrowsException<InvalidOperationException>(() => list.Peek());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.Pop());
    }

    
    [TestMethod]
    public void TestSort()
    {
        var list = new UnsafeList<int>.N1024()
        {
            4,
            3,
            2,
            1
        };
        list.Sort((left, right) => left.CompareTo(right));
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }

    [TestMethod]
    public void TestSortStringWithComparer()
    {
        var list = new UnsafeList<string>.N1024()
        {
            "4",
            "3",
            "2",
            "1"
        };
        list.Sort(StringComparer.Ordinal);

        Assert.AreEqual("1", list[0]);
        Assert.AreEqual("2", list[1]);
        Assert.AreEqual("3", list[2]);
        Assert.AreEqual("4", list[3]);
    }

    [TestMethod]
    public void TestSortByRef()
    {
        var list = new UnsafeList<int>.N1024()
        {
            4,
            3,
            2,
            1
        };
        list.SortByRef(new IntComparerByRef());
        Assert.AreEqual(1, list[0]);
        Assert.AreEqual(2, list[1]);
        Assert.AreEqual(3, list[2]);
        Assert.AreEqual(4, list[3]);
    }
    
    private struct IntComparerByRef : IComparerByRef<int>
    {
        public bool LessThan(in int left, in int right) => left.CompareTo(right) < 0;
    }
}
