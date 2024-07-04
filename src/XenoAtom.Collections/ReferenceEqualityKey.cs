// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XenoAtom.Collections;

[DebuggerDisplay("{Value,nq}")]
public readonly record struct ReferenceEqualityKey<T>(T Value) where T: class
{
    public bool Equals(ReferenceEqualityKey<T> other) => ReferenceEquals(Value, other.Value);

    public override int GetHashCode() => RuntimeHelpers.GetHashCode(Value);

    public static implicit operator ReferenceEqualityKey<T>(T value) => new(value);
}


