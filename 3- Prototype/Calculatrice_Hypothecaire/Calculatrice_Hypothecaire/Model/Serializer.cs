using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Calculatrice_Hypothecaire.Model
{
    class Serializer
    {
        public static void SerializeToXML<T>(T t, string inFilename)
        {
            XmlSerializer serializer = new XmlSerializer(t.GetType());
            TextWriter textWriter = new StreamWriter(inFilename);
            serializer.Serialize(textWriter, t);
            textWriter.Close();
        }

        public static T DeserializeFromXML<T>(string inFilename)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StreamReader(inFilename);
            T retVal = (T)deserializer.Deserialize(textReader);
            textReader.Close();
            return retVal;
        }
    }
}
