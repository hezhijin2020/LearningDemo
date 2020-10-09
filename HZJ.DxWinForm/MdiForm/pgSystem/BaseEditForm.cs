using System.Windows.Forms;

namespace HZJ.DxWinComm.BaseWinFrom
{
    /// <summary>
    /// 编辑基类窗体
    /// </summary>
    public partial class BaseEditForm : Form
    {
        /// <summary>
        /// 是否新增
        /// </summary>
        private bool IsAddNew = true;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isAddNew">是否新增</param>
        public BaseEditForm(bool isAddNew = true)
        {
            InitializeComponent();
            
            IsAddNew = isAddNew;
            if (isAddNew)
            {
                this.Text = "新增" + this.Text;
            }
            else
            {
                this.Text = "编辑" + this.Text;
            }
        }

        /// <summary>
        /// 回车到下一个控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        /// <summary>
        /// 检验数据的合法性
        /// </summary>
        /// <returns></returns>
        public virtual bool IsDataValid()
        {
            return false;
        }

    }
}
