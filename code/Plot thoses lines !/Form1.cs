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

            // Initialisation du Chart
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // Incliner les labels si les années sont longues
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void btnChargerCSV_Click(object sender, EventArgs e) //button
        {

        }

        private void ChargerCSV_FileOk(object sender, CancelEventArgs e) //openFileDialog
        {

        }
    }
}
