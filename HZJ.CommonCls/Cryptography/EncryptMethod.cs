using System;

namespace HZJ.CommonCls.Cryptography
{
    public static class EncryptMethod
    {
        public static string ByteArrToBase64String(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static byte[] ObjectToByte2<T>(T obj) where T : class
        {
            return ObjectToByteArray.ObjectToBytes(obj);
        }

        public static string ObjectToByte2Base64<T>(T obj) where T : class
        {
            return ByteArrToBase64String(ObjectToByte2(obj));
        }

        public static string EncryptObject<T>(T obj) where T : class
        {
            return Encrypt.EncryptString(ObjectToByte2Base64(obj));
        }

        public static string Encrypt3DESObject<T>(T obj, string keyStr) where T : class
        {
            return Encrypt.Encrypt3DES(ObjectToByte2Base64(obj), keyStr);
        }
    }

}
