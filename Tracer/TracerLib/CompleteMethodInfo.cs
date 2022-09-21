using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class CompleteMethodInfo
    {
        public string Name { get; }
        public string ClassName { get; }
        public long Time { get; }

        public IReadOnlyList<CompleteMethodInfo> ChildMethods { get; }

        public CompleteMethodInfo(MethodInfo methodInfo)
        {
            Name = methodInfo.Name;
            ClassName = methodInfo.ClassName;
            Time = methodInfo.Time;
            var temp = new List<CompleteMethodInfo>();
            foreach (var childMethodInfo in methodInfo.ChildMethods)
            {
                temp.Add(new CompleteMethodInfo(childMethodInfo));
            }
            ChildMethods = temp;
        }

    }
}
