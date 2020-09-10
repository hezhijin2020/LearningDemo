using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZJ.CommonCls.logs
{
    /// <summary>
    /// 文本日志
    /// </summary>
    public class clsTextLogs
    {
        /// <summary>
        /// 写入日志信息
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="ex">异常信息</param>
        public static void WriteLog(string fileName, System.Exception ex)
        {
            IO.FileHelper.WriteTextFileLine(fileName, string.Format("Message:{0} Source:{1} TargetSite:{2} StackTrace:{3}", new object[]
            {
                ex.Message,
                ex.Source,
                ex.TargetSite.ToString(),
                ex.StackTrace
            }), true);
        }
    }
}
