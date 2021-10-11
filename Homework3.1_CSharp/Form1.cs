using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Homework3._1_CSharp
{
    public partial class Form1 : Form
    {
        public IEnumerable<CDay> sample;
        private Timer timer = new Timer();
        public List<MyRectangle> rectangles;
        Random random = new Random();

        public double mean = 0;
        public int n = 0;
        public Form1()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;
            this.UpdateStyles();

            this.sample = null;
            this.rectangles = new List<MyRectangle>();

            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.Controls.Add(this.pictureBox1);

            timer.Tick += new EventHandler(timer1_Tick);
            timer.Interval = 20;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sample = File.ReadAllLines(@"CSV\covid_italia.csv")
                   .Skip(1)
                   .Select(x => x.Split(','))
                   .Select(x => new CDay(x[0], Int32.Parse(x[2]),
                   Int32.Parse(x[3]), Int32.Parse(x[4]), Int32.Parse(x[5]),
                   Int32.Parse(x[7]), Int32.Parse(x[8]), Int32.Parse(x[9]), Int32.Parse(x[10])));

            int columnWidth = pictureBox1.Width / sample.Count();
            foreach(CDay day in sample)
            {
                SolidBrush color = new SolidBrush(Color.FromArgb(1 * day.nuovi_positivi/80, 0, 0));
                //This simple formula allows automatic offset of rectangles
                int sX = columnWidth * rectangles.Count;
                rectangles.Add(new MyRectangle(columnWidth, day.nuovi_positivi/50, sX, 0, color, pictureBox1));
            }

            timer.Start();
            foreach (CDay day in sample)
            {
                label1.Text = "Mean: " + calculateMean(day.nuovi_positivi).ToString();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach(MyRectangle rect in rectangles)
            {
                rect.Update();
                rect.Draw(g);
            }
        }

        private double calculateMean(int nextNumber)
        {
            n += 1;
            mean = mean + ((1.0d / n) * (nextNumber - mean));
            return mean;
        }
    }
}
