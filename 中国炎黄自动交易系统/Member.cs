using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;
using System.Runtime.InteropServices;
using System.Threading;

namespace 中国炎黄自动交易系统
{
    [ComVisible(true)]

    public partial class Member : Form
    {
        static Child childform = new Child();
        string current_URL;
        DataSet tradeSet = new DataSet();
        DataTable tradeList = new DataTable();
        string buyTime;

        static int count = 0;


        public Member()
        {
            InitializeComponent();
        }

        private void Member_Load(object sender, EventArgs e)
        {
            Thread t1 = new Thread(new ThreadStart(AutoTradeThread));
            t1.Start();

            StateInfo.showChild = false;
            StateInfo.option = 0;
            childform.showInfoEvent += new webDelegate(show_Info);
            childform.Size = new Size(this.Height, this.Height);
            childform.webBrowser1.Navigate("http://www.cgcape.pro/member.aspx");
            current_URL = "http://www.cgcape.pro/member.aspx";
            tabControl1.SelectedIndex = 3;
            tradeSet.ReadXml("trade.xml");
            tradeList = tradeSet.Tables[0];
            dataGridView2.DataSource = tradeList;
            initGrid();
        }

        public static void AutoTradeThread()
        {
            System.Timers.Timer ti = new System.Timers.Timer();
            //while (true)
            //{
                ti.Interval = 1000 * 3;
                ti.Elapsed += new System.Timers.ElapsedEventHandler(checkOut);//到达时间的时候执行事件；
                ti.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
                ti.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            //}
        }

        public static void checkOut(object source, System.Timers.ElapsedEventArgs e)
        {
            //StateInfo.option = 8;
            //childform.webBrowser1.Navigate("http://www.cgcape.pro/shareTradeRecord.aspx");
            
            
            //count++;
            //MessageBox.Show(count.ToString());
        }


