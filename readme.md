# XenoAtom.Collections [![ci](https://github.com/XenoAtom/XenoAtom.Collections/actions/workflows/ci.yml/badge.svg)](https://github.com/XenoAtom/XenoAtom.Collections/actions/workflows/ci.yml) [![NuGet](https://img.shields.io/nuget/v/XenoAtom.Collections.svg)](https://www.nuget.org/packages/XenoAtom.Collections/)

<img align="right" width="160px" height="160px" src="https://raw.githubusercontent.com/XenoAtom/XenoAtom.Collections/main/img/XenoAtom.Collections.png">

This library provides struct based collections for high performance and low memory usage. It includes `UnsafeList<T>`, `UnsafeDictionary<TKey, TValue>` and `UnsafeHashSet<T>`. These collections are value types with a backed by a managed array and designed to be used in performance critical scenarios where you want to avoid memory allocations of an additional managed object for the container.

The code has been derived from the .NET Core runtime released under the MIT license.

> These collections contains unsafe methods (prefixed by Unsafe) and should be used with caution.

## ✨ Features

- 3 collections: `UnsafeList<T>`, `UnsafeDictionary<TKey, TValue>` and `UnsafeHashSet<T>`
- Faster access for Dictionary and HashSet by requiring the key to be `IEquatable<TKey>`
- Struct based collections to avoid an allocation for the container and improve locality
- Proper debugger support for collections with custom `DebuggerDisplay`
- A few advanced unsafe methods to avoid checks
    - e.g `Unsafe<T>.UnsafeSetCount`, `Unsafe<T>.UnsafeGetRefAt`...
- NativeAOT compatible
- `net8.0`+ support

## 📖 Usage

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

## 🪪 License

This software is released under the [BSD-2-Clause license](https://opensource.org/licenses/BSD-2-Clause). 

## 🤗 Author

Alexandre Mutel aka [XenoAtom](https://xoofx.github.io).
