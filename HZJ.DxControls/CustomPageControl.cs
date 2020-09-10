using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DevComponents.Controls
{
    [ToolboxItem(true)]
    public partial class CustomPageControl :DevExpress.XtraEditors.XtraUserControl
    {
        public CustomPageControl()
        {
            InitializeComponent();

            this.SetControlEnabled(false);
            this.txtPageSize.Text = this.PageSize.ToString();
        }

        private bool _isTextChanged;
        private int _totalCount;
        private int _pageSize = 100;
        private int _pageMaxSize = 100;
        private int _pageIndex = 1;
        private int _pageCount;
        private bool trigger;

        public event EventHandler OnPageChanged;


        /// <summary>
        /// 页总数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (this._pageSize != 0)
                {
                    this._pageCount = this.GetPageCount();
                }
                return this._pageCount;
            }
        }

        /// <summary>
        /// 当前第几页
        /// </summary>
        public virtual int PageIndex
        {
            get
            {
                return this._pageIndex;
            }
            set
            {
                this._pageIndex = value;
            }
        }

        /// <summary>
        /// 每页最大记录数
        /// </summary>
        public int PageMaxSize
        {
            get
            {
                return this._pageMaxSize;
            }
            set
            {
                this._pageMaxSize = value;
            }
        }

       /// <summary>
       /// 每页记录数
       /// </summary>
        public virtual int PageSize
        {
            get
            {
                return this._pageSize;
            }
            set
            {
                this._pageSize = value;
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount
        {
            get
            {
                return this._totalCount;
            }
            set
            {
                this._totalCount = value;
            }
        }
        
        /// <summary>
        /// 第一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            this._pageIndex = 1;
            this.DrawControl(true);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.PageIndex = Math.Max(1, this.PageIndex - 1);
            this.DrawControl(true);
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.PageIndex = Math.Min(this.PageCount, this.PageIndex + 1);
            this.DrawControl(true);
        }

        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            this.PageIndex = this.PageCount;
            this.DrawControl(true);
        }

        /// <summary>
        /// 跳转指定页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (this.CheckPageNum())
            {
                int result = 0;
                if (int.TryParse(this.txtPageNum.Text.Trim(), out result) && (0 < result))
                {
                    this.PageIndex = result;
                    this.DrawControl(true);
                }
            }
        }

        /// <summary>
        /// 焦点在当前行控件上铵下键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void txtPageNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnGo_Click(null, null);
            }
        }

        /// <summary>
        /// 离开当前行控件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageNum_Leave(object sender, EventArgs e)
        {
            this.btnGo_Click(null, null);
        }

        /// <summary>
        /// 当前页改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            if (this.CheckPageNum())
            {
                int result = 0;
                if ((int.TryParse(this.txtPageNum.Text.Trim(), out result) && (result > 0)) && (result > this.PageCount))
                {
                    this.txtPageNum.Text = this.PageCount.ToString();
                }
            }
        }

        /// <summary>
        /// 焦点在页行数控件 按下铵钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (((e.KeyCode == Keys.Enter) && this.CheckPageSize()) && this._isTextChanged)
            {
                this._isTextChanged = false;
                this.btnFirst_Click(null, null);
            }
        }

       /// <summary>
       /// 焦点在页行数控件 按下铵钮抬起事件
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void txtPageSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.CheckPageSize() && this._isTextChanged)
            {
                this._isTextChanged = false;
                this.btnFirst_Click(null, null);
            }
        }

        /// <summary>
        /// 光标离开页行数控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageSize_Leave(object sender, EventArgs e)
        {
            if (this.CheckPageSize() && this._isTextChanged)
            {
                this._isTextChanged = false;
                this.btnFirst_Click(null, null);
            }
        }

        /// <summary>
        /// 每页行数改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (this.CheckPageSize() && this._isTextChanged)
            {
                this._isTextChanged = false;
                this.btnFirst_Click(null, null);
            }
        }

        /// <summary>
        /// 重绘控件方法
        /// </summary>
        /// <param name="callEvent"></param>
        private void DrawControl(bool callEvent)
        {
            this.txtPageSize.Text = this.PageSize.ToString();
            this.lblCurrentPage.Text = this.PageIndex.ToString();
            this.lblPageCount.Text = this.PageCount.ToString();
            this.lblPCount.Text = this.PageCount.ToString();
            this.lblTotalCount.Text = this.TotalCount.ToString();

            
            if ((!this.trigger && callEvent) && (this.OnPageChanged != null))
            {
                this.OnPageChanged(this, null);
            }

            this.SetControlEnabled(true); //启用操作按钮

            if (1 == this.PageCount)
            {
                this.btnFirst.Enabled = false;
                this.btnPrevious.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
                this.txtPageNum.Enabled = false;
                this.btnGo.Enabled = false;
            }
            else if (1 == this.PageIndex)
            {
                this.btnFirst.Enabled = false;
                this.btnPrevious.Enabled = false;
            }
            else if (this.PageIndex == this.PageCount)
            {
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
            }
            if (this.TotalCount == 0)
            {
                this.btnLast.Enabled = false;
                this.btnNext.Enabled = false;
                this.txtPageSize.Enabled = false;
                this.txtPageNum.Enabled = false;
                this.btnGo.Enabled = false;
            }
            if (this.PageSize == 0)
            {
                this.btnFirst.Enabled = false;
                this.btnPrevious.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
                this.txtPageNum.Enabled = false;
                this.btnGo.Enabled = false;
                this.lblCurrentPage.Text = "1";
                this.lblPageCount.Text = "1";
                this.lblPCount.Text = "1";
            }
        }

        /// <summary>
        /// 外部调用
        /// </summary>
        /// <param name="count"></param>
        public void DrawControl(int count)
        {
            this._totalCount = count;
            this.DrawControl(false);
        }

        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <returns></returns>
        private int GetPageCount()
        {
            if (this.PageSize == 0)
            {
                return 0;
            }
            int num = this.TotalCount / this.PageSize;
            if ((this.TotalCount % this.PageSize) == 0)
            {
                return num;
            }
            return num + 1;
        }

        /// <summary>
        /// 检查每页的记录数量
        /// </summary>
        /// <returns></returns>
        private bool CheckPageSize()
        {
            int result = 0;
            string pattern = @"^\d+$";
            string str2 = this.txtPageSize.Text.Trim();
            bool ss= string.IsNullOrEmpty(str2);bool sss=Regex.IsMatch(str2, pattern);
            if (!string.IsNullOrEmpty(str2) && ((!Regex.IsMatch(str2, pattern) || !int.TryParse(str2, out result)) || (Convert.ToInt32(str2) > this.PageMaxSize)))
            {
                MessageBox.Show(string.Format("只能输入大于等于0小于等于{0}的正整数！", this.PageMaxSize), "提示");
                result = this.PageSize;
                this.txtPageSize.Text = this.PageMaxSize.ToString();
                if (result != this.PageMaxSize)
                {
                    this.trigger = true;
                }
                return false;
            }
            this._isTextChanged = true;
            this._pageSize = result;
            return true;
        }

        /// <summary>
        /// 检查总页数
        /// </summary>
        /// <returns></returns>
        private bool CheckPageNum()
        {
            int result = 0;
            string pattern = @"^\d+$";
            string str2 = this.txtPageNum.Text.Trim();
            if (string.IsNullOrEmpty(str2) || (Regex.IsMatch(str2, pattern) && int.TryParse(str2, out result)))
            {
                return true;
            }
            MessageBox.Show(string.Format("只能输入大于等于0小于等于{0}的正整数！", 0x7fffffff), "提示");
            this.txtPageNum.Text = "1";
            this.txtPageNum.Focus();
            return false;
        }

        /// <summary>
        /// 操作按钮的启用与禁用
        /// </summary>
        /// <param name="enable"></param>
        private void SetControlEnabled(bool enable)
        {
            this.btnFirst.Enabled = enable;
            this.btnPrevious.Enabled = enable;
            this.btnNext.Enabled = enable;
            this.btnLast.Enabled = enable;
            this.btnGo.Enabled = enable;
            this.txtPageNum.Enabled = enable;
            this.txtPageSize.Enabled = enable;
        }

        /// <summary>
        /// 重定位输入库位置在中间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            int y = (this.panelControl1.Height - txtPageNum.Height )/ 2;
            int x1 = this.txtPageNum.Location.X;
            int x2 = this.txtPageSize.Location.X;
            txtPageNum.Location = new Point(x1,y+1);
            txtPageSize.Location = new Point(x2, y+1);
        }
    }
}
