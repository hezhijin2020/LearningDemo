#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：651c951d-f521-4202-834c-4528eac1447a
// 文件名：ConfigEntity
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-10-03 15:47:23
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-10-03 15:47:23
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion


namespace HZJ.DxWinForm.Utility.CommCls
{
    public class ConfigEntity
    {
        /// <summary>
        /// 自定义节点对象
        /// </summary>
        /// <param name="_value">节点的value值</param>
        /// <param name="_type">节点的type值</param>
        public ConfigEntity(string _value, string _type)
        {
            this.Value = _value;
            this.Type = _type;
        }

        public string _value;
        public string _type;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
