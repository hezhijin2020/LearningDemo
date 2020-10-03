#region << 版 本 注 释 >>
//----------------------------------------------------------------
// Copyright © 2020  版权所有：湖南办事处（IT-hezhijin）
// 唯一码：0434d010-8e7f-4858-a76f-16205d87f9a3
// 文件名：RegisterCustomGridLookUpEdit
// 文件功能描述：
// 创建者：HZJ-(zhijinhe2020) 
// 计算机名：IT-HZJ
// QQ: 413961980
// 时间：2020-09-26 14:09:10
// 修改人：HZJ-(zhijinhe2020) 
// 时间：2020-09-26 14:09:10
// 修改说明：
// 版本：V1.0.0   当前系统CLR（运行时版.NET）版本号:4.0.30319.42000
//----------------------------------------------------------------
#endregion

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using System.ComponentModel;

namespace HZJ.DxControls
{
    [UserRepositoryItem("RegisterCustomGridLookUpEdit")]
    [DXCategory("Properties")]
    public class RepositoryItemCustomGridLookUpEdit : RepositoryItemGridLookUpEdit
    {
        static RepositoryItemCustomGridLookUpEdit() { RegisterCustomGridLookUpEdit(); }

        public RepositoryItemCustomGridLookUpEdit()
        {
            TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            AutoComplete = true;
        }
        [Browsable(false)]
        public override DevExpress.XtraEditors.Controls.TextEditStyles TextEditStyle { get { return base.TextEditStyle; } set { base.TextEditStyle = value; } }

        /// <summary>
        /// 编辑器的名称
        /// </summary>
        public const string CustomGridLookUpEditName = "CustomGridLookUpEdit";

        /// <summary>
        /// 编辑器名称
        /// </summary>
        public override string EditorTypeName { get { return CustomGridLookUpEditName; } }

        /// <summary>
        /// 注册编辑器
        /// </summary>
        public static void RegisterCustomGridLookUpEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomGridLookUpEditName,
              typeof(CustomGridLookUpEdit), typeof(RepositoryItemCustomGridLookUpEdit),
              typeof(GridLookUpEditBaseViewInfo), new ButtonEditPainter(), true));
        }

        /// <summary>
        /// 创建自定义GridView
        /// </summary>
        /// <returns></returns>
        protected override ColumnView CreateViewInstance() { return new CustomGridView(); }

        /// <summary>
        /// 创建自定义GridControl
        /// </summary>
        /// <returns></returns>
        protected override GridControl CreateGrid() { return new CustomGridControl(); }
    }
}
