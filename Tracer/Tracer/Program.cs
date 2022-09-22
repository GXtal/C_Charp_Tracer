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
            var serializer = new JsonResultSerializer();
            string res = serializer.GetSerializedContent(tracer.GetTraceResult());
            Console.WriteLine(res);
        }
    }
}