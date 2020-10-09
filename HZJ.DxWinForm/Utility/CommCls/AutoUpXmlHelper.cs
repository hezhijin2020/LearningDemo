#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：c3c4c148-78ed-4fb5-bfee-c97774017988
// 文件名：AutoUpXmlHelper
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-10-09 16:15:27
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-10-09 16:15:27
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using System.Xml;

namespace HZJ.DxWinForm.Utility.CommCls
{
    public class AutoUpXmlHelper:XmlDocument
    {
        private string _XmlFileName;
        public AutoUpXmlHelper(string xmlFile)
        {
            _XmlFileName = xmlFile;
            this.Load(xmlFile);
        }
        /// <summary>
        /// 给定一个节点的xPath表达式并返回一个节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public XmlNode FindNode(string xPath)
        {
            XmlNode xmlNode = this.SelectSingleNode(xPath);
            return xmlNode;
        }
        /// <summary>
        /// 给定一个节点的xPath表达式返回其值
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public string GetNodeValue(string xPath)
        {
            XmlNode xmlNode = this.SelectSingleNode(xPath);
            return xmlNode.InnerText;
        }
        /// <summary>
        /// 给定一个节点的表达式返回此节点下的孩子节点列表
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public XmlNodeList GetNodeList(string xPath)
        {
            XmlNodeList nodeList = this.SelectSingleNode(xPath).ChildNodes;
            return nodeList;
        }

    }
}
