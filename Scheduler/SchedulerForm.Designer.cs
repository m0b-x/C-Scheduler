namespace Scheduler
{
    partial class SchedulerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richBoxCodInitial = new System.Windows.Forms.RichTextBox();
            this.richBoxCodFinal = new System.Windows.Forms.RichTextBox();
            this.labelScrisInitial = new System.Windows.Forms.Label();
            this.labelMovMerging = new System.Windows.Forms.Label();
            this.labelImmediateMerging = new System.Windows.Forms.Label();
            this.labelMovReabsorbtion = new System.Windows.Forms.Label();
            this.buttonIncarcaFisier = new System.Windows.Forms.Button();
            this.labelFisierIncarcat = new System.Windows.Forms.Label();
            this.buttonRepornesteAplicatia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richBoxCodInitial
            // 
            this.richBoxCodInitial.Location = new System.Drawing.Point(12, 38);
            this.richBoxCodInitial.Name = "richBoxCodInitial";
            this.richBoxCodInitial.ReadOnly = true;
            this.richBoxCodInitial.Size = new System.Drawing.Size(344, 492);
            this.richBoxCodInitial.TabIndex = 0;
            this.richBoxCodInitial.Text = "";
            // 
            // richBoxCodFinal
            // 
            this.richBoxCodFinal.Location = new System.Drawing.Point(617, 38);
            this.richBoxCodFinal.Name = "richBoxCodFinal";
            this.richBoxCodFinal.ReadOnly = true;
            this.richBoxCodFinal.Size = new System.Drawing.Size(344, 492);
            this.richBoxCodFinal.TabIndex = 1;
            this.richBoxCodFinal.Text = "";
            // 
            // labelScrisInitial
            // 
            this.labelScrisInitial.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelScrisInitial.Location = new System.Drawing.Point(444, 38);
            this.labelScrisInitial.Name = "labelScrisInitial";
            this.labelScrisInitial.Size = new System.Drawing.Size(111, 47);
            this.labelScrisInitial.TabIndex = 2;
            this.labelScrisInitial.Text = "Se va folosi:";
            // 
            // labelMovMerging
            // 
            this.labelMovMerging.BackColor = System.Drawing.Color.Red;
            this.labelMovMerging.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelMovMerging.Location = new System.Drawing.Point(444, 101);
            this.labelMovMerging.Name = "labelMovMerging";
            this.labelMovMerging.Size = new System.Drawing.Size(111, 48);
            this.labelMovMerging.TabIndex = 3;
            this.labelMovMerging.Text = "Mov merging";
            this.labelMovMerging.Click += new System.EventHandler(this.labelMovMerging_Click);
            // 
            // labelImmediateMerging
            // 
            this.labelImmediateMerging.BackColor = System.Drawing.Color.Red;
            this.labelImmediateMerging.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelImmediateMerging.Location = new System.Drawing.Point(440, 179);
            this.labelImmediateMerging.Name = "labelImmediateMerging";
            this.labelImmediateMerging.Size = new System.Drawing.Size(115, 48);
            this.labelImmediateMerging.TabIndex = 4;
            this.labelImmediateMerging.Text = "Immediate Merging";
            this.labelImmediateMerging.Click += new System.EventHandler(this.labelImmediateMerging_Click);
            // 
            // labelMovReabsorbtion
            // 
            this.labelMovReabsorbtion.BackColor = System.Drawing.Color.Red;
            this.labelMovReabsorbtion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelMovReabsorbtion.Location = new System.Drawing.Point(440, 258);
            this.labelMovReabsorbtion.Name = "labelMovReabsorbtion";
            this.labelMovReabsorbtion.Size = new System.Drawing.Size(115, 48);
            this.labelMovReabsorbtion.TabIndex = 5;
            this.labelMovReabsorbtion.Text = "Mov Reabsorbtion";
            this.labelMovReabsorbtion.Click += new System.EventHandler(this.labelMovReabsorbtion_Click);
            // 
            // buttonIncarcaFisier
            // 
            this.buttonIncarcaFisier.Location = new System.Drawing.Point(12, 553);
            this.buttonIncarcaFisier.Name = "buttonIncarcaFisier";
            this.buttonIncarcaFisier.Size = new System.Drawing.Size(110, 42);
            this.buttonIncarcaFisier.TabIndex = 7;
            this.buttonIncarcaFisier.Text = "Alege Fisierul";
            this.buttonIncarcaFisier.UseVisualStyleBackColor = true;
            this.buttonIncarcaFisier.Click += new System.EventHandler(this.buttonLoadFile_Click);
            // 
            // labelFisierIncarcat
            // 
            this.labelFisierIncarcat.AutoSize = true;
            this.labelFisierIncarcat.BackColor = System.Drawing.Color.Red;
            this.labelFisierIncarcat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelFisierIncarcat.Location = new System.Drawing.Point(145, 562);
            this.labelFisierIncarcat.Name = "labelFisierIncarcat";
            this.labelFisierIncarcat.Size = new System.Drawing.Size(189, 21);
            this.labelFisierIncarcat.TabIndex = 8;
            this.labelFisierIncarcat.Text = "Fisierul nu este incarcat";
            // 
            // buttonRepornesteAplicatia
            // 
            this.buttonRepornesteAplicatia.Location = new System.Drawing.Point(429, 553);
            this.buttonRepornesteAplicatia.Name = "buttonRepornesteAplicatia";
            this.buttonRepornesteAplicatia.Size = new System.Drawing.Size(126, 34);
            this.buttonRepornesteAplicatia.TabIndex = 9;
            this.buttonRepornesteAplicatia.Text = "Reporneste app.";
            this.buttonRepornesteAplicatia.UseVisualStyleBackColor = true;
            this.buttonRepornesteAplicatia.Click += new System.EventHandler(this.buttonRepornesteAplicatia_Click);
            // 
            // SchedulerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 597);
            this.Controls.Add(this.buttonRepornesteAplicatia);
            this.Controls.Add(this.labelFisierIncarcat);
            this.Controls.Add(this.buttonIncarcaFisier);
            this.Controls.Add(this.labelMovReabsorbtion);
            this.Controls.Add(this.labelImmediateMerging);
            this.Controls.Add(this.labelMovMerging);
            this.Controls.Add(this.labelScrisInitial);
            this.Controls.Add(this.richBoxCodFinal);
            this.Controls.Add(this.richBoxCodInitial);
            this.Name = "SchedulerForm";
            this.Text = "Scheduler";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox richBoxCodInitial;
        private RichTextBox richBoxCodFinal;
        private Label labelScrisInitial;
        private Label labelMovMerging;
        private Label labelImmediateMerging;
        private Label labelMovReabsorbtion;
        private Button buttonIncarcaFisier;
        private Label labelFisierIncarcat;
        private Button buttonRepornesteAplicatia;
    }
}