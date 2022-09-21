
namespace TracerLib
{
    public class TraceResult
    {
        public IReadOnlyList<CompleteThreadInfo> ThreadsInfo { get; }

        public TraceResult(List<ThreadInfo> threadsInfo)
        {
            var temp = new List<CompleteThreadInfo>();
            
            foreach(var threadInfo in threadsInfo)
            {
                temp.Add(new CompleteThreadInfo(threadInfo));
            }
            ThreadsInfo = temp;            
        }
    }
    

    

    
}
