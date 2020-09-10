using DevExpress.Utils.Layout;
using System;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Forms;

namespace HZJ.DxWinForm.Utility.ClsCommon
{

    public class Global
    {
        public static string _AppDir => Application.StartupPath;

        public static string _AppLogs
        { 
            get{
                var filename= _AppDir + $"\\Logs\\{DateTime.Now.ToString("yyyymmdd")}.log";
                HZJ.CommonCls.IO.FileHelper.CheckFileExists(filename);
                return filename;
            }
        }

        public static string _IniConfigFile
        {
            get
            {
                var filename = _AppDir + "\\appConfig.ini";
                HZJ.CommonCls.IO.FileHelper.CheckFileExists(filename);
                return filename;
            }
        }

        public static string _ConnectionString
        {
            get {
                try {
                    return ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
                }
                catch {
                    throw new Exception("获取配置文件的连接字符串出错！");
                }
            }
        }
       
        public static RightingSysManager _AppRight=new RightingSysManager();

        public static Session _Session=new Session();

        public static HZJ.CommonCls.DB.SqlDbHelper _SqlDb= new CommonCls.DB.SqlDbHelper(_ConnectionString);
    }
}
