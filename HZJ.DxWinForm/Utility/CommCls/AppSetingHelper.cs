#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：41acdd40-d4cb-46b3-af9c-70d8ec7d6366
// 文件名：AppSetingHelper
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-10-03 16:23:34
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-10-03 16:23:34
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using System;
using System.Configuration;

namespace HZJ.DxWinForm.Utility.CommCls
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class AppSetingHelper
    {
        /// <summary>
        /// 获取默认主题名
        /// </summary>
        /// <returns>主题名</returns>
        public static string GetDefaultTheme()
        {
            return GetValue("DefaultTheme", "Springtime");
        }

       /// <summary>
       /// 设置默认的主题名称
       /// </summary>
       /// <param name="themeName">主题名称</param>
        public static bool SetDefaultTheme(string themeName)
        {
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "Springtime";
            }
            return SetValue("DefaultTheme", themeName);
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="key">连接字符串键</param>
        /// <returns></returns>
        public static string GetConnectionString(string name)
        {
            try
            {
                var value = ConfigurationManager.ConnectionStrings[name].ConnectionString;
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception($"读取配置文件的连接字符串出错！");
                }
                else
                {
                    return DxPublic.DecryptString(value);
                }

            }
            catch 
            {
                return "";
            }
        }

        /// <summary>
        /// 获取连接字符串，存在则修改，不存在则添加
        /// </summary>
        /// <param name="key">连接字符串键</param>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        public static bool SetConnectionString(string name,string connString,string providerName= "System.Data.SqlClient")
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.ConnectionStrings.ConnectionStrings;
                connString = DxPublic.EncryptString(connString);
                if (settings[name] == null)
                {
                    settings.Add(new ConnectionStringSettings(name,connString, providerName));
                }
                else
                {
                    settings[name].ConnectionString= connString;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"定入配置文件出错：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns name="LoginInfo">登录信息</returns>
        public static vwModels.LoginInfo GetLoginInfo()
        {
            var loginInfo = new vwModels.LoginInfo();
            loginInfo.LoginName = GetValue("LoginName");
            loginInfo.IsRberPwd = Convert.ToBoolean(GetValue("IsRberPwd", "false"));
            if (loginInfo.IsRberPwd)
            {
                loginInfo.LoginPwd = DxPublic.DecryptString(GetValue("LoginPwd"));
            }
            return loginInfo;
        }

        /// <summary>
        /// 设置登录信息
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <returns></returns>
        public static bool SetLoginInfo(vwModels.LoginInfo loginInfo)
        {
            loginInfo.LoginPwd = DxPublic.EncryptString(loginInfo.LoginPwd);
            if (!loginInfo.IsRberPwd)
            {
                loginInfo.LoginPwd = "";
            }
            if ( SetValue("LoginName", loginInfo.LoginName)
                &&SetValue("LoginPwd", loginInfo.LoginPwd)
                && SetValue("IsRberPwd", loginInfo.IsRberPwd.ToString()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取系统ID
        /// </summary>
        /// <returns></returns>
        public static Guid GetSystemId()
        {
            return DxPublic.GetObjGUID(AppSetingHelper.GetValue("SystemId"));
        }

        /// <summary>
        /// 设置系统ID
        /// </summary>
        /// <param name="systemId">系统ID</param>
        /// <returns></returns>
        public static bool SetSystemId(Guid systemId)
        {
            return AppSetingHelper.SetValue("SystemId", systemId.ToString()) ;
        }

        /// <summary>
        /// 获取系统名称
        /// </summary>
        /// <returns></returns>
        public static string GetSystemName()
        {
            return AppSetingHelper.GetValue("SystemName");
        }

        /// <summary>
        /// 设置系统名称
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public static bool SetSystemName(string systemName)
        {
            return AppSetingHelper.SetValue("SystemName", systemName);
        }

        /// <summary>
        /// 获取程序集名称
        /// </summary>
        /// <returns></returns>
        public static string GetAppName()
        {
            return AppSetingHelper.GetValue("AppName");
        }


        /// <summary>
        /// 写入AppSeting 节点,键存在就修改，不存在就新增
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        private static bool SetValue(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                   settings.Add(key,value);
                }
                else
                {
                    settings[key].Value=value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"定入配置文件出错：{ex.Message}");
            }
        }

        /// <summary>
        /// 读取AppSeting 节点配置
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">当值为空时返回默认值</param>
        private static string GetValue(string key, string Defaultvalue="")
        {
            try
            {
                var value = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(value))
                {
                    value= Defaultvalue;
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception($"读取配置文件出错：{ex.Message}");
            }
        }

    }
}
