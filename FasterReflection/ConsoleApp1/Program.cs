using BenchmarkDotNet.Running;
using DotNext.Reflection;
using System.Diagnostics;
using System.Reflection;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmark>();
        }
    }
}