namespace Helpers
{
    using System.IO;
    using System.Xml.Serialization;

    public static class CopyHelper
    {
        public static string Serialize<T>(this T serializable)
        {
            var serializer = new XmlSerializer(serializable.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, serializable);
                return textWriter.ToString();
            }
        }

        public static T Deserialize<T>(this string deserializable)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(deserializable))
            {
                return (T) serializer.Deserialize(reader);
            }
        }

        public static T Copy<T>(this object o)
        {
            string result = Serialize(o);
            return Deserialize<T>(result);
        }
    }
}
