using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using mshtml;
using System.Runtime.InteropServices;



namespace 中国炎黄自动交易系统
{
    [ComVisible(true)]

    public partial class Login : Form
    {
        DataSet conf = new DataSet();
        DataTable user = new DataTable();
        HtmlDocument html;

        bool autoLogin = false;

        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.cgcape.pro/");
            webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);
            webBrowser1.ScriptErrorsSuppressed = true;
            button3.Visible = false;
            label4.Visible = false;
            conf.ReadXml("conf.xml");
            user = conf.Tables[0];
            comboBox1.DataSource = user;
            comboBox1.DisplayMember = "userID";
            comboBox1.ValueMember = "userID";
            initData();
            if (StateInfo.reLogin)
            {
                checkBox2.Checked = false;
                autoLogin = false;
            }
            if (autoLogin)
            {
                button1_Click(sender, e);
            }
        }

        private void initData()
        {
            this.comboBox1.SelectedIndex = 0;
            if (user.Rows[0]["RemPass"].ToString().Trim().CompareTo("1") == 0)
            {
                this.textBox2.Text = user.Rows[0]["Password"].ToString().Trim();
                checkBox1.Checked = true;
            }
            if (user.Rows[0]["AutoLogin"].ToString().Trim().CompareTo("1") == 0)
            {
                checkBox2.Checked = true;
                autoLogin = true;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.comboBox1.SelectedIndex;
            if (user.Rows[index]["RemPass"].ToString().CompareTo("1") == 0)
            {
                checkBox1.Checked = true;
                textBox2.Text = user.Rows[index]["password"].ToString();
            }
            else
            {
                checkBox1.Checked = false;
                textBox2.Text = "";
            }
            checkBox2.Checked = false;
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            IHTMLWindow2 win = (IHTMLWindow2)webBrowser1.Document.Window.DomWindow;
            string s = @"function confirm() {return true;}function alert(str){window.external.alertMessage(str);}";
            win.execScript(s, "javascript");
            webBrowser1.ObjectForScripting = this;
        }

        public void alertMessage(string s)
        {
            StateInfo.alart = s;
        }


        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {
                html = webBrowser1.Document;
                HtmlElement img = html.GetElementById("CodeImg");
                if (img == null)
                    return;
                img.Style = "position:absolute;z-index:9999;top:0px;left:0px";
                Bitmap b = new Bitmap(img.ClientRectangle.Width, img.ClientRectangle.Height);
                webBrowser1.DrawToBitmap(b, new Rectangle(new Point(), img.ClientRectangle.Size));
                pictureBox1.Image = b;
                if (StateInfo.alart.Contains("密码"))
                {
                    autoLogin = false;
                    MessageBox.Show("密码错误", "提示");
                    checkBox2.Checked = false;
                    StateInfo.alart = "";
                    button3_Click(sender, e);
                }
                else if (StateInfo.alart.Contains("验证码"))
                {
                    autoLogin = false;
                    MessageBox.Show("验证码错误", "提示");
                    checkBox2.Checked = false;
                    StateInfo.alart = "";
                    button3_Click(sender, e);
                    textBox1.Focus();
                    textBox1.Text = "";
                }
                else if (StateInfo.alart == "click")
                {
                    StateInfo.userID = comboBox1.Text;
                    StateInfo.password = textBox2.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                modifyConf(this.comboBox1.Text, this.textBox2.Text);
            }
            if (webBrowser1.ReadyState == WebBrowserReadyState.Interactive)
            {
                //MessageBox.Show(StateInfo.alart, "Int");
            }
        }


        public void changeState(bool isLogin)
        {
            if (isLogin)
            {
                button1.Text = "正在登录...";
                button1.Enabled = false;
                button2.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                textBox2.Enabled = false;
                comboBox1.Enabled = false;
                button3.Visible = true;
                label4.Visible = true;
            }
            else
            {
                button1.Text = "登      录";
                button1.Enabled = true;
                button2.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                textBox2.Enabled = true;
                comboBox1.Enabled = true;
                button3.Visible = false;
                label4.Visible = false;
            }
        }

        public void modifyConf(string id, string pass)
        {
            if (id == "")
                return;
            DataRow newuser = conf.Tables[0].NewRow();
            newuser["userID"] = id;
            newuser["password"] = pass;
            newuser["RemPass"] = checkBox1.Checked ? 1 : 0;
            newuser["AutoLogin"] = checkBox2.Checked ? 1 : 0;
            user.Rows.InsertAt(newuser, 0);
            initData();
            for (int i = 1; i < user.Rows.Count; i++)
            {
                if (user.Rows[i]["userID"].ToString().CompareTo(id) == 0)
                {
                    user.Rows.RemoveAt(i);
                    break;
                }
            }
            conf.WriteXml("conf.xml");
            initData();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            changeState(true);
            StateInfo.alart = "click";
            HtmlElementCollection inputs = html.GetElementsByTagName("input");
            inputs[0].InnerText = comboBox1.Text;
            inputs[1].InnerText = textBox2.Text;
            inputs[2].InnerText = textBox1.Text;
            inputs[3].InvokeMember("click");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
                return;
            for (int i = 0; i < user.Rows.Count; i++)
            {
                if (user.Rows[i]["userID"].ToString().CompareTo(comboBox1.Text) == 0)
                {
                    user.Rows.RemoveAt(i);
                    break;
                }
            }
            conf.WriteXml("conf.xml");
            comboBox1.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.cgcape.pro/");
            changeState(false);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.cgcape.pro/");
        }
    }
}
