#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：e2e17450-cfe2-46fd-98fd-f504659198fc
// 文件名：LengthAttribute
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-05 9:50:03
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-05 9:50:03
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion


namespace HZJ.ORnodeSqlDataValidate
{
    /// <summary>
    /// 值的长度难证
    /// </summary>
    public class LengthAttribute : AbstractValidateAttribute
    {
        private int _Min = 0;
        private int _Max = 0;

        /// <summary>
        /// 设置INT类型的最大值和最小值
        /// </summary>
        /// <param name="min">最大值</param>
        /// <param name="max">最小值</param>
        public LengthAttribute(int minLength = 0, int maxLength = 0)
        {
            this._Min = minLength;
            this._Max = maxLength;
        }

        /// <summary>
        /// 验证数据方法
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public override bool Validate(object value)
        {
            return value != null
              && value.ToString().Length >= this._Min
              && value.ToString().Length <= this._Max;
        }
    }
}
