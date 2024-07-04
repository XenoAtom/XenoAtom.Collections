// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license.
// See license.txt file in the project root for full license information.

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XenoAtom.Collections;

public static class SpanSortExtensions
{
    // This is the threshold where Introspective sort switches to Insertion sort.
    // Empirically, 16 seems to speed up most cases without slowing down others, at least for integers.
    // Large value types may benefit from a smaller number.
    internal const int IntrosortSizeThreshold = 16;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SortByRef<T, TComparer>(this T[] keys, in TComparer comparer) where TComparer : struct, IComparerByRef<T>
    {
        SortHelperByRef<T, TComparer>.Sort(keys, comparer);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SortByRef<T, TComparer>(this Span<T> keys, in TComparer comparer) where TComparer : struct, IComparerByRef<T>
    {
        SortHelperByRef<T, TComparer>.Sort(keys, comparer);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<T>(this Span<T> keys) where T : IComparable<T>
    {
        SortHelperByRef<T, ComparableComparer<T>>.Sort(keys, new());
    }

    private struct ComparableComparer<T> : IComparerByRef<T> where T: IComparable<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool LessThan(in T left, in T right) => left.CompareTo(right) < 0;
    }

    internal static class SortHelperByRef<T, TComparer> where TComparer : IComparerByRef<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Sort(Span<T> keys, in TComparer comparer)
        {
            if (keys.Length > 1)
            {
                IntroSort(ref MemoryMarshal.GetReference(keys), keys.Length, 2 * (BitOperations.Log2((uint)keys.Length) + 1), comparer);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SwapIfGreater(ref T rkey, TComparer comparer, nint i, nint j)
        {
            Debug.Assert(i != j);

            // i and j swapped to compare if keys[i] is greater than keys[j] by performing keys[j] < keys[i]
            if (comparer.LessThan(in Unsafe.Add(ref rkey, j), in Unsafe.Add(ref rkey, i)))
            {
                T key = Unsafe.Add(ref rkey, i);
                Unsafe.Add(ref rkey, i) = Unsafe.Add(ref rkey, j);
                Unsafe.Add(ref rkey, j) = key;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Swap(ref T rkey, nint i, nint j)
        {
            Debug.Assert(i != j);

            T t = Unsafe.Add(ref rkey, i);
            Unsafe.Add(ref rkey, i) = Unsafe.Add(ref rkey, j);
            Unsafe.Add(ref rkey, j) = t;
        }

        private static void IntroSort(ref T rkey, nint partitionSize, nint depthLimit, in TComparer comparer)
        {
            Debug.Assert(depthLimit >= 0);

            while (partitionSize > 1)
            {
                if (partitionSize <= IntrosortSizeThreshold)
                {

                    if (partitionSize == 2)
                    {
                        SwapIfGreater(ref rkey, comparer, 0, 1);
                        return;
                    }

                    if (partitionSize == 3)
                    {
                        SwapIfGreater(ref rkey, comparer, 0, 1);
                        SwapIfGreater(ref rkey, comparer, 0, 2);
                        SwapIfGreater(ref rkey, comparer, 1, 2);
                        return;
                    }

                    InsertionSort(ref rkey, partitionSize, comparer);
                    return;
                }

                if (depthLimit == 0)
                {
                    HeapSort(ref rkey, partitionSize, comparer);
                    return;
                }
                depthLimit--;

                nint p = PickPivotAndPartition(ref rkey, partitionSize, comparer);

                // Note we've already partitioned around the pivot and do not have to move the pivot again.
                IntroSort(ref Unsafe.Add(ref rkey, p + 1), partitionSize - p - 1, depthLimit, comparer);
                partitionSize = p;
            }
        }

        private static nint PickPivotAndPartition(ref T rkey, nint partitionSize, in TComparer comparer)
        {
            Debug.Assert(partitionSize >= IntrosortSizeThreshold);

            nint hi = partitionSize - 1;

            // Compute median-of-three.  But also partition them, since we've done the comparison.
            nint middle = hi >> 1;

            // Sort lo, mid and hi appropriately, then pick mid as the pivot.
            SwapIfGreater(ref rkey, comparer, 0, middle);  // swap the low with the mid point
            SwapIfGreater(ref rkey, comparer, 0, hi);   // swap the low with the high
            SwapIfGreater(ref rkey, comparer, middle, hi); // swap the middle with the high

            T pivot = Unsafe.Add(ref rkey, middle);
            Swap(ref rkey, middle, hi - 1);
            nint left = 0, right = hi - 1;  // We already partitioned lo and hi and put the pivot in hi - 1.  And we pre-increment & decrement below.

            while (left < right)
            {
                while (comparer.LessThan(Unsafe.Add(ref rkey, ++left), pivot))
                {
                }

                while (comparer.LessThan(pivot, Unsafe.Add(ref rkey, --right)))
                {
                }

                if (left >= right)
                    break;

                Swap(ref rkey, left, right);
            }

            // Put pivot in the right location.
            if (left != hi - 1)
            {
                Swap(ref rkey, left, hi - 1);
            }
            return left;
        }

        private static void HeapSort(ref T rkey, nint n, in TComparer comparer)
        {
            for (nint i = n >> 1; i >= 1; i--)
            {
                DownHeap(ref rkey, i, n, comparer);
            }

            for (nint i = n; i > 1; i--)
            {
                Swap(ref rkey, 0, i - 1);
                DownHeap(ref rkey, 1, i - 1, comparer);
            }
        }

        private static void DownHeap(ref T rkey, nint i, nint n, in TComparer comparer)
        {
            T d = Unsafe.Add(ref rkey, i - 1);
            while (i <= n >> 1)
            {
                nint child = 2 * i;
                if (child < n && comparer.LessThan(Unsafe.Add(ref rkey, child - 1), Unsafe.Add(ref rkey, child)))
                {
                    child++;
                }

                if (!(comparer.LessThan(d, Unsafe.Add(ref rkey, child - 1))))
                    break;

                Unsafe.Add(ref rkey, i - 1) = Unsafe.Add(ref rkey, child - 1);
                i = child;
            }

            Unsafe.Add(ref rkey, i - 1) = d;
        }

        private static void InsertionSort(ref T rkey, nint length, in TComparer comparer)
        {
            for (nint i = 0; i < length - 1; i++)
            {
                T t = Unsafe.Add(ref rkey, i + 1);

                nint j = i;
                while (j >= 0 && comparer.LessThan(t, Unsafe.Add(ref rkey, j)))
                {
                    Unsafe.Add(ref rkey, j + 1) = Unsafe.Add(ref rkey, j);
                    j--;
                }

                Unsafe.Add(ref rkey, j + 1) = t;
            }
        }
    }
}
