using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class MethodInfoComplete
    {
        public string Name { get; }
        public string ClassName { get; }
        public long Time { get; }

        public IReadOnlyList<MethodInfoComplete> ChildMethods { get; }

        public MethodInfoComplete(MethodInfo methodInfo)
        {
            Name = methodInfo.Name;
            ClassName = methodInfo.ClassName;
            Time = methodInfo.Time;
            var temp = new List<MethodInfoComplete>();
            foreach (var childMethodInfo in methodInfo.ChildMethods)
            {
                temp.Add(new MethodInfoComplete(childMethodInfo));
            }
            ChildMethods = temp;
        }

    }
}
