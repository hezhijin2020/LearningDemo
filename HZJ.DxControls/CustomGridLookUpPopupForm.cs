#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：c038ccbc-06fc-40ef-86ed-4aba11355a55
// 文件名：CustomGridLookUpPopupForm
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-26 14:07:46
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-26 14:07:46
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using System.Windows.Forms;

namespace HZJ.DxControls
{
    public class CustomGridLookUpPopupForm : PopupGridLookUpEditForm
    {
        public CustomGridLookUpPopupForm(GridLookUpEdit ownerEdit) : base(ownerEdit)
        {
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                this.OwnerEdit.EditValue = QueryResultValue();
                this.OwnerEdit.SendKey(e);
            }

            base.OnKeyDown(e);
        }
    }
}
