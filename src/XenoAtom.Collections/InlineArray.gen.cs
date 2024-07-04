
using System.Runtime.CompilerServices;

namespace XenoAtom.Collections;

[InlineArray(2)]
public struct InlineArray2<T> : IInlineArray<T>
{
    private T _firstElement;

    public static int Length => 2;
}

[InlineArray(3)]
public struct InlineArray3<T> : IInlineArray<T>
{
    private T _firstElement;

    public static int Length => 3;
}

[InlineArray(4)]
public struct InlineArray4<T> : IInlineArray<T>
{
    private T _firstElement;

    public static int Length => 4;
}

[InlineArray(5)]
public struct InlineArray5<T> : IInlineArray<T>
{
    private T _firstElement;

    public static int Length => 5;
}

[InlineArray(6)]
public struct InlineArray6<T> : IInlineArray<T>
{
    private T _firstElement;

    public static int Length => 6;
}

[InlineArray(7)]
public struct InlineArray7<T> : IInlineArray<T>
{
    private T _firstElement;

    public static int Length => 7;
}

[InlineArray(8)]
public struct InlineArray8<T> : IInlineArray<T>
{
    private T _firstElement;

    public static int Length => 8;
}
