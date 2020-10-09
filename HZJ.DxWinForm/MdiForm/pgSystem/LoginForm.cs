using HZJ.DxWinForm.Utility.CommCls;
using HZJ.DxWinForm.Utility.vwModels;
using System;
using System.Data;
using System.Windows.Forms;

namespace HZJ.DxWinForm.MdiForm.pgSystem
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
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
                AppSetingHelper.SetLoginInfo(
                    new Utility.vwModels.LoginInfo
                    {
                        LoginName = Global._Session._LoginName,
                        LoginPwd = Global._Session._LoginPwd,
                        IsRberPwd = Global._Session._IsRberPwd
                    });
                base.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <returns></returns>
        private bool doLogin()
        {
            string LoginName = this.txtLoginName.Text.Trim();
            string LoginPwd = this.txtLoginPwd.Text.Trim();
            try
            {
                DataTable dataTable = Global._AppRight.GetUserInfo(LoginName, LoginPwd);
                if (dataTable == null || dataTable.Rows.Count < 1)
                {
                    DxPublic.ShowMessage("用户或密码错误！", this.Text);
                    return false;
                }

                Global._Session._IsRberPwd = CheckRemPwd.Checked;
                Global._Session._LoginPwd = LoginPwd;
                Global._Session._LoginName = LoginName;

                DataRow dataRow = dataTable.Rows[0];
                Global._Session._UserId = DxPublic.GetObjGUID(dataRow["Id"]);
                Global._Session._FullName = DxPublic.GetObjString(dataRow["FullName"]);
                Global._Session._DepartmentId = DxPublic.GetObjGUID(dataRow["DepartmentId"]);
                Global._Session._DepartmentName = DxPublic.GetObjString(dataRow["DepartmentName"]);

                return true;
            }
            catch (Exception ex)
            {
                DxPublic.ShowMessage($"用户登录出错：{ex.Message}", this.Text);
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
            LoginInfo info=  AppSetingHelper.GetLoginInfo();//读取用户信息

            //设置读取的用户信息
            txtLoginName.Text = info.LoginName;
            CheckRemPwd.Checked = info.IsRberPwd;
            txtLoginPwd.Text = info.LoginPwd;
        }
    }
}


