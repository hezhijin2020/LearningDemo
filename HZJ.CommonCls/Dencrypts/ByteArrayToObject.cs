using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HZJ.CommonCls.Dencrypts
{
    // BIN.DataAccess.ByteArrayToObject


    public static class ByteArrayToObject
    {
        public static T BytesToObject<T>(byte[] Bytes) where T : class
        {
            using (MemoryStream serializationStream = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(serializationStream);
            }
        }
    }

}
