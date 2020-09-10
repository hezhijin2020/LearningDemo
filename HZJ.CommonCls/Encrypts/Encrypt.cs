using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HZJ.CommonCls.Encrypts
{
    /// <summary>
    /// 
    /// </summary>
    public static class Encrypt
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

        public static string EncryptString(string str)
        {
            byte b = 0;
            ArrayList arrayList = new ArrayList(Encoding.UTF8.GetBytes(str));
            int count = arrayList.Count;
            int num = count / 3;
            int num2 = 0;
            if ((num2 = count % 3) > 0)
            {
                for (int i = 0; i < 3 - num2; i++)
                {
                    arrayList.Add(b);
                }
                num++;
            }
            StringBuilder stringBuilder = new StringBuilder(num * 4);
            for (int j = 0; j < num; j++)
            {
                byte[] array = new byte[3]
                {
                (byte)arrayList[j * 3],
                (byte)arrayList[j * 3 + 1],
                (byte)arrayList[j * 3 + 2]
                };
                int[] array2 = new int[4]
                {
                array[0] >> 2,
                ((array[0] & 3) << 4) ^ (array[1] >> 4),
                0,
                0
                };
                if (!array[1].Equals(b))
                {
                    array2[2] = (((array[1] & 0xF) << 2) ^ (array[2] >> 6));
                }
                else
                {
                    array2[2] = 64;
                }
                if (!array[2].Equals(b))
                {
                    array2[3] = (array[2] & 0x3F);
                }
                else
                {
                    array2[3] = 64;
                }
                stringBuilder.Append(Base64Code[array2[0]]);
                stringBuilder.Append(Base64Code[array2[1]]);
                stringBuilder.Append(Base64Code[array2[2]]);
                stringBuilder.Append(Base64Code[array2[3]]);
            }
            return stringBuilder.ToString();
        }

        public static string EncryptBy16MD5(string strProclaimed)
        {
            using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                return BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(strProclaimed)), 4, 8);
            }
        }

        public static string EncryptBy32MD5(string strProclaimed)
        {
            using (MD5 mD = MD5.Create())
            {
                string text = "";
                byte[] array = mD.ComputeHash(Encoding.UTF8.GetBytes(strProclaimed));
                for (int i = 0; i < array.Length; i++)
                {
                    text += array[i].ToString("x");
                }
                return text;
            }
        }

        public static string Encrypt3DES(string strString, string strKey)
        {
            using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
                {
                    tripleDESCryptoServiceProvider.Key = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(strKey));
                    tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
                    using (ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor())
                    {
                        byte[] bytes = Encoding.Default.GetBytes(strString);
                        return Convert.ToBase64String(cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length));
                    }
                }
            }
        }

        public static string EncryptDES(string encryptString, string encryptKey)
        {
            if (8 != encryptKey.Length)
            {
                throw new ArgumentException("length of encryptKey must equal 8!");
            }
            byte[] bytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] keys = Keys;
            byte[] bytes2 = Encoding.UTF8.GetBytes(encryptString);
            using (DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, keys), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytes2, 0, bytes2.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }
    }

}
