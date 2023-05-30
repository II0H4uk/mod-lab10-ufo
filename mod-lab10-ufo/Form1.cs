using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;


namespace mod_lab10_ufo {
    public partial class Form1 : Form {

        List<PointF> points = new List<PointF>();
        PointF start;
        PointF end;

        int stepCount = 10;
        int currStep;
        int allowErr = 10;
        bool isFinished = false;

        int pointRad = 5;

        public Form1() {
            InitializeComponent();
            timer1.Interval = 500;
            textBox1.Text = stepCount.ToString();
            textBox2.Text = allowErr.ToString();
            textBox3.Text = "0 0";
            textBox4.Text = "800 800";

            Reset();
        }

        void Line(PointF start, PointF end, int seriesLength = 1) {
            points.Clear();

            PointF currentPoint = start;
            points.Add(currentPoint);

            double angle = TrigonometricFunc.Atan(Math.Abs(end.Y - start.Y) / Math.Abs(end.X - start.X), seriesLength);

            double distance = CalcDist(start, end);
            double stepVal = distance / stepCount;

            for(int i = 0; i < stepCount; i++) {
                currentPoint.X += (float)(stepVal * TrigonometricFunc.Cos(angle, seriesLength));
                currentPoint.Y += (float)(stepVal * TrigonometricFunc.Sin(angle, seriesLength));

                points.Add(currentPoint);
            }
        }

        double CalcDist(PointF start, PointF end) {
            return Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
        }

        List<double> ErrCalc() {
            List<double> errors = new List<double>();
            for (int i = 1; i < 50; i++) {
                Line(start, end, i);
                errors.Add(CalcDist(points[points.Count - 1], end));
            }
            return errors;
        }

        private void Reset() {
            string[] numBuf = textBox3.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            start = new PointF(int.Parse(numBuf[0]), int.Parse(numBuf[1]));
            numBuf = textBox4.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            end = new PointF(int.Parse(numBuf[0]), int.Parse(numBuf[1]));

            allowErr = int.Parse(textBox2.Text);
            stepCount = int.Parse(textBox1.Text);
            currStep = 0;
            isFinished = false;

            Line(start, end, 40);
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.ScaleTransform(0.5f, 0.5f);

            Pen pen = new Pen(Color.Black, 5);

            g.DrawLine(new Pen(Color.Black, 2), points[0], points[points.Count - 1]);
            g.DrawEllipse(pen, points[currStep].X, points[currStep].Y, pointRad, pointRad);
            currStep++;
            if (currStep == points.Count)
                isFinished = true;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (!isFinished)
             panel1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e) {
            if(timer1.Enabled)
                timer1.Stop();
            else
                timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e) {
            Reset();
        }

        private void button4_Click(object sender, EventArgs e) {
            Graph graph = new Graph(ErrCalc());
            graph.Show();
        }
    }
}
