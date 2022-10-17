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
            this.buttonOptimize = new System.Windows.Forms.Button();
            this.buttonLoadFile = new System.Windows.Forms.Button();
            this.loadedFileLabel = new System.Windows.Forms.Label();
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
            this.labelScrisInitial.Location = new System.Drawing.Point(444, 105);
            this.labelScrisInitial.Name = "labelScrisInitial";
            this.labelScrisInitial.Size = new System.Drawing.Size(111, 47);
            this.labelScrisInitial.TabIndex = 2;
            this.labelScrisInitial.Text = "Se va folosi:";
            // 
            // labelMovMerging
            // 
            this.labelMovMerging.BackColor = System.Drawing.Color.Red;
            this.labelMovMerging.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelMovMerging.Location = new System.Drawing.Point(440, 152);
            this.labelMovMerging.Name = "labelMovMerging";
            this.labelMovMerging.Size = new System.Drawing.Size(115, 27);
            this.labelMovMerging.TabIndex = 3;
            this.labelMovMerging.Text = "Mov merging";
            this.labelMovMerging.Click += new System.EventHandler(this.labelMovMerging_Click);
            // 
            // labelImmediateMerging
            // 
            this.labelImmediateMerging.BackColor = System.Drawing.Color.Red;
            this.labelImmediateMerging.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelImmediateMerging.Location = new System.Drawing.Point(440, 200);
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
            this.labelMovReabsorbtion.Location = new System.Drawing.Point(440, 265);
            this.labelMovReabsorbtion.Name = "labelMovReabsorbtion";
            this.labelMovReabsorbtion.Size = new System.Drawing.Size(115, 48);
            this.labelMovReabsorbtion.TabIndex = 5;
            this.labelMovReabsorbtion.Text = "Mov Reabsorbtion";
            this.labelMovReabsorbtion.Click += new System.EventHandler(this.labelMovReabsorbtion_Click);
            // 
            // buttonOptimize
            // 
            this.buttonOptimize.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonOptimize.Location = new System.Drawing.Point(440, 35);
            this.buttonOptimize.Name = "buttonOptimize";
            this.buttonOptimize.Size = new System.Drawing.Size(111, 51);
            this.buttonOptimize.TabIndex = 6;
            this.buttonOptimize.Text = "Optimizeaza";
            this.buttonOptimize.UseVisualStyleBackColor = true;
            this.buttonOptimize.Click += new System.EventHandler(this.buttonOptimize_Click);
            // 
            // buttonLoadFile
            // 
            this.buttonLoadFile.Location = new System.Drawing.Point(12, 553);
            this.buttonLoadFile.Name = "buttonLoadFile";
            this.buttonLoadFile.Size = new System.Drawing.Size(110, 42);
            this.buttonLoadFile.TabIndex = 7;
            this.buttonLoadFile.Text = "Alege Fisierul";
            this.buttonLoadFile.UseVisualStyleBackColor = true;
            this.buttonLoadFile.Click += new System.EventHandler(this.buttonLoadFile_Click);
            // 
            // loadedFileLabel
            // 
            this.loadedFileLabel.BackColor = System.Drawing.Color.Red;
            this.loadedFileLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.loadedFileLabel.Location = new System.Drawing.Point(145, 562);
            this.loadedFileLabel.Name = "loadedFileLabel";
            this.loadedFileLabel.Size = new System.Drawing.Size(192, 27);
            this.loadedFileLabel.TabIndex = 8;
            this.loadedFileLabel.Text = "Fisierul nu este incarcat";
            // 
            // SchedulerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 597);
            this.Controls.Add(this.loadedFileLabel);
            this.Controls.Add(this.buttonLoadFile);
            this.Controls.Add(this.buttonOptimize);
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

        }

        #endregion

        private RichTextBox richBoxCodInitial;
        private RichTextBox richBoxCodFinal;
        private Label labelScrisInitial;
        private Label labelMovMerging;
        private Label labelImmediateMerging;
        private Label labelMovReabsorbtion;
        private Button buttonOptimize;
        private Button buttonLoadFile;
        private Label loadedFileLabel;
    }
}