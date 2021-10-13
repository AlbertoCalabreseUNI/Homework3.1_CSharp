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
        public IEnumerable<Student> sample;
        public List<double> gpaList;
        private Timer timer = new Timer();
        public List<MyRectangle> rectangles;
        Random random = new Random();

        public double mean = 0;
        public double deviation = 0;
        public int n = 0;
        public Form1()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;
            this.UpdateStyles();

            this.sample = null;
            this.rectangles = new List<MyRectangle>();
            this.gpaList = new List<double>();

            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.Controls.Add(this.pictureBox1);

            timer.Tick += new EventHandler(timer1_Tick);
            timer.Interval = 50;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sample = File.ReadAllLines(@"CSV\satgpa.csv")
                   .Skip(1)
                   .Select(x => x.Split(','))
                   .Select(x => new Student(Int32.Parse(x[0]), double.Parse(x[5].Replace('.', ','))));

            int columnWidth = (pictureBox1.Width / sample.Count()) + 1;
            foreach(Student student in sample)
            {
                SolidBrush color = new SolidBrush(Color.FromArgb(Math.Abs((student.sex - 1) * 255), 0, Math.Abs((student.sex-2) * 255)));
                //This simple formula allows automatic offset of rectangles
                int sX = columnWidth * rectangles.Count;
                int height = Convert.ToInt32(Math.Truncate(student.getGpa() + 1)) * 50;
                rectangles.Add(new MyRectangle(columnWidth, height, sX, 0, color, pictureBox1));
            }

            timer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Student student in sample)
            {
                label1.Text = "Mean: " + calculateMean(student.getGpa()).ToString();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            gpaList.Clear();
            foreach (Student student in sample)
            {
                gpaList.Add(student.getGpa());
            }
            label2.Text = "Deviation: " + calculateDeviation(gpaList).ToString().Remove(5);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label3.Text = "Males 3+ GPA: " + calculateMaleHigh().ToString();
            label4.Text = "Females 3+ GPA: " + calculateFemaleHigh().ToString();
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
                rect.Draw(g);
                rect.Update();
            }
        }

        private double calculateMean(double nextNumber)
        {
            n += 1;
            mean = mean + ((1.0d / n) * (nextNumber - mean));
            return mean;
        }

        private double calculateDeviation(IEnumerable<double> values)
        {
            double standardDeviation = 0;

            if (values.Any())
            {
                double avg = values.Average();  
                double sum = values.Sum(d => Math.Pow(d - avg, 2));  
                standardDeviation = Math.Sqrt((sum) / (values.Count() - 1));
            }
            return standardDeviation;
        }

        private int calculateFemaleHigh()
        {
            return sample.Where(x => x.getGpa() >= 3 && x.sex == 2).Count();
        }

        private int calculateMaleHigh()
        {
            return sample.Where(x => x.getGpa() >= 3 && x.sex == 1).Count();
        }
    }

}