        private void initGrid1()
        {
            dataGridView1.ColumnCount = 10;
            dataGridView1.Columns[0].HeaderText = "交易编号";
            dataGridView1.Columns[1].HeaderText = "买入类型";
            dataGridView1.Columns[2].HeaderText = "交易量";
            dataGridView1.Columns[3].HeaderText = "交易价格";
            dataGridView1.Columns[4].HeaderText = "拆分次数";
            dataGridView1.Columns[5].HeaderText = "剩余股数";
            dataGridView1.Columns[6].HeaderText = "挂单股数";
            dataGridView1.Columns[7].HeaderText = "买入时间";
            dataGridView1.Columns[8].HeaderText = "最后成交时间";
            dataGridView1.Columns[9].HeaderText = "操作";
            for (int i = 0; i < 10; i++)
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void initGrid2()
        {
            dataGridView4.ColumnCount = 9;
            dataGridView4.Columns[0].HeaderText = "交易编号";
            dataGridView4.Columns[1].HeaderText = "交易方式";
            dataGridView4.Columns[2].HeaderText = "交易量";
            dataGridView4.Columns[3].HeaderText = "成交量";
            dataGridView4.Columns[4].HeaderText = "交易价格";
            dataGridView4.Columns[5].HeaderText = "交易发起时间";
            dataGridView4.Columns[6].HeaderText = "最后成交时间";
            dataGridView4.Columns[7].HeaderText = "状态";
            dataGridView4.Columns[8].HeaderText = "操作";
            for (int i = 0; i < 9; i++)
                dataGridView4.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void initGrid3()
        {
            dataGridView3.ColumnCount = 9;
            dataGridView3.Columns[0].HeaderText = "交易编号";
            dataGridView3.Columns[1].HeaderText = "会员编号";
            dataGridView3.Columns[2].HeaderText = "交易方式";
            dataGridView3.Columns[3].HeaderText = "交易量";
            dataGridView3.Columns[4].HeaderText = "成交量";
            dataGridView3.Columns[5].HeaderText = "交易价格";
            dataGridView3.Columns[6].HeaderText = "交易发起时间";
            dataGridView3.Columns[7].HeaderText = "最后成交时间";
            dataGridView3.Columns[8].HeaderText = "操作";
            for (int i = 0; i < 9; i++)
                dataGridView3.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void initGrid()
        {
            dataGridView2.Columns[0].HeaderText = "交易量";
            dataGridView2.Columns[1].HeaderText = "交易价格";
            dataGridView2.Columns[2].HeaderText = "买入时间";
            dataGridView2.Columns[3].HeaderText = "交易发起时间";
            dataGridView2.Columns[4].HeaderText = "挂单次数";
            dataGridView2.Columns[5].HeaderText = "三级密码";
            dataGridView2.Columns[6].HeaderText = "操作";
            for (int i = 0; i < 7; i++)
                dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.cgcape.pro");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (current_URL != "http://www.cgcape.pro/shareTradeRecord.aspx")
                    {
                        StateInfo.option = 1;
                        current_URL = "http://www.cgcape.pro/shareTradeRecord.aspx";
                        childform.webBrowser1.Navigate(current_URL);
                    }
                    break;
                case 1:
                    if (current_URL != "http://www.cgcape.pro/shareTradeList.aspx")
                    {
                        StateInfo.option = 2;
                        current_URL = "http://www.cgcape.pro/shareTradeList.aspx";
                        childform.webBrowser1.Navigate(current_URL);
                    }
                    break;
                case 2:
                    if (current_URL != "http://www.cgcape.pro/shareSell.aspx")
                    {
                        StateInfo.option = 3;
                        current_URL = "http://www.cgcape.pro/shareSell.aspx";
                        childform.webBrowser1.Navigate(current_URL);
                    }
                    break;
                case 3:
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!StateInfo.showChild)
            {
                childform.Location = new Point(this.Width + this.Location.X, this.Location.Y);
                //childform.Dock = DockStyle.None;
                childform.Show();
                StateInfo.showChild = true;
                button6.Text = "<<";
            }
            else
            {
                childform.Hide();
                StateInfo.showChild = false;
                button6.Text = ">>";
            }

        }

        private void Member_Move(object sender, EventArgs e)
        {
            childform.Location = new Point(this.Width + this.Location.X, this.Location.Y);
        }

        public void show_Info(int option)
        {
            HtmlDocument html = childform.webBrowser1.Document;
            switch (option)
            {
                case 0:   //账户信息
                    HtmlElement table = html.GetElementsByTagName("table")[1];
                    HtmlElementCollection tds = table.GetElementsByTagName("td");
                    label1.Text = tds[2].InnerText;
                    label2.Text = tds[3].InnerText.Substring(0, tds[3].InnerText.Length - 5);
                    label3.Text = tds[4].InnerText;
                    label4.Text = tds[5].InnerText;
                    label5.Text = tds[6].InnerText;
                    label6.Text = tds[7].InnerText;
                    label7.Text = tds[8].InnerText;
                    label8.Text = tds[1].InnerText;
                    break;
                case 1:    //个人股仓
                     initGrid2();
                    dataGridView4.Rows.Clear();
                    HtmlElement table1 = html.GetElementsByTagName("table")[0];
                    HtmlElementCollection trs1 = table1.GetElementsByTagName("tr");
                    for (int i = 2; i < trs1.Count; i++)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        HtmlElement tr1 = trs1[i];
                        HtmlElementCollection tds1 = tr1.GetElementsByTagName("td");
                        if (tds1.Count == 0)
                            continue;
                        for (int j = 0; j < tds1.Count; j++)
                        {
                            int truevalue = (j) % tds1.Count;
                            HtmlElement td1 = tds1[truevalue];
                            if (j == tds1.Count - 1)
                            {
                                HtmlElement lastTd = tds1[truevalue - 1];
                                if (lastTd.InnerText.Contains("已完成"))
                                {
                                    DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                                    textboxcell.Value = td1.InnerText;
                                    row.Cells.Add(textboxcell);
                                }
                                else
                                {
                                    DataGridViewButtonCell buttoncell = new DataGridViewButtonCell();
                                    buttoncell.Value = td1.InnerText;
                                    row.Cells.Add(buttoncell);
                                }
                            }
                            else
                            {
                                DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                                textboxcell.Value = td1.InnerText;
                                row.Cells.Add(textboxcell);
                            }

                        }
                        dataGridView4.Rows.Add(row);
                    }
                    break;
                case 2:    //交易行情

                    initGrid3();
                    dataGridView3.Rows.Clear();
                    HtmlElement table2 = html.GetElementsByTagName("table")[0];
                    HtmlElementCollection trs2 = table2.GetElementsByTagName("tr");
                    for (int i = 2; i < trs2.Count; i++)
                    {
                        DataGridViewRow row2 = new DataGridViewRow();
                        HtmlElement tr2 = trs2[i];
                        HtmlElementCollection tds2 = tr2.GetElementsByTagName("td");
                        if (tds2.Count == 0)
                            continue;
                        for (int j = 0; j < tds2.Count; j++)
                        {
                            int truevalue = (j) % tds2.Count;
                            HtmlElement td2 = tds2[truevalue];
                            DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                            textboxcell.Value = td2.InnerText;
                            row2.Cells.Add(textboxcell);
                        }
                        dataGridView3.Rows.Add(row2);
                    }
                    break;
                case 3:    //积分卖出
                    initGrid1();
                    dataGridView1.Rows.Clear();
                    HtmlElement table3 = html.GetElementsByTagName("table")[0];
                    HtmlElementCollection trs3 = table3.GetElementsByTagName("tr");
                    for (int i = 2; i < trs3.Count; i++)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        HtmlElement tr3 = trs3[i];
                        HtmlElementCollection tds3 = tr3.GetElementsByTagName("td");
                        if (tds3.Count == 0)
                            continue;
                        for (int j = 0; j < tds3.Count-1; j++)
                        {
                            int truevalue = (j + 1) % tds3.Count;
                            HtmlElement td3 = tds3[truevalue];
                            if (j == tds3.Count - 2)
                            {
                                if (td3.InnerText.Contains("已完成"))
                                {
                                    DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                                    textboxcell.Value = td3.InnerText;
                                    row.Cells.Add(textboxcell);
                                }
                                else
                                {
                                    DataGridViewButtonCell buttoncell = new DataGridViewButtonCell();
                                    buttoncell.Value = td3.InnerText;
                                    row.Cells.Add(buttoncell);
                                }
                            }
                            else
                            {
                                DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                                textboxcell.Value = td3.InnerText;
                                row.Cells.Add(textboxcell);
                            }
                        }
                        dataGridView1.Rows.Add(row);
                    }
                    break;
                case 7:

                case 8:
                    for (int i = 0; i < dataGridView2.Rows.Count;i++ )
                    {
                        string buyT = "";
                        string state = buyTime;
                        if (buyT==buyTime)
                        {
                            if (state=="未完成")
                            {
                                return;
                            }
                            if (state=="已完成")
                            {
                                dataGridView2.Rows.RemoveAt(0);
                            }
                        }
                        else
                        {
                            StateInfo.option = 8;
                            childform.webBrowser1.Navigate("http://www.cgcape.pro/shareTradeRecord.aspx");
                        }

                    }
                    break;
                case 9:
                    HtmlElement btn = html.GetElementById("repeater1_tdOP_" + dataGridView1.CurrentRow.Index.ToString());
                    btn.Children[0].InvokeMember("click");
                    string price =html.GetElementById("tradePrice").Parent.InnerText;
                    html.GetElementById("tradeAmount").InnerText = "100";
                    html.GetElementById("tradePrice").InnerText = price.Substring(8,price.Length-10); 
                    html.GetElementById("passEncry").InnerText = "000000";
                    html.GetElementById("Submit2").InvokeMember("click");
                    dataGridView2.Rows.RemoveAt(0);
                    break;
                default:
                    break;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.Value.ToString() == "[卖出]")
            {
                HtmlDocument html = childform.webBrowser1.Document;
                HtmlElement btn = html.GetElementById("repeater1_tdOP_" + dataGridView1.CurrentRow.Index.ToString());
                btn.Children[0].InvokeMember("click");
                textBox1.Text = "";

                string price =html.GetElementById("tradePrice").Parent.InnerText;
                textBox2.Text = price.Substring(8,price.Length-10); 
                textBox3.Text = "000000";
                checkBox1.Checked = true;
                panel2.Visible = true;   
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            HtmlDocument html = childform.webBrowser1.Document;
            
            html.GetElementById("tradeAmount").InnerText = textBox1.Text;
            html.GetElementById("tradePrice").InnerText = textBox2.Text;
            html.GetElementById("passEncry").InnerText = textBox3.Text;
            html.GetElementById("Submit2").InvokeMember("click");
            panel2.Visible = false;
            if (checkBox1.Checked)
            {
                addTrade(textBox1.Text, textBox3.Text, buyTime, "");
            }
        }

        private void addTrade(string amount, string pass3, string buyTime, string sellTime)
        {
            //tradeList.Rows.RemoveAt(tradeList.Rows.Count);
            tradeList.Rows.Add(textBox1.Text, textBox2.Text, buyTime, sellTime, "0", "", textBox3.Text);
            //tradeList.Rows.Add("", "", "", "", "", "", "");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (current_URL != "http://www.cgcape.pro/member.aspx")
            {
                StateInfo.option = 0;
                current_URL = "http://www.cgcape.pro/member.aspx";
                childform.webBrowser1.Navigate(current_URL);
            }
        }
    }
}
