using TracerLib;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ITracer tracer = new Tracer();
            ClassB temp = new ClassB(tracer);

            temp.TestFunction();

            Console.WriteLine(tracer.GetTraceResult());
        }
    }
}