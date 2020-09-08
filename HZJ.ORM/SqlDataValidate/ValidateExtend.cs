#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：34406e5e-5cfc-4530-8584-f08461c1f3b0
// 文件名：ValidateExtend
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-05 9:55:45
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-05 9:55:45
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using System;
using System.Reflection;

namespace HZJ.ORnodeSqlDataValidate
{
    /// <summary>
    /// 数据验证扩展方法
    /// </summary>
    public static class ValidateExtend
    {
        /// <summary>
        /// 数据验证扩展方法
        /// </summary>
        public static bool Validate<T>(this T t)
        {
            Type type = t.GetType();
            foreach (var prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(AbstractValidateAttribute),true))
                {
                    object value = prop.GetValue(t);
                    var attributes = prop.GetCustomAttributes<AbstractValidateAttribute>();
                    foreach (var att in attributes)
                    {
                        if (att.Validate(value))
                        {
                            continue;
                        }
                        else {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
