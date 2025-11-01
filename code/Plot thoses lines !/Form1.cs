/**********************************************************************************************
 *  Développeur: Moreira Thomas
 *
 *  Projet: Concevoir un logiciel pour afficher des graphiques sur des données
 *
 *  Fichier: Form1.cs
 *  Auteur: Moreira Thomas
 *  Date: 2025.11.01
 **********************************************************************************************/

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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Plot_thoses_lines__
{
    public partial class Form1 : Form
    {
        // chemin par defaut du CSV
        private string csvFilePath = Path.Combine(Application.StartupPath, "data.csv");

        // structure pour garder un snapshot de donnees tracees
        private class SeriesData
        {
            public string Name { get; set; }
            public double[] XValues { get; set; }
            public double[] YValues { get; set; }
        }

        // stockage des series en cours pour le survol souris
        private List<SeriesData> allSeriesData = new List<SeriesData>();

        // types de chart disponibles
        private enum ChartType
        {
            Line,
            Scatter,
            Bar
        }

        // type de chart courant
        private ChartType currentChartType = ChartType.Line;

        // modele courant: X commun et dictionnaire des series
        private List<double> currentX;
        private Dictionary<string, List<double>> currentData;

        public Form1()
        {
            InitializeComponent();

            // abonne le handler de survol souris
            formsPlot1.MouseMove += formsPlot1_MouseMove;

            // charge auto data.csv si present
            if (File.Exists(csvFilePath))
                LoadCsvAndPlot(csvFilePath);
            else
                MessageBox.Show("Le fichier data.csv n'existe pas.");
        }

        // compare deux fichiers octet par octet
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

        // lecture stricte du CSV
        // - verifie entetes
        // - verifie types
        // - refuse NaN et Inf
        // - aligne toutes les colonnes Y sur X
        private bool TryReadCsvStrict(
            string path,
            out List<double> x,
            out Dictionary<string, List<double>> data,
            out string[] headers,
            out string errorMessage)
        {
            x = new List<double>();                     // collecte X
            data = new Dictionary<string, List<double>>(); // collecte Y par serie
            headers = Array.Empty<string>();            // entetes
            errorMessage = string.Empty;                // message erreur

            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    // lit entetes
                    if (!csv.Read() || !csv.ReadHeader())
                    {
                        errorMessage = "En-tetes manquants ou fichier vide.";
                        return false;
                    }

                    headers = csv.HeaderRecord ?? Array.Empty<string>();
                    if (headers.Length < 2)
                    {
                        errorMessage = "Le CSV doit contenir au minimum 2 colonnes (Year puis au moins une serie).";
                        return false;
                    }

                    // cree les listes Y pour chaque colonne apres la premiere
                    data = headers
                        .Skip(1)
                        .ToDictionary(h => h, _ => new List<double>());

                    int row = 1; // compteur de lignes apres entetes
                    while (csv.Read())
                    {
                        row++;

                        // lit X
                        var rawX = csv.GetField(headers[0]);
                        if (string.IsNullOrWhiteSpace(rawX))
                        {
                            errorMessage = "Valeur vide en colonne '" + headers[0] + "' ligne " + row + ".";
                            return false;
                        }

                        if (!double.TryParse(rawX, NumberStyles.Float, CultureInfo.InvariantCulture, out double xVal))
                        {
                            errorMessage = "Mauvais type en colonne '" + headers[0] + "' ligne " + row + " ('" + rawX + "').";
                            return false;
                        }

                        if (double.IsNaN(xVal) || double.IsInfinity(xVal))
                        {
                            errorMessage = "Valeur impossible en colonne '" + headers[0] + "' ligne " + row + " ('" + rawX + "').";
                            return false;
                        }

                        x.Add(xVal); // ajoute X

                        // lit chaque Y pour la ligne courante
                        var keys = new List<string>(data.Keys); // copie defensive
                        foreach (var key in keys)
                        {
                            var raw = csv.GetField(key);
                            if (string.IsNullOrWhiteSpace(raw))
                            {
                                errorMessage = "Valeur vide en colonne '" + key + "' ligne " + row + ".";
                                return false;
                            }

                            if (!double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out double y))
                            {
                                errorMessage = "Mauvais type en colonne '" + key + "' ligne " + row + " ('" + raw + "').";
                                return false;
                            }

                            if (double.IsNaN(y) || double.IsInfinity(y))
                            {
                                errorMessage = "Valeur impossible en colonne '" + key + "' ligne " + row + " ('" + raw + "').";
                                return false;
                            }

                            data[key].Add(y); // ajoute Y a la serie
                        }
                    }

                    // controle tailles: chaque serie doit avoir la meme longueur que X
                    // important: pas de capture de parametre out dans lambda, on clone la valeur
                    int xCount = x.Count;

                    if (data.Any(kv => kv.Value.Count != xCount))
                    {
                        // trouve la premiere serie invalide
                        var bad = data.First(kv => kv.Value.Count != xCount).Key;
                        errorMessage = "Longueur incoherente pour la serie '" + bad + "'.";
                        return false;
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

        // compare dataset courant et dataset propose
        // verifie entetes, X, et valeurs serie par serie
        private bool DatasetEquals(
            string[] newHeaders,
            List<double> newX,
            Dictionary<string, List<double>> newData)
        {
            if (currentX == null || currentData == null) return false;

            // normalise entetes
            var currentHeaders = new[] { "X" }.Concat(currentData.Keys);
            var normalizedNew = new[] { "X" }.Concat(newHeaders.Skip(1));

            if (!currentHeaders.SequenceEqual(normalizedNew))
                return false;

            if (!currentX.SequenceEqual(newX))
                return false;

            // toutes les series doivent etre identiques
            return currentData.All(kv =>
                newData.TryGetValue(kv.Key, out var ys) && kv.Value.SequenceEqual(ys));
        }

        // fusionne un nouveau dataset dans le modele courant
        // union triée de X, ecrase les recouvrements par les nouvelles valeurs
        private void MergeIntoCurrentData(List<double> newX, Dictionary<string, List<double>> newData)
        {
            // cas initial: aucun dataset courant
            if (currentX == null || currentData == null || currentData.Count == 0)
            {
                currentX = new List<double>(newX);
                currentData = new Dictionary<string, List<double>>(newData, StringComparer.OrdinalIgnoreCase);
                return;
            }

            // union triee des X
            var unionX = currentX.Concat(newX).Distinct().OrderBy(v => v).ToList();

            // index de position pour chaque X
            // v = valeur, i = index
            var idx = unionX.Select((v, i) => (v, i)).ToDictionary(t => t.v, t => t.i);

            // ensemble des noms de series
            var allSeries = currentData.Keys
                .Concat(newData.Keys)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            // prepare des buffers NaN pour toutes les series
            var merged = allSeries.ToDictionary(
                s => s,
                _ => Enumerable.Repeat(double.NaN, unionX.Count).ToArray(),
                StringComparer.OrdinalIgnoreCase);

            // injecte ancien dataset
            // on parcourt par index pour recoller X[i] a Y[i]
            // limite superieure securisee pour eviter IndexOutOfRange si donnees mal formees
            for (int i = 0; i < Math.Min(currentX.Count, currentData.Values.FirstOrDefault()?.Count ?? 0); i++)
            {
                var xval = currentX[i];
                if (!idx.TryGetValue(xval, out int j)) continue; // X inconnu

                foreach (var kv in currentData)
                {
                    if (i < kv.Value.Count) merged[kv.Key][j] = kv.Value[i];
                }
            }

            // injecte nouveau dataset en ecrasant les recouvrements
            for (int i = 0; i < Math.Min(newX.Count, newData.Values.FirstOrDefault()?.Count ?? 0); i++)
            {
                var xval = newX[i];
                if (!idx.TryGetValue(xval, out int j)) continue;

                foreach (var kv in newData)
                {
                    if (i < kv.Value.Count) merged[kv.Key][j] = kv.Value[i];
                }
            }

            // reconstruit l etat courant
            currentX = unionX;
            currentData = merged.ToDictionary(
                kv => kv.Key,
                kv => kv.Value.ToList(),
                StringComparer.OrdinalIgnoreCase);
        }

        // charge un CSV et redraw
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

                // remplace le dataset courant
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

        // init du combobox de type de chart
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxChartType.Items.AddRange(new string[] { "Line", "Scatter", "Bar" });
            comboBoxChartType.SelectedIndex = 0;
        }

        // handler bouton charger CSV
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

                    // refuse le meme fichier
                    if (File.Exists(csvFilePath) && FileCompare(selectedFile, csvFilePath))
                    {
                        MessageBox.Show("Le fichier selectionne est identique au fichier deja importe.",
                                        "Import CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // lit le nouveau dataset
                    if (!TryReadCsvStrict(selectedFile, out var newX, out var newData, out var newHeaders, out var error))
                    {
                        MessageBox.Show(error, "Import CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // annule si rien ne change logiquement
                    if (currentX != null && currentData != null && DatasetEquals(newHeaders, newX, newData))
                    {
                        MessageBox.Show("Aucun changement detecte dans les entetes et les donnees. Import annule.",
                                        "Import CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // fusionne dans le modele courant
                    MergeIntoCurrentData(newX, newData);

                    // persiste le nouveau fichier comme base
                    File.Copy(selectedFile, csvFilePath, true);

                    // redraw
                    RedrawChart();

                    MessageBox.Show("Donnees importees avec succes.", "Import CSV",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // survol souris: cherche le point le plus proche
        private void formsPlot1_MouseMove(object sender, MouseEventArgs e)
        {
            // genere toutes les combinaisons (serie, index)
            // calcule la distance pixel souris -> point
            var nearest = allSeriesData
                .SelectMany(s => Enumerable.Range(0, s.XValues.Length)
                    .Select(i => new
                    {
                        s.Name,
                        X = s.XValues[i],
                        Y = s.YValues[i],
                        Pixel = formsPlot1.Plot.GetPixel(new ScottPlot.Coordinates(s.XValues[i], s.YValues[i]))
                    }))
                .Where(p => !double.IsNaN(p.X) && !double.IsNaN(p.Y))
                .Select(p => new
                {
                    p.Name,
                    p.X,
                    p.Y,
                    Dist = Math.Sqrt(Math.Pow(p.Pixel.X - e.X, 2) + Math.Pow(p.Pixel.Y - e.Y, 2))
                })
                .OrderBy(p => p.Dist)
                .FirstOrDefault();

            // aucun point proche
            if (nearest == null || nearest.Dist >= 50)
            {
                LabelInfo.Text = "";
                formsPlot1.Cursor = Cursors.Default;
            }
            else
            {
                // point proche: maj label et curseur
                LabelInfo.Text = string.Format("{0} | Year: {1:F0} | Value: {2:F2}", nearest.Name, nearest.X, nearest.Y);
                formsPlot1.Cursor = Cursors.Hand;
            }
        }

        // change le titre du graphique
        private void ChangeTitle_TextChanged(object sender, EventArgs e)
        {
            formsPlot1.Plot.Title(ChangeTitle.Text);
            formsPlot1.Refresh();
        }

        // change le type de graphique
        private void comboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBoxChartType.SelectedItem.ToString();
            if (selected == "Line") currentChartType = ChartType.Line;
            else if (selected == "Scatter") currentChartType = ChartType.Scatter;
            else if (selected == "Bar") currentChartType = ChartType.Bar;

            RedrawChart();
        }

        // reconstruit le graphique a partir de currentX et currentData
        private void RedrawChart()
        {
            formsPlot1.Plot.Clear();     // vide le plot
            allSeriesData.Clear();       // vide le cache de survol

            if (currentX == null || currentData == null || currentData.Count == 0)
            {
                formsPlot1.Refresh();
                return;
            }

            // convertit X en tableau pour ScottPlot
            double[] xVals = currentX.ToArray();

            // trace chaque serie
            foreach (var kvp in currentData)
            {
                double[] yVals = kvp.Value.ToArray();

                switch (currentChartType)
                {
                    case ChartType.Line:
                        // trace en ligne
                        var line = formsPlot1.Plot.Add.Scatter(xVals, yVals);
                        line.LegendText = kvp.Key; // nom de serie
                        line.LineWidth = 2;
                        line.MarkerSize = 0;
                        break;

                    case ChartType.Scatter:
                        // trace en nuage de points
                        var scatter = formsPlot1.Plot.Add.Scatter(xVals, yVals);
                        scatter.LegendText = kvp.Key;
                        scatter.LineWidth = 0; // pas de ligne
                        scatter.MarkerSize = 5;
                        break;

                    case ChartType.Bar:
                        // construit les barres une par une
                        var bars = new List<ScottPlot.Bar>();
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

                // met a jour le cache pour le survol
                allSeriesData.Add(new SeriesData
                {
                    Name = kvp.Key,
                    XValues = xVals,
                    YValues = yVals
                });
            }

            // affiche la legende
            formsPlot1.Plot.Legend.IsVisible = true;

            // rafraichit
            formsPlot1.Refresh();
        }

        // handlers vides pour le designer
        private void chart1_Click(object sender, EventArgs e) { }
        private void ChargerCSV_FileOk(object sender, CancelEventArgs e) { }
        private void formsPlot1_Load(object sender, EventArgs e) { }
        private void toolTip1_Popup(object sender, PopupEventArgs e) { }
        private void LabelInfo_Click(object sender, EventArgs e) { }
    }
}
