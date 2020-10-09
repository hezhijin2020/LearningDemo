#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：7e6652d5-5994-4687-9727-b015ed8318d6
// 文件名：AppStartupHelper
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-10-05 13:56:30
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-10-05 13:56:30
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion


using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace HZJ.DxWinForm.Utility.CommCls
{
   /// <summary>
   /// 程序启动帮助类
   /// </summary>
    public class AppStartupHelper
    {
        #region 自动更新
        /// <summary>
        /// 程序是否有更新
        /// </summary>
        /// <returns></returns>
        public static bool IsNeedUpdate()
        {
            bool result = false;
            try
            {
                string FileName = Global._AppUpdateList;
                AutoUpXmlHelper xdoc = new AutoUpXmlHelper(FileName);
                string version = xdoc.GetNodeValue("AutoUpdate/Application/Version");
                result = Global._AppRight.IsUpdateVersion(Global._Session._SystemId, version);
            }
            catch (Exception ex)
            {
                DxPublic.ShowException(ex);
                result = false;
            }
            return result;
        }
       
        /// <summary>
        /// 启动更新程序
        /// </summary>
        public static void StartUpdate()
        {
            try
            {
                Process[] processesByName = Process.GetProcessesByName(Global._AppProName);
                Process[] array = processesByName;
                for (int i = 0; i < array.Length; i++)
                {
                    Process process = array[i];
                    process.Kill();
                }
                Process.Start(Global._AppUpdateName); 
            }
            catch (Exception ex)
            {
                DxPublic.ShowException(ex);
            }
        }

        #endregion

        #region 程序单列运行
        /// <summary>
        /// 程序是否在运行
        /// </summary>
        /// <returns></returns>
        public static bool IsRuning()
        {
            bool isRun = false;
            SingleInstanceApplication.Guard(out isRun);
            return isRun;
        }
        #endregion

        #region   在Winform程序中捕获全局异常

        /// <summary>
        /// 绑定程序中的异常处理
        /// </summary>
        public static void BindExceptionHandler()
        {
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理未捕获的异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        /// <summary>
        /// 处理UI线程异常
        /// </summary>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            //clsPublicLogs.LogError(null, e.Exception as Exception);
        }

        /// <summary>
        /// 处理未捕获的异常
        /// </summary>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //clsPublicLogs.LogError(null, e.ExceptionObject as Exception);
        }
        #endregion
    }
}
