// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

namespace XenoAtom.Collections;

/// <summary>
/// Interface to get a batch of data elements.
/// </summary>
/// <typeparam name="T">The type of the element</typeparam>
public interface IUnsafeDataBatch<T>
{
    /// <summary>
    /// Gets a batch of elements from the data provider with the specified count and capacity.
    /// </summary>
    /// <param name="additionalCount">The number of elements to get.</param>
    /// <param name="marginCount">The additional capacity of elements to get - in addition to the count.</param>
    /// <returns>A reference to the first element of the batch of elements.</returns>
    /// <remarks>
    /// The returned reference is valid only if the data provider is not modified when using this reference.
    /// </remarks>
    ref T UnsafeGetBatch(int additionalCount, int marginCount);
}