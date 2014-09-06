using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 中国炎黄自动交易系统
{
    public delegate void webDelegate(int option);
    public partial class Child : Form
    {
        
        public event webDelegate showInfoEvent;     

        public Child()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {
                showInfoEvent(StateInfo.option); 
            }
        }
    }
}
