#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：60f5cbbf-bf26-4053-ab07-465da73487e3
// 文件名：ColumnMappingAttribute
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-01 8:46:54
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-01 8:46:54
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion


using System;

namespace HZJ.ORM.Mapping
{
    /// <summary>
    /// 列特性映射
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnMappingAttribute:AbstractMappingAttribute
    {
        public ColumnMappingAttribute(string MapColumnName) : base(MapColumnName)
        {

        }
    }
}
