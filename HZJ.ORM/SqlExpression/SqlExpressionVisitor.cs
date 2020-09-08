#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：93bfc93c-fcb6-4fb3-9e3a-bfcf4050b88f
// 文件名：SqlExpressionVisitor
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-07 14:54:31
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-07 14:54:31
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HZJ.ORM.SqlExpression
{
    /// <summary>
    /// SQL表达式目录树。转成SQL条件语句类
    /// </summary>
    public  class SqlExpressionVisitor:ExpressionVisitor
    {

        #region 声明字段和方法
        private  ExpressionSqlType ReadSqlType = ExpressionSqlType.Where;
        private string WhereResult = string.Empty;
        private string OrderResult = string.Empty;

        private int depth = 0;
        private TextWriter StringWriter;
        public int OrderTime { get; set; } = 0;
        public int WhereTime { get; set; } = 0;

        #endregion

        #region 

        //VisitBinary(二元运算) 
        //VisitUnary（一元运算）
        //VisitConditional(条件运算) 
        //VisitConstant(常量表达式)
        //VisitElementInit( 元素的初始化)

        /// <summary>
        /// 二元运算入口方法
        /// </summary>
        /// <param name="node">二元运算表达式目录树</param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            this.Write("(");
            if (node.NodeType == ExpressionType.Power)//幂运算
            {
                this.Write("POWER(");
                this.VisitValue(node.Left);
                this.Write(", ");
                this.VisitValue(node.Right);
                this.Write(")");
            }
            else if (node.NodeType == ExpressionType.Coalesce)//联合或合并运算
            {
                this.Write("COALESCE(");
                this.VisitValue(node.Left);
                this.Write(", ");
                Expression rigth = node.Right;
                while (rigth.NodeType == ExpressionType.Coalesce)
                {
                    BinaryExpression binary = (BinaryExpression)rigth;
                    this.VisitValue(binary.Left);
                    this.Write(", ");
                    rigth = binary.Right;
                }

                this.VisitValue(rigth);
                this.Write(")");

            }
            else if (node.NodeType == ExpressionType.LeftShift)//按位左移运算
            {
                this.Write("(");
                this.VisitValue(node.Left);
                this.Write(" * POWER(2, ");
                this.VisitValue(node.Right);
                this.Write("))");
            }
            else if (node.NodeType == ExpressionType.RightShift)//按位右移运算
            {
                this.Write("(");
                this.VisitValue(node.Left);
                this.Write(" * POWER(2, ");
                this.VisitValue(node.Right);
                this.Write("))");
            }
            else {
                this.Visit(node.Left);
                this.Write(" ");
                this.Write(GetOperator(node.NodeType));
                this.Write(" ");
                this.Visit(node.Right);
            }
            this.Write(")");
            return node;
        }

        /// <summary>
        /// 一元运算符表达式入口方法
        /// </summary>
        /// <param name="node">一元运算表达式树</param>
        /// <returns></returns>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Convert://转换方法
                case ExpressionType.ConvertChecked:
                    this.Visit(node.Operand);
                    break;
                case ExpressionType.ArrayLength://数组长度
                    this.Visit(node.Operand);
                    this.Write(".Length");
                    break;
                case ExpressionType.Quote://常量表达式
                    this.Visit(node.Operand);
                    break;

                case ExpressionType.TypeAs: //转换类型
                    this.Visit(node.Operand);
                    this.Write(" as ");
                    this.Write(this.GetTypeName(node.Type));
                    break;
                case ExpressionType.UnaryPlus:
                    this.Visit(node.Operand);
                    break;
                default:
                    this.Write(this.GetOperator(node.NodeType));
                    this.Visit(node.Operand);
                    break;
            }
            return node;
        }

        /// <summary>
        /// 条件运算入口方法
        /// </summary>
        /// <param name="node">条件运算符表达式</param>
        /// <returns></returns>
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            this.Visit(node.Test);
            this.WriteLine(Indentation.Inner);
            this.Write("? ");
            this.Visit(node.IfTrue);
            this.WriteLine(Indentation.Same);
            this.Write(": ");
            this.Visit(node.IfFalse);
            this.Indent(Indentation.Outer);
            return node;
        }

        //protected override IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
        //{
        //    for (int i = 0; i < original.Count; i++)
        //    {
        //        this.VisitMemberBinding(original[i]);
        //        if (i < original.Count - 1)
        //        {
        //            this.Write(",");
        //            this.WriteLine(Indentation.Same);
        //        }
        //    }
        //    return original;
        //}

        /// <summary>
        /// 常量表达式
        /// </summary>
        /// <param name="node">常量表达式</param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value == null)
            {
                this.Write("null");
            }
            else if (node.Type == typeof(DateTime))
            {
                this.Write("'");
                this.Write((DateTime.Parse(node.Value.ToString())).ToString("yyyy-MM-dd HH:mm:ss"));
                this.Write("'");
            }
            else if (node.Type == typeof(Guid))
            {
                this.Write("'");
                this.Write(node.Value.ToString());
                this.Write("'");
            }
            else {
                switch (Type.GetTypeCode(node.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        this.Write(((bool)node.Value) ? "1" :"0");
                        break;
                    case TypeCode.Single:
                    case TypeCode.Double:
                        string str = node.Value.ToString();
                        if (!str.Contains('.'))
                        {
                            str += ".0";
                        }
                        this.Write(str);
                        break;
                    case TypeCode.DateTime:
                        this.Write("'");
                        this.Write((DateTime.Parse(node.Value.ToString())).ToString("yyyy-MM-dd HH:mm:ss"));
                        this.Write("'");
                        break;
                    case TypeCode.String:
                        this.Write("'");
                        this.Write(node.Value.ToString().Replace("'","\""));
                        this.Write("'");
                        break;
                    default:
                        this.Write(node.Value.ToString());
                        break;
                }
            }
            return node;
        }

        /// <summary>
        /// 集合元素的初化
        /// </summary>
        /// <param name="node">元素集合</param>
        /// <returns></returns>
        protected override ElementInit VisitElementInit(ElementInit node)
        {
            if (node.Arguments.Count > 1)
            {
                this.Write("{");
                for (int i = 0; i < node.Arguments.Count; i++)
                {
                    this.Visit(node.Arguments[i]);
                    if (i < node.Arguments.Count - 1)
                    {
                        this.Write(", ");
                    }
                }
                this.Write("}");
            }
            else {
                this.Visit(node.Arguments[0]);

            }
            return node;
        }

        /// <summary>
        /// 方法调用
        /// </summary>
        /// <param name="node">方法表达式</param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            string MethodName = node.Method.Name.ToLower();
            switch (MethodName)
            {
                case "where":
                    if (ReadSqlType == ExpressionSqlType.Where)
                    {
                        WhereTime = WhereTime + 1;
                    }
                    else
                    {
                        this.Visit(node.Arguments[0]);
                        return node;
                    }
                    break;
                case "orderby":
                case "orderbydescending":
                case "thenbydescending":
                case "thenby":
                    if (ReadSqlType == ExpressionSqlType.Order)
                    {
                        OrderTime = OrderTime + 1;
                    }
                    else
                    {
                        this.Visit(node.Arguments[0]);
                        return node;
                    }
                    break;
            }

            #region

            if (node.Method.DeclaringType == typeof(string))
            {
                switch (node.Method.Name)
                {
                    case "StartsWith":
                        this.Write("(");
                        this.Visit(node.Object);
                        this.Write(" LIKE ");
                        this.Visit(node.Arguments[0]);
                        this.Write(" + '%')");
                        return  node;
                    case "EndsWith":
                        this.Write("(");
                        this.Visit(node.Object);
                        this.Write(" LIKE '%' + ");
                        this.Visit(node.Arguments[0]);
                        this.Write(")");
                        return  node;
                    case "Contains":
                        this.Write("(");
                        this.Visit(node.Object);
                        this.Write(" LIKE '%' + ");
                        this.Visit(node.Arguments[0]);
                        this.Write(" + '%')");
                        return  node;
                    case "Concat":
                        IList<Expression> args = node.Arguments;
                        if (args.Count == 1 && args[0].NodeType == ExpressionType.NewArrayInit)
                        {
                            args = ((NewArrayExpression)args[0]).Expressions;
                        }
                        for (int i = 0, n = args.Count; i < n; i++)
                        {
                            if (i > 0) this.Write(" + ");
                            this.Visit(args[i]);
                        }
                        return  node;
                    case "IsNullOrEmpty":
                        this.Write("(");
                        this.Visit(node.Arguments[0]);
                        this.Write("  IS NULL OR ");
                        this.Visit(node.Arguments[0]);
                        this.Write(" = '')");
                        return  node;
                    case "IsNullOrWhiteSpace":
                        this.Write("(");
                        this.Visit(node.Arguments[0]);
                        this.Write("  IS NULL OR ");
                        this.Write("RTRIM(");
                        this.Visit(node.Arguments[0]);
                        this.Write(") = '')");
                        return  node;
                    case "ToUpper":
                        this.Write("UPPER(");
                        this.Visit(node.Object);
                        this.Write(")");
                        return  node;
                    case "ToLower":
                        this.Write("LOWER(");
                        this.Visit(node.Object);
                        this.Write(")");
                        return  node;
                    case "Replace":
                        this.Write("REPLACE(");
                        this.Visit(node.Object);
                        this.Write(", ");
                        this.Visit(node.Arguments[0]);
                        this.Write(", ");
                        this.Visit(node.Arguments[1]);
                        this.Write(")");
                        return  node;
                    case "Substring":
                        this.Write("SUBSTRING(");
                        this.Visit(node.Object);
                        this.Write(", ");
                        this.Visit(node.Arguments[0]);
                        this.Write(" + 1, ");
                        if (node.Arguments.Count == 2)
                        {
                            this.Visit(node.Arguments[1]);
                        }
                        else
                        {
                            this.Write("8000");
                        }
                        this.Write(")");
                        return  node;
                    case "Remove":
                        this.Write("STUFF(");
                        this.Visit(node.Object);
                        this.Write(", ");
                        this.Visit(node.Arguments[0]);
                        this.Write(" + 1, ");
                        if (node.Arguments.Count == 2)
                        {
                            this.Visit(node.Arguments[1]);
                        }
                        else
                        {
                            this.Write("8000");
                        }
                        this.Write(", '')");
                        return  node;
                    case "IndexOf":
                        this.Write("(CHARINDEX(");
                        this.Visit(node.Arguments[0]);
                        this.Write(", ");
                        this.Visit(node.Object);
                        if (node.Arguments.Count == 2 && node.Arguments[1].Type == typeof(int))
                        {
                            this.Write(", ");
                            this.Visit(node.Arguments[1]);
                            this.Write(" + 1");
                        }
                        this.Write(") - 1)");
                        return  node;
                    case "Trim":
                        this.Write("RTRIM(LTRIM(");
                        this.Visit(node.Object);
                        this.Write("))");
                        return  node;
                }
            }
            else if (node.Method.DeclaringType == typeof(DateTime))
            {
                switch (node.Method.Name)
                {
                    case "op_Subtract":
                        if (node.Arguments[1].Type == typeof(DateTime))
                        {
                            this.Write("DATEDIFF(");
                            this.Visit(node.Arguments[0]);
                            this.Write(", ");
                            this.Visit(node.Arguments[1]);
                            this.Write(")");
                            return  node;
                        }
                        break;
                    case "AddYears":
                        this.Write("DATEADD(YYYY,");
                        this.Visit(node.Arguments[0]);
                        this.Write(",");
                        this.Visit(node.Object);
                        this.Write(")");
                        return  node;
                    case "AddMonths":
                        this.Write("DATEADD(MM,");
                        this.Visit(node.Arguments[0]);
                        this.Write(",");
                        this.Visit(node.Object);
                        this.Write(")");
                        return  node;
                    case "AddDays":
                        this.Write("DATEADD(DAY,");
                        this.Visit(node.Arguments[0]);
                        this.Write(",");
                        this.Visit(node.Object);
                        this.Write(")");
                        return  node;
                    case "AddHours":
                        this.Write("DATEADD(HH,");
                        this.Visit(node.Arguments[0]);
                        this.Write(",");
                        this.Visit(node.Object);
                        this.Write(")");
                        return  node;
                    case "AddMinutes":
                        this.Write("DATEADD(MI,");
                        this.Visit(node.Arguments[0]);
                        this.Write(",");
                        this.Visit(node.Object);
                        this.Write(")");
                        return  node;
                    case "AddSeconds":
                        this.Write("DATEADD(SS,");
                        this.Visit(node.Arguments[0]);
                        this.Write(",");
                        this.Visit(node.Object);
                        this.Write(")");
                        return  node;
                    case "AddMilliseconds":
                        this.Write("DATEADD(MS,");
                        this.Visit(node.Arguments[0]);
                        this.Write(",");
                        this.Visit(node.Object);
                        this.Write(")");
                        return  node;
                }
            }
            else if (node.Method.DeclaringType == typeof(Decimal))
            {
                switch (node.Method.Name)
                {
                    case "Add":
                    case "Subtract":
                    case "Multiply":
                    case "Divide":
                    case "Remainder":
                        this.Write("(");
                        this.VisitValue(node.Arguments[0]);
                        this.Write(" ");
                        this.Write(GetOperator(node.Method.Name));
                        this.Write(" ");
                        this.VisitValue(node.Arguments[1]);
                        this.Write(")");
                        return  node;
                    case "Negate":
                        this.Write("-");
                        this.Visit(node.Arguments[0]);
                        this.Write("");
                        return  node;
                    case "Ceiling":
                    case "Floor":
                        this.Write(node.Method.Name.ToUpper());
                        this.Write("(");
                        this.Visit(node.Arguments[0]);
                        this.Write(")");
                        return  node;
                    case "Round":
                        if (node.Arguments.Count == 1)
                        {
                            this.Write("ROUND(");
                            this.Visit(node.Arguments[0]);
                            this.Write(", 0)");
                            return  node;
                        }
                        else if (node.Arguments.Count == 2 && node.Arguments[1].Type == typeof(int))
                        {
                            this.Write("ROUND(");
                            this.Visit(node.Arguments[0]);
                            this.Write(", ");
                            this.Visit(node.Arguments[1]);
                            this.Write(")");
                            return  node;
                        }
                        break;
                    case "Truncate":
                        this.Write("ROUND(");
                        this.Visit(node.Arguments[0]);
                        this.Write(", 0, 1)");
                        return  node;
                }
            }
            else if (node.Method.DeclaringType == typeof(Math))
            {
                switch (node.Method.Name)
                {
                    case "Abs":
                    case "Acos":
                    case "Asin":
                    case "Atan":
                    case "Cos":
                    case "Exp":
                    case "Log10":
                    case "Sin":
                    case "Tan":
                    case "Sqrt":
                    case "Sign":
                    case "Ceiling":
                    case "Floor":
                        this.Write(node.Method.Name.ToUpper());
                        this.Write("(");
                        this.Visit(node.Arguments[0]);
                        this.Write(")");
                        return  node;
                    case "Atan2":
                        this.Write("ATN2(");
                        this.Visit(node.Arguments[0]);
                        this.Write(", ");
                        this.Visit(node.Arguments[1]);
                        this.Write(")");
                        return  node;
                    case "Log":
                        if (node.Arguments.Count == 1)
                        {
                            goto case "Log10";
                        }
                        break;
                    case "Pow":
                        this.Write("POWER(");
                        this.Visit(node.Arguments[0]);
                        this.Write(", ");
                        this.Visit(node.Arguments[1]);
                        this.Write(")");
                        return  node;
                    case "Round":
                        if (node.Arguments.Count == 1)
                        {
                            this.Write("ROUND(");
                            this.Visit(node.Arguments[0]);
                            this.Write(", 0)");
                            return  node;
                        }
                        else if (node.Arguments.Count == 2 && node.Arguments[1].Type == typeof(int))
                        {
                            this.Write("ROUND(");
                            this.Visit(node.Arguments[0]);
                            this.Write(", ");
                            this.Visit(node.Arguments[1]);
                            this.Write(")");
                            return  node;
                        }
                        break;
                    case "Truncate":
                        this.Write("ROUND(");
                        this.Visit(node.Arguments[0]);
                        this.Write(", 0, 1)");
                        return  node;
                }
            }

            if (node.Method.Name == "ToString")
            {
                if (node.Object.Type != typeof(string))
                {
                    this.Write("CONVERT(NVARCHAR, ");
                    this.Visit(node.Object);
                    this.Write(")");
                }
                else
                {
                    this.Visit(node.Object);
                }
                return  node;
            }
            else if (!node.Method.IsStatic && node.Method.Name == "CompareTo" && node.Method.ReturnType == typeof(int) && node.Arguments.Count == 1)
            {
                this.Write("(CASE WHEN ");
                this.Visit(node.Object);
                this.Write(" = ");
                this.Visit(node.Arguments[0]);
                this.Write(" THEN 0 WHEN ");
                this.Visit(node.Object);
                this.Write(" < ");
                this.Visit(node.Arguments[0]);
                this.Write(" THEN -1 ELSE 1 END)");
                return  node;
            }
            else if (node.Method.IsStatic && node.Method.Name == "Compare" && node.Method.ReturnType == typeof(int) && node.Arguments.Count == 2)
            {
                this.Write("(CASE WHEN ");
                this.Visit(node.Arguments[0]);
                this.Write(" = ");
                this.Visit(node.Arguments[1]);
                this.Write(" THEN 0 WHEN ");
                this.Visit(node.Arguments[0]);
                this.Write(" < ");
                this.Visit(node.Arguments[1]);
                this.Write(" THEN -1 ELSE 1 END)");
                return  node;
            }
            #endregion

            if (node.Arguments.Count > 1)
            {
                this.WriteLine(Indentation.Outer);
            }
            base.VisitMethodCall(node);
            if (node.Arguments.Count > 1)
            {
                switch (MethodName)
                {
                    case "orderbydescending":
                    case "thenbydescending":
                        OrderResult = OrderResult + " Desc"; 
                        break;

                }
                if (ReadSqlType == ExpressionSqlType.Order)
                {
                    OrderResult = OrderResult + ",";
                }
                else
                {

                    WhereResult = WhereResult + " and ";


                }
                this.WriteLine(Indentation.Outer);
            }

            return  node;



        }

        /// <summary>
        /// 生成新的节点
        /// </summary>
        /// <param name="node">构造函数调用</param>
        /// <returns></returns>
        protected override Expression VisitNew(NewExpression node)
        {
            if (node.Constructor.DeclaringType == typeof(DateTime))
            {
                if (node.Arguments.Count == 3)
                {
                    this.Write("Convert(DateTime, ");
                    this.Write("Convert(nvarchar, ");
                    this.Visit(node.Arguments[0]);
                    this.Write(") + '/' + ");
                    this.Write("Convert(nvarchar, ");
                    this.Visit(node.Arguments[1]);
                    this.Write(") + '/' + ");
                    this.Write("Convert(nvarchar, ");
                    this.Visit(node.Arguments[2]);
                    this.Write("))");
                    return node;
                }
                else if (node.Arguments.Count == 6)
                {
                    this.Write("Convert(DateTime, ");
                    this.Write("Convert(nvarchar, ");
                    this.Visit(node.Arguments[0]);
                    this.Write(") + '/' + ");
                    this.Write("Convert(nvarchar, ");
                    this.Visit(node.Arguments[1]);
                    this.Write(") + '/' + ");
                    this.Write("Convert(nvarchar, ");
                    this.Visit(node.Arguments[2]);
                    this.Write(") + ' ' + ");
                    this.Write("Convert(nvarchar, ");
                    this.Visit(node.Arguments[3]);
                    this.Write(") + ':' + ");
                    this.Write("Convert(nvarchar, ");
                    this.Visit(node.Arguments[4]);
                    this.Write(") + ':' + ");
                    this.Write("Convert(nvarchar, ");
                    this.Visit(node.Arguments[5]);
                    this.Write("))");
                    return node;
                }
            }
            return base.VisitNew(node);
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            //this.Write("new ");
            //this.Write(this.GetTypeName(AtkTypeHelper.GetElementType(na.Type)));
            //this.Write("[] {");
            //if (node.Expressions.Count > 1)
            //    this.WriteLine(Indentation.Inner);
            //this.VisitExpressionList(node.Expressions);
            //if (node.Expressions.Count > 1)
            //    this.WriteLine(Indentation.Outer);
            //this.Write("}");
            return node;
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }


        #endregion

        #region 公用方法


        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        protected virtual string GetTypeName(Type type)
        {
            string name = type.Name;
            name = name.Replace("+", ".");
            int iGeneneric = name.IndexOf('`');
            if (iGeneneric > 0)
            {
                name = name.Substring(0, iGeneneric);
            }
            if (type.IsGenericType || type.IsGenericTypeDefinition)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(name);
                sb.Append("<");
                var args = type.GetGenericArguments();
                for (int i = 0; i < args.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    if (type.IsGenericType)
                    {
                        sb.Append(this.GetTypeName(args[i]));
                    }
                }
                sb.Append(">");
                name = sb.ToString();
            }
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expr">表达式树</param>
        /// <returns></returns>
        protected Expression VisitValue(Expression expr)
        {
            if (IsPredicate(expr))
            {
                this.Write("CASE WHEN(");
                this.Visit(expr);
                this.Write(") THEN 1 ELSE 0 END");
            }
            return expr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expr">表达式树</param>
        /// <returns></returns>
        protected virtual bool IsPredicate(Expression expr)
        {
            switch (expr.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return IsBoolean(((BinaryExpression)expr).Type);
                case ExpressionType.Not:
                    return IsBoolean(((UnaryExpression)expr).Type);
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case (ExpressionType)DBExpressionType.IsNull:
                case (ExpressionType)DBExpressionType.Between:
                case (ExpressionType)DBExpressionType.Exists:
                case (ExpressionType)DBExpressionType.In:
                    return true;
                case ExpressionType.Call:
                    return IsBoolean(((MethodCallExpression)expr).Type);
                default:
                    return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">文件内容</param>
        protected void Write(string text)
        {
            switch (ReadSqlType)
            {
                case ExpressionSqlType.Where:WhereResult = WhereResult + text;break;
                case ExpressionSqlType.Order:OrderResult = OrderResult + text;break;
            }
           this.StringWriter.Write(text);
        }

        private void Indent(Indentation Style)
        {
            if (Style == Indentation.Inner)
            {
             // this.depth++;
            }
            else if (Style == Indentation.Outer)
            {
                // this.depth--;
                System.Diagnostics.Debug.Assert(this.depth > 0);
            }
        }

        /// <summary>
        /// 写入行
        /// </summary>
        /// <param name="style"> 样式</param>
        private void WriteLine(Indentation style)
        {
            this.StringWriter.WriteLine();
        }

        /// <summary>
        /// 检查数据类型是否为bool
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        protected virtual  bool IsBoolean(Type type)
        {
           return type == typeof(bool) || type == typeof(bool?);
        }
         
        #endregion

        #region  或取SQL操作方法



        /// <summary>
        /// 获取SQL操作方法
        /// </summary>
        /// <param name="type">表达试目录树节点类型</param>
        /// <returns></returns>
        protected virtual string GetOperator(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Not:
                    return " NOT ";//"!";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return "+";
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return "-";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return "*";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.And:
                    return "&";
                case ExpressionType.AndAlso:
                    return "and";
                case ExpressionType.Or:
                    return "Or";
                case ExpressionType.OrElse:
                    return "Or";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "!=";
                case ExpressionType.Coalesce:
                    return "??";
                case ExpressionType.RightShift:
                    return ">>";
                case ExpressionType.LeftShift:
                    return "<<";
                case ExpressionType.ExclusiveOr:
                    return "^";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取SQL操作方法
        /// </summary>
        /// <param name="methodName">方法名称</param>
        /// <returns></returns>
        protected virtual string GetOperator(string methodName)
        {
            switch (methodName)
            {
                case "Add": return "+";
                case "Subtract": return "-";
                case "Multiply": return "*";
                case "Divide": return "/";
                case "Negate": return "-";
                case "Remainder": return "%";
                default: return null;
            }
        }

        /// <summary>
        /// 获取SQL操作方法
        /// </summary>
        /// <param name="b">二元表达式</param>
        /// <returns></returns>
        protected virtual string GetOperator(BinaryExpression b)
        {
            switch (b.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return (IsBoolean(b.Left.Type)) ? "AND" : "&";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return (IsBoolean(b.Left.Type) ? "OR" : "|");
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return "+";
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return "-";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return "*";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.ExclusiveOr:
                    return "^";
                case ExpressionType.LeftShift:
                    return "<<";
                case ExpressionType.RightShift:
                    return ">>";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取SQL操作方法
        /// </summary>
        /// <param name="b">一元表达式</param>
        /// <returns></returns>
        protected virtual string GetOperator(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                    return "-";
                case ExpressionType.UnaryPlus:
                    return "+";
                case ExpressionType.Not:
                    return IsBoolean(u.Operand.Type) ? "NOT" : "~";
                default:
                    return "";
            }
        }

        #endregion
    }
}
