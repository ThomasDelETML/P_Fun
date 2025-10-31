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

        private class SeriesData
        {
            public string Name { get; set; }
            public double[] XValues { get; set; }
            public double[] YValues { get; set; }
        }

        private List<SeriesData> allSeriesData = new List<SeriesData>();

        private enum ChartType
        {
            Line,
            Scatter,
            Bar
        }

        private ChartType currentChartType = ChartType.Line;

        private List<double> currentX;
        private Dictionary<string, List<double>> currentData;

        public Form1()
        {
            InitializeComponent();

            formsPlot1.MouseMove += formsPlot1_MouseMove;

            if (File.Exists(csvFilePath))
                LoadCsvAndPlot(csvFilePath);
            else
                MessageBox.Show("Le fichier data.csv n'existe pas.");
        }

        // --- VALIDATION & COMPARAISONS --------------------------------------------

        private bool FileCompare(string file1, string file2)
        {
            if (string.Equals(file1, file2, StringComparison.OrdinalIgnoreCase)) return true;

            using (var fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (fs1.Length != fs2.Length) return false;

                int b1, b2;
                do
                {
                    b1 = fs1.ReadByte();
                    b2 = fs2.ReadByte();
                } while (b1 == b2 && b1 != -1);

                return b1 == b2;
            }
        }

        private bool TryReadCsvStrict(
            string path,
            out List<double> x,
            out Dictionary<string, List<double>> data,
            out string[] headers,
            out string errorMessage)
        {
            x = new List<double>();
            data = new Dictionary<string, List<double>>();
            headers = new string[0];
            errorMessage = string.Empty;

            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    if (!csv.Read() || !csv.ReadHeader())
                    {
                        errorMessage = "En-têtes manquants ou fichier vide.";
                        return false;
                    }

                    headers = csv.HeaderRecord ?? new string[0];
                    if (headers.Length < 2)
                    {
                        errorMessage = "Le CSV doit contenir au minimum 2 colonnes (X puis au moins une série).";
                        return false;
                    }

                    // C# 7.3: pas de LINQ inline dans foreach
                    for (int i = 1; i < headers.Length; i++)
                        data[headers[i]] = new List<double>();

                    int row = 1; // 1 = après la ligne d'en-têtes
                    while (csv.Read())
                    {
                        row++;

                        var rawX = csv.GetField(headers[0]);
                        if (string.IsNullOrWhiteSpace(rawX))
                        {
                            errorMessage = "Valeur vide en colonne '" + headers[0] + "' ligne " + row + ".";
                            return false;
                        }

                        double xVal;
                        if (!double.TryParse(rawX, NumberStyles.Float, CultureInfo.InvariantCulture, out xVal))
                        {
                            errorMessage = "Mauvais type en colonne '" + headers[0] + "' ligne " + row + " ('" + rawX + "').";
                            return false;
                        }

                        if (double.IsNaN(xVal) || double.IsInfinity(xVal))
                        {
                            errorMessage = "Valeur impossible en colonne '" + headers[0] + "' ligne " + row + " ('" + rawX + "').";
                            return false;
                        }

                        x.Add(xVal);

                        var keys = new List<string>(data.Keys);
                        foreach (var key in keys)
                        {
                            var raw = csv.GetField(key);
                            if (string.IsNullOrWhiteSpace(raw))
                            {
                                errorMessage = "Valeur vide en colonne '" + key + "' ligne " + row + ".";
                                return false;
                            }

                            double y;
                            if (!double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out y))
                            {
                                errorMessage = "Mauvais type en colonne '" + key + "' ligne " + row + " ('" + raw + "').";
                                return false;
                            }

                            if (double.IsNaN(y) || double.IsInfinity(y))
                            {
                                errorMessage = "Valeur impossible en colonne '" + key + "' ligne " + row + " ('" + raw + "').";
                                return false;
                            }

                            data[key].Add(y);
                        }
                    }

                    foreach (var kv in data)
                    {
                        if (kv.Value.Count != x.Count)
                        {
                            errorMessage = "Longueur incohérente pour la série '" + kv.Key + "'.";
                            return false;
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Erreur de lecture du CSV : " + ex.Message;
                return false;
            }
        }

        private bool DatasetEquals(
            string[] newHeaders,
            List<double> newX,
            Dictionary<string, List<double>> newData)
        {
            if (currentX == null || currentData == null) return false;

            var currentHeaders = new List<string>();
            currentHeaders.Add("X");
            currentHeaders.AddRange(currentData.Keys);

            var normalizedNew = new List<string>();
            normalizedNew.Add("X");
            for (int i = 1; i < newHeaders.Length; i++)
                normalizedNew.Add(newHeaders[i]);

            if (!currentHeaders.SequenceEqual(normalizedNew))
                return false;

            if (!currentX.SequenceEqual(newX))
                return false;

            foreach (var key in currentData.Keys)
            {
                List<double> ys;
                if (!newData.TryGetValue(key, out ys)) return false;
                if (!currentData[key].SequenceEqual(ys)) return false;
            }
            return true;
        }

        // -----------------------------------------------------------------------

        private void LoadCsvAndPlot(string path)
        {
            try
            {
                List<double> x;
                Dictionary<string, List<double>> data;
                string[] headers;
                string error;

                if (!TryReadCsvStrict(path, out x, out data, out headers, out error))
                {
                    MessageBox.Show(error, "Import CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                currentX = x;
                currentData = data;
                RedrawChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement du CSV : " + ex.Message, "Import CSV",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxChartType.Items.AddRange(new string[] { "Line", "Scatter", "Bar" });
            comboBoxChartType.SelectedIndex = 0;
        }

        private void chart1_Click(object sender, EventArgs e)
        {
        }

        private void btnChargerCSV_Click(object sender, EventArgs e)
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

                    if (File.Exists(csvFilePath) && FileCompare(selectedFile, csvFilePath))
                    {
                        MessageBox.Show("Le fichier sélectionné est identique au fichier déjà importé.",
                                        "Import CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    List<double> newX;
                    Dictionary<string, List<double>> newData;
                    string[] newHeaders;
                    string error;

                    if (!TryReadCsvStrict(selectedFile, out newX, out newData, out newHeaders, out error))
                    {
                        MessageBox.Show(error, "Import CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (currentX != null && currentData != null && DatasetEquals(newHeaders, newX, newData))
                    {
                        MessageBox.Show("Aucun changement détecté dans les en-têtes et les données. Import annulé.",
                                        "Import CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    File.Copy(selectedFile, csvFilePath, true);
                    currentX = newX;
                    currentData = newData;
                    RedrawChart();

                    MessageBox.Show("Données importées avec succès.", "Import CSV",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ChargerCSV_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {
        }

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
                LabelInfo.Text = string.Format("{0} | Year: {1:F0} | Value: {2:F2}", matchedName, matchedX, matchedY);
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

        private void comboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBoxChartType.SelectedItem.ToString();
            if (selected == "Line") currentChartType = ChartType.Line;
            else if (selected == "Scatter") currentChartType = ChartType.Scatter;
            else if (selected == "Bar") currentChartType = ChartType.Bar;

            RedrawChart();
        }

        private void RedrawChart()
        {
            formsPlot1.Plot.Clear();
            allSeriesData.Clear();

            if (currentX == null || currentData == null || currentData.Count == 0)
            {
                formsPlot1.Refresh();
                return;
            }

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
