using DevExpress.XtraEditors;
using HZJ.DxWinForm.Utility.CommCls;
using System;

namespace HZJ.DxWinForm.Utility.BaseWinFrom
{
    /// <summary>
    /// 基类窗体
    /// </summary>
    public partial class BaseForm : XtraForm
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        #region 声明变量、功能ID（FuncId）、可用的功能权限（activeBtns）、
        public Guid FuncId = Guid.Empty;
        public FeatureButton[] activeBtns;

        public IMainForm Mainform { get; set; }
        #endregion

        #region  窗体方法初始化方法

        public virtual void InitFeatureButton()
        {

        }

        protected void SetFeatureButton(params FeatureButton[] buttons)
        {
            if (this.Mainform == null)
            {
                return;
            }
            if (FuncId != null && FuncId != Guid.Empty)
            {
                buttons = this.GetValidFeatureButton(buttons);
            }
            this.activeBtns = buttons;
            this.Mainform.SetFeatureButton(buttons);
        }

        private FeatureButton[] GetValidFeatureButton(FeatureButton[] btns)
        {
            System.Collections.Generic.List<FeatureButton> list = new System.Collections.Generic.List<FeatureButton>();
            if (btns != null && 0 <= btns.Length && this.Mainform != null)
            {
                for (int i = 0; i < btns.Length; i++)
                {
                    FeatureButton featureButton = btns[i];
                    if (featureButton == FeatureButton.Cancel || featureButton == FeatureButton.Save)
                    {
                        list.Add(featureButton);
                    }
                    else if (featureButton == FeatureButton.Add)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.Add))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.Approve)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.Approve))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.Delete)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.Delete))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.Export)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.Export))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.Import)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.Import))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.Modify)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.Modify))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.Preview)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.Preview))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.Print)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.Print))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.Query)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.Query))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.UnApprove)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.UnApprove))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.First)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.Query))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.Previous)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.Query))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.Next)
                    {
                        if (this.Mainform.UserHasRight(FuncId, FeatureButton.Query))
                        {
                            list.Add(featureButton);
                        }
                    }
                    else if (featureButton == FeatureButton.Last && this.Mainform.UserHasRight(FuncId, FeatureButton.Query))
                    {
                        list.Add(featureButton);
                    }
                }
            }
            return list.ToArray();
        }

        protected void SetFeatureButton(bool NoAuth, params FeatureButton[] buttons)
        {
            if (!NoAuth)
            {
                this.SetFeatureButton(buttons);
                return;
            }
            if (this.Mainform == null)
            {
                return;
            }
            this.activeBtns = buttons;
            this.Mainform.SetFeatureButton(buttons);
        }

        #endregion

        #region 编辑工具栏虚拟方法
        public virtual void AddNew()
        {
        }

        public virtual void Delete()
        {
        }

        public virtual void Query()
        {
        }

        public virtual void Modify()
        {
        }

        public virtual void Cancel()
        {
        }

        public virtual void Save()
        {
        }

        public virtual void Approve()
        {
        }

        public virtual void UnApprove()
        {
        }

        public virtual void Import()
        {
        }

        public virtual void Export()
        {
        }

        public virtual void Preview()
        {
        }

        public virtual void Print()
        {
        }

        public virtual void First()
        {
        }

        public virtual void Previous()
        {
        }

        public virtual void Next()
        {
        }

        public virtual void Last()
        {
        }
        #endregion
    }
}
