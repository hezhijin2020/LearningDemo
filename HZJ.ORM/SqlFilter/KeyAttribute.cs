#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：95153274-1261-4ac5-87d3-afe3a68505fe
// 文件名：KeyFilter
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-01 15:58:32
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-01 15:58:32
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using System;

namespace HZJ.ORnodeSqlFilter
{
    /// <summary>
    /// 主键类特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute:Attribute
    {
    }
}
