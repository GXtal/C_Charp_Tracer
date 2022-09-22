using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLib;

namespace ConsoleApp
{
    public class SimpleClass
    {
        private ITracer tracer;

        public SimpleClass(ITracer tracer)
        {
            this.tracer = tracer;
        }

        public void InnerFunction1()
        {
            tracer.StartTrace();

            Thread.Sleep(400);

            tracer.StopTrace();
        }
        public void InnerFunction2()
        {
            tracer.StartTrace();

            Thread.Sleep(100);

            tracer.StopTrace();
        }
    }
}
