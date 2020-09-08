#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：0715b8ed-3dcc-487b-939c-77be9ea1767e
// 文件名：TableMappingAttribute
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-01 8:44:06
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-01 8:44:06
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZJ.ORM.Mapping
{
    /// <summary>
    /// 表名特性映射
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableMappingAttribute : AbstractMappingAttribute
    {
        public TableMappingAttribute(string MapTableName) : base(MapTableName)
        { 
        }
    }
}
