using TracerLib;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ITracer tracer = new Tracer();
            ComplexClass temp = new ComplexClass(tracer);

            temp.OutsideFunction();

            IResultSerializer serializer = new JsonResultSerializer();
            string res = serializer.GetSerializedContent(tracer.GetTraceResult());
            IMyWriter myWriter = new MyWriterConsole();
            myWriter.Write(res);

            serializer = new XmlResultSerializer();
            res = serializer.GetSerializedContent(tracer.GetTraceResult());
            myWriter = new MyWriterFile("temp.txt");
            myWriter.Write(res);

        }
    }
}