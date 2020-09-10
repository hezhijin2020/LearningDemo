using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HZJ.CommonCls.Dencrypts
{

    public static class JsDencrypt
    {
        public static string DecryptAsy(string data)
        {
            return DecryptAsy(data, "2567", "hell");
        }

        public static string DecryptAsy(string data, string keyStr, string ivStr)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(keyStr);
            byte[] bytes2 = Encoding.Unicode.GetBytes(ivStr);
            byte[] array = Convert.FromBase64String(data);
            using (DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(bytes, bytes2), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(array, 0, array.Length);
                        cryptoStream.FlushFinalBlock();
                        string @string = Encoding.Unicode.GetString(memoryStream.ToArray());
                        memoryStream.Close();
                        cryptoStream.Close();
                        return @string;
                    }
                }
            }
        }
    }

}
