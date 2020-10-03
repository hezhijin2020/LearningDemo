using HZJ.DxWinComm.BaseWinFrom;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HZJ.DxWinComm.CommCls
{
    public interface IMainForm
    {
        void SetEditPageVisible(bool Visible);
        void SetFeatureButton(params FeatureButton[] btns);
        void DisableButtons();
        void MdiShow(BaseForm frm, object FuncId);
        void MdiShow(BaseForm frm, object FuncId, bool ReStart = false);
        void MdiShow2(BaseForm frm);
        Form[] GetMDIChildren();
        bool OperFuncVeify(Guid FuncId);
        void SetButtonEnableByCode(FeatureButton code, bool enabled);
        void ShowAlertInfo(string Title, string Text, System.Windows.Forms.ToolTipIcon iconType);
        void ShowAlertInfo(string Title, string Text);
        bool UserHasRight(Guid FuncId);
        bool UserHasRight(Guid FuncId, FeatureButton OpCode);
        List<int> GetUserOpCode(Guid FuncId);
        void ShowNoRight(string FuncId);
        void ShowNoRight(string FuncId, FeatureButton opCode);

        #region 功能权限方法
        void PerformAddNew();
        void PerformDelete();
        void PerformQuery();
        void PerformModify();
        void PerformCancel();
        void PerformSave();
        void PerformApprove();
        void PerformUnApprove();
        void PerformImport();
        void PerformExport();
        void PerformPreview();
        void PerformPrint();
        void PerformFirst();
        void PerformPrevious();
        void PerformNext();
        void PerformLast();
        #endregion
    }
}
