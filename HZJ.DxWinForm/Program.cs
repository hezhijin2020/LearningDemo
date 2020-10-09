using DevExpress.Skins;
using DevExpress.UserSkins;
using HZJ.DxWinForm.Utility.CommCls;
using System;
using System.Windows.Forms;

namespace HZJ.DxWinForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppStartupHelper.BindExceptionHandler();//异常处理
            //程序是否需要更新
            if (AppStartupHelper.IsNeedUpdate())
            {
                AppStartupHelper.StartUpdate();
            }
            else
            {
                BonusSkins.Register();//系统主题
                SkinManager.EnableFormSkins();

                //检查程序是否已经运行
                if (AppStartupHelper.IsRuning())
                {
                    DxPublic.ShowMessage("程序正在运行！");
                    return;
                }
                Application.Run(new MainForm());
            }
        }
    }
}
