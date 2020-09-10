using System;
using System.Text.RegularExpressions;

namespace HZJ.CommonCls.RegEx
{
    /// <summary>
    /// 正则表达式帮助类,常用的数据验证方法
    /// </summary>
    public class RegExHelper
    {
        #region  正则表达式验证

        /// <summary>
        /// 正则表达式验证
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <param name="RegStr">正则式</param>
        /// <returns></returns>
        public static string RegexMatched(string Str, string RegStr)
        {
            Regex regex = new Regex(RegStr);
            return regex.Match(Str).Value;
        }

        /// <summary>
        /// 正则表达式验证
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <param name="RegStr">正则式</param>
        /// <returns></returns>
        public static string RegexMatchedIngoreCase(string Str, string RegStr)
        {
            Regex regex = new Regex(RegStr, RegexOptions.IgnoreCase);
            return regex.Match(Str).Value;
        }

        /// <summary>
        /// 正则表达式验证
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <param name="RegStr">正则式</param>
        /// <returns></returns>
        public static bool RegexMatched2(string Str, string RegStr)
        {
            Regex regex = new Regex(RegStr);
            return regex.Match(Str).Value != "";
        }

        /// <summary>
        /// 正则表达式验证
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <param name="RegStr">正则式</param>
        /// <returns></returns>
        public static bool RegexMatchedIngoreCase2(string Str, string RegStr)
        {
            Regex regex = new Regex(RegStr, RegexOptions.IgnoreCase);
            return regex.Match(Str).Value != "";
        }
        #endregion

        #region 身份证验证

        /// <summary>
        /// 身份证验证
        /// </summary>
        /// <param name="CardNo">证件号</param>
        /// <returns></returns>
        private static bool CheckIDCard(string CardNo)
        {
            if (CardNo.Length == 18)
            {
                return CheckIDCard18(CardNo);
            }
            else if (CardNo.Length == 15)
            {
                return CheckIDCard15(CardNo);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证18位身份证号
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        public static bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 验证15位身份证号
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        public static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 根据身份证号获取出生年月日
        /// </summary>
        /// <param name="IDCard">证件号</param>
        /// <returns></returns>
        public static DateTime GetIDCardToBirthday(string IDCard)
        {
            string BirthDay = " ";
            string strYear;
            string strMonth;
            string strDay;
            if (IDCard.Length == 15)
            {
                strYear = IDCard.Substring(6, 4);
                strMonth = IDCard.Substring(8, 2);
                strDay = IDCard.Substring(10, 2);
                BirthDay = strYear + "- " + strMonth + "- " + strDay;
            }
            if (IDCard.Length == 18)
            {
                strYear = IDCard.Substring(6, 4);
                strMonth = IDCard.Substring(10, 2);
                strDay = IDCard.Substring(12, 2);
                BirthDay = strYear + "- " + strMonth + "- " + strDay;
            }
            return clsPublic.ToDateTime(BirthDay);
        }
        #endregion
    }
}
