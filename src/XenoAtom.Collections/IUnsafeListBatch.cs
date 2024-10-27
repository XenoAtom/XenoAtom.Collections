// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

namespace XenoAtom.Collections;

/// <summary>
/// Interface to get a batch of elements from an <see cref="UnsafeList{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the element</typeparam>
[Obsolete($"Use IUnsafeDataBatch<T> instead")]
public interface IUnsafeListBatch<T> : IUnsafeDataBatch<T>;