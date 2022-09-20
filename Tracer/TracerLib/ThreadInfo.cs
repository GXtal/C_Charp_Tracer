using System.Diagnostics;


namespace TracerLib
{
    public class ThreadInfo
    {
        public Stopwatch Timer;
        public Stack<MethodInfo> RunningMethods;
        public List<MethodInfo> CompleteMethods;
        public ThreadInfo()
        {            
            RunningMethods = new Stack<MethodInfo>();
            CompleteMethods = new List<MethodInfo>();

            Timer = new Stopwatch();
            Timer.Start();
        }
    }
}
