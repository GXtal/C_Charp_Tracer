using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class Tracer : ITracer
    {
        private ConcurrentDictionary<int,ThreadInfo> allThreads;
        public TraceResult GetTraceResult()
        {
            var threads=new List<ThreadInfo>();

            foreach(var thread in allThreads)
            {
                threads.Add(thread.Value);
            }

            return new TraceResult(threads);
        }

        public void StartTrace()
        {
            int id = Thread.CurrentThread.ManagedThreadId;

            ThreadInfo currentThread = allThreads.GetOrAdd(id, new ThreadInfo());

            var currentMethod = new StackFrame(1).GetMethod();

            if(currentMethod==null)
            {
                throw new ApplicationException("не обнаружен метод на стеке");
            }

            string ? className="no class found";
            if(currentMethod.ReflectedType!=null)
            {
               className = currentMethod.ReflectedType.Name;
            }
                
            currentThread.RunningMethods.Push(new MethodInfo(currentMethod.Name,className,
                                                             currentThread.Timer.ElapsedMilliseconds));
        }

        public void StopTrace()
        {
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;

            if (!allThreads.ContainsKey(currentThreadId))
            {
                throw new ApplicationException("не сохранен ийди потока для отслкживаемого метода");
            }

            var threadInfo = allThreads[currentThreadId];
            var methodInfo = threadInfo.RunningMethods.Pop();

            var currentMethod = new StackFrame(1).GetMethod();

            if (currentMethod == null)
            {
                throw new ApplicationException("не обнаружен метод на стеке");
            }


            //this should be illigal??
            if (currentMethod.Name!=methodInfo.Name)
            {
                throw new ApplicationException("не соответсвие вершины стека и закрываемого метода");
            }

            MethodInfo topMethodInfo;

            //method is called inside another?
            if (threadInfo.RunningMethods.TryPeek(out topMethodInfo))
            {
                topMethodInfo.ChildMethods.Add(methodInfo);
            }
            else
            {
                threadInfo.CompleteMethods.Add(methodInfo);
            }

        }
    }
}
