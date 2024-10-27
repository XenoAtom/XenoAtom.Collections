// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

namespace XenoAtom.Collections;

/// <summary>
/// Interface to get a batch of elements from an <see cref="UnsafeList{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the element</typeparam>
public interface IUnsafeListBatch<T>
{
    /// <summary>
    /// Gets a batch of elements from the list with the specified additional count and capacity.
    /// </summary>
    /// <param name="additionalCount">The additional count of elements to get</param>
    /// <param name="marginCount">The additional capacity of elements to get - in addition to the count</param>
    /// <returns>A reference to the first element of the batch of elements.</returns>
    ref T UnsafeGetBatch(int additionalCount, int marginCount); // TODO: rename marginCount to additionalCapacity during a breaking change version
}