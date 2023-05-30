using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace mod_lab10_ufo
{
    public partial class Graph : Form
    {
        public Graph(List<double> errors)
        {
            InitializeComponent();

            chart1.Series.Add("График ошибки");
            chart1.Series[0].ChartType = SeriesChartType.Line;
            for (int i = 0; i < errors.Count; i++) {
                chart1.Series[0].Points.AddXY(i, errors[i]);
            }
        }
    }
}
