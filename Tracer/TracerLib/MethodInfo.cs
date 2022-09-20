using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    internal class MethodInfo
    {

        public readonly string Name;

        public readonly string ClassName;

        public readonly int StartTime;
        public int EndTime;

        public List<MethodInfo> ChildMethods;

        public MethodInfo(string name, string className, int startTime)
        {
            Name = name;
            ClassName = className;
            StartTime = startTime;
            ChildMethods = new List<MethodInfo>();
        }
        
    }
}
