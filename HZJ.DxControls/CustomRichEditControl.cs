using System.ComponentModel;
using System.Windows.Forms;

namespace HZJ.DxControls
{
    [ToolboxItem(true)]
    public partial class CustomRichEditControl : UserControl
    {
        public CustomRichEditControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 文本内容
        /// </summary>
        public string TextEdit { get {return myRichEdit.RtfText; }
            set {this.myRichEdit.RtfText = value; } }
    }
}
