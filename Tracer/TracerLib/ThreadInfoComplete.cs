using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class ThreadInfoComplete
    {        
        public int ThreadId { get; }
        public long Time { get; }
        public IReadOnlyList<MethodInfoComplete> CompleteMethods { get; }
        public ThreadInfoComplete(ThreadInfo threadInfo)
        {
            ThreadId = threadInfo.ThreadId;
            Time = threadInfo.Time;
            var temp = new List<MethodInfoComplete>();

            foreach(var methodInfo in threadInfo.CompleteMethods)
            {
                temp.Add(new MethodInfoComplete(methodInfo));
            }
            
            CompleteMethods=temp;
        }
    }
}
