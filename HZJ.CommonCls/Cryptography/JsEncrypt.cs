using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HZJ.CommonCls.Cryptography
{
    // BIN.DataAccess.JsEncrypt


    public static class JsEncrypt
    {
        private static readonly string ApiDesKey = "mytestkey";

        private static string DES(string key, string strMessage, bool isEncrypt, int mode, string strIV)
        {
            int[] array = new int[64]
            {
            16843776,
            0,
            65536,
            16843780,
            16842756,
            66564,
            4,
            65536,
            1024,
            16843776,
            16843780,
            1024,
            16778244,
            16842756,
            16777216,
            4,
            1028,
            16778240,
            16778240,
            66560,
            66560,
            16842752,
            16842752,
            16778244,
            65540,
            16777220,
            16777220,
            65540,
            0,
            1028,
            66564,
            16777216,
            65536,
            16843780,
            4,
            16842752,
            16843776,
            16777216,
            16777216,
            1024,
            16842756,
            65536,
            66560,
            16777220,
            1024,
            4,
            16778244,
            66564,
            16843780,
            65540,
            16842752,
            16778244,
            16777220,
            1028,
            66564,
            16843776,
            1028,
            16778240,
            16778240,
            0,
            65540,
            66560,
            0,
            16842756
            };
            int[] array2 = new int[64]
            {
            -2146402272,
            -2147450880,
            32768,
            1081376,
            1048576,
            32,
            -2146435040,
            -2147450848,
            -2147483616,
            -2146402272,
            -2146402304,
            -134217728,
            -2147450880,
            1048576,
            32,
            -2146435040,
            1081344,
            1048608,
            -2147450848,
            0,
            -134217728,
            32768,
            1081376,
            -2146435072,
            1048608,
            -2147483616,
            0,
            1081344,
            32800,
            -2146402304,
            -2146435072,
            32800,
            0,
            1081376,
            -2146435040,
            1048576,
            -2147450848,
            -2146435072,
            -2146402304,
            32768,
            -2146435072,
            -2147450880,
            32,
            -2146402272,
            1081376,
            32,
            32768,
            -134217728,
            32800,
            -2146402304,
            1048576,
            -2147483616,
            1048608,
            -2147450848,
            -2147483616,
            1048608,
            1081344,
            0,
            -2147450880,
            32800,
            -134217728,
            -2146435040,
            -2146402272,
            1081344
            };
            int[] array3 = new int[64]
            {
            520,
            134349312,
            0,
            134348808,
            134218240,
            0,
            131592,
            134218240,
            131080,
            134217736,
            134217736,
            131072,
            134349320,
            131080,
            134348800,
            520,
            134217728,
            8,
            134349312,
            512,
            131584,
            134348800,
            134348808,
            131592,
            134218248,
            131584,
            131072,
            134218248,
            8,
            134349320,
            512,
            134217728,
            134349312,
            134217728,
            131080,
            520,
            131072,
            134349312,
            134218240,
            0,
            512,
            131080,
            134349320,
            134218240,
            134217736,
            512,
            0,
            134348808,
            134218248,
            131072,
            134217728,
            134349320,
            8,
            131592,
            131584,
            134217736,
            134348800,
            134218248,
            520,
            134348800,
            131592,
            8,
            134348808,
            131584
            };
            int[] array4 = new int[64]
            {
            8396801,
            8321,
            8321,
            128,
            8396928,
            8388737,
            8388609,
            8193,
            0,
            8396800,
            8396800,
            8396929,
            129,
            0,
            8388736,
            8388609,
            1,
            8192,
            8388608,
            8396801,
            128,
            8388608,
            8193,
            8320,
            8388737,
            1,
            8320,
            8388736,
            8192,
            8396928,
            8396929,
            129,
            8388736,
            8388609,
            8396800,
            8396929,
            129,
            0,
            0,
            8396800,
            8320,
            8388736,
            8388737,
            1,
            8396801,
            8321,
            8321,
            128,
            8396929,
            129,
            1,
            8192,
            8388609,
            8193,
            8396928,
            8388737,
            8193,
            8320,
            8388608,
            8396801,
            128,
            8388608,
            8192,
            8396928
            };
            int[] array5 = new int[64]
            {
            256,
            34078976,
            34078720,
            1107296512,
            524288,
            256,
            1073741824,
            34078720,
            1074266368,
            524288,
            33554688,
            1074266368,
            1107296512,
            1107820544,
            524544,
            1073741824,
            33554432,
            1074266112,
            1074266112,
            0,
            1073742080,
            1107820800,
            1107820800,
            33554688,
            1107820544,
            1073742080,
            0,
            1107296256,
            34078976,
            33554432,
            1107296256,
            524544,
            524288,
            1107296512,
            256,
            33554432,
            1073741824,
            34078720,
            1107296512,
            1074266368,
            33554688,
            1073741824,
            1107820544,
            34078976,
            1074266368,
            256,
            33554432,
            1107820544,
            1107820800,
            524544,
            1107296256,
            1107820800,
            34078720,
            0,
            1074266112,
            1107296256,
            524544,
            33554688,
            1073742080,
            524288,
            0,
            1074266112,
            34078976,
            1073742080
            };
            int[] array6 = new int[64]
            {
            536870928,
            541065216,
            16384,
            541081616,
            541065216,
            16,
            541081616,
            4194304,
            536887296,
            4210704,
            4194304,
            536870928,
            4194320,
            536887296,
            536870912,
            16400,
            0,
            4194320,
            536887312,
            16384,
            4210688,
            536887312,
            16,
            541065232,
            541065232,
            0,
            4210704,
            541081600,
            16400,
            4210688,
            541081600,
            536870912,
            536887296,
            16,
            541065232,
            4210688,
            541081616,
            4194304,
            16400,
            536870928,
            4194304,
            536887296,
            536870912,
            16400,
            536870928,
            541081616,
            4210688,
            541065216,
            4210704,
            541081600,
            0,
            541065232,
            16,
            16384,
            541065216,
            4210704,
            16384,
            4194320,
            536887312,
            0,
            541081600,
            536870912,
            4194320,
            536887312
            };
            int[] array7 = new int[64]
            {
            2097152,
            69206018,
            67110914,
            0,
            2048,
            67110914,
            2099202,
            69208064,
            69208066,
            2097152,
            0,
            67108866,
            2,
            67108864,
            69206018,
            2050,
            67110912,
            2099202,
            2097154,
            67110912,
            67108866,
            69206016,
            69208064,
            2097154,
            69206016,
            2048,
            2050,
            69208066,
            2099200,
            2,
            67108864,
            2099200,
            67108864,
            2099200,
            2097152,
            67110914,
            67110914,
            69206018,
            69206018,
            2,
            2097154,
            67108864,
            67110912,
            2097152,
            69208064,
            2050,
            2099202,
            69208064,
            2050,
            67108866,
            69208066,
            69206016,
            2099200,
            0,
            2,
            69208066,
            0,
            2099202,
            69206016,
            2048,
            67108866,
            67110912,
            2048,
            2097154
            };
            int[] array8 = new int[64]
            {
            268439616,
            4096,
            262144,
            268701760,
            268435456,
            268439616,
            64,
            268435456,
            262208,
            268697600,
            268701760,
            266240,
            268701696,
            266304,
            4096,
            64,
            268697600,
            268435520,
            268439552,
            4160,
            266240,
            262208,
            268697664,
            268701696,
            4160,
            0,
            0,
            268697664,
            268435520,
            268439552,
            266304,
            262144,
            266304,
            262144,
            268701696,
            4096,
            64,
            268697664,
            4096,
            266304,
            268439552,
            64,
            268435520,
            268697600,
            268697664,
            268435456,
            262144,
            268439616,
            0,
            268701760,
            262208,
            268435520,
            268697600,
            268439552,
            268439616,
            0,
            268701760,
            266240,
            266240,
            4160,
            4160,
            262208,
            268435456,
            268701696
            };
            int[] array9 = DES_CreateKey(key);
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int length = strMessage.Length;
            int num6 = 0;
            int num7 = (array9.Length == 32) ? 3 : 9;
            int[] array10 = (num7 != 3) ? ((!isEncrypt) ? new int[9]
            {
            94,
            62,
            -2,
            32,
            64,
            2,
            30,
            -2,
            -2
            } : new int[9]
            {
            0,
            32,
            2,
            62,
            30,
            -2,
            64,
            96,
            2
            }) : (isEncrypt ? new int[3]
            {
            0,
            32,
            2
            } : new int[3]
            {
            30,
            -2,
            -2
            });
            strMessage += "\0\0\0\0\0\0\0\0";
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            if (mode == 1)
            {
                int length2 = strIV.Length;
                char[] array11 = strIV.ToCharArray();
                int[] array12 = new int[length2 + 8];
                for (int i = 0; i < length2; i++)
                {
                    array12[i] = Convert.ToInt32(array11[i]);
                }
                for (int i = length2; i < length2 + 8; i++)
                {
                    array12[i] = 0;
                }
                num2 = ((array12[num++] << 24) | (array12[num++] << 16) | (array12[num++] << 8) | array12[num++]);
                num4 = ((array12[num++] << 24) | (array12[num++] << 16) | (array12[num++] << 8) | array12[num++]);
                num = 0;
            }
            while (num < length)
            {
                int[] array13 = new int[length + 8];
                char[] array14 = strMessage.ToCharArray();
                for (int i = 0; i < length + 8; i++)
                {
                    array13[i] = Convert.ToInt32(array14[i]);
                }
                int num9;
                int num8;
                if (isEncrypt)
                {
                    num8 = ((array13[num++] << 16) | array13[num++]);
                    num9 = ((array13[num++] << 16) | array13[num++]);
                }
                else
                {
                    num8 = ((array13[num++] << 24) | (array13[num++] << 16) | (array13[num++] << 8) | array13[num++]);
                    num9 = ((array13[num++] << 24) | (array13[num++] << 16) | (array13[num++] << 8) | array13[num++]);
                }
                if (mode == 1)
                {
                    if (isEncrypt)
                    {
                        num8 ^= num2;
                        num9 ^= num4;
                    }
                    else
                    {
                        num3 = num2;
                        num5 = num4;
                        num2 = num8;
                        num4 = num9;
                    }
                }
                int num10 = (MoveByte(num8, 4) ^ num9) & 0xF0F0F0F;
                num9 ^= num10;
                num8 ^= num10 << 4;
                num10 = ((MoveByte(num8, 16) ^ num9) & 0xFFFF);
                num9 ^= num10;
                num8 ^= num10 << 16;
                num10 = ((MoveByte(num9, 2) ^ num8) & 0x33333333);
                num8 ^= num10;
                num9 ^= num10 << 2;
                num10 = ((MoveByte(num9, 8) ^ num8) & 0xFF00FF);
                num8 ^= num10;
                num9 ^= num10 << 8;
                num10 = ((MoveByte(num8, 1) ^ num9) & 0x55555555);
                num9 ^= num10;
                num8 ^= num10 << 1;
                num8 = ((num8 << 1) | MoveByte(num8, 31));
                num9 = ((num9 << 1) | MoveByte(num9, 31));
                for (int j = 0; j < num7; j += 3)
                {
                    int num11 = array10[j + 1];
                    int num12 = array10[j + 2];
                    for (int i = array10[j]; i != num11; i += num12)
                    {
                        int num13 = num9 ^ array9[i];
                        int num14 = (MoveByte(num9, 4) | (num9 << 28)) ^ array9[i + 1];
                        num10 = num8;
                        num8 = num9;
                        num9 = (num10 ^ (array2[MoveByte(num13, 24) & 0x3F] | array4[MoveByte(num13, 16) & 0x3F] | array6[MoveByte(num13, 8) & 0x3F] | array8[num13 & 0x3F] | array[MoveByte(num14, 24) & 0x3F] | array3[MoveByte(num14, 16) & 0x3F] | array5[MoveByte(num14, 8) & 0x3F] | array7[num14 & 0x3F]));
                    }
                    num10 = num8;
                    num8 = num9;
                    num9 = num10;
                }
                num8 = (MoveByte(num8, 1) | (num8 << 31));
                num9 = (MoveByte(num9, 1) | (num9 << 31));
                num10 = ((MoveByte(num8, 1) ^ num9) & 0x55555555);
                num9 ^= num10;
                num8 ^= num10 << 1;
                num10 = ((MoveByte(num9, 8) ^ num8) & 0xFF00FF);
                num8 ^= num10;
                num9 ^= num10 << 8;
                num10 = ((MoveByte(num9, 2) ^ num8) & 0x33333333);
                num8 ^= num10;
                num9 ^= num10 << 2;
                num10 = ((MoveByte(num8, 16) ^ num9) & 0xFFFF);
                num9 ^= num10;
                num8 ^= num10 << 16;
                num10 = ((MoveByte(num8, 4) ^ num9) & 0xF0F0F0F);
                num9 ^= num10;
                num8 ^= num10 << 4;
                if (mode == 1)
                {
                    if (isEncrypt)
                    {
                        num2 = num8;
                        num4 = num9;
                    }
                    else
                    {
                        num8 ^= num3;
                        num9 ^= num5;
                    }
                }
                if (isEncrypt)
                {
                    stringBuilder2.Append(Convert.ToChar(MoveByte(num8, 24)));
                    stringBuilder2.Append(Convert.ToChar(MoveByte(num8, 16) & 0xFF));
                    stringBuilder2.Append(Convert.ToChar(MoveByte(num8, 8) & 0xFF));
                    stringBuilder2.Append(Convert.ToChar(num8 & 0xFF));
                    stringBuilder2.Append(Convert.ToChar(MoveByte(num9, 24)));
                    stringBuilder2.Append(Convert.ToChar(MoveByte(num9, 16) & 0xFF));
                    stringBuilder2.Append(Convert.ToChar(MoveByte(num9, 8) & 0xFF));
                    stringBuilder2.Append(Convert.ToChar(num9 & 0xFF));
                }
                else
                {
                    stringBuilder2.Append(Convert.ToChar(MoveByte(num8, 16) & 0xFFFF));
                    stringBuilder2.Append(Convert.ToChar(num8 & 0xFFFF));
                    stringBuilder2.Append(Convert.ToChar(MoveByte(num9, 16) & 0xFFFF));
                    stringBuilder2.Append(Convert.ToChar(num9 & 0xFFFF));
                }
                num6 = ((!isEncrypt) ? (num6 + 8) : (num6 + 16));
                if (num6 == 512)
                {
                    stringBuilder.Append(stringBuilder2.ToString());
                    stringBuilder2.Remove(0, stringBuilder2.Length);
                    num6 = 0;
                }
            }
            return stringBuilder.ToString() + stringBuilder2.ToString();
        }

        private static int[] DES_CreateKey(string strKey)
        {
            int[] array = new int[16]
            {
            0,
            4,
            536870912,
            536870916,
            65536,
            65540,
            536936448,
            536936452,
            512,
            516,
            536871424,
            536871428,
            66048,
            66052,
            536936960,
            536936964
            };
            int[] array2 = new int[16]
            {
            0,
            1,
            1048576,
            1048577,
            67108864,
            67108865,
            68157440,
            68157441,
            256,
            257,
            1048832,
            1048833,
            67109120,
            67109121,
            68157696,
            68157697
            };
            int[] array3 = new int[16]
            {
            0,
            8,
            2048,
            2056,
            16777216,
            16777224,
            16779264,
            16779272,
            0,
            8,
            2048,
            2056,
            16777216,
            16777224,
            16779264,
            16779272
            };
            int[] array4 = new int[16]
            {
            0,
            2097152,
            134217728,
            136314880,
            8192,
            2105344,
            134225920,
            136323072,
            131072,
            2228224,
            134348800,
            136445952,
            139264,
            2236416,
            134356992,
            136454144
            };
            int[] array5 = new int[16]
            {
            0,
            262144,
            16,
            262160,
            0,
            262144,
            16,
            262160,
            4096,
            266240,
            4112,
            266256,
            4096,
            266240,
            4112,
            266256
            };
            int[] array6 = new int[16]
            {
            0,
            1024,
            32,
            1056,
            0,
            1024,
            32,
            1056,
            33554432,
            33555456,
            33554464,
            33555488,
            33554432,
            33555456,
            33554464,
            33555488
            };
            int[] array7 = new int[16]
            {
            0,
            268435456,
            524288,
            268959744,
            2,
            268435458,
            524290,
            268959746,
            0,
            268435456,
            524288,
            268959744,
            2,
            268435458,
            524290,
            268959746
            };
            int[] array8 = new int[16]
            {
            0,
            65536,
            2048,
            67584,
            536870912,
            536936448,
            536872960,
            536938496,
            131072,
            196608,
            133120,
            198656,
            537001984,
            537067520,
            537004032,
            537069568
            };
            int[] array9 = new int[16]
            {
            0,
            262144,
            0,
            262144,
            2,
            262146,
            2,
            262146,
            33554432,
            33816576,
            33554432,
            33816576,
            33554434,
            33816578,
            33554434,
            33816578
            };
            int[] array10 = new int[16]
            {
            0,
            268435456,
            8,
            268435464,
            0,
            268435456,
            8,
            268435464,
            1024,
            268436480,
            1032,
            268436488,
            1024,
            268436480,
            1032,
            268436488
            };
            int[] array11 = new int[16]
            {
            0,
            32,
            0,
            32,
            1048576,
            1048608,
            1048576,
            1048608,
            8192,
            8224,
            8192,
            8224,
            1056768,
            1056800,
            1056768,
            1056800
            };
            int[] array12 = new int[16]
            {
            0,
            16777216,
            512,
            16777728,
            2097152,
            18874368,
            2097664,
            18874880,
            67108864,
            83886080,
            67109376,
            83886592,
            69206016,
            85983232,
            69206528,
            85983744
            };
            int[] array13 = new int[16]
            {
            0,
            4096,
            134217728,
            134221824,
            524288,
            528384,
            134742016,
            134746112,
            16,
            4112,
            134217744,
            134221840,
            524304,
            528400,
            134742032,
            134746128
            };
            int[] array14 = new int[16]
            {
            0,
            4,
            256,
            260,
            0,
            4,
            256,
            260,
            1,
            5,
            257,
            261,
            1,
            5,
            257,
            261
            };
            int num = (strKey.Length < 24) ? 1 : 3;
            int[] array15 = new int[32 * num];
            int[] array16 = new int[16]
            {
            0,
            0,
            1,
            1,
            1,
            1,
            1,
            1,
            0,
            1,
            1,
            1,
            1,
            1,
            1,
            0
            };
            int num2 = 0;
            int num3 = 0;
            char[] array17 = strKey.ToCharArray();
            int length = strKey.Length;
            int num4 = length + num * 8;
            int[] array18 = new int[num4];
            for (int i = 0; i < length; i++)
            {
                array18[i] = Convert.ToInt32(array17[i]);
            }
            for (int j = length; j < num4; j++)
            {
                array18[j] = 0;
            }
            for (int k = 0; k < num; k++)
            {
                int num5 = (array18[num2++] << 24) | (array18[num2++] << 16) | (array18[num2++] << 8) | array18[num2++];
                int num6 = (array18[num2++] << 24) | (array18[num2++] << 16) | (array18[num2++] << 8) | array18[num2++];
                int num7 = (MoveByte(num5, 4) ^ num6) & 0xF0F0F0F;
                num6 ^= num7;
                num5 ^= num7 << 4;
                num7 = ((MoveByte(num6, -16) ^ num5) & 0xFFFF);
                num5 ^= num7;
                num6 ^= num7 << 16;
                num7 = ((MoveByte(num5, 2) ^ num6) & 0x33333333);
                num6 ^= num7;
                num5 ^= num7 << 2;
                num7 = ((MoveByte(num6, -16) ^ num5) & 0xFFFF);
                num5 ^= num7;
                num6 ^= num7 << 16;
                num7 = ((MoveByte(num5, 1) ^ num6) & 0x55555555);
                num6 ^= num7;
                num5 ^= num7 << 1;
                num7 = ((MoveByte(num6, 8) ^ num5) & 0xFF00FF);
                num5 ^= num7;
                num6 ^= num7 << 8;
                num7 = ((MoveByte(num5, 1) ^ num6) & 0x55555555);
                num6 ^= num7;
                num5 ^= num7 << 1;
                num7 = ((num5 << 8) | (MoveByte(num6, 20) & 0xF0));
                num5 = ((num6 << 24) | ((num6 << 8) & 0xFF0000) | (MoveByte(num6, 8) & 0xFF00) | (MoveByte(num6, 24) & 0xF0));
                num6 = num7;
                int num8 = array16.Length;
                for (int l = 0; l < num8; l++)
                {
                    if (array16[l] == 1)
                    {
                        num5 = ((num5 << 2) | MoveByte(num5, 26));
                        num6 = ((num6 << 2) | MoveByte(num6, 26));
                    }
                    else
                    {
                        num5 = ((num5 << 1) | MoveByte(num5, 27));
                        num6 = ((num6 << 1) | MoveByte(num6, 27));
                    }
                    num5 &= -15;
                    num6 &= -15;
                    int num9 = array[MoveByte(num5, 28)] | array2[MoveByte(num5, 24) & 0xF] | array3[MoveByte(num5, 20) & 0xF] | array4[MoveByte(num5, 16) & 0xF] | array5[MoveByte(num5, 12) & 0xF] | array6[MoveByte(num5, 8) & 0xF] | array7[MoveByte(num5, 4) & 0xF];
                    int num10 = array8[MoveByte(num6, 28)] | array9[MoveByte(num6, 24) & 0xF] | array10[MoveByte(num6, 20) & 0xF] | array11[MoveByte(num6, 16) & 0xF] | array12[MoveByte(num6, 12) & 0xF] | array13[MoveByte(num6, 8) & 0xF] | array14[MoveByte(num6, 4) & 0xF];
                    num7 = ((MoveByte(num10, 16) ^ num9) & 0xFFFF);
                    array15[num3++] = (num9 ^ num7);
                    array15[num3++] = (num10 ^ (num7 << 16));
                }
            }
            return array15;
        }

        private static int MoveByte(int val, int pos)
        {
            string empty = string.Empty;
            empty = Convert.ToString(val, 2);
            if (val >= 0)
            {
                empty = Convert.ToString(val, 2);
                int length = empty.Length;
                length = 32 - length;
                for (int i = 0; i < length; i++)
                {
                    empty = "0" + empty;
                }
            }
            pos = ((pos < 0) ? (pos + 32) : pos);
            for (int j = 0; j < pos; j++)
            {
                empty = "0" + empty.Substring(0, 31);
            }
            return Convert.ToInt32(empty, 2);
        }

        private static string StringToHex(string s)
        {
            StringBuilder stringBuilder = new StringBuilder();
            char[] array = new char[16]
            {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'A',
            'B',
            'C',
            'D',
            'E',
            'F'
            };
            int length = s.Length;
            char[] array2 = s.ToCharArray();
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(array[(int)array2[i] >> 4]);
                stringBuilder.Append(array[array2[i] & 0xF]);
            }
            return stringBuilder.ToString();
        }

        private static string HexToString(string s)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int length = s.Length;
            for (int i = 0; i < length; i += 2)
            {
                char value = Convert.ToChar(Convert.ToInt16("0x" + s.Substring(i, 2), 16));
                stringBuilder.Append(value);
            }
            return stringBuilder.ToString();
        }

        public static string DesEncrypt(string key, string message)
        {
            return StringToHex(DES(key, message, isEncrypt: true, 0, ""));
        }

        public static string DesEncrypt(string data)
        {
            return DesEncrypt(ApiDesKey, data);
        }

        public static string DesDecrypt(string key, string message)
        {
            return DES(key, HexToString(message), isEncrypt: false, 0, "");
        }

        public static string DesDecrypt(string data)
        {
            return DesDecrypt(ApiDesKey, data);
        }

        public static string EncryptAsy(string data)
        {
            return EncryptAsy(data, "2567", "hell");
        }

        public static string EncryptAsy(string data, string keyStr, string ivStr)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(keyStr);
            byte[] bytes2 = Encoding.Unicode.GetBytes(ivStr);
            byte[] bytes3 = Encoding.Unicode.GetBytes(data);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider())
                {
                    dESCryptoServiceProvider.Key = bytes;
                    dESCryptoServiceProvider.IV = bytes2;
                    using (ICryptoTransform transform = dESCryptoServiceProvider.CreateEncryptor())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(bytes3, 0, bytes3.Length);
                            cryptoStream.FlushFinalBlock();
                            string result = Convert.ToBase64String(memoryStream.ToArray());
                            memoryStream.Close();
                            cryptoStream.Close();
                            return result;
                        }
                    }
                }
            }
        }
    }

}
