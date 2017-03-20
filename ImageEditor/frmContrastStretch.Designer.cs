namespace ImageEditor
{
    partial class frmContrastStretch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picDest = new System.Windows.Forms.PictureBox();
            this.histDest = new HistogramController.HistogramController();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picDest)).BeginInit();
            this.SuspendLayout();
            // 
            // picDest
            // 
            this.picDest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picDest.Location = new System.Drawing.Point(12, 12);
            this.picDest.Name = "picDest";
            this.picDest.Size = new System.Drawing.Size(557, 342);
            this.picDest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDest.TabIndex = 1;
            this.picDest.TabStop = false;
            // 
            // histDest
            // 
            this.histDest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.histDest.HistColor = System.Drawing.Color.Black;
            this.histDest.Location = new System.Drawing.Point(12, 369);
            this.histDest.Name = "histDest";
            this.histDest.Offset = 10;
            this.histDest.Size = new System.Drawing.Size(557, 230);
            this.histDest.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(575, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(575, 12);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // frmContrastStretch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 611);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.histDest);
            this.Controls.Add(this.picDest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmContrastStretch";
            this.ShowInTaskbar = false;
            this.Text = "Contrast Stretch";
            this.Load += new System.EventHandler(this.frmContrastStretch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picDest;
        private HistogramController.HistogramController histDest;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
    }
}