# XenoAtom.Collections [![ci](https://github.com/XenoAtom/XenoAtom.Collections/actions/workflows/ci.yml/badge.svg)](https://github.com/XenoAtom/XenoAtom.Collections/actions/workflows/ci.yml) [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Collections.svg)](https://www.nuget.org/packages/XenoAtom.Collections/)

<img align="right" width="160px" height="160px" src="https://raw.githubusercontent.com/XenoAtom/XenoAtom.Collections/main/img/XenoAtom.Collections.png">

This .NET library offers structure-based collections optimized for high performance and minimal memory usage. It features `UnsafeList<T>`, `UnsafeDictionary<TKey, TValue>`, and `UnsafeHashSet<T>`. These collections are implemented as value types backed by a managed array (except for fixed capacity list `UnsafeList<T>.N2` ... `UnsafeList<T>.N1024`), specifically designed for performance-critical scenarios where avoiding additional memory allocations for container objects is essential.

The code has been derived from the .NET Core runtime released under the MIT license.

> These collections contains unsafe methods (prefixed by Unsafe) and should be used with caution.

## ✨ Features

- Three main collections: `UnsafeList<T>`, `UnsafeDictionary<TKey, TValue>` and `UnsafeHashSet<T>`
  - Plus fixed capacity list for efficient stack allocation:
    - `UnsafeList<T>.N2`
    - `UnsafeList<T>.N4`
    - `UnsafeList<T>.N8`
    - `UnsafeList<T>.N16`
    - `UnsafeList<T>.N32`
    - `UnsafeList<T>.N64`
    - `UnsafeList<T>.N128`
    - `UnsafeList<T>.N256`
    - `UnsafeList<T>.N512`
    - `UnsafeList<T>.N1024`
- Faster access for Dictionary and HashSet by requiring the key to be `IEquatable<TKey>`
- Struct based collections to avoid an allocation for the container and improve locality
- Proper debugger support for collections with custom `DebuggerDisplay`
- A few advanced unsafe methods to avoid checks
    - e.g `Unsafe<T>.UnsafeSetCount`, `Unsafe<T>.UnsafeGetRefAt`...
- NativeAOT compatible
- `net8.0`+ support

## 📖 Usage

In general, the API is similar to the standard collections.

> It is recommended to not expose in public APIs these unsafe collections but instead use them internally for performance-critical scenarios.

Here are some examples


- `UnsafeList<T>`
  ```c#
  // UnsafeList is a struct, but the underlying array is a managed array
  var list = new UnsafeList<int>();
  list.Add(1);
  list.Add(2);
  list.Add(3);
  list.Add(4);
  var span = list.AsSpan();
  foreach(var value in span)
  {
      Console.WriteLine(value);
  }
  ```
- `UnsafeList<T>.N4`
  ```c#
  // UnsafeList is a struct, and the underlying array is a fixed size array (on the stack)
  var list = new UnsafeList<int>.N4();
  list.Add(1);
  list.Add(2);
  list.Add(3);
  list.Add(4);
  var span = list.AsSpan();
  foreach(var value in span)
  {
      Console.WriteLine(value);
  }
  ```
- `UnsafeDictionary<TKey, TValue>`
  ```c#
  // UnsafeDictionary is a struct, but the underlying array is a managed array
  var dictionary = new UnsafeDictionary<int, string>();
  dictionary.Add(1, "One");
  dictionary.Add(2, "Two");
  dictionary.Add(3, "Three");
  dictionary.Add(4, "Four");
  foreach(var pair in dictionary)
  {
      Console.WriteLine($"{pair.Key} = {pair.Value}");
  }
  ```
- `UnsafeHashSet<T>`
  ```c#
  // UnsafeDictionary is a struct, but the underlying array is a managed array
  var hashSet = new UnsafeHashSet<int>();
  hashSet.Add(1);
  hashSet.Add(2);
  hashSet.Add(3);
  hashSet.Add(4);
  foreach(var value in hashSet)
  {
      Console.WriteLine(value);
  }
  ```

## 📊 Benchmarks

Some benchmarks are available in the `src\XenoAtom.Collections.Bench` folder. Here are some results:

- `UnsafeList<T>` and `UnsafeDictionary<TKey, TValue>` can be up to 20% faster than the standard `List<T>` and `Dictionary<TKey, TValue>` for some scenarios.
- `UnsafeList<T>.N16` can be up to 50% faster than the standard `List<T>` for adding elements.


| Method                    | Mean     | Error     | StdDev    | Ratio |
|-------------------------- |---------:|----------:|----------:|------:|
| `UnsafeList<int>.Add`     | 4.807 ns | 0.0667 ns | 0.0624 ns |  0.79 |
| `UnsafeList<int>.N16.Add` | 2.570 ns | 0.0172 ns | 0.0161 ns |  0.42 |
| `List<int>.Add`           | 6.090 ns | 0.0361 ns | 0.0338 ns |  1.00 |


| Method                       | Mean     | Error    | StdDev   | Ratio |
|----------------------------- |---------:|---------:|---------:|------:|
| `UnsafeDictionary<int, int>` | 44.85 ns | 0.577 ns | 0.539 ns |  0.81 |
| `Dictionary<int, int>`       | 55.06 ns | 0.307 ns | 0.287 ns |  1.00 |



## 🪪 License

This software is released under the [BSD-2-Clause license](https://opensource.org/licenses/BSD-2-Clause). 

## 🤗 Author

Alexandre Mutel aka [XenoAtom](https://xoofx.github.io).
