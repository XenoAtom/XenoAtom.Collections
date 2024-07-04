using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XenoAtom.Collections;

internal static class HashHelpers
{
    const uint c1 = 0xcc9e2d51;
    const uint c2 = 0x1b873593;

    // Licensed to the .NET Foundation under one or more agreements.
    // The .NET Foundation licenses this file to you under the MIT license.
    // See the LICENSE file in the project root for more information.

    public const int HashCollisionThreshold = 100;

    public const int HashPrime = 101;

    // Table of prime numbers to use as hash table sizes. 
    // A typical resize algorithm would pick the smallest prime number in this array
    // that is larger than twice the previous capacity. 
    // Suppose our Hashtable currently has capacity x and enough elements are added 
    // such that a resize needs to occur. Resizing first computes 2x then finds the 
    // first prime in the table greater than 2x, i.e. if primes are ordered 
    // p_1, p_2, ..., p_i, ..., it finds p_n such that p_n-1 < 2x < p_n. 
    // Doubling is important for preserving the asymptotic complexity of the 
    // hashtable operations such as add.  Having a prime guarantees that double 
    // hashing does not lead to infinite loops.  IE, your hash function will be 
    // h1(key) + i*h2(key), 0 <= i < size.  h2 and the size must be relatively prime.
    public static readonly int[] Primes = {
        3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
        1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
        17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
        187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
        1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369};

    public static bool IsPrime(int candidate)
    {
        if ((candidate & 1) != 0)
        {
            int limit = (int)Math.Sqrt(candidate);
            for (int divisor = 3; divisor <= limit; divisor += 2)
            {
                if ((candidate % divisor) == 0)
                    return false;
            }
            return true;
        }
        return (candidate == 2);
    }

    public static int GetPrime(int min)
    {
        if (min < 0)
            ThrowHelper.ThrowArgumentException(ExceptionArgument.min);

        for (int i = 0; i < Primes.Length; i++)
        {
            int prime = Primes[i];
            if (prime >= min) return prime;
        }

        //outside of our predefined table. 
        //compute the hard way. 
        for (int i = (min | 1); i < Int32.MaxValue; i += 2)
        {
            if (IsPrime(i) && ((i - 1) % HashPrime != 0))
                return i;
        }
        return min;
    }

    // Returns size of hashtable to grow to.
    public static int ExpandPrime(int oldSize)
    {
        int newSize = 2 * oldSize;

        // Allow the hashtables to grow to maximum possible size (~2G elements) before encoutering capacity overflow.
        // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
        if ((uint)newSize > MaxPrimeArrayLength && MaxPrimeArrayLength > oldSize)
        {
            Debug.Assert(MaxPrimeArrayLength == GetPrime(MaxPrimeArrayLength), "Invalid MaxPrimeArrayLength");
            return MaxPrimeArrayLength;
        }

        return GetPrime(newSize);
    }


    // This is the maximum prime smaller than Array.MaxArrayLength
    public const int MaxPrimeArrayLength = 0x7FEFFFFD;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SimpleHash(int hash, int value)
    {
        return (hash * 397) ^ value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long SimpleHash(long hash, int value)
    {
        return (hash * 397) ^ value;
    }

    public static unsafe void MurmurHash3(void* key, int len, uint seed, void* output)
    {
        byte* data = (byte*)key;
        int nblocks = len / 4;

        uint h1 = seed;

        //----------
        // body

        uint* blocks = (uint*)(data + nblocks * 4);

        for (int i = -nblocks; i != 0; i++)
        {
            uint k1 = blocks[i];

            k1 *= c1;
            k1 = Rotl32(k1, 15);
            k1 *= c2;

            h1 ^= k1;
            h1 = Rotl32(h1, 13);
            h1 = h1 * 5 + 0xe6546b64;
        }

        //----------
        // tail

        byte* tail = (byte*)(data + nblocks * 4);

        uint k2 = 0;

        switch (len & 3)
        {
            case 3:
                k2 ^= (uint)(tail[2] << 16);
                k2 ^= (uint)(tail[1] << 8);
                k2 ^= tail[0];
                k2 *= c1; k2 = Rotl32(k2, 15); k2 *= c2; h1 ^= k2;
                break;
            case 2:
                k2 ^= (uint)(tail[1] << 8);
                k2 ^= tail[0];
                k2 *= c1; k2 = Rotl32(k2, 15); k2 *= c2; h1 ^= k2;
                break;
            case 1:
                k2 ^= tail[0];
                k2 *= c1; k2 = Rotl32(k2, 15); k2 *= c2; h1 ^= k2;
                break;
        };

        //----------
        // finalization

        h1 ^= (uint)len;

        h1 = Fmix32(h1);

        *(uint*)output = h1;
    }

    public static uint MurmurHash3(int[] key, uint seed)
    {
        uint h1 = seed;

        //----------
        // body
        for (int i = 0; i < key.Length; i++)
        {
            uint k1 = (uint)key[i];

            k1 *= c1;
            k1 = Rotl32(k1, 15);
            k1 *= c2;

            h1 ^= k1;
            h1 = Rotl32(h1, 13);
            h1 = h1 * 5 + 0xe6546b64;
        }

        //----------
        // finalization

        h1 ^= (uint)key.Length;
        h1 = Fmix32(h1);
        return h1;
    }

    public static uint MurmurHash3(uint k1)
    {
        uint h1 = 0;

        //----------
        // body
        {
            k1 *= c1;
            k1 = Rotl32(k1, 15);
            k1 *= c2;

            h1 ^= k1;
            h1 = Rotl32(h1, 13);
            h1 = h1 * 5 + 0xe6546b64;
        }

        //----------
        // finalization

        h1 ^= 1;
        h1 = Fmix32(h1);
        return h1;
    }

    public static uint MurmurHash3(int[] key, int len, uint seed)
    {
        uint h1 = seed;

        //----------
        // body
        for (int i = 0; i < len; i++)
        {
            uint k1 = (uint)key[i];

            k1 *= c1;
            k1 = Rotl32(k1, 15);
            k1 *= c2;

            h1 ^= k1;
            h1 = Rotl32(h1, 13);
            h1 = h1 * 5 + 0xe6546b64;
        }

        //----------
        // finalization

        h1 ^= (uint)len;
        h1 = Fmix32(h1);
        return h1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint Fmix32(uint h)
    {
        h ^= h >> 16;
        h *= 0x85ebca6b;
        h ^= h >> 13;
        h *= 0xc2b2ae35;
        h ^= h >> 16;

        return h;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint Rotl32(uint x, int r)
    {
        return (x << r) | (x >> (32 - r));
    }
}