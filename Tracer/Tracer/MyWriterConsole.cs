using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class MyWriterConsole : IMyWriter
    {      
        public void Write(string value)
        {
            Console.WriteLine(value);
        }       
    }
}
