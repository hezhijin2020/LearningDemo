
using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
namespace HZJ.CommonCls.Cryptography
{
    public static class Dencrypt
    {
        private static readonly string Base64Code = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/=";

        private static readonly byte[] Keys = new byte[8]
        {
        18,
        239,
        52,
        86,
        120,
        144,
        171,
        205
        };

        public static string DecryptString(string str)
        {
            if (str.Length % 4 != 0)
            {
                throw new DecodeException("encode error!", "str");
            }
            if (!Regex.IsMatch(str, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
            {
                throw new DecodeException("encode error!", "str");
            }
            int num = str.Length / 4;
            ArrayList arrayList = new ArrayList(num * 3);
            char[] array = str.ToCharArray();
            for (int i = 0; i < num; i++)
            {
                byte[] array2 = new byte[4]
                {
                (byte)Base64Code.IndexOf(array[i * 4]),
                (byte)Base64Code.IndexOf(array[i * 4 + 1]),
                (byte)Base64Code.IndexOf(array[i * 4 + 2]),
                (byte)Base64Code.IndexOf(array[i * 4 + 3])
                };
                byte[] array3 = new byte[3]
                {
                (byte)((array2[0] << 2) ^ ((array2[1] & 0x30) >> 4)),
                0,
                0
                };
                if (array2[2] != 64)
                {
                    array3[1] = (byte)((array2[1] << 4) ^ ((array2[2] & 0x3C) >> 2));
                }
                else
                {
                    array3[2] = 0;
                }
                if (array2[3] != 64)
                {
                    array3[2] = (byte)((array2[2] << 6) ^ array2[3]);
                }
                else
                {
                    array3[2] = 0;
                }
                arrayList.Add(array3[0]);
                if (array3[1] != 0)
                {
                    arrayList.Add(array3[1]);
                }
                if (array3[2] != 0)
                {
                    arrayList.Add(array3[2]);
                }
            }
            byte[] bytes = (byte[])arrayList.ToArray(Type.GetType("System.Byte"));
            return Encoding.UTF8.GetString(bytes);
        }

        public static string DecryptDES(string decryptString, string decryptKey)
        {
            if (8 != decryptKey.Length)
            {
                throw new ArgumentException("length of decryptKey must equal 8!");
            }
            byte[] bytes = Encoding.UTF8.GetBytes(decryptKey);
            byte[] keys = Keys;
            byte[] array = Convert.FromBase64String(decryptString);
            using (DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(bytes, keys), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(array, 0, array.Length);
                        cryptoStream.FlushFinalBlock();
                        return Encoding.UTF8.GetString(memoryStream.ToArray());
                    }
                }
            }
        }

        public static string Dencrypt3DES(string strData, string strKey)
        {
            using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
                {
                    tripleDESCryptoServiceProvider.Key = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(strKey));
                    tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
                    using (ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor())
                    {
                        try
                        {
                            byte[] array = Convert.FromBase64String(strData);
                            return Encoding.UTF8.GetString(cryptoTransform.TransformFinalBlock(array, 0, array.Length));
                        }
                        catch (Exception)
                        {
                            throw new DecodeException("decode error!", "strData/strKey");
                        }
                    }
                }
            }
        }
    }

}
