
using System;
using System.Data.SqlClient;

namespace HZJ.CommonCls.Logs
{
    public class clslog4net
    {
        private static readonly log4net.ILog appLog = log4net.LogManager.GetLogger("appLogs");
        private static readonly log4net.ILog LogOpration = log4net.LogManager.GetLogger("LogOpration");
        private static readonly log4net.ILog LogEvent = log4net.LogManager.GetLogger("LogEvent");
        private static readonly log4net.ILog LogSQL = log4net.LogManager.GetLogger("LogSQL");


        #region  程序的日志信息
        //一般日志信息
        public static void LogInfo(string msg)
        {
            if (appLog.IsInfoEnabled)
            {
                appLog.Info(msg);
            }
        }
        public static void LogInfo(string msg,Exception ex)
        {
            if (appLog.IsInfoEnabled)
            {
                appLog.Info(msg,ex);
            }
        }

        //调试日志信息
        public static void LogDebug(string msg)
        {
            if (appLog.IsDebugEnabled)
            {
                appLog.Debug(msg);
            }
        }
        public static void LogDebug(string msg, Exception ex = null)
        {
            appLog.Debug(msg, ex);
        }

        //错误日志信息
        public static void LogError(string msg)
        {
            if (appLog.IsErrorEnabled)
            {
                appLog.Error(msg);
            }
        }
        public static void LogError(string msg, Exception ex = null)
        {
            appLog.Error(msg, ex);
        }


        // 致命日志信息
        public static void LogFatal(string msg)
        {
            if (appLog.IsFatalEnabled)
            {
                appLog.Fatal(msg);
            }
        }
        public static void LogFatal(string msg, Exception ex = null)
        {
            appLog.Fatal(msg, ex);
        }

        //警告日志信息
        public static void LogWarn(string msg)
        {
            if (appLog.IsWarnEnabled)
            {
                appLog.Warn(msg);
            }
        }
        public static void LogWarn(string msg, Exception ex = null)
        {
            appLog.Warn(msg, ex);
        }

        #endregion



        #region LogEvent

        public static void EventInfo(string msg)
        {
            if (LogEvent.IsInfoEnabled)
            {
                LogEvent.Info(msg);
            }
        }
        public static void EventDebug(string msg)
        {
            if (LogEvent.IsDebugEnabled)
            {
                LogEvent.Debug(msg);
            }
        }
        public static void EventError(string msg)
        {
            if (LogEvent.IsErrorEnabled)
            {
                LogEvent.Error(msg);
            }
        }
        public static void EventFatal(string msg)
        {

            if (LogEvent.IsFatalEnabled)
            {
                LogEvent.Fatal(msg);
            }
        }
        public static void EventWarn(string msg)
        {

            if (LogEvent.IsWarnEnabled)
            {
                LogEvent.Warn(msg);
            }
        }

        #endregion

        #region LogSQL

        public static void SQLInfo(string msg)
        {
            if (LogSQL.IsInfoEnabled)
            {
                LogSQL.Info(msg);
            }
        }
        public static void SQLDebug(string msg)
        {
            if (LogSQL.IsDebugEnabled)
            {
                LogSQL.Debug(msg);
            }
        }
        public static void SQLError(string msg)
        {
            if (LogSQL.IsErrorEnabled)
            {
                LogSQL.Error(msg);
            }
        }
        public static void SQLFatal(string msg)
        {

            if (LogSQL.IsFatalEnabled)
            {
                LogSQL.Fatal(msg);
            }
        }
        public static void SQLWarn(string msg)
        {

            if (LogSQL.IsWarnEnabled)
            {
                LogSQL.Warn(msg);
            }
        }
        #endregion
    }
}
