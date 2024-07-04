using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XenoAtom.Collections;

public interface IInlineArray<T>
{
    abstract static int Length { get; }
}


public static class InlineArrayExtension
{
    public static Span<T> AsSpan<TInlineArray, T>(ref this TInlineArray array) where TInlineArray: struct, IInlineArray<T>
    {
        return MemoryMarshal.CreateSpan(ref Unsafe.As<TInlineArray, T>(ref array), TInlineArray.Length);
    }
}
