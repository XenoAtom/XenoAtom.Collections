// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

namespace XenoAtom.Collections;

public interface IComparerByRef<T>
{
    bool LessThan(in T left, in T right);
}