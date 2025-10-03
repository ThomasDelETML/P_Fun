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
            this.components = new System.ComponentModel.Container();
            this.btnChargerCSV = new System.Windows.Forms.Button();
            this.ChargerCSV = new System.Windows.Forms.OpenFileDialog();
            this.formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            this.ChangeTitle = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.LabelInfo = new System.Windows.Forms.Label();
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
            // ChangeTitle
            // 
            this.ChangeTitle.Location = new System.Drawing.Point(426, 7);
            this.ChangeTitle.Name = "ChangeTitle";
            this.ChangeTitle.Size = new System.Drawing.Size(201, 20);
            this.ChangeTitle.TabIndex = 3;
            this.ChangeTitle.TextChanged += new System.EventHandler(this.ChangeTitle_TextChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // LabelInfo
            // 
            this.LabelInfo.AutoSize = true;
            this.LabelInfo.Location = new System.Drawing.Point(13, 7);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(51, 13);
            this.LabelInfo.TabIndex = 4;
            this.LabelInfo.Text = "LabelInfo";
            this.LabelInfo.Click += new System.EventHandler(this.LabelInfo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 606);
            this.Controls.Add(this.LabelInfo);
            this.Controls.Add(this.ChangeTitle);
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
        private System.Windows.Forms.TextBox ChangeTitle;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label LabelInfo;
    }
}

