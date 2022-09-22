using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace TracerLib
{
    public class ThreadInfoSerializable
    {
        [XmlAttribute("id")]
        [JsonPropertyName("id")]
        public int ThreadId { get; set; }

        [XmlAttribute("time")]
        [JsonPropertyName("time")]
        public long Time { get; set; }

        [XmlElement("method")]
        [JsonPropertyName("methods")]
        public List<MethodInfoSerializable> CompleteMethods { get; set; }
        public ThreadInfoSerializable(ThreadInfoComplete threadInfo)
        {
            ThreadId = threadInfo.ThreadId;
            Time = threadInfo.Time;
            CompleteMethods = new List<MethodInfoSerializable>();

            foreach (var methodInfo in threadInfo.CompleteMethods)
            {
                CompleteMethods.Add(new MethodInfoSerializable(methodInfo));
            }
        }
        public ThreadInfoSerializable()
        {
            ThreadId = 0;
            CompleteMethods = new List<MethodInfoSerializable>();            
        }
    }
}
