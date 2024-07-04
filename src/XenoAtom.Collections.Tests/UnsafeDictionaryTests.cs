// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Runtime.CompilerServices;

namespace XenoAtom.Collections.Tests;

[TestClass]
public class UnsafeDictionaryTests
{
    // TODO: Add more tests

    [TestMethod]
    public void TestAdd()
    {
        var dict = new UnsafeDictionary<int, int>();
        dict.Add(1, 1);
        Assert.IsTrue(dict.ContainsKey(1));
        Assert.AreEqual(1, dict[1]);
        Assert.AreEqual(1, dict.Count);
        
        dict.Add(2, 2);
        Assert.IsTrue(dict.ContainsKey(2));
        Assert.AreEqual(2, dict[2]);
        Assert.AreEqual(2, dict.Count);

        dict.Add(3, 3);
        Assert.IsTrue(dict.ContainsKey(3));
        Assert.AreEqual(3, dict[3]);
        Assert.AreEqual(3, dict.Count);

        dict.Add(4, 4);
        Assert.IsTrue(dict.ContainsKey(4));
        Assert.AreEqual(4, dict[4]);
        Assert.AreEqual(4, dict.Count);

        Assert.IsTrue(dict.ContainsKey(1));
        Assert.IsTrue(dict.ContainsKey(2));
        Assert.IsTrue(dict.ContainsKey(3));
        Assert.IsTrue(dict.ContainsKey(4));
    }

    [TestMethod]
    public void TestAddRemove()
    {
        var dict = new UnsafeDictionary<int, int>();
        dict.Add(1, 1);
        Assert.IsTrue(dict.ContainsKey(1));
        dict.Remove(1);
        Assert.IsFalse(dict.ContainsKey(1));

        Assert.AreEqual(0, dict.Count);
    }

    // Generate lots of different test cases with other methods

    [TestMethod]
    public void TestKeys()
    {
        var dict = new UnsafeDictionary<int, int>();
        dict.Add(1, 1);
        dict.Add(2, 2);
        dict.Add(3, 3);
        dict.Add(4, 4);

        var keys = dict.Keys;
        Assert.AreEqual(4, keys.Count);
        Assert.IsTrue(keys.Contains(1));
        Assert.IsTrue(keys.Contains(2));
        Assert.IsTrue(keys.Contains(3));
        Assert.IsTrue(keys.Contains(4));
    }

    [TestMethod]
    public void TestValues()
    {
        var dict = new UnsafeDictionary<int, int>();
        dict.Add(1, 5);
        dict.Add(2, 6);
        dict.Add(3, 7);
        dict.Add(4, 8);

        var values = dict.Values;
        Assert.AreEqual(4, values.Count);
        Assert.IsTrue(values.Contains(5));
        Assert.IsTrue(values.Contains(6));
        Assert.IsTrue(values.Contains(7));
        Assert.IsTrue(values.Contains(8));
    }

    [TestMethod]
    public void TestClear()
    {
        var dict = new UnsafeDictionary<int, int>();
        dict.Add(1, 1);
        dict.Add(2, 2);
        dict.Add(3, 3);
        dict.Add(4, 4);

        Assert.AreEqual(4, dict.Count);
        dict.Clear();
        Assert.AreEqual(0, dict.Count);
    }

    [TestMethod]
    public void TestTryGetValue()
    {
        var dict = new UnsafeDictionary<int, int>();
        dict.Add(1, 1);
        dict.Add(2, 2);
        dict.Add(3, 3);
        dict.Add(4, 4);

        Assert.IsTrue(dict.TryGetValue(1, out var value));
        Assert.AreEqual(1, value);

        Assert.IsTrue(dict.TryGetValue(2, out value));
        Assert.AreEqual(2, value);

        Assert.IsTrue(dict.TryGetValue(3, out value));
        Assert.AreEqual(3, value);

        Assert.IsTrue(dict.TryGetValue(4, out value));
        Assert.AreEqual(4, value);

        Assert.IsFalse(dict.TryGetValue(5, out _));
    }

    [TestMethod]
    public void TestContainsKey()
    {
        var dict = new UnsafeDictionary<int, int>();
        dict.Add(1, 1);
        dict.Add(2, 2);
        dict.Add(3, 3);
        dict.Add(4, 4);

        Assert.IsTrue(dict.ContainsKey(1));
        Assert.IsTrue(dict.ContainsKey(2));
        Assert.IsTrue(dict.ContainsKey(3));
        Assert.IsTrue(dict.ContainsKey(4));
        Assert.IsFalse(dict.ContainsKey(5));
    }

    [TestMethod]
    public void TestAddByRef()
    {
        var dict = new UnsafeDictionary<int, int>();
        dict.Add(1, 1);
        dict.Add(2, 2);
        dict.Add(3, 3);
        dict.Add(4, 4);

        ref var value = ref dict.GetValueRefOrAddDefault(5, out bool exists);
        Assert.IsFalse(exists);
        value = 6;
        Assert.IsTrue(dict.ContainsKey(5));
        Assert.AreEqual(6, dict[5]);


        ref var nullRef = ref dict.GetValueRefOrNullRef(6, out exists);
        Assert.IsFalse(exists);
        Assert.IsTrue(Unsafe.IsNullRef(ref nullRef));
    }
}
