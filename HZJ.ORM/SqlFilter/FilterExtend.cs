#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：eb176598-c2db-4dd5-9c50-80c61e361ebf
// 文件名：FilterExtend
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-01 16:03:01
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-01 16:03:01
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HZJ.ORnodeSqlFilter
{
    public static class FilterExtend
    {

        /// <summary>
        /// 过滤掉标记了Key 特性的属性， 返回其它全部属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesWithoutKey(this Type type)
        {
            return type.GetProperties().Where(a => !a.IsDefined(typeof(KeyAttribute)));
        }

        /// <summary>
        /// 就是通过json字符串找出更新的字段
        /// </summary>
        /// <param name="type"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesInJson(this Type type, string json)
        {
            return type.GetProperties().Where(p => json.Contains($"'{p.Name}':") || json.Contains($"\"{p.Name}\":"));
        }

    }
}
