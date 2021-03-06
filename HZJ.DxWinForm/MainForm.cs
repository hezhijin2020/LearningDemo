﻿using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using HZJ.DxWinForm.MdiForm.pgSystem;
using HZJ.DxWinForm.Utility.BaseWinFrom;
using HZJ.DxWinForm.Utility.CommCls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace HZJ.DxWinForm
{
    public partial class MainForm :RibbonForm,IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region  声明变量
        private BaseForm currentForm = null;//子窗体基类、用于存放当前子窗体
        private bool CurrentUserIsAdmin = false;//当前用户是否是管理员
        private bool LoginedIn = false;//用户是否登录
        private int AlertHoldTime = 3;//提示框显示的时长，单位秒
        private static List<Models.ACL_Role_Function> ListUserFuntions = null;//用户权限集
        private bool MenuInited = false;//指示功能菜单的初始化
        private LoginForm LoginForm = null;//登录窗体

        #endregion

        #region 接口成员

        #region  显示子窗体方法

        public void FormShow(XtraForm form)
        {
            form.ShowDialog();
            form.StartPosition = FormStartPosition.CenterParent;
        }
        public void MdiShow(BaseForm frm, object FuncId)
        {
            this.MdiShow(frm, FuncId, false);
        }

        public void MdiShow(BaseForm frm, object FuncId, bool ReStart = false)
        {
            try
            {
                System.Windows.Forms.Form[] mdiChildren = base.MdiChildren;
                for (int i = 0; i < mdiChildren.Length; i++)
                {
                    System.Windows.Forms.Form form = mdiChildren[i];
                    if (!ReStart)
                    {
                        if (form.GetType().Equals(frm.GetType()))
                        {
                            form.Activate();
                            form.Show();
                            frm.Dispose();
                            return;
                        }
                    }
                    else if (form.GetType().Equals(frm.GetType()))
                    {
                        form.Close();
                        form.Dispose();
                    }
                }
                string objectString = DxPublic.GetObjString(FuncId);
                if (!string.IsNullOrEmpty(objectString))
                {
                    frm.FuncId = Guid.Parse(objectString);
                }
                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frm.MdiParent = this;
                frm.KeyPreview = true;
                frm.Mainform = this;
                frm.InitFeatureButton();
                frm.Show();
            }
            catch (System.Exception ex)
            {
                DxPublic.ShowException(ex, this.Text);
            }
        }

        public void MdiShow2(BaseForm frm)
        {
            try
            {
                System.Windows.Forms.Form[] mdiChildren = base.MdiChildren;
                for (int i = 0; i < mdiChildren.Length; i++)
                {
                    System.Windows.Forms.Form form = mdiChildren[i];
                    if (form.GetType().Equals(frm.GetType()))
                    {
                        form.Activate();
                        form.Show();
                        frm.Dispose();
                        return;
                    }
                }
                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frm.MdiParent = this;
                frm.KeyPreview = true;
                frm.Mainform = this;
                frm.InitFeatureButton();
                frm.Show();
            }
            catch (System.Exception ex)
            {
                DxPublic.ShowException(ex, this.Text);
            }
        }
        #endregion

        #region 编辑工具栏方法
        public void PerformAddNew()
        {
            this.btnAddNew_ItemClick(null, null);
        }

        public void PerformDelete()
        {
            this.btnDelete_ItemClick(null, null);
        }

        public void PerformQuery()
        {
            this.btnQuery_ItemClick(null, null);
        }

        public void PerformModify()
        {
            this.btnModify_ItemClick(null, null);
        }

        public void PerformCancel()
        {
            this.btnCancel_ItemClick(null, null);
        }

        public void PerformSave()
        {
            this.btnSave_ItemClick(null, null);
        }

        public void PerformApprove()
        {
            this.btnApprove_ItemClick(null, null);
        }

        public void PerformUnApprove()
        {
            this.btnUnApprove_ItemClick(null, null);
        }

        public void PerformImport()
        {
            this.btnImport_ItemClick(null, null);
        }

        public void PerformExport()
        {
            this.btnExport_ItemClick(null, null);
        }

        public void PerformPreview()
        {
            this.btnPreview_ItemClick(null, null);
        }

        public void PerformPrint()
        {
            this.btnPrint_ItemClick(null, null);
        }

        public void PerformFirst()
        {
            this.btnFirst_ItemClick(null, null);
        }

        public void PerformPrevious()
        {
            this.btnPre_ItemClick(null, null);
        }

        public void PerformNext()
        {
            this.btnNext_ItemClick(null, null);
        }

        public void PerformLast()
        {
            this.btnLast_ItemClick(null, null);
        }

        #endregion

        #region  功能铵钮事件
        private void btnAddNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barAddNew.Enabled)
            {
                this.currentForm.AddNew();
            }
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barDelete.Enabled)
            {
                this.currentForm.Delete();
            }
        }

        private void btnQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barQuery.Enabled)
            {
                this.currentForm.Query();
            }
        }

        private void btnModify_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barModify.Enabled)
            {
                this.currentForm.Modify();
            }
        }

        private void btnCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barCancel.Enabled)
            {
                this.currentForm.Cancel();
            }
        }

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barSave.Enabled)
            {
                this.currentForm.Save();
            }
        }

        private void btnApprove_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barApprove.Enabled)
            {
                this.currentForm.Approve();
            }
        }

        private void btnUnApprove_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barUnApprove.Enabled)
            {
                this.currentForm.UnApprove();
            }
        }

        private void btnImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barImport.Enabled)
            {
                this.currentForm.Import();
            }
        }

        private void btnExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barExport.Enabled)
            {
                this.currentForm.Export();
            }
        }

        private void btnPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barPreview.Enabled)
            {
                this.currentForm.Preview();
            }
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barPrint.Enabled)
            {
                this.currentForm.Print();
            }
        }

        private void btnFirst_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barFirst.Enabled)
            {
                this.currentForm.First();
            }
        }

        private void btnPre_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barPrevious.Enabled)
            {
                this.currentForm.Previous();
            }
        }

        private void btnNext_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barNext.Enabled)
            {
                this.currentForm.Next();
            }
        }

        private void btnLast_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.currentForm != null && this.barLast.Enabled)
            {
                this.currentForm.Last();
            }
        }
        #endregion

        /// <summary>
        /// 禁用所有功能按钮
        /// </summary>
        public void DisableButtons()
        {
            base.Invoke(new EventHandler(delegate
            {
                this.barAddNew.Enabled =
                this.barQuery.Enabled =
                this.barModify.Enabled =
                this.barDelete.Enabled =
                this.barCancel.Enabled =
                this.barSave.Enabled =
                this.barApprove.Enabled =
                this.barUnApprove.Enabled =
                this.barImport.Enabled =
                this.barExport.Enabled =
                this.barPreview.Enabled =
                this.barPrint.Enabled =
                this.barFirst.Enabled =
                this.barPrevious.Enabled =
                this.barNext.Enabled =
                this.barLast.Enabled = false;
            }));
        }

        /// <summary>
        /// 获取MdI子窗体
        /// </summary>
        /// <returns></returns>
        public Form[] GetMDIChildren()
        {
            return this.MdiChildren;
        }

        /// <summary>
        /// 获该用户指定功能的操作权限
        /// </summary>
        /// <param name="FuncId">功能Id</param>
        /// <returns></returns>
        public List<int> GetUserOpCode(Guid FuncId)
        {
            List<int> Opcodes = new List<int>();
            if (MainForm.ListUserFuntions == null)
            {
                this.InitUserFunction(
                    Global._Session._SystemId,
                    Global._Session._UserId,
                    Global._Session._DepartmentId);
            }
            Opcodes = MainForm.ListUserFuntions.FindAll(s => s.FunctionId == FuncId).Select(s => s.OpCode).ToList(); ;

            return Opcodes;
        }

        /// <summary>
        /// 操作功能权限验证
        /// </summary>
        /// <param name="FuncId">功能Id</param>
        /// <returns></returns>
        public bool OperFuncVeify(Guid FuncId)
        {
            if (this.CurrentUserIsAdmin)
            {
                return true;
            }
            if (MainForm.ListUserFuntions == null)
            {
                MainForm.ListUserFuntions = this.InitUserFunction(
                    Global._Session._SystemId,
                    Global._Session._UserId,
                    Global._Session._DepartmentId);
            }
            return MainForm.ListUserFuntions.FirstOrDefault(s => s.FunctionId == FuncId) != null;
        }

        /// <summary>
        /// 根据操作码设置工具栏按钮
        /// </summary>
        /// <param name="code">操作码</param>
        /// <param name="enabled">是否起用</param>
        public void SetButtonEnableByCode(FeatureButton code, bool enabled)
        {
            if (code <= FeatureButton.Import)
            {
                if (code <= FeatureButton.Cancel)
                {
                    switch (code)
                    {
                        case FeatureButton.Add:
                            this.barAddNew.Enabled = enabled;
                            return;
                        case FeatureButton.Query:
                            this.barQuery.Enabled = enabled;
                            return;
                        case (FeatureButton)3:
                            break;
                        case FeatureButton.Modify:
                            this.barModify.Enabled = enabled;
                            return;
                        default:
                            if (code == FeatureButton.Delete)
                            {
                                this.barDelete.Enabled = enabled;
                                return;
                            }
                            if (code != FeatureButton.Cancel)
                            {
                                return;
                            }
                            this.barCancel.Enabled = enabled;
                            return;
                    }
                }
                else if (code <= FeatureButton.Approve)
                {
                    if (code == FeatureButton.Save)
                    {
                        this.barSave.Enabled = enabled;
                        return;
                    }
                    if (code != FeatureButton.Approve)
                    {
                        return;
                    }
                    this.barApprove.Enabled = enabled;
                    return;
                }
                else
                {
                    if (code == FeatureButton.UnApprove)
                    {
                        this.barUnApprove.Enabled = enabled;
                        return;
                    }
                    if (code != FeatureButton.Import)
                    {
                        return;
                    }
                    this.barImport.Enabled = enabled;
                    return;
                }
            }
            else if (code <= FeatureButton.Print)
            {
                if (code == FeatureButton.Export)
                {
                    this.barExport.Enabled = enabled;
                    return;
                }
                if (code == FeatureButton.Preview)
                {
                    this.barPreview.Enabled = enabled;
                    return;
                }
                if (code != FeatureButton.Print)
                {
                    return;
                }
                this.barPrint.Enabled = enabled;
                return;
            }
            else if (code <= FeatureButton.Previous)
            {
                if (code == FeatureButton.First)
                {
                    this.barFirst.Enabled = enabled;
                    return;
                }
                if (code != FeatureButton.Previous)
                {
                    return;
                }
                this.barPrevious.Enabled = enabled;
                return;
            }
            else
            {
                if (code == FeatureButton.Next)
                {
                    this.barNext.Enabled = enabled;
                    return;
                }
                if (code != FeatureButton.Last)
                {
                    return;
                }
                this.barLast.Enabled = enabled;
            }
        }

        /// <summary>
        /// 编辑工具栏是否启用方法
        /// </summary>
        /// <param name="Visible"></param>
        public void SetEditPageVisible(bool Visible)
        {
            this.pageEditor.Visible = Visible;
        }

        /// <summary>
        /// 设置工具栏显式功能按钮
        /// </summary>
        /// <param name="btns">功能按钮集合</param>
        public void SetFeatureButton(params FeatureButton[] btns)
        {
            if (btns != null)
            {
                this.DisableButtons();
                for (int i = 0; i < btns.Length; i++)
                {
                    FeatureButton code = btns[i];
                    this.SetButtonEnableByCode(code, true);
                }
            }
        }

        #region 系统右下角状态栏提示控件 （NotifyIcon）相关方法

        public void ShowAlertInfo(string Title, string Text, ToolTipIcon IconType)
        {
            this.ntyAlert.ShowBalloonTip(this.AlertHoldTime * 1000, Title, Text, IconType);
        }
        public void ShowAlertInfo(string Title, string Text)
        {
            this.ntyAlert.ShowBalloonTip(this.AlertHoldTime * 1000, Title, Text,ToolTipIcon.Info);
        }
        #endregion

        /// <summary>
        /// 没有操作权限
        /// </summary>
        /// <param name="guidFuncId">功能Id</param>
        public void ShowNoRight(string FuncId)
        {
            DxPublic.ShowMessage("权限不足");
        }

        /// <summary>
        /// 没有操作权限
        /// </summary>
        /// <param name="guidFuncId">功能Id</param>
        public void ShowNoRight(string FuncId, FeatureButton opCode)
        {
            if (opCode == FeatureButton.None)
            {
                this.ShowNoRight(FuncId);
                return;
            }
        }

        /// <summary>
        /// 检查用户可用的功能操作权限
        /// </summary>
        /// <param name="guidFuncId">功能ID</param>
        /// <param name="opCode">操作码</param>
        /// <returns></returns>
        public bool UserHasRight(Guid guidFuncId, FeatureButton opCode)
        {
            if (this.CurrentUserIsAdmin)
            {
                return true;
            }

            List<int> Opcodes = this.GetUserOpCode(guidFuncId);

            foreach (int m in Opcodes)
            {
                if (m <= 0)
                {
                    break;
                }
                int ia = (m & (int)opCode);
                bool flag = (m & (int)opCode) == (int)opCode;
                if (!flag && (opCode.Equals(FeatureButton.First) || opCode.Equals(FeatureButton.Last) || opCode.Equals(FeatureButton.Previous) || opCode.Equals(FeatureButton.Next)))
                {
                    int num = 2;
                    flag = ((m & num) == num);
                }
                if (flag)
                {
                    return flag;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查用户可用的功能权限
        /// </summary>
        /// <param name="FuncId">功能ID</param>
        /// <returns></returns>
        public bool UserHasRight(Guid FuncId)
        {
            return this.UserHasRight(FuncId, FeatureButton.None);
        }

        #endregion

        /// <summary>
        /// 子窗体激活事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_MdiChildActivate(object sender, System.EventArgs e)
        {
            this.currentForm = (base.ActiveMdiChild as BaseForm);
            if (this.currentForm == null)
            {
                this.SetEditPageVisible(false);
                return;
            }
            DisableButtons();
            this.currentForm.InitFeatureButton();
            this.SetEditPageVisible(true);
            this.MainRibbon.SelectedPage = this.pageEditor;
        }

        #region 登录方法

        private void doLogin()
        {
            this.Invoke((EventHandler)delegate
            {
                LoginForm = new LoginForm();
                if (this.LoginForm.ShowDialog() !=DialogResult.OK)
                {
                    this.pageEditor.Visible = false;
                    this.pageSystem.Visible = true;
                    this.MainRibbon.SelectedPage = this.pageSystem;
                    this.LoginedIn = false;
                    this.btnUserLogout.Caption = "用户登录";
                    return;
                }
                else
                {
                    this.CloseAllWin();
                    this.btnUserLogout.Caption = "用户注销";
                    this.LoginedIn = true;
                }
                this.Init();
            });
        }

        /// <summary>
        /// 关闭所有子窗体
        /// </summary>
        private void CloseAllWin()
        {
            foreach (BaseForm sub in base.MdiChildren)
            {
                sub.Dispose();
            }
        }

        /// <summary>
        /// 关闭程序前事件 1、写入日志 2、退出所线程
        /// </summary>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }

        /// <summary>
        /// 主窗体加载事件
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitialUserSkin();//初始化主题

            CloseAllWin();//关闭所有子窗体

            Menu_Null();//初始化受权
          
            if (!Global._SqlDb.IsConnectionTest())
            {
                DxPublic.ShowMessage("数据库连接失败，请重新配置", Text);
                this.btnDBConnSetup_ItemClick(null, null);
            }
            else
            {
                if (this.LoginForm != null)
                {
                    this.LoginForm = null;
                }
                try
                {
                    Thread ThLogin = new Thread(doLogin);
                    ThLogin.Start();
                }
                catch (System.Exception ex)
                {
                    DxPublic.ShowException(ex, this.Text);
                }
                finally
                {
                    if (this.LoginForm != null)
                    {
                        this.LoginForm.Dispose();
                        this.LoginForm = null;
                    }
                }
            }
            
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            MainForm.ListUserFuntions = null; //i清空权限
            this.InitUserFunc();//获取权限列表
            this.Menu_Init();//初始化菜单
        }

        /// <summary>
        /// 初始化用户方法
        /// </summary>
        public void InitUserFunc()
        {
            this.InitUserFunction(
                Global._Session._SystemId,
                Global._Session._UserId,
                Global._Session._DepartmentId);
        }

        /// <summary>
        /// 根据系统ID、用户ID、部门Id获取权限列表
        /// </summary>
        /// <param name="pSystemId">系统ID</param>
        /// <param name="pUserId">用户ID</param>
        /// <param name="pDepartmentId">部门Id</param>
        /// <returns></returns>
        public List<Models.ACL_Role_Function> InitUserFunction(Guid pSystemId, Guid pUserId, Guid pDepartmentId)
        {
            MainForm.ListUserFuntions = Global._AppRight.GetUserFunctionList(pSystemId, pUserId, pDepartmentId).ToList();
            return MainForm.ListUserFuntions;
        }

        /// <summary>
        /// 菜单的初始化
        /// </summary>
        private void Menu_Init()
        {
            base.Invoke(new EventHandler(delegate
            {
                if (Global._Session._UserId != null && Global._Session._UserId != Guid.Empty)
                {
                    this.StatusLoginName.Caption = "用户：" + Global._Session._LoginName;
                    this.statusLogintime.Caption = DateTime.Now.ToString("yyyy年MM月dd HH:mm:ss dddd");
                    this.statusIP.Caption = "  登录IP：" + Global._Session._IPAddress;
                    this.statusMac.Caption = "  登录MAC：" + Global._Session._MACAddress;
                    this.statusFullName.Caption = "  真实姓名：" + Global._Session._FullName;
                    try
                    {
                        this.Menu_Visible();
                        this.MenuInited = true;
                    }
                    catch (System.Exception ex)
                    {
                        DxPublic.ShowException(ex, this.Text);
                    }
                }
            }));
        }

        /// <summary>
        /// 没有权限菜单初始化
        /// </summary>
        private void Menu_Null()
        {
            base.Invoke(new EventHandler(delegate
            {
                this.StatusLoginName.Caption = "用户：未验证";
                this.statusLogintime.Caption = DateTime.Now.ToString("yyyy年MM月dd HH:mm:ss dddd");
                this.statusIP.Caption = "  登录IP：" + Global._Session._IPAddress;
                this.statusMac.Caption = "  登录MAC：" + Global._Session._MACAddress;
                this.statusFullName.Caption = "  真实姓名：未验证";
                foreach (RibbonPage ribbonPage in this.MainRibbon.Pages)
                {
                    ribbonPage.Visible = false;
                }
                this.pageSystem.Visible = true;
                this.btnUserLogout.Visibility = BarItemVisibility.Always;
                this.btnAppExit.Visibility = BarItemVisibility.Always;
                this.btnDBConnSetup.Visibility = BarItemVisibility.Always;
                this.btnModifyPwd.Visibility = BarItemVisibility.Never;

                this.pageHelper.Visible = true;
                this.btnAbout.Visibility = BarItemVisibility.Always;
                this.btnCalc.Visibility = BarItemVisibility.Always;
                this.btnNotepad.Visibility = BarItemVisibility.Always;
                this.btnstikynot.Visibility = BarItemVisibility.Always;

                if (this.LoginForm != null)
                {
                    this.LoginForm.SetFocus();
                }
            }));
        }

        /// <summary>
        /// 菜单栏是否显示受权
        /// </summary>
        private void Menu_Visible()
        {
            try
            {
                for (int i = 0; i < this.MainRibbon.Items.Count; i++)
                {
                    if ((this.MainRibbon.Items[i].Tag != null) && (this.MainRibbon.Items[i].Tag.ToString() != "NO") && (this.MainRibbon.Items[i] is BarButtonItem || this.MainRibbon.Items[i] is BarSubItem))
                    {
                        object tag = this.MainRibbon.Items[i].Tag;
                        object cass = this.MainRibbon.Items[i].Caption;
                        if (tag == null || tag.ToString().Trim() == "")
                        {
                            this.MainRibbon.Items[i].Visibility = BarItemVisibility.Always;
                        }
                        else
                        {
                            this.MainRibbon.Items[i].Visibility = (this.OperFuncVeify(Guid.Parse(this.MainRibbon.Items[i].Tag.ToString().Trim())) ? BarItemVisibility.Always : BarItemVisibility.Never);
                        }
                    }
                }
                foreach (RibbonPage ribbonPage in this.MainRibbon.Pages)
                {
                    object tag2 = ribbonPage.Tag;
                    if (tag2 == null || tag2.ToString().Trim() == "")
                    {
                        ribbonPage.Visible = false;
                    }
                    else
                    {
                        ribbonPage.Visible = this.OperFuncVeify(Guid.Parse(ribbonPage.Tag.ToString().Trim()));
                    }
                    if (ribbonPage.Visible)
                    {
                        int count = ribbonPage.Groups.Count;
                        for (int j = 0; j < count; j++)
                        {
                            this.DealPageGroupVisible(ribbonPage.Groups[j]);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.pageEditor.Visible = false;
                this.pageSystem.Visible = true;
                this.pageHelper.Visible = true;
                this.MainRibbon.SelectedPage = this.pageSystem;
            }
        }

        /// <summary>
        /// RibbonPageGroup 分组框是否显示
        /// </summary>
        /// <param name="rpg"> 分组框</param>
        private void DealPageGroupVisible(RibbonPageGroup rpg)
        {
            if (rpg == null)
            {
                return;
            }
            int count = rpg.ItemLinks.Count;
            for (int i = 0; i < count; i++)
            {
                BarItem item = rpg.ItemLinks[i].Item;
                if (item != null && item.Visibility == BarItemVisibility.Always)
                {
                    rpg.Visible = true;
                    return;
                }
            }
            rpg.Visible = false;
        }

        private void btnAppExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!DxPublic.GetMessageBoxYesNoResult("是否退出登录？", this.Text))
            {
                return;
            }
            Application.ExitThread();
            Application.Exit();
        }

        private void btnLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.LoginedIn)
            {
                this.MainForm_Load(null, null);
                return;
            }
            if (!DxPublic.GetMessageBoxYesNoResult("是否注销系统？", this.Text))
            {
                return;
            }
            this.CloseAllWin();
            Global._Session.SessionIntial();
            this.Menu_Null();
            this.MainForm_Load(null, null);
            this.LoginedIn = false;
        }

        private void btnCalc_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void btnNotepad_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe");
        }

        private void btnModifyPwd_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SubForm.pageSystem.FModifyUserPwd sub = new SubForm.pageSystem.FModifyUserPwd(_appRight);
            //sub.ShowDialog();
        }
        #endregion

        #region 系统窗体主题
        private void InitialUserSkin()
        {
            try
            {
                SkinHelper.InitSkinGallery(skinRibbon);
                var SkinName = AppSetingHelper.GetDefaultTheme();
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(SkinName);//skinName为皮肤名 
                skinRibbon.Caption = "主题：" + SkinName;
            }
            catch(Exception ex) {
                DxPublic.ShowException(ex);
                throw ex;
            }
           
        }

        private void skinRibbon_Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            if (skinRibbon.Gallery == null) return;
            var  SkinName = skinRibbon.Gallery.GetCheckedItems()[0].Value.ToString();//主题的描述
             AppSetingHelper.SetDefaultTheme(SkinName);
            skinRibbon.Caption = "主题：" + SkinName;
        }

        #endregion

        private void btnDBConnSetup_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.FormShow(new DbSettingForm());
        }
    }
}
