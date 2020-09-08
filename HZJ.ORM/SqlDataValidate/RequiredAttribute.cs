#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：b688083e-6b89-4f9c-a8db-c1e16d3d9c04
// 文件名：RequiredAttribute
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-05 9:52:18
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-05 9:52:18
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion


namespace HZJ.ORnodeSqlDataValidate
{
    /// <summary>
    /// 必须的不为空验证
    /// </summary>
    public class RequiredAttribute:AbstractValidateAttribute
    {
        /// <summary>
        ///数据不为空验证
        /// </summary>
        /// <param name="value">验证值</param>
        /// <returns></returns>
        public override bool Validate(object value)
        {
            return value != null && !string.IsNullOrEmpty(value.ToString());
        }
    }
}
