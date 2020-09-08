#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：9f2a3073-e86e-4d0e-8520-0ddacc2e8d01
// 文件名：ReflectionExpressionExtend
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-08 11:28:47
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-08 11:28:47
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using System;
using System.Reflection;

namespace HZJ.ORM.SqlExpression
{
    /// <summary>
    /// 成员反射操作扩展方法
    /// </summary>
    internal static class ReflectionExpressionExtend
    {
        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="member">成员</param>
        /// <param name="instance">实列</param>
        /// <returns></returns>
        public static object GetValue(this MemberInfo member, object instance)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Property:
                    return ((PropertyInfo)(member)).GetValue(instance, null);
                case MemberTypes.Field:
                    return ((FieldInfo)(member)).GetValue(instance);
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// 设置成员值
        /// </summary>
        /// <param name="member">成员</param>
        /// <param name="instance">实列</param>
        /// <param name="value">值</param>
        public static void SetValue(this MemberInfo member,object instance,object value)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Property:
                    var pi = (PropertyInfo)member;
                    pi.SetValue(instance, value, null);
                    break;
                case MemberTypes.Field:
                    var fi = (FieldInfo)member;
                    fi.SetValue(instance, value);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
