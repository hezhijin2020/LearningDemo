#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：3e7427e4-4600-4aa7-8d3b-9ad064650a34
// 文件名：AppSetingHandler
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-10-03 15:43:02
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-10-03 15:43:02
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace HZJ.DxWinForm.Utility.CommCls
{
    public class AppSetingHandler : IConfigurationSectionHandler
    {
        /// <summary>
        /// 返回自定义节点对象字典
        /// </summary>
        /// <param name="parent">父对象</param>
        /// <param name="configContext">配置上下文对象</param>
        /// <param name="section">节 XML 节点</param>
        /// <returns> 创建的节处理程序对象。</returns>
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            Dictionary<string, ConfigEntity> config = new Dictionary<string, ConfigEntity>();
            foreach (XmlNode node in section)
            {
                string key = string.Empty, value = string.Empty, type = string.Empty;
                if (node.Attributes["key"] != null)
                    key = node.Attributes["key"].Value;
                if (node.Attributes["value"] != null)
                    value = node.Attributes["value"].Value;
                if (node.Attributes["type"] != null)
                    type = node.Attributes["type"].Value;
                config.Add(key, new ConfigEntity(value, type));
            }
            return config;
        }
    }
}
