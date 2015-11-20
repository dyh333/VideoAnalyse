using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NVRCsharpDemo
{
    public partial class DrawTest : Form
    {
        public DrawTest()
        {
            InitializeComponent();            
        }

        private DrawTools dt;

        private void DrawTest_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics; //创建画板,这里的画板是由Form提供的.
            //Pen p = new Pen(Color.Blue, 2);//定义了一个蓝色,宽度为的画笔

            //g.DrawLine(p, 10, 10, 100, 100);//在画板上画直线,起始坐标为(10,10),终点坐标为(100,100)
            //g.DrawRectangle(p, 10, 10, 100, 100);//在画板上画矩形,起始坐标为(10,10),宽为,高为
            //g.DrawEllipse(p, 10, 10, 100, 100);//在画板上画椭圆,起始坐标为(10,10),外接矩形的宽为,高为


        }

        private void DrawTest_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            //Bitmap bmp = new Bitmap(pbImg.Width, pbImg.Height);
            //Graphics g = Graphics.FromImage(bmp);
            //g.FillRectangle(new SolidBrush(pbImg.BackColor), new Rectangle(0, 0, pbImg.Width, pbImg.Height));
            //g.Dispose();


            Bitmap bmpformfile = new Bitmap(@"test.bmp");//获取打开的文件
            Bitmap bmp = new Bitmap(pbImg.Width, pbImg.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(pbImg.BackColor), new Rectangle(0, 0, pbImg.Width, pbImg.Height));//不使用这句话，那么这个bmp的背景就是透明的
            g.DrawImage(bmpformfile, 0, 0, bmpformfile.Width, bmpformfile.Height);//将图片画到画板上
            g.Dispose();//释放画板所占资源
            //不直接使用pbImg.Image = Image.FormFile(ofd.FileName)是因为这样会让图片一直处于打开状态，也就无法保存修改后的图片；详见http://www.wanxin.org/redirect.php?tid=3&goto=lastpost

            dt = new DrawTools(this.pbImg.CreateGraphics(), Color.Red, bmp);//实例化工具类
        }

        private void pbImg_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(dt.OrginalImg, 0, 0);
        }

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            pbImg.Cursor = Cursors.Cross;
        }

        private void pbImg_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dt != null)
                {
                    dt.startDraw = true;//相当于所选工具被激活，可以开始绘图
                    dt.startPointF = new PointF(e.X, e.Y);
                }
            }
        }

        private void pbImg_MouseMove(object sender, MouseEventArgs e)
        {
            Thread.Sleep(6);//减少cpu占用率
            //mousePostion.Text = e.Location.ToString();
            if (dt.startDraw)
            {
                dt.Draw(e, "Line");
            }
        }

        private void pbImg_MouseUp(object sender, MouseEventArgs e)
        {
            if (dt != null)
            {
                dt.EndDraw();
            }
        }

        
    }
}
