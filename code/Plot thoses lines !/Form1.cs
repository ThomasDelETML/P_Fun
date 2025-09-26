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
using System.Globalization;
using CsvHelper;
using ScottPlot;

namespace Plot_thoses_lines__
{
    public partial class Form1 : Form
    {
        private string csvFilePath = Path.Combine(Application.StartupPath, "data.csv");
        public Form1()
        {
            InitializeComponent();

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

                    var xValues = new List<double>();
                    var data = new Dictionary<string, List<double>>();

                    // Première colonne = X (ex: années ou temps), on saute
                    foreach (var header in headers.Skip(1))
                        data[header] = new List<double>();

                    while (csv.Read())
                    {
                        if (double.TryParse(csv.GetField(headers[0]), out double x))
                            xValues.Add(x);
                        else
                            xValues.Add(double.NaN);

                        foreach (var key in data.Keys.ToList())
                        {
                            if (double.TryParse(csv.GetField(key), out double val))
                                data[key].Add(val);
                            else
                                data[key].Add(double.NaN);
                        }
                    }

                    double[] dataX = xValues.ToArray();

                    // Nettoie l'ancien graphe
                    formsPlot1.Plot.Clear();

                    foreach (var kvp in data)
                    {
                        double[] dataY = kvp.Value.ToArray();
                        var scatter = formsPlot1.Plot.Add.Scatter(dataX, dataY);
                        scatter.LegendText = kvp.Key; // Nom de la série = entête CSV
                    }

                    formsPlot1.Plot.Title("Mesures depuis CSV");
                    formsPlot1.Plot.XLabel(headers[0]);
                    formsPlot1.Plot.YLabel("Valeurs");
                    formsPlot1.Plot.Legend.IsVisible = true;

                    formsPlot1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement du CSV : " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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

        private void ChargerCSV_FileOk(object sender, CancelEventArgs e) //openFileDialog
        {

        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }
    }
}
