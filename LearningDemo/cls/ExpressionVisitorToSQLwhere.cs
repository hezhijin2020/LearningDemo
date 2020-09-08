using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LearningDemo.cls
{
    public  class ExpressionVisitorToSQLwhere:ExpressionVisitor
    {

        public  Stack<string> SQLStack = new Stack<string>();
        int Deep = 0;

        public override Expression Visit(Expression node)
        {
            Deep++;
            Console.WriteLine($"[ Visit ] 深度:{Deep} 节点类型：{node.NodeType} 节点：{node.Type} 内容String：{node.ToString()}");
            return base.Visit(node);
        }

       
        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            Console.WriteLine($"Catchblock");
            return  base.VisitCatchBlock(node);
        }
       
        protected override ElementInit VisitElementInit(ElementInit node)
        {
            Console.WriteLine($"VisitElementInit");
            return base.VisitElementInit(node);
        }
        
        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            Console.WriteLine($"VisitLabelTarget");
            return base.VisitLabelTarget(node);
        }
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            Console.WriteLine($"VisitMemberAssignment");
            return base.VisitMemberAssignment(node);
        }
        
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            return base.VisitMemberBinding(node);
        }
       
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            return base.VisitMemberListBinding(node);
        }
     
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            return base.VisitMemberMemberBinding(node);
        }
        
        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            return base.VisitSwitchCase(node);
        }
       
        protected  override Expression VisitBinary(BinaryExpression node)
        {

            
            Console.WriteLine(@" [ VisitBinary ] 节点类型:{0} 方法:{1} 左边：{2} 右边：{3} " + "\r\n",node.NodeType,node.Method,node.Left,node.Right);
           
            {
                this.SQLStack.Push(")");
                this.Visit(node.Right); //右边递归
                this.SQLStack.Push(node.NodeType.ToSqlOperator());
                this.Visit(node.Left);
                this.SQLStack.Push("(");
                return node;
            }
        }

        
        protected  override Expression VisitBlock(BlockExpression node)
        {
            Console.WriteLine(@"VisitBlock");
            return base.VisitBlock(node);
        }
       
        protected  override Expression VisitConditional(ConditionalExpression node)
        {
            Console.WriteLine(@"VisitConditional");
            return base.VisitConditional(node);
        }
     
        protected  override Expression VisitConstant(ConstantExpression node)
        {
            Console.WriteLine($"Visitconstant:节点类型[{node.NodeType}]-节点【{node.ToString()}】数据类型[{node.Type.Name}]");

            var value = "";
            if (node.Type.IsValueType)
            {
                value = node.Value.ToString();
            }
            else
            {
                value = $"'{node.Value}'";
            }
            this.SQLStack.Push(value);
            return node;
        }
       
        protected  override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            return base.VisitDebugInfo(node);
        }
        
        protected  override Expression VisitDefault(DefaultExpression node)
        {
            return base.VisitDefault(node);
        }
        
        protected  override Expression VisitDynamic(DynamicExpression node)
        {
            return base.VisitDynamic(node);
        }
       

        protected  override Expression VisitExtension(Expression node)
        {
            return base.VisitExtension(node);
        }
       
        protected  override Expression VisitGoto(GotoExpression node)
        {
            return base.VisitGoto(node);
        }
        
        protected  override Expression VisitIndex(IndexExpression node)
        {
            return base.VisitIndex(node);
        }
        
        protected  override Expression VisitInvocation(InvocationExpression node)
        {
            return base.VisitInvocation(node);
        }
      
        protected  override Expression VisitLabel(LabelExpression node)
        {
            return base.VisitLabel(node);
        }
       
        protected  override Expression VisitLambda<T>(Expression<T> node)
        {
            return base.VisitLambda(node);
        }
        
        protected  override Expression VisitListInit(ListInitExpression node)
        {
            return base.VisitListInit(node);
        }
       
        protected  override Expression VisitLoop(LoopExpression node)
        {
            return base.VisitLoop(node);
        }
       
        protected  override Expression VisitMember(MemberExpression node)
        {
            Console.WriteLine($"[ VisitMember ] 节点类型:{node.NodeType} 节点:{node.Type} 内容String:{node.ToString()} Express:{node.Expression}");

            if (node.Expression is ConstantExpression)
            {
                var value1 = this.InvokeValue(node);
                var value2 = this.ReflectionValue(node);
                Console.WriteLine($" is Constant:{node.Expression.ToString()} value1[{value1}]  value2[{value2}]");
            }
            else {
                Console.WriteLine($" Not Constant:{node.Expression.ToString()}");
            }


            this.SQLStack.Push($"{node.Member.Name}");

            //if (node.Expression is ConstantExpression)
            //{
            //    //var value1 = this.InvokeValue(node);
            //    //var value2 = this.ReflectionValue(node);
            //    ////this.ConditionStack.Push($"'{value1}'");
            //    //this._TempValue = value1;
            //}
            //else
            //{
            //    this.SQLStack.Push($"{node.Member.Name}");
            //    //this.ConditionStack.Push($"{node.Member.Name}");
            //    //this.ConditionStack.Push($"{node.Member.GetMappingName()}");//映射数据
            //    //if (this._TempValue != null)
            //    //{
            //    //    string name = node.Member.GetMappingName();
            //    //    string paraName = $"@{name}{this._SqlParameterList.Count}";
            //    //    string sOperator = this.ConditionStack.Pop();
            //    //    this.ConditionStack.Push(paraName);
            //    //    this.ConditionStack.Push(sOperator);
            //    //    this.ConditionStack.Push(name);

            //    //    var tempValue = this._TempValue;
            //    //    this._SqlParameterList.Add(new SqlParameter(paraName, tempValue));
            //    //    this._TempValue = null;
            //    //}
            //}
            return node;
        }


        private object InvokeValue(MemberExpression member)
        {
            var objExp = Expression.Convert(member, typeof(object));//struct需要
            return Expression.Lambda<Func<object>>(objExp).Compile().Invoke();
        }

        private object ReflectionValue(MemberExpression member)
        {
            var obj = (member.Expression as ConstantExpression).Value;
            return (member.Member as System.Reflection.FieldInfo).GetValue(obj);
        }
        protected  override Expression VisitMemberInit(MemberInitExpression node)
        {
            Console.WriteLine($"[VisitMemberInit] {node.NodeType}");
            return base.VisitMemberInit(node);
        }
        
        protected  override Expression VisitMethodCall(MethodCallExpression m)
        {

            Console.WriteLine($"[ VisitMethodCall ] 节点类型:{m.NodeType} 节点:{m.Type} 内容String:{m.ToString()} Method:{m.Method}");
            if (m == null) throw new ArgumentNullException("MethodCallExpression");

            this.Visit(m.Arguments[0]);
            string format;
            switch (m.Method.Name)
            {
                case "StartsWith":
                    format = "({0} LIKE '{1}%')";
                    break;

                case "Contains":
                    format = "({0} LIKE '%{1}%')";
                    break;

                case "EndsWith":
                    format = "({0} LIKE '%{1}')";
                    break;

                default:
                    throw new NotSupportedException(m.NodeType + " is not supported!");
            }
            this.SQLStack.Push(format);
            this.Visit(m.Object);
            string left = this.SQLStack.Pop();
            format = this.SQLStack.Pop();
            string right = this.SQLStack.Pop();
            this.SQLStack.Push(String.Format(format, left, right));

            return m;
        }
        
        protected  override Expression VisitNew(NewExpression node)
        {
            return base.VisitNew(node);
        }
   
        protected  override Expression VisitNewArray(NewArrayExpression node)
        {
            return base.VisitNewArray(node);
        }
       
        protected  override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }
       
        protected  override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            return base.VisitRuntimeVariables(node);
        }
        
        protected  override Expression VisitSwitch(SwitchExpression node)
        {
            return base.VisitSwitch(node);
        }
      
        protected  override Expression VisitTry(TryExpression node)
        {
            return base.VisitTry(node);
        }
       
        protected  override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            return base.VisitTypeBinary(node);
        }
       
        protected  override Expression VisitUnary(UnaryExpression node)
        {
            return base.VisitUnary(node);
        }

    }
}
