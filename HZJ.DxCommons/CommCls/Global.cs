using HZJ.DxWinComm.CommCls;

namespace HZJ.DxWinForm.Utility.ClsCommon
{
    public static class Global
    {
        public static string _AppDir { get; set; }

        public static string _AppFolderLogs { get; set; }
      
        public static string _ConnectionString { get; set; } = string.Empty;

        public static Session _Session = null;

        public static HZJ.CommonCls.DB.SqlDbHelper _SqlDb = null;
    }
}
