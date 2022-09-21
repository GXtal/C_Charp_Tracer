using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLib;

namespace ConsoleApp
{
    public class ClassA
    {
        private ITracer tracer;

        public ClassA(ITracer tracer)
        {
            this.tracer = tracer;
        }

        public void TestFunction()
        {
            tracer.StartTrace();

            Thread.Sleep(120);

            tracer.StopTrace();
        }
    }
}
