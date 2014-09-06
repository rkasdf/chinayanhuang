using System;
using System.Drawing;
using System.Windows.Forms;

namespace 中国炎黄自动交易系统
{
    class CheckCode
    {
        public static string getCode(Bitmap bmp)
        {
            String sCode;
            Rectangle rect = new Rectangle(0, 0, bmp.Width / 4, bmp.Height);
            Bitmap code = bmp.Clone(rect, bmp.PixelFormat);
            sCode = getOneCode(code).ToString();
            rect = new Rectangle(bmp.Width / 4, 0, bmp.Width / 4, bmp.Height);
            code = bmp.Clone(rect, bmp.PixelFormat);
            sCode += getOneCode(code).ToString();
            rect = new Rectangle(bmp.Width / 2, 0, bmp.Width / 4, bmp.Height);
            code = bmp.Clone(rect, bmp.PixelFormat);
            sCode += getOneCode(code).ToString();
            rect = new Rectangle(3 * bmp.Width / 4, 0, bmp.Width / 4, bmp.Height);
            code = bmp.Clone(rect, bmp.PixelFormat);
            sCode += getOneCode(code).ToString();
            return sCode;
        }

        public static int getOneCode(Bitmap bmp)
        {
            
            for (int i = 0; i < 10; i++)
            {
                bool flag = false;
                //Bitmap temp = new Bitmap("n" + i.ToString() + ".bmp");
                Bitmap code = new Bitmap("image/n" + i.ToString() + ".bmp");
                for (int width = 0; width < bmp.Width; width++)
                {
                    for (int height = 0; height < bmp.Height; height++)
                    {
                        if (bmp.GetPixel(width, height) != code.GetPixel(width, height))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == true)
                        break;
                }
                if (flag == false)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
