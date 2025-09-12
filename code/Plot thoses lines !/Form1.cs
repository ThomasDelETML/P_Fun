using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
using System.IO;

namespace Plot_thoses_lines__
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Création d'une série temporelle
            var serie = new Series("Mesures");
            serie.ChartType = SeriesChartType.Line; // Ligne pour le temps
            serie.XValueType = ChartValueType.DateTime; // Axe X = DateTime

            // Ajouter quelques données fictives
            serie.Points.AddXY(DateTime.Now.AddMinutes(-4), 10);
            serie.Points.AddXY(DateTime.Now.AddMinutes(-3), 20);
            serie.Points.AddXY(DateTime.Now.AddMinutes(-2), 15);
            serie.Points.AddXY(DateTime.Now.AddMinutes(-1), 25);
            serie.Points.AddXY(DateTime.Now, 18);

            // Ajouter la série au Chart
            chart1.Series.Clear();
            chart1.Series.Add(serie);

            // Configurer les axes
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss"; // Format des heures
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

            // Optionnel : défilement/zoom
            chart1.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
