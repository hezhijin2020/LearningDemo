namespace HZJ.DxWinForm.MdiForm.pgSystem
{
    partial class DbSettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbSettingForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnConnTest = new DevExpress.XtraEditors.SimpleButton();
            this.tabPageDb = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageSQL = new DevExpress.XtraTab.XtraTabPage();
            this.txtSQLPort = new DevExpress.XtraEditors.TextEdit();
            this.txtSQLPwd = new DevExpress.XtraEditors.TextEdit();
            this.txtSQLUser = new DevExpress.XtraEditors.TextEdit();
            this.txtSQLDbName = new DevExpress.XtraEditors.TextEdit();
            this.txtSQLServer = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tabPageMysql = new DevExpress.XtraTab.XtraTabPage();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit4 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit5 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabPageDb)).BeginInit();
            this.tabPageDb.SuspendLayout();
            this.tabPageSQL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLDbName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLServer.Properties)).BeginInit();
            this.tabPageMysql.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.btnConnTest);
            this.panelControl1.Controls.Add(this.tabPageDb);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnSubmit);
            this.panelControl1.Controls.Add(this.separatorControl1);
            this.panelControl1.Location = new System.Drawing.Point(8, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(347, 279);
            this.panelControl1.TabIndex = 1;
            // 
            // btnConnTest
            // 
            this.btnConnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConnTest.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnConnTest.ImageOptions.Image")));
            this.btnConnTest.Location = new System.Drawing.Point(6, 236);
            this.btnConnTest.Name = "btnConnTest";
            this.btnConnTest.Size = new System.Drawing.Size(86, 35);
            this.btnConnTest.TabIndex = 5;
            this.btnConnTest.Text = "测试连接";
            this.btnConnTest.Click += new System.EventHandler(this.btnConnTest_Click);
            // 
            // tabPageDb
            // 
            this.tabPageDb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPageDb.Location = new System.Drawing.Point(5, 7);
            this.tabPageDb.Name = "tabPageDb";
            this.tabPageDb.SelectedTabPage = this.tabPageSQL;
            this.tabPageDb.Size = new System.Drawing.Size(329, 207);
            this.tabPageDb.TabIndex = 4;
            this.tabPageDb.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageSQL,
            this.tabPageMysql});
            // 
            // tabPageSQL
            // 
            this.tabPageSQL.Controls.Add(this.txtSQLPort);
            this.tabPageSQL.Controls.Add(this.txtSQLPwd);
            this.tabPageSQL.Controls.Add(this.txtSQLUser);
            this.tabPageSQL.Controls.Add(this.txtSQLDbName);
            this.tabPageSQL.Controls.Add(this.txtSQLServer);
            this.tabPageSQL.Controls.Add(this.labelControl5);
            this.tabPageSQL.Controls.Add(this.labelControl4);
            this.tabPageSQL.Controls.Add(this.labelControl3);
            this.tabPageSQL.Controls.Add(this.labelControl2);
            this.tabPageSQL.Controls.Add(this.labelControl1);
            this.tabPageSQL.Name = "tabPageSQL";
            this.tabPageSQL.Size = new System.Drawing.Size(327, 181);
            this.tabPageSQL.Text = "SQL数据库";
            // 
            // txtSQLPort
            // 
            this.txtSQLPort.EditValue = "1433";
            this.txtSQLPort.Enabled = false;
            this.txtSQLPort.Location = new System.Drawing.Point(85, 140);
            this.txtSQLPort.Name = "txtSQLPort";
            this.txtSQLPort.Properties.MaxLength = 20;
            this.txtSQLPort.Size = new System.Drawing.Size(200, 20);
            this.txtSQLPort.TabIndex = 9;
            // 
            // txtSQLPwd
            // 
            this.txtSQLPwd.EditValue = "yishion";
            this.txtSQLPwd.Location = new System.Drawing.Point(85, 110);
            this.txtSQLPwd.Name = "txtSQLPwd";
            this.txtSQLPwd.Properties.MaxLength = 20;
            this.txtSQLPwd.Properties.PasswordChar = '*';
            this.txtSQLPwd.Size = new System.Drawing.Size(200, 20);
            this.txtSQLPwd.TabIndex = 8;
            // 
            // txtSQLUser
            // 
            this.txtSQLUser.EditValue = "sa";
            this.txtSQLUser.Location = new System.Drawing.Point(85, 80);
            this.txtSQLUser.Name = "txtSQLUser";
            this.txtSQLUser.Properties.MaxLength = 20;
            this.txtSQLUser.Size = new System.Drawing.Size(200, 20);
            this.txtSQLUser.TabIndex = 7;
            // 
            // txtSQLDbName
            // 
            this.txtSQLDbName.EditValue = "RightingSys";
            this.txtSQLDbName.Location = new System.Drawing.Point(85, 50);
            this.txtSQLDbName.Name = "txtSQLDbName";
            this.txtSQLDbName.Properties.MaxLength = 20;
            this.txtSQLDbName.Size = new System.Drawing.Size(200, 20);
            this.txtSQLDbName.TabIndex = 6;
            // 
            // txtSQLServer
            // 
            this.txtSQLServer.EditValue = ".";
            this.txtSQLServer.Location = new System.Drawing.Point(85, 20);
            this.txtSQLServer.Name = "txtSQLServer";
            this.txtSQLServer.Properties.MaxLength = 20;
            this.txtSQLServer.Size = new System.Drawing.Size(200, 20);
            this.txtSQLServer.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Enabled = false;
            this.labelControl5.Location = new System.Drawing.Point(54, 143);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "端口";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(54, 113);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "密码";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(42, 83);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "用户名";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(42, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "数据库";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(42, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "服务器";
            // 
            // tabPageMysql
            // 
            this.tabPageMysql.Controls.Add(this.textEdit1);
            this.tabPageMysql.Controls.Add(this.textEdit2);
            this.tabPageMysql.Controls.Add(this.textEdit3);
            this.tabPageMysql.Controls.Add(this.textEdit4);
            this.tabPageMysql.Controls.Add(this.textEdit5);
            this.tabPageMysql.Controls.Add(this.labelControl6);
            this.tabPageMysql.Controls.Add(this.labelControl7);
            this.tabPageMysql.Controls.Add(this.labelControl8);
            this.tabPageMysql.Controls.Add(this.labelControl9);
            this.tabPageMysql.Controls.Add(this.labelControl10);
            this.tabPageMysql.Name = "tabPageMysql";
            this.tabPageMysql.PageEnabled = false;
            this.tabPageMysql.Size = new System.Drawing.Size(327, 181);
            this.tabPageMysql.Text = "MYSQL数据库";
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "3306";
            this.textEdit1.Enabled = false;
            this.textEdit1.Location = new System.Drawing.Point(85, 140);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(200, 20);
            this.textEdit1.TabIndex = 19;
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(85, 110);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.PasswordChar = '*';
            this.textEdit2.Size = new System.Drawing.Size(200, 20);
            this.textEdit2.TabIndex = 18;
            // 
            // textEdit3
            // 
            this.textEdit3.Location = new System.Drawing.Point(85, 80);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(200, 20);
            this.textEdit3.TabIndex = 17;
            // 
            // textEdit4
            // 
            this.textEdit4.Location = new System.Drawing.Point(85, 50);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Size = new System.Drawing.Size(200, 20);
            this.textEdit4.TabIndex = 16;
            // 
            // textEdit5
            // 
            this.textEdit5.Location = new System.Drawing.Point(85, 20);
            this.textEdit5.Name = "textEdit5";
            this.textEdit5.Size = new System.Drawing.Size(200, 20);
            this.textEdit5.TabIndex = 15;
            // 
            // labelControl6
            // 
            this.labelControl6.Enabled = false;
            this.labelControl6.Location = new System.Drawing.Point(54, 143);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 14);
            this.labelControl6.TabIndex = 14;
            this.labelControl6.Text = "端口";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(54, 113);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 14);
            this.labelControl7.TabIndex = 13;
            this.labelControl7.Text = "密码";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(42, 83);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(36, 14);
            this.labelControl8.TabIndex = 12;
            this.labelControl8.Text = "用户名";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(42, 53);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(36, 14);
            this.labelControl9.TabIndex = 11;
            this.labelControl9.Text = "数据库";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(42, 23);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(36, 14);
            this.labelControl10.TabIndex = 10;
            this.labelControl10.Text = "服务器";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(249, 236);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSubmit.ImageOptions.Image")));
            this.btnSubmit.Location = new System.Drawing.Point(151, 236);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(86, 35);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // separatorControl1
            // 
            this.separatorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorControl1.Location = new System.Drawing.Point(5, 219);
            this.separatorControl1.Margin = new System.Windows.Forms.Padding(2);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(3);
            this.separatorControl1.Size = new System.Drawing.Size(335, 12);
            this.separatorControl1.TabIndex = 1;
            // 
            // DbSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 290);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DbSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "指定数据库";
            this.Load += new System.EventHandler(this.DbSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabPageDb)).EndInit();
            this.tabPageDb.ResumeLayout(false);
            this.tabPageSQL.ResumeLayout(false);
            this.tabPageSQL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLDbName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLServer.Properties)).EndInit();
            this.tabPageMysql.ResumeLayout(false);
            this.tabPageMysql.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSubmit;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraTab.XtraTabControl tabPageDb;
        private DevExpress.XtraTab.XtraTabPage tabPageSQL;
        private DevExpress.XtraTab.XtraTabPage tabPageMysql;
        private DevExpress.XtraEditors.SimpleButton btnConnTest;
        private DevExpress.XtraEditors.TextEdit txtSQLPort;
        private DevExpress.XtraEditors.TextEdit txtSQLPwd;
        private DevExpress.XtraEditors.TextEdit txtSQLUser;
        private DevExpress.XtraEditors.TextEdit txtSQLDbName;
        private DevExpress.XtraEditors.TextEdit txtSQLServer;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraEditors.TextEdit textEdit4;
        private DevExpress.XtraEditors.TextEdit textEdit5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
    }
}