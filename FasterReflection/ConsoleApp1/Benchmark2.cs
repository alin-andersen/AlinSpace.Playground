using BenchmarkDotNet.Attributes;
using DotNext;
using DotNext.Reflection;

namespace ConsoleApp1
{
    public class Benchmark2
    {
        [Benchmark]
        public void WithDotNetReflection()
        {
            var constructor = typeof(MyClass2).GetConstructor(new[] { typeof(string), typeof(int) });
            var b = (MyClass2)constructor.Invoke(new object[0]);
        }

        [Benchmark]
        public void WithDotNextReflection()
        {
            var t = typeof(MyClass2).GetConstructor(new[] { typeof(string), typeof(int) }).Unreflect();

            var n = new object[0];

            var b = (MyClass2)t(null, n);
        }

        [Benchmark]
        public void WithDotNextReflectionWithDelegate()
        {
            var t = typeof(MyClass2).GetConstructor(new Type[0]).Unreflect<Func<MyClass2>>();
            
            var b = t.Invoke();
        }

        [Benchmark]
        public void WithActivator()
        {
            var t = Activator.CreateInstance(typeof(MyClass2));
        }
    }
}
