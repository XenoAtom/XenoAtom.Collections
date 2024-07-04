// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Collections;

namespace XenoAtom.Collections.Tests;

[TestClass]
public class UnsafeHashSetTests
{
    // TODO: Add more tests
    
    [TestMethod]
    public void TestAdd()
    {
        var set = new UnsafeHashSet<int>();
        Assert.IsTrue(set.Add(1));
        Assert.IsFalse(set.Add(1));
        Assert.IsTrue(set.Contains(1));
        Assert.IsFalse(set.Contains(2));
    }

    [TestMethod]
    public void TestAddRemove()
    {
        var set = new UnsafeHashSet<int>();
        set.Add(1);
        Assert.IsTrue(set.Contains(1));
        set.Remove(1);
        Assert.IsFalse(set.Contains(1));
    }

    [TestMethod]
    public void TestAddRemoveAdd()
    {
        var set = new UnsafeHashSet<int>();
        set.Add(1);
        Assert.IsTrue(set.Contains(1));
        set.Remove(1);
        Assert.IsFalse(set.Contains(1));
        set.Add(1);
        Assert.IsTrue(set.Contains(1));
    }

    [TestMethod]
    public void TestAddRemoveAddRemove()
    {
        var set = new UnsafeHashSet<int>();
        set.Add(1);
        Assert.IsTrue(set.Contains(1));
        set.Remove(1);
        Assert.IsFalse(set.Contains(1));
        set.Add(1);
        Assert.IsTrue(set.Contains(1));
        set.Remove(1);
        Assert.IsFalse(set.Contains(1));
    }

    [TestMethod]
    public void TestAddRemoveAddRemoveAdd()
    {
        var set = new UnsafeHashSet<int>();
        set.Add(1);
        Assert.IsTrue(set.Contains(1));
        set.Remove(1);
        Assert.IsFalse(set.Contains(1));
        set.Add(1);
        Assert.IsTrue(set.Contains(1));
        set.Remove(1);
        Assert.IsFalse(set.Contains(1));
        set.Add(1);
        Assert.IsTrue(set.Contains(1));
    }

    [TestMethod]
    public void TestAddRemoveAddRemoveAddRemove()
    {
        var set = new UnsafeHashSet<int>();
        set.Add(1);
        Assert.IsTrue(set.Contains(1));
        set.Remove(1);
        Assert.IsFalse(set.Contains(1));
        set.Add(1);
        Assert.IsTrue(set.Contains(1));
        set.Remove(1);
        Assert.IsFalse(set.Contains(1));
        set.Add(1);
        Assert.IsTrue(set.Contains(1));
        set.Remove(1);
        Assert.IsFalse(set.Contains(1));
    }

    [TestMethod]
    public void TestMultipleAdds()
    {
        var set = new UnsafeHashSet<int>();
        for (int i = 0; i < 1000; i++)
        {
            Assert.IsTrue(set.Add(i));
            Assert.IsFalse(set.Add(i));
            Assert.IsTrue(set.Contains(i));
        }
        Assert.AreEqual(1000, set.Count);
    }
}