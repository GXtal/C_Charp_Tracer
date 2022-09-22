using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class MyWriterFile : IMyWriter
    {
        public StreamWriter FileStream { get; }

        public MyWriterFile(string fileName)
        {
            FileStream = File.CreateText(fileName);
        }

        public void Write(string value)
        {
            FileStream.WriteLine(value);
            FileStream.Flush();
        }
    }
}
