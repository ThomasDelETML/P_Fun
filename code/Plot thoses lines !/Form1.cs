using CsvHelper;
using ScottPlot;
using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace Plot_thoses_lines__
{
    public partial class Form1 : Form
    {
        private string csvFilePath = Path.Combine(Application.StartupPath, "data.csv");
        //Prit de la doc ScottPlot
        private class SeriesData
        {
            public string Name { get; set; }
            public double[] XValues { get; set; }
            public double[] YValues { get; set; }
        }
        private List<SeriesData> allSeriesData = new List<SeriesData>();

        //Mémorise le type de graphique actuellement sélctionné
        private enum ChartType
        {
            Line,
            Scatter,
            Bar
        }
        private ChartType currentChartType = ChartType.Line;
        private List<double> currentX;
        private Dictionary<string, List<double>> currentData;
        //a ici


        public Form1()
        {
            InitializeComponent();

            formsPlot1.MouseMove += formsPlot1_MouseMove;

            // Charge le CSV s'il existe déjà
            if (File.Exists(csvFilePath))
                LoadCsvAndPlot(csvFilePath);
            else
                MessageBox.Show("Le fichier data.csv n'existe pas.");
        }

        private void LoadCsvAndPlot(string path)
        {
            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    var headers = csv.HeaderRecord;

                    currentX = new List<double>();
                    currentData = new Dictionary<string, List<double>>();

                    foreach (var header in headers.Skip(1))
                        currentData[header] = new List<double>();

                    while (csv.Read())
                    {
                        if (double.TryParse(csv.GetField(headers[0]), out double x))
                            currentX.Add(x);
                        else
                            currentX.Add(double.NaN);

                        foreach (var key in currentData.Keys.ToList())
                        {
                            if (double.TryParse(csv.GetField(key), out double val))
                                currentData[key].Add(val);
                            else
                                currentData[key].Add(double.NaN);
                        }
                    }
                }

                RedrawChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement du CSV : " + ex.Message);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //Permet de choisir le type de graphique
            comboBoxChartType.Items.AddRange(new string[] { "Line", "Scatter", "Bar" });
            comboBoxChartType.SelectedIndex = 0;

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void btnChargerCSV_Click(object sender, EventArgs e) //button
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"C:\";
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;

                    // copie dans le dossier de l'application
                    File.Copy(selectedFile, csvFilePath, true);

                    LoadCsvAndPlot(csvFilePath);
                }
            }
        }

        private void ChargerCSV_FileOk(object sender, CancelEventArgs e) 
        {

        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }

        //Prit de la doc officiel de ScottPlot : https://github.com/ScottPlot/ScottPlot/blob/main/src/ScottPlot5/ScottPlot5%20Demos/ScottPlot5%20WinForms%20Demo/Demos/ShowValueOnHover.cs
        private void formsPlot1_MouseMove(object sender, MouseEventArgs e)
        {
            var mouseCoord = formsPlot1.Plot.GetCoordinates(e.X, e.Y);
            double minDistance = double.MaxValue;
            double matchedX = double.NaN;
            double matchedY = double.NaN;
            string matchedName = "";

            foreach (var series in allSeriesData)
            {
                for (int i = 0; i < series.XValues.Length; i++)
                {
                    if (double.IsNaN(series.XValues[i]) || double.IsNaN(series.YValues[i]))
                        continue;

                    var pointPixel = formsPlot1.Plot.GetPixel(new ScottPlot.Coordinates(series.XValues[i], series.YValues[i]));
                    double dx = pointPixel.X - e.X;
                    double dy = pointPixel.Y - e.Y;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance < minDistance && distance < 50)
                    {
                        minDistance = distance;
                        matchedX = series.XValues[i];
                        matchedY = series.YValues[i];
                        matchedName = series.Name;
                    }
                }
            }

            if (double.IsNaN(matchedX) || double.IsNaN(matchedY))
            {
                LabelInfo.Text = "";
                formsPlot1.Cursor = Cursors.Default;
            }
            else
            {
                LabelInfo.Text = $"{matchedName} | Year: {matchedX:F0} | Value: {matchedY:F2}";
                formsPlot1.Cursor = Cursors.Hand;
            }
        }

        private void ChangeTitle_TextChanged(object sender, EventArgs e)
        {
                string chartTitle = ChangeTitle.Text;
                formsPlot1.Plot.Title(ChangeTitle.Text);
                formsPlot1.Refresh();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void LabelInfo_Click(object sender, EventArgs e)
        {

        }

        //Redessine le graphique selon le choix de l'user
        private void comboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBoxChartType.SelectedItem.ToString();
            if (selected == "Line") currentChartType = ChartType.Line;
            else if (selected == "Scatter") currentChartType = ChartType.Scatter;
            else if (selected == "Bar") currentChartType = ChartType.Bar;

            RedrawChart();
        }

        //Redessine le graphique selon le type sélectionné en utilisant les données déjà chargées, sans relire le CSV.
        //Met aussi à jour la légende et le survol des points.
        private void RedrawChart()
        {
            formsPlot1.Plot.Clear();
            allSeriesData.Clear();

            double[] xVals = currentX.ToArray();

            foreach (var kvp in currentData)
            {
                double[] yVals = kvp.Value.ToArray();

                switch (currentChartType)
                {
                    case ChartType.Line:
                        var line = formsPlot1.Plot.Add.Scatter(xVals, yVals);
                        line.LegendText = kvp.Key;
                        line.LineWidth = 2;
                        break;

                    case ChartType.Scatter:
                        var scatter = formsPlot1.Plot.Add.Scatter(xVals, yVals);
                        scatter.LegendText = kvp.Key;
                        scatter.LineWidth = 0;
                        scatter.MarkerSize = 5;
                        break;

                    case ChartType.Bar:
                        List<ScottPlot.Bar> bars = new List<ScottPlot.Bar>();
                        for (int i = 0; i < xVals.Length; i++)
                        {
                            bars.Add(new ScottPlot.Bar()
                            {
                                Position = xVals[i],
                                Value = yVals[i],
                                Label = kvp.Key
                            });
                        }
                        formsPlot1.Plot.Add.Bars(bars);
                        break;
                }

                allSeriesData.Add(new SeriesData
                {
                    Name = kvp.Key,
                    XValues = xVals,
                    YValues = yVals
                });
            }

            formsPlot1.Plot.Legend.IsVisible = true;
            formsPlot1.Refresh();
        }

    }
}
