using BenchmarkDotNet.Attributes;
using DotNext;
using DotNext.Reflection;

namespace ConsoleApp1
{
    
    //|                            Method |     Mean |   Error |  StdDev |
    //|---------------------------------- |---------:|--------:|--------:|
    //|              WithDotNetReflection | 217.8 ns | 1.68 ns | 1.49 ns |
    //| WithDotNextReflectionWithDelegate | 179.0 ns | 1.57 ns | 1.47 ns |
    //| WithDotNextReflectionWithFunction | 163.8 ns | 0.92 ns | 0.82 ns |
    //|                     WithActivator | 288.1 ns | 4.14 ns | 3.46 ns |


    public class Benchmark
    {
        [Benchmark]
        public void WithDotNetReflection()
        {
            var constructor = typeof(MyClass).GetConstructor(new[] { typeof(string), typeof(int) });

            var n = new object[] { "test", 5 };

            var b = (MyClass)constructor.Invoke(n);
        }

        //[Benchmark]
        //public void WithDotNextReflection()
        //{
        //    var t = typeof(MyClass).GetConstructor(new[] { typeof(string), typeof(int) }).Unreflect();

        //    var n = new object[] { "test", 5 };

        //    var b = (MyClass)t(null, n);
        //}

        [Benchmark]
        public void WithDotNextReflectionWithDelegate()
        {
            var t = typeof(MyClass).GetConstructor(new[] { typeof(string), typeof(int) }).Unreflect<Func<string, int, MyClass>>();
            
            var b = t.Invoke("test", 5);
        }

        [Benchmark]
        public void WithDotNextReflectionWithFunction()
        {
            Function <(string,int),MyClass> t = typeof(MyClass).GetConstructor(new[] { typeof(string), typeof(int) }).Unreflect<Function<(string, int), MyClass>>();

            var args = t.ArgList();
            args.Item1 = "test";
            args.Item2 = 5;

            var b= t(args);
        }

        [Benchmark]
        public void WithActivator()
        {
            var t = Activator.CreateInstance(typeof(MyClass), new object[] { "test", 5 });
        }
    }
}
