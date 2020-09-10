
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace HZJ.CommonCls.Encrypts
{
    public static class XmlSerial
    {
        public static string Serialize(object obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter textWriter = new StringWriter(stringBuilder))
            {
                xmlSerializer.Serialize(textWriter, obj);
            }
            return stringBuilder.ToString();
        }
    }

}
