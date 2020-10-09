using DevExpress.XtraEditors;
using HZJ.DxWinForm.Utility.CommCls;
using HZJ.DxWinForm.Utility.vwModels;
using System;
using System.Windows.Forms;

namespace HZJ.DxWinForm.MdiForm.pgSystem
{
    public partial class DbSettingForm : XtraForm
    {

        private DbSettingInfo SQLSetting;

        public DbSettingForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存数据库连接设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, System.EventArgs e)
        {
            if (this.Save())
            {
                DxPublic.ShowMessage("保存成功！");
                Application.ExitThread();
                Application.Exit();
            }
            else {
                DxPublic.ShowMessage("保存失败！");
            }
        }

        /// <summary>
        /// 数据库连接测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnTest_Click(object sender, System.EventArgs e)
        {
            if (this.Save())
            {
                if (Global._SqlDb.IsConnectionTest(SQLSetting.GetConnString()))
                {
                    DxPublic.ShowMessage("数据库连接成功！");
                    Application.ExitThread();
                    Application.Exit();
                }
                else
                {
                    DxPublic.ShowMessage("数据库连接失败！");
                }
            }
            else {
                DxPublic.ShowMessage("数据库连接失败！");
            }
        }

        private void DbSettingForm_Load(object sender, System.EventArgs e)
        {
            Inital();
        }

        //数据库初始化
        public void Inital()
        {
            try {
                string Sqlconn = AppSetingHelper.GetConnectionString("SqlConnStringMain");
                SQLSetting = new DbSettingInfo(Sqlconn);
                SQLSetting.DBconnStrType = ConnStrType.SQL;
                this.txtSQLServer.Text = SQLSetting.DBServer;
                this.txtSQLDbName.Text = SQLSetting.DBName;
                this.txtSQLUser.Text = SQLSetting.DBUser;
                this.txtSQLPwd.Text = SQLSetting.DBUserPwd;
                // this.txtSQLPort.Text   = SQLSetting.DBPort;
            }
            catch(Exception ex)
            {
                DxPublic.ShowException(ex, Text);
            }
        }

        /// <summary>
        /// 保存数据设置
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            try
            {
                if (txtSQLServer.Text.Trim() == "" && txtSQLUser.Text.Trim() == "" && txtSQLPwd.Text.Trim() == "" && txtSQLDbName.Text.Trim() == "")
                {
                    DxPublic.ShowMessage("输入信息不能为空");
                    return false;
                }
                SQLSetting.DBServer = this.txtSQLServer.Text.Trim();
                SQLSetting.DBName = this.txtSQLDbName.Text.Trim();
                SQLSetting.DBUser = this.txtSQLUser.Text.Trim();
                SQLSetting.DBUserPwd = this.txtSQLPwd.Text.Trim();
                SQLSetting.DBPort = this.txtSQLPort.Text.Trim();
                AppSetingHelper.SetConnectionString("SqlConnStringMain", SQLSetting.GetConnString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
