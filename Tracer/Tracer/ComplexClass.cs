using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLib;

namespace ConsoleApp
{
    public class ComplexClass
    {
        private ITracer _tracer;

        private SimpleClass _testInnerObject;
        public ComplexClass(ITracer tracer)
        {
            
            _tracer = tracer;
            _testInnerObject = new SimpleClass(_tracer);
        }

        public void OutsideFunction()
        {
            _tracer.StartTrace();

            Thread.Sleep(200);
            _testInnerObject.InnerFunction2();
            _testInnerObject.InnerFunction1();

            _tracer.StopTrace();
        }

        private int count = 2;
        public void RecursFunction()
        {
            _tracer.StartTrace();

            if(count>0)
            {
                Thread.Sleep(100);
                count--;
                RecursFunction();
            }

            _tracer.StopTrace();
        }
    }
}
