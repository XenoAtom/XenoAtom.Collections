// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace XenoAtom.Collections;

internal static class ThrowHelper
{
    public const string Arg_RankMultiDimNotSupported = "Only single dimensional arrays are supported for the requested action";
    public const string NotSupported_KeyCollectionSet = "Mutating a key collection derived from a dictionary is not allowed.";
    public const string Arg_ArrayPlusOffTooSmall = "Destination array is not long enough to copy all the items in the collection. Check array index and length";
    public const string Arg_NonZeroLowerBound = "The lower bound of target array must be zero.";
    public const string NotSupported_ValueCollectionSet = "Mutating a value collection derived from a dictionary is not allowed.";

    [Conditional("DEBUG")]
    public static void CheckOutOfRange(int index, int length)
    {
        if ((uint)index >= (uint)length) throw new IndexOutOfRangeException($"Index {index} is out of range of length {length}");
    }

    [DoesNotReturn]
    public static void ThrowIndexOutOfRangeException(int index)
    {
        throw new IndexOutOfRangeException($"Index {index} is out of range");
    }

    [DoesNotReturn]
    public static void ThrowIndexOutOfRangeException(nint index)
    {
        throw new IndexOutOfRangeException($"Index {index} is out of range");
    }

    public static void ThrowArgumentOutOfRangeException(ExceptionArgument ex)
    {
        throw GetArgumentOutOfRangeException(ex);
    }

    public static void ThrowArgumentNullException(ExceptionArgument ex)
    {
        throw GetArgumentNullException(ex);
    }

    private static ArgumentOutOfRangeException GetArgumentOutOfRangeException(ExceptionArgument argument)
    {
        return new ArgumentOutOfRangeException(GetArgumentName(argument));
    }

    private static ArgumentNullException GetArgumentNullException(ExceptionArgument argument)
    {
        return new ArgumentNullException(GetArgumentName(argument));
    }

    // This function will convert an ExceptionArgument enum value to the argument name string.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static string GetArgumentName(ExceptionArgument argument)
    {
        Debug.Assert(Enum.IsDefined(typeof(ExceptionArgument), argument), "The enum value is not defined, please check the ExceptionArgument Enum.");
        return argument.ToString();
    }

    [DoesNotReturn]
    public static void ThrowNotSupportedException(string notSupportedKeyCollectionSet)
    {
        throw new NotSupportedException(notSupportedKeyCollectionSet);
    }

    [DoesNotReturn]
    public static void ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen()
    {
        throw new InvalidOperationException();
    }

    [DoesNotReturn]
    public static void ThrowKeyNotFoundException<TKey>(TKey key)
    {
        throw new KeyNotFoundException($"Key {key} was not found");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void ThrowInvalidCurrentContainer()
    {
        throw new InvalidOperationException("Cannot add a child. The current container is not set.");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void ThrowInvalidContainer(ExceptionArgument argument)
    {
        throw new ArgumentException("The node is not a container", GetArgumentName(argument));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void ThrowCannotPopCurrentContainer()
    {
        throw new InvalidOperationException($"Cannot pop current container. The current container is null or root.");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void ThrowKeyAlreadyInserted(ExceptionArgument argName, string name)
    {
        throw new ArgumentException($"Invalid key {name} is duplicated", GetArgumentName(argName));
    }

    [DoesNotReturn]
    public static void ThrowArgumentException(ExceptionArgument argArrayPlusOffTooSmall)
    {
        throw new NotImplementedException();
    }

    [DoesNotReturn]
    public static void ThrowArgumentException(string message)
    {
        throw new ArgumentException(message);
    }

    [DoesNotReturn]
    public static void ThrowIndexArgumentOutOfRange_NeedNonNegNumException()
    {
        throw new IndexOutOfRangeException();
    }

    [DoesNotReturn]
    public static void ThrowAddingDuplicateWithKeyArgumentException<TKey>(TKey key)
    {
        throw new ArgumentException($"Invalid key {key} duplicated");
    }

    [DoesNotReturn]
    public static void ThrowInvalidOperationSystemParameters()
    {
        throw new InvalidOperationException("The SystemParameters cannot be retrieved");
    }

    [DoesNotReturn]
    internal static void ThrowArgumentException_Argument_IncompatibleArrayType()
    {
        throw new ArgumentException("Target array type is not compatible with the type of items in the collection.");
    }

    [DoesNotReturn]
    public static void ThrowInvalidOperationPeekOnEmptyList()
    {
        throw new InvalidOperationException("Cannot peek an element from an empty list");
    }

    [DoesNotReturn]
    public static void ThrowInvalidOperationRemoveOnEmptyList()
    {
        throw new InvalidOperationException("Cannot remove an element from an empty list");
    }
}

internal enum ExceptionArgument
{
    index,
    array,
    capacity,
    dictionary,
    collection,
    key,
    min,
    match,
    count
}