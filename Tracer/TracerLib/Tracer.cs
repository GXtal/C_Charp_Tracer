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

        public Tracer()
        {
            allThreads = new ConcurrentDictionary<int,ThreadInfo>();
        }
        public TraceResult GetTraceResult()
        {
            var threads=new List<ThreadInfo>();

            foreach(var thread in allThreads)
            {
                
                long time = 0;
                foreach(var a in thread.Value.CompleteMethods)
                {
                    time += a.Time;
                }
                thread.Value.Time = time;
                threads.Add(thread.Value);

            }

            TraceResult traceResult = new TraceResult(threads);

            return traceResult;
        }

        public void StartTrace()
        {
            int id = Thread.CurrentThread.ManagedThreadId;

            ThreadInfo currentThread = allThreads.GetOrAdd(id, new ThreadInfo(id));

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
            long stopTime = threadInfo.Timer.ElapsedMilliseconds;

            var methodInfo = threadInfo.RunningMethods.Pop();

            var currentMethod = new StackFrame(1).GetMethod();

            if (currentMethod == null)
            {
                throw new ApplicationException("не обнаружен метод на стеке");
            }

            methodInfo.MethodClose(stopTime);
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
