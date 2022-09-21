using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLib;

namespace ConsoleApp
{
    public class ClassB
    {
        private ITracer _tracer;

        private ClassA _testObject;
        public ClassB(ITracer tracer)
        {
            
            _tracer = tracer;
            _testObject = new ClassA(_tracer);
        }

        public void TestFunction()
        {
            _tracer.StartTrace();

            Thread.Sleep(200);
            _testObject.TestFunction();

            _tracer.StopTrace();
        }
    }
}
