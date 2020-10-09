using System;
namespace HZJ.CommonCls.Cryptography
{
    public static class DencryptMethod
    {
        public static byte[] Base64StringToByteArr(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        public static T Byte2ToObject<T>(byte[] data) where T : class
        {
            return ByteArrayToObject.BytesToObject<T>(data);
        }

        public static T Byte2Base64ToObject<T>(string str) where T : class
        {
            return Byte2ToObject<T>(Base64StringToByteArr(str));
        }

        public static T DencryptObject<T>(string cryptedData) where T : class
        {
            return Byte2Base64ToObject<T>(Dencrypt.DecryptString(cryptedData));
        }

        public static T Dencrypt3DESObject<T>(string cryptedData, string keyStr) where T : class
        {
            return Byte2Base64ToObject<T>(Dencrypt.Dencrypt3DES(cryptedData, keyStr));
        }
    }

}
