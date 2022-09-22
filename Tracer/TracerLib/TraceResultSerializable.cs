using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace TracerLib
{
    [XmlRoot("root")]
    public class TraceResultSerializable
    {
        [JsonPropertyName("threads")]
        [XmlElement("thread")]
        public List<ThreadInfoSerializable> ThreadsInfo { get; set; }
        public TraceResultSerializable(TraceResult traceResult)
        {
            ThreadsInfo = new List<ThreadInfoSerializable>();
            foreach (var threadInfo in traceResult.ThreadsInfo)
            {
                ThreadsInfo.Add(new ThreadInfoSerializable(threadInfo));
            }
        }

        public TraceResultSerializable()
        {
            ThreadsInfo = new List<ThreadInfoSerializable>();
        }
    }
}
