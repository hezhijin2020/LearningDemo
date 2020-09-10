using System.Xml;
using System.Xml.Serialization;

namespace HZJ.CommonCls.Dencrypts
{
    public static class XmlUnSerial
    {
        public static T Deserialize<T>(string s)
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(s);
                using (XmlNodeReader xmlReader = new XmlNodeReader(xmlDocument.DocumentElement))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    object obj = xmlSerializer.Deserialize(xmlReader);
                    return (T)obj;
                }
            }
            catch
            {
                return default(T);
            }
        }
    }

}
