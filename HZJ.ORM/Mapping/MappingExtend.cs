#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：6ce51b27-699b-43d2-bf60-63bfa1bd3dfa
// 文件名：MappingExtend
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-01 8:51:54
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-01 8:51:54
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion


using System.Reflection;

namespace HZJ.ORnodeMapping
{
    /// <summary>
    /// 自定义映射的扩展方法
    /// </summary>
    public static class MappingExtend
    {
        //private static string _Name=null;
        /// <summary>
        /// 获取特性映射名称
        /// </summary>
        /// <typeparam name="T">泛型类</typeparam>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static string GetMapingName<T>(this T type) where T : MemberInfo
        {
            if (type.IsDefined(typeof(AbstractMappingAttribute)))
            {
                var attribute = type.GetCustomAttribute<AbstractMappingAttribute>();
                return attribute.MapName;
            }
            else
            {
               return type.Name;
            }
        }

      //  public static string MappingName => _Name;
    }
}
