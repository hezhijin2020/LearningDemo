using HZJ.DxWinForm.Utility.CommCls;
using System.Windows.Forms;

namespace HZJ.DxWinForm.Utility.CommCls
{
    public class Global
    {
        public static string _AppDir => Application.StartupPath;

        public static string _AppProName => AppSetingHelper.GetAppName();

        public static string _AppUpdateName =>$"{Global._AppDir}\\AutoUpdate.exe";

        public static string _AppUpdateList => $"{Global._AppDir}\\UpdateList.xml";

        public static string _AppFolderLogs = $"{Global._AppDir}\\Logs\\";

        public static string _ConnectionString => AppSetingHelper.GetConnectionString("SqlConnStringMain");

        public static Session _Session = new Session();

        public static RightingSysManager _AppRight = new RightingSysManager();

        public static SqlDbHelper _SqlDb = new SqlDbHelper(_ConnectionString);
    }
}
