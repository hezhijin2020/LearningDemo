using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearningDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            HZJ.ORM.BuildSQLString<HZJ.Model.ACL_User> orm = new HZJ.ORM.BuildSQLString<HZJ.Model.ACL_User>();
            StringBuilder builder = new StringBuilder();
            //builder.Append(orm.GetSelectString()+"\r\n");
            //builder.Append(orm.GetInsertString() + "\r\n");
            //builder.Append(orm.GetUpdateString() + "\r\n");
            //builder.Append(orm.GetDeleteString(Guid.Empty) + "\r\n");

            var nam = "woshihaoren";
            Expression<Func<HZJ.Model.ACL_User, bool>> Expr = a => a.LoginName == nam || a.UserName == "admin" || a.LoginPwd.Contains("1234") && a.IsRemoved == false; ;
             cls.ExpressionVisitorToSQLwhere exprs = new cls.ExpressionVisitorToSQLwhere();
              exprs.Visit(Expr);
            Console.WriteLine(string.Join("",exprs.SQLStack.ToArray()));
              
        }

       
      
    }
}
