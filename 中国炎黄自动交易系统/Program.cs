using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 中国炎黄自动交易系统
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Login());
            start:
                Login f1 = new Login();
            f1.ShowDialog();
            if (f1.DialogResult == DialogResult.OK)
            {
                Member f2 = new Member();
                Application.Run(f2);
                if (f2.DialogResult == DialogResult.OK)
                {
                    StateInfo.reLogin = true;
                    StateInfo.alart = "";
                    goto start;
                }
            }
            
        }
    }
}
