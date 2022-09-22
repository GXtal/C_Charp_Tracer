using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TracerLib
{
    public class XmlResultSerializer : IResultSerializer
    {
        public string GetSerializedContent(TraceResult traceResult)
        {
            TraceResultSerializable temp = new TraceResultSerializable(traceResult);
            StringWriter writer = new StringWriter();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TraceResultSerializable));

            xmlSerializer.Serialize(writer, temp);
            string nonFormatString = writer.ToString();
            writer.Close();
            //return nonFormatString;
            XDocument document = XDocument.Parse(nonFormatString);
            
            string formatString = document.ToString();
            return formatString;
        }
    }
}
