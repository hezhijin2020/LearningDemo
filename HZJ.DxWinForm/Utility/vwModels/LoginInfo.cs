#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：85aa0bf8-f4de-40ae-85f4-6ba8ecc86aca
// 文件名：LoginInfo
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-10-05 11:24:30
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-10-05 11:24:30
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion


namespace HZJ.DxWinForm.Utility.vwModels
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// 登录名称
        /// </summary>
        public string LoginName { get; set; } = "";

        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPwd { get; set; } = "";

        /// <summary>
        /// 是否记信密码
        /// </summary>
        public bool IsRberPwd { get; set; } = false;
    }
}
