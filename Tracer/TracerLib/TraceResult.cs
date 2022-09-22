
namespace TracerLib
{
    public class TraceResult
    {
        public IReadOnlyList<ThreadInfoComplete> ThreadsInfo { get; }
        public TraceResult(List<ThreadInfo> threadsInfo)
        {
            var temp = new List<ThreadInfoComplete>();
            
            foreach(var threadInfo in threadsInfo)
            {
                temp.Add(new ThreadInfoComplete(threadInfo));
            }
            ThreadsInfo = temp;            
        }
    }
          
}
