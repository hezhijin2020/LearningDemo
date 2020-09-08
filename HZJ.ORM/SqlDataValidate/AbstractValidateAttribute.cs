#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：144569ee-0371-4199-9c6c-8356200f60ef
// 文件名：AbstractValidateAttribute
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-05 9:34:03
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-05 9:34:03
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using System;

namespace HZJ.ORnodeSqlDataValidate
{
    /// <summary>
    /// 数据验验证类，只能作用于属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class AbstractValidateAttribute:Attribute
    {
       /// <summary>
       /// 验证的抽像方法
       /// </summary>
       /// <param name="value">验证的值</param>
       /// <returns></returns>
        public abstract bool Validate(object value);
    }
}
