#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：f8048580-08c8-46f5-aee5-c7fa4c020544
// 文件名：DbSettingInfo
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-10-08 9:43:55
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-10-08 9:43:55
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using HZJ.DxWinForm.Utility.CommCls;
using System;

namespace HZJ.DxWinForm.Utility.vwModels
{
    public class DbSettingInfo
    {   
        /// <summary>
        /// 数据库连接类型
        /// </summary>
        public ConnStrType DBconnStrType { get; set; }
        /// <summary>
        /// 服务器
        /// </summary>
        public string DBServer { get; set; } = ".";
        /// <summary>
        /// 数据库
        /// </summary>
        public string DBName { get; set; } = "RightingSys";
        /// <summary>
        /// 用户
        /// </summary>
        public string DBUser { get; set; } = "sa";
        /// <summary>
        /// 密码
        /// </summary>
        public string DBUserPwd { get; set; } = "yishion";

        /// <summary>
        /// 端口
        /// </summary>
        public string DBPort { get; set; } = "1433";

        public DbSettingInfo(string connStr)
        {
            try
            {
                string[] items = connStr.Split(';');
                if (items.Length >= 5)
                {
                    this.DBServer = items[0].Split(',')[0].Replace("Data Source=", "");
                    this.DBPort = items[0].Split(',')[1];
                    this.DBName = items[1].Replace("Initial Catalog=", "");
                    this.DBUser = items[2].Replace("User Id=", "");
                    this.DBUserPwd = items[3].Replace("Password=", "");

                }
            }
            catch 
            {
               
            }
        }

        public DbSettingInfo()
        {

        }

        public string GetConnString()
        {
            string connStr = string.Empty;
            switch (DBconnStrType)
            {
                case ConnStrType.SQL:
                    connStr= $"Data Source={DBServer},{DBPort};Initial Catalog={DBName};User Id={DBUser};Password={DBUserPwd};Connect Timeout=5;";
                    break;
                case ConnStrType.MYSQL:
                    connStr = $"Data Source={DBServer},{DBPort};Initial Catalog={DBName};User Id={DBUser};Password={DBUserPwd};Connect Timeout=5;";
                    break;
                case ConnStrType.ACCESS:
                    connStr = $"Data Source={DBServer},{DBPort};Initial Catalog={DBName};User Id={DBUser};Password={DBUserPwd};Connect Timeout=5;";
                    break;
                case ConnStrType.ORACLE:
                    connStr = $"Data Source={DBServer},{DBPort};Initial Catalog={DBName};User Id={DBUser};Password={DBUserPwd};Connect Timeout=5;";
                    break;
                case ConnStrType.SQLITE:
                    connStr = $"Data Source={DBServer},{DBPort};Initial Catalog={DBName};User Id={DBUser};Password={DBUserPwd};Connect Timeout=5;";
                    break;
                default:
                    new Exception("没有找到指定的数据库连接");
                    break;
            }
            return connStr;
        }
    }
}
