#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：6f21cd2e-193e-47f7-92bc-8c81935128fd
// 文件名：IntAttribute
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-05 9:37:51
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-05 9:37:51
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion


namespace HZJ.ORnodeSqlDataValidate
{
    /// <summary>
    /// INT类型验证
    /// </summary>
    public class IntAttribute : AbstractValidateAttribute
    {
        private int _Min = 0;
        private int _Max = 0;

        /// <summary>
        /// 设置INT类型的最大值和最小值
        /// </summary>
        /// <param name="min">最大值</param>
        /// <param name="max">最小值</param>
        public IntAttribute(int min = 0, int max = 0)
        {
            this._Min = min;
            this._Max = max;
        }

        /// <summary>
        /// 验证数据方法
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public override bool Validate(object value)
        {
            if (value == null) return false ;

            int result = 0;
            if (int.TryParse(value.ToString(), out result))
            {
               return result >= this._Min && result <= this._Max;
            }
            else
            {
                return false;
            }
        }
    }
}
