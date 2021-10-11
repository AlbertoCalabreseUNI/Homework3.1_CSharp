using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3._1_CSharp
{
    public class MyRectangle
    {
        public int posX { get; set; }
        public int posY { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        private int finalHeight { get; set; }
        public Brush rectColor { get; set; }
        private Pen borderColor = Pens.Black;
        private MyPictureBox pictureBox;

        public MyRectangle(int w, int h, int x, int y, Brush color, MyPictureBox pb)
        {
            this.width = w;
            this.height = 0;
            this.finalHeight = h;
            this.posX = x;
            this.posY = pb.Height;
            this.rectColor = color;
            this.pictureBox = pb;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(this.rectColor, this.posX, this.posY, this.width, this.height);
            g.DrawRectangle(this.borderColor, this.posX, this.posY, this.width, this.height);
        }

        public void Update()
        {
            if (this.height >= this.finalHeight) return;
            height += 5;
            posY -= 5;
        }
    }
}
