
namespace TracerLib
{
    public class TraceResult
    {
        public readonly IReadOnlyList<ThreadInfo> ThreadInfo;

        public TraceResult(IReadOnlyList<ThreadInfo> threadInfo)
        {
            ThreadInfo = threadInfo;
        }
    }
    

    

    
}
