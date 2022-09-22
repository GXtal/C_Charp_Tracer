using System.Diagnostics;


namespace TracerLib
{
    public class ThreadInfo
    {
        public Stopwatch Timer;

        public int ThreadId;
        public long Time;
        public Stack<MethodInfo> RunningMethods;
        public List<MethodInfo> CompleteMethods;
        public ThreadInfo(int threadId)
        {
            ThreadId = threadId;
            RunningMethods = new Stack<MethodInfo>();
            CompleteMethods = new List<MethodInfo>();

            Timer = new Stopwatch();
            Timer.Start();            
        }
    }
}
