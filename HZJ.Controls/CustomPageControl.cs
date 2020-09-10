using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DevComponents.Controls
{
    public class CustomPageControl : UserControl
    {
        // Fields
        private bool _isTextChanged;
        private int _pageCount;
        private int _pageIndex = 1;
        private int _pageMaxSize = 100;
        private int _pageSize = 100;
        private int _totalCount;
        private BindingNavigator bindingNavigator1;
        private ToolStripSeparator bindingNavigatorSeparator2;
        private ToolStripButton btnFirst;
        private ToolStripButton btnGo;
        private ToolStripButton btnLast;
        private ToolStripButton btnNext;
        private ToolStripButton btnPrev;
        private IContainer components;
        private ToolStripLabel lblCurrentPage;
        private ToolStripLabel lblPageCount;
        private ToolStripLabel lblPCount;
        private ToolStripLabel lblTotalCount;
        private ToolStripLabel lbPageCount;
        private ToolStripLabel toolStripLabel1;
        private ToolStripLabel toolStripLabel2;
        private ToolStripLabel toolStripLabel3;
        private ToolStripLabel toolStripLabel4;
        private ToolStripLabel toolStripLabel5;
        private ToolStripLabel toolStripLabel6;
        private ToolStripLabel toolStripLabel7;
        private ToolStripLabel toolStripLabel8;
        private ToolStripLabel toolStripLabel9;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private bool trigger;
        private ToolStripTextBox txtPageNum;
        private ToolStripTextBox txtPageSize;

        // Events
        public event EventHandler OnPageChanged;

        // Methods
        public CustomPageControl()
        {
            this.InitializeComponent();
            this.SetControlEnabled(false);
            this.txtPageSize.Text = this.PageSize.ToString();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            this.PageIndex = 1;
            this.DrawControl(true);
        }

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

        private void btnLast_Click(object sender, EventArgs e)
        {
            this.PageIndex = this.PageCount;
            this.DrawControl(true);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.PageIndex = Math.Min(this.PageCount, this.PageIndex + 1);
            this.DrawControl(true);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            this.PageIndex = Math.Max(1, this.PageIndex - 1);
            this.DrawControl(true);
        }

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

        private bool CheckPageSize()
        {
            int result = 0;
            string pattern = @"^\d+$";
            string str2 = this.txtPageSize.Text.Trim();
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

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
            this.SetControlEnabled(true);
            if (1 == this.PageCount)
            {
                this.btnFirst.Enabled = false;
                this.btnPrev.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
                this.txtPageNum.Enabled = false;
                this.btnGo.Enabled = false;
            }
            else if (1 == this.PageIndex)
            {
                this.btnFirst.Enabled = false;
                this.btnPrev.Enabled = false;
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
                this.btnPrev.Enabled = false;
                this.btnNext.Enabled = false;
                this.btnLast.Enabled = false;
                this.txtPageNum.Enabled = false;
                this.btnGo.Enabled = false;
                this.lblCurrentPage.Text = "1";
                this.lblPageCount.Text = "1";
                this.lblPCount.Text = "1";
            }
        }

        public void DrawControl(int count)
        {
            this._totalCount = count;
            this.DrawControl(false);
        }

        private int GetPageCount()
        {
            if (this.PageSize == 0)
            {
                return 0;
            }
            int num = this.TotalCount / this.PageSize;
            if ((this.TotalCount % this.PageSize) == 0)
            {
                return (this.TotalCount / this.PageSize);
            }
            return ((this.TotalCount / this.PageSize) + 1);
        }

      

        private void SetControlEnabled(bool enable)
        {
            this.btnFirst.Enabled = enable;
            this.btnPrev.Enabled = enable;
            this.btnNext.Enabled = enable;
            this.btnLast.Enabled = enable;
            this.btnGo.Enabled = enable;
            this.txtPageNum.Enabled = enable;
            this.txtPageSize.Enabled = enable;
        }

        private void txtPageNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnGo_Click(null, null);
            }
        }

        private void txtPageNum_Leave(object sender, EventArgs e)
        {
            this.btnGo_Click(null, null);
        }

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

        private void txtPageSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (((e.KeyCode == Keys.Enter) && this.CheckPageSize()) && this._isTextChanged)
            {
                this._isTextChanged = false;
                this.btnFirst_Click(null, null);
            }
        }

        private void txtPageSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.CheckPageSize() && this._isTextChanged)
            {
                this._isTextChanged = false;
                this.btnFirst_Click(null, null);
            }
        }

        private void txtPageSize_Leave(object sender, EventArgs e)
        {
            if (this.CheckPageSize() && this._isTextChanged)
            {
                this._isTextChanged = false;
                this.btnFirst_Click(null, null);
            }
        }

        private void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            if (this.CheckPageSize() && this._isTextChanged)
            {
                this._isTextChanged = false;
                this.btnFirst_Click(null, null);
            }
        }

        // Properties
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.lbPageCount = new System.Windows.Forms.ToolStripLabel();
            this.txtPageSize = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.lblTotalCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.lblPageCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.lblCurrentPage = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel9 = new System.Windows.Forms.ToolStripLabel();
            this.lblPCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.btnPrev = new System.Windows.Forms.ToolStripButton();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtPageNum = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.btnGo = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(32, 35);
            this.toolStripLabel4.Text = "每页";
            // 
            // lbPageCount
            // 
            this.lbPageCount.Name = "lbPageCount";
            this.lbPageCount.Size = new System.Drawing.Size(0, 35);
            // 
            // txtPageSize
            // 
            this.txtPageSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPageSize.ForeColor = System.Drawing.SystemColors.InfoText;
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(50, 38);
            this.txtPageSize.Leave += new System.EventHandler(this.txtPageSize_Leave);
            this.txtPageSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPageSize_KeyDown);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(20, 35);
            this.toolStripLabel6.Text = "条";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(20, 35);
            this.toolStripLabel3.Text = "共";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(15, 35);
            this.lblTotalCount.Text = "0";
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(44, 35);
            this.toolStripLabel8.Text = "条记录";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(20, 35);
            this.toolStripLabel7.Text = "共";
            // 
            // lblPageCount
            // 
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(15, 35);
            this.lblPageCount.Text = "0";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(20, 35);
            this.toolStripLabel5.Text = "页";
            // 
            // lblCurrentPage
            // 
            this.lblCurrentPage.Name = "lblCurrentPage";
            this.lblCurrentPage.Size = new System.Drawing.Size(15, 35);
            this.lblCurrentPage.Text = "1";
            // 
            // toolStripLabel9
            // 
            this.toolStripLabel9.Name = "toolStripLabel9";
            this.toolStripLabel9.Size = new System.Drawing.Size(13, 35);
            this.toolStripLabel9.Text = "/";
            // 
            // lblPCount
            // 
            this.lblPCount.Name = "lblPCount";
            this.lblPCount.Size = new System.Drawing.Size(15, 35);
            this.lblPCount.Text = "0";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // btnFirst
            // 
            this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFirst.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(48, 35);
            this.btnFirst.Text = "第一页";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPrev.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(48, 35);
            this.btnPrev.Text = "上一页";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNext.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(48, 35);
            this.btnNext.Text = "下一页";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLast.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(60, 35);
            this.btnLast.Text = "最后一页";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(20, 35);
            this.toolStripLabel1.Text = "第";
            // 
            // txtPageNum
            // 
            this.txtPageNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPageNum.ForeColor = System.Drawing.SystemColors.InfoText;
            this.txtPageNum.Name = "txtPageNum";
            this.txtPageNum.Size = new System.Drawing.Size(50, 38);
            this.txtPageNum.Leave += new System.EventHandler(this.txtPageNum_Leave);
            this.txtPageNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPageNum_KeyDown);
            this.txtPageNum.TextChanged += new System.EventHandler(this.txtPageNum_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(20, 35);
            this.toolStripLabel2.Text = "页";
            // 
            // btnGo
            // 
            this.btnGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnGo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(36, 35);
            this.btnGo.Text = "跳转";
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bindingNavigator1.CountItem = null;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.lbPageCount,
            this.txtPageSize,
            this.toolStripLabel6,
            this.bindingNavigatorSeparator2,
            this.toolStripLabel3,
            this.lblTotalCount,
            this.toolStripLabel8,
            this.toolStripSeparator3,
            this.toolStripLabel7,
            this.lblPageCount,
            this.toolStripLabel5,
            this.lblCurrentPage,
            this.toolStripLabel9,
            this.lblPCount,
            this.toolStripSeparator2,
            this.btnFirst,
            this.btnPrev,
            this.btnNext,
            this.btnLast,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.txtPageNum,
            this.toolStripLabel2,
            this.btnGo});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = null;
            this.bindingNavigator1.MoveNextItem = null;
            this.bindingNavigator1.MovePreviousItem = null;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = null;
            this.bindingNavigator1.Size = new System.Drawing.Size(880, 38);
            this.bindingNavigator1.TabIndex = 0;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // CustomPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bindingNavigator1);
            this.Name = "CustomPageControl";
            this.Size = new System.Drawing.Size(880, 38);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }

}
