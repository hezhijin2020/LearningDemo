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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZJ.DxWinForm.Utility.ClsCommon
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
            try
            {
                NameValueCollection nvc = (NameValueCollection)ConfigurationManager.GetSection("coustomSection/theme");
                if (nvc.AllKeys.Length > 0)
                    return nvc[0];
                else
                    return "Springtime";
            }
            catch (Exception ex)
            {
                throw new Exception($"配置文件：{ex.Message}");
            }
        }

       /// <summary>
       /// 设置默认的主题名称
       /// </summary>
       /// <param name="themeName">主题名称</param>
        public static void SetDefaultTheme(string themeName)
        {
            try
            {
                if (string.IsNullOrEmpty(themeName))
                {
                    themeName = "Springtime";
                }

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConfigurationSection Section = config.GetSection("coustomSection/theme");
                var obj= Section.SectionInformation;
                obj.
                config.Save();
                ConfigurationManager.RefreshSection("coustomSection/theme");  //让修改之后的结果生效
            }
            catch (Exception ex)
            {
                throw new Exception($"配置文件：{ex.Message}");
            }
            
        }


      
    }
}
