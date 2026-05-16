
namespace Dan.monitor.Common.Helpers
{
    using System.IO;
    using System.Xml.Serialization;

    public static class XmlHelper
    {
        public static string SerializeObject<T>(this T toSerialize)
        {
            var xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
    }

 
}
