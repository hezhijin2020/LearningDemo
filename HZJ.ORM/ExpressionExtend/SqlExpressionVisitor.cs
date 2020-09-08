#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：6a251a76-0e1b-45e7-9697-97faa25083ea
// 文件名：SQLExpressionVisitor
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-01 17:16:53
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-01 17:16:53
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using HZJ.ORM.Mapping;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HZJ.ORM.ExpressionExtend
{
    /// <summary>
    /// 表达式目录树分析 To SQL 条件语句
    /// </summary>
    public class SQLExpressionVisitor:ExpressionVisitor
    {
        private Stack<string> ConditionStack = new Stack<string>();

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ConstantExpression)
            {
                var value1 = this.InvokeValue(node);
                this.ConditionStack.Push($"'{value1}'");
            }
            else
            {
                string name = node.Member.GetMapingName();
                this.ConditionStack.Push("@"+name);
            }
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            this.ConditionStack.Push($"'{node.Value.ToString()}'");
            return node;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            this.ConditionStack.Push(" ) ");
            base.Visit(node.Right);
            this.ConditionStack.Push(node.NodeType.ToSqlOperator());
            base.Visit(node.Left);
            this.ConditionStack.Push(" ( ");
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node == null) throw new ArgumentNullException("MethodCallExpression");

            this.Visit(node.Arguments[0]);
            string format;
            switch (node.Method.Name)
            {
                case "StartsWith":
                    format = "({0} LIKE {1}+'%')";
                    break;

                case "Contains":
                    format = "({0} LIKE '%'+{1}+'%')";
                    break;

                case "EndsWith":
                    format = "({0} LIKE '%'+{1})";
                    break;

                default:
                    throw new NotSupportedException(node.NodeType + " is not supported!");
            }
            this.ConditionStack.Push(format);
            this.Visit(node.Object);
            string left = this.ConditionStack.Pop();
            format = this.ConditionStack.Pop();
            string right = this.ConditionStack.Pop();
            this.ConditionStack.Push(String.Format(format, left, right));
            return node;
        }

        protected  object InvokeValue(MemberExpression node)
        {
            var objExp = Expression.Convert(node, typeof(object));//struct需要
            return Expression.Lambda<Func<object>>(objExp).Compile().Invoke();
        }

       
    }
}
