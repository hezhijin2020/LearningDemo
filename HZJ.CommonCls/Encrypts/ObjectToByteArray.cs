// BIN.DataAccess.ObjectToByteArray
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace HZJ.CommonCls.Encrypts
{


    public static class ObjectToByteArray
    {
        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, obj);
                return memoryStream.GetBuffer();
            }
        }
    }

}
