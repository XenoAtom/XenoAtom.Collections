using BenchmarkDotNet.Running;

namespace XenoAtom.Collections.Bench;

internal class Program
{
    static void Main(string[] args)
    {
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}