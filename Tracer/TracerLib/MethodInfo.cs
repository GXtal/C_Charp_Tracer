using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class MethodInfo
    {

        public string Name { get; set; }
        public string ClassName { get; set; }
        public long Time { get; set; }

        public List<MethodInfo> ChildMethods { get; set; }

        public MethodInfo(string name, string className, long startTime)
        {
            Name = name;
            ClassName = className;
            Time = startTime;
            ChildMethods = new List<MethodInfo>();
        }
        public void MethodClose(long endTime)
        {
            Time = endTime - Time;
        }
        
    }
}
