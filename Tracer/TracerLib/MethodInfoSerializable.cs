using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace TracerLib
{
    public class MethodInfoSerializable
    {
        [XmlAttribute("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [XmlAttribute("class")]
        [JsonPropertyName("class")]
        public string ClassName { get; set; }
        [JsonPropertyName("time")]
        [XmlAttribute("time")]
        public long Time { get; set; }
        [XmlElement("method")]
        [JsonPropertyName("methods")]
        public List<MethodInfoSerializable> ChildMethods { get; set; }

        public MethodInfoSerializable(MethodInfoComplete methodInfo)
        {
            Name = methodInfo.Name;
            ClassName = methodInfo.ClassName;
            Time = methodInfo.Time;

            ChildMethods = new List<MethodInfoSerializable>();
            foreach (var childMethodInfo in methodInfo.ChildMethods)
            {
                ChildMethods.Add(new MethodInfoSerializable(childMethodInfo));
            }            
        }
        public MethodInfoSerializable()
        {
            Name = "";
            ClassName = "";
            Time = 0;
            ChildMethods = new List<MethodInfoSerializable>();
        }
    }
}
