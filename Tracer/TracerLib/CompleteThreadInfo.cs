using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class CompleteThreadInfo
    {        

        public int ThreadId { get; }
        public IReadOnlyList<CompleteMethodInfo> CompleteMethods { get; }
        public CompleteThreadInfo(ThreadInfo threadInfo)
        {
            ThreadId = threadInfo.ThreadId;
            var temp = new List<CompleteMethodInfo>();

            foreach(var methodInfo in threadInfo.CompleteMethods)
            {
                temp.Add(new CompleteMethodInfo(methodInfo));
            }
            
            CompleteMethods=temp;
        }
    }
}
