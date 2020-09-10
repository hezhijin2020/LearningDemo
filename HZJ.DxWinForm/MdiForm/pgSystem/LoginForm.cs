using HZJ.CommonCls;
using HZJ.DxWinForm.Utility.ClsCommon;
using System;
using System.Data;
using System.Windows.Forms;

namespace HZJ.DxWinForm.MdiForm.pgSystem
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="appRight">权限业务类</param>
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        public void SetFocus()
        {
            if (this.txtLoginName.Text == "")
            {
                this.txtLoginName.Focus();
                return;
            }
            this.txtLoginPwd.Focus();
        }

        /// <summary>
        /// 取消登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (doLogin())
            {
                Global._AppRight.SaveLoginConfig();
                base.DialogResult = DialogResult.OK;
            }
        }


        /// <summary>
        /// 登录方法
        /// </summary>
        /// <returns></returns>
        private bool doLogin()
        {
            string text = this.txtLoginName.Text.Trim();
            string text2 = this.txtLoginPwd.Text.Trim();
            try
            {
                DataTable dataTable = Global._AppRight.GetUserInfo(text, text2);
                if (dataTable == null || dataTable.Rows.Count < 1)
                {
                    clsPublic.ShowMessage("用户和密码错误", this.Text);
                    return false;
                }
                Global._Session._IsRemPwd = CheckRemPwd.Checked;
                Global._Session._LoginPwd = text2;

                DataRow dataRow = dataTable.Rows[0];
                Global._Session._UserId = clsPublic.GetObjGUID(dataRow["Id"]);
                Global._Session._LoginName = clsPublic.GetObjectString(dataRow["LoginName"]);
                Global._Session._FullName = clsPublic.GetObjectString(dataRow["FullName"]);
                Global._Session._DepartmentId = clsPublic.GetObjGUID(dataRow["DepartmentId"]);
                Global._Session._DepartmentName = clsPublic.GetObjectString(dataRow["DepartmentName"]);
                return true;
            }
            catch (Exception ex)
            {
                clsPublic.ShowMessage("系统出错,错误信息："+ex.Message, this.Text);
                return false;
            }
        }
        

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FLogin_Load(object sender, EventArgs e)
        {
            Global._AppRight.ReadLoginConfig();//读取用户信息

            //设置读取的用户信息
            txtLoginName.Text =Global._Session._LoginName;
            CheckRemPwd.Checked = Global._Session._IsRemPwd;
            txtLoginPwd.Text = Global._Session._IsRemPwd? Global._Session._LoginPwd:"";
        }
    }
}


