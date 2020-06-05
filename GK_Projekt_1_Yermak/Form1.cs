using System;
using System.Drawing;
using System.Windows.Forms;

namespace GK_Projekt_1_Yermak
{
    public partial class Form1 : Form
    {
        bool click = false;
        int X, Y, X1, Y1;
        Bitmap bitmap = new Bitmap(800, 500);
        bool czyBresenham;
        bool czyPrzyrostowy;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            click = true;
            X = e.X;
            Y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (click)
            {
                X1 = e.X;
                Y1 = e.Y;

                if (e.X < 0)
                {
                    X1 = 0;
                }
                if (e.Y < 0)
                {
                    Y1 = 0;
                }
                if (e.X >= pictureBox1.Width)
                {
                    X1 = pictureBox1.Width - 1;
                }
                if (e.Y >= pictureBox1.Height)
                {
                    Y1 = pictureBox1.Height - 1;
                }
                Bitmap bitmap2 = new Bitmap(bitmap);
                if (czyBresenham == true)
                {
                    bitmap2 = AlgorytmBresenhama(bitmap2, X, Y, X1, Y1);
                }
                if (czyPrzyrostowy == true)
                {
                    bitmap2 = AlgorytmPrzyrostowy(bitmap2, X, Y, X1, Y1);
                }
                pictureBox1.Image = bitmap2;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            click = false;
            
            if (czyBresenham == true)
            {
                Bitmap b = AlgorytmBresenhama(bitmap, X, Y, X1, Y1);
                bitmap = b;
                pictureBox1.Image = b;

            }
            if (czyPrzyrostowy == true)
            {
                Bitmap b = AlgorytmPrzyrostowy(bitmap, X, Y, X1, Y1);
                bitmap = b;
                pictureBox1.Image = b;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            czyBresenham = true;
            czyPrzyrostowy = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            czyPrzyrostowy = true;
            czyBresenham = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            X = 0;
            X1 = 0;
            Y = 0;
            Y1 = 0;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            czyBresenham = false;
            czyPrzyrostowy = false;
        }
        public Bitmap AlgorytmBresenhama(Bitmap btm, int X1, int Y1, int X2, int Y2)
        {
            int deltaX, deltaY, kX, kY, zm;

            deltaX = X2 - X1;
            if (deltaX > 0)
            {
                kX = +1;
            }
            else kX = -1;
            deltaX = Math.Abs(deltaX);
            deltaY = Y2 - Y1;
            if (deltaY > 0)
            {
                kY = +1;
            } 
            else kY = -1;
            deltaY = Math.Abs(deltaY);
            if (deltaX > deltaY)
            {
                zm = -deltaX;
                while (X1 != X2)
                {
                    btm.SetPixel(X1, Y1, Color.Black);
                    zm += 2 * deltaY;
                    if (zm > 0) 
                    { 
                        Y1 += kY; 
                        zm -= 2 * deltaX; 
                    }
                    X1 += kX;
                }
            }
            else
            {
                zm = -deltaY;
                while (Y1 != Y2)
                {
                    btm.SetPixel(X1, Y1, Color.Black);
                    zm += 2 * deltaX;
                    if (zm > 0) 
                    { 
                        X1 += kX; 
                        zm -= 2 * deltaY; 
                    }
                    Y1 += kY;
                }
            }
            return btm;
        }
        public Bitmap AlgorytmPrzyrostowy(Bitmap btm, int X1, int Y1, int X2, int Y2)
        {
            int x;
            int y;
            
            float deltaX, deltaY, m ;
            deltaX = X2 - X1;
            deltaY = Y2 - Y1;
            m = deltaY / deltaX;
            float yM = Y1;
            float xM = X1;
            
            if (Math.Abs(m) >= 1)
            {
                for (y = Y1; y <= Y2; y++)
                {
                    btm.SetPixel((int)Math.Floor(xM + 0.5), y, Color.Brown);
                    xM = xM + (1 / m);
                }
            }
            else
            {
                for (x = X1; x <= X2; x++)
                {
                    btm.SetPixel(x, (int)Math.Floor(yM + 0.5), Color.Brown);
                    yM += m;
                }
            }
            
            if (Y1 > Y2 || X1 > X2)
            {
                if (Math.Abs(m) >= 1)
                {
                    xM = X2;
                    for (y = Y2; y <= Y1; y++)
                    {
                        btm.SetPixel((int)Math.Floor(xM + 0.5), y, Color.Brown);
                        xM = xM + (1 / m);
                    }
                }
                else
                {
                    yM = Y2;
                    for (x = X2; x <= X1; x++)
                    {
                        btm.SetPixel(x, (int)Math.Floor(yM + 0.5), Color.Brown);
                        yM += m;
                    }
                }
            }

            return btm;
        }
    }
}
