#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：d0d9430a-2737-4a76-b21c-b620614ce829
// 文件名：AbstractMappingAttribute
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-01 8:36:50
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-01 8:36:50
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using System;

namespace HZJ.ORM.Mapping
{
    /// <summary>
    /// 数据库特性基类
    /// </summary>
    public abstract class AbstractMappingAttribute:Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="MappingName">映射名称</param>
        public AbstractMappingAttribute(string MappingName)
        {
            MapName = MappingName;
        }

        /// <summary>
        /// 映射名称
        /// </summary>
        public string MapName { get; } = null;
    }
}
