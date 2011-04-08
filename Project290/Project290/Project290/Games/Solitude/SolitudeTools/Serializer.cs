using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Project290.Games.Solitude.SolitudeTools
{
    class Serializer
    {
        private static MemoryStream stream;

        public static T Deserialize<T>(string str)
        {
            XmlSerializer x = new XmlSerializer(typeof(T));
            stream = new MemoryStream(Encoding.UTF8.GetBytes(str));
            return (T)x.Deserialize(stream);
        }

        public static string Serialize(Object o)
        {
            XmlSerializer x = new XmlSerializer(o.GetType());
            stream = new MemoryStream();
            x.Serialize(stream, o);
            return System.Text.UTF8Encoding.UTF8.GetString(stream.ToArray(), 0, (int)stream.Length);
        }

        public static T DeserializeFile<T>(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string text = reader.ReadToEnd();
                return Deserialize<T>(text);
            }
        }

        public static void SerializeFile(string filename, object o)
        {
            string text = Serialize(o);
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(text);
            }
        }
    }
}
