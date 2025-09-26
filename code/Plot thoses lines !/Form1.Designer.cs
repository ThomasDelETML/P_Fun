namespace Plot_thoses_lines__
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnChargerCSV = new System.Windows.Forms.Button();
            this.ChargerCSV = new System.Windows.Forms.OpenFileDialog();
            this.formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            this.mtbCsvName = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // btnChargerCSV
            // 
            this.btnChargerCSV.Location = new System.Drawing.Point(873, 7);
            this.btnChargerCSV.Name = "btnChargerCSV";
            this.btnChargerCSV.Size = new System.Drawing.Size(112, 21);
            this.btnChargerCSV.TabIndex = 1;
            this.btnChargerCSV.Text = "Import a Chart";
            this.btnChargerCSV.UseVisualStyleBackColor = true;
            this.btnChargerCSV.Click += new System.EventHandler(this.btnChargerCSV_Click);
            // 
            // ChargerCSV
            // 
            this.ChargerCSV.FileName = "ChargerCSV";
            this.ChargerCSV.FileOk += new System.ComponentModel.CancelEventHandler(this.ChargerCSV_FileOk);
            // 
            // formsPlot1
            // 
            this.formsPlot1.DisplayScale = 0F;
            this.formsPlot1.Location = new System.Drawing.Point(-1, 34);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(993, 573);
            this.formsPlot1.TabIndex = 2;
            this.formsPlot1.Load += new System.EventHandler(this.formsPlot1_Load);
            // 
            // mtbCsvName
            // 
            this.mtbCsvName.Location = new System.Drawing.Point(428, 7);
            this.mtbCsvName.MaximumSize = new System.Drawing.Size(200, 50);
            this.mtbCsvName.MinimumSize = new System.Drawing.Size(4, 20);
            this.mtbCsvName.Name = "mtbCsvName";
            this.mtbCsvName.Size = new System.Drawing.Size(200, 20);
            this.mtbCsvName.TabIndex = 3;
            this.mtbCsvName.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.mtbCsvName_MaskInputRejected);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 606);
            this.Controls.Add(this.mtbCsvName);
            this.Controls.Add(this.formsPlot1);
            this.Controls.Add(this.btnChargerCSV);
            this.Name = "Form1";
            this.Text = "PlotThosesLines";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnChargerCSV;
        private System.Windows.Forms.OpenFileDialog ChargerCSV;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private System.Windows.Forms.MaskedTextBox mtbCsvName;
    }
}

