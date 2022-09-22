using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TracerLib
{
    public class JsonResultSerializer : IResultSerializer
    {
        public string GetSerializedContent(TraceResult traceResult)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            return JsonSerializer.Serialize(new TraceResultSerializable(traceResult), typeof(TraceResultSerializable),options);
            
        }
    }
}
