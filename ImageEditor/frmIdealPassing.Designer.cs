namespace ImageEditor
{
    partial class frmIdealPassing
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
            this.picFT = new System.Windows.Forms.PictureBox();
            this.tbThreshold = new System.Windows.Forms.TrackBar();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picDest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // picDest
            // 
            this.picDest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picDest.Location = new System.Drawing.Point(12, 12);
            this.picDest.Name = "picDest";
            this.picDest.Size = new System.Drawing.Size(435, 377);
            this.picDest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picDest.TabIndex = 7;
            this.picDest.TabStop = false;
            // 
            // picFT
            // 
            this.picFT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picFT.Location = new System.Drawing.Point(453, 28);
            this.picFT.Name = "picFT";
            this.picFT.Size = new System.Drawing.Size(254, 189);
            this.picFT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picFT.TabIndex = 8;
            this.picFT.TabStop = false;
            // 
            // tbThreshold
            // 
            this.tbThreshold.Location = new System.Drawing.Point(509, 256);
            this.tbThreshold.Maximum = 255;
            this.tbThreshold.Name = "tbThreshold";
            this.tbThreshold.Size = new System.Drawing.Size(198, 45);
            this.tbThreshold.TabIndex = 9;
            this.tbThreshold.TickFrequency = 16;
            this.tbThreshold.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbThreshold_KeyUp);
            this.tbThreshold.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbThreshold_MouseUp);
            // 
            // cmbMode
            // 
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Items.AddRange(new object[] {
            "Low Pass",
            "High Pass"});
            this.cmbMode.Location = new System.Drawing.Point(509, 229);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(198, 21);
            this.cmbMode.TabIndex = 10;
            this.cmbMode.SelectedIndexChanged += new System.EventHandler(this.cmbMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(453, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Complex Image:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(453, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Mode:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(453, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Threshold:";
            // 
            // frmIdealPassing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 402);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.tbThreshold);
            this.Controls.Add(this.picFT);
            this.Controls.Add(this.picDest);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIdealPassing";
            this.Text = "Ideal Low/High Pass";
            this.Load += new System.EventHandler(this.frmDFT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picDest;
        private System.Windows.Forms.PictureBox picFT;
        private System.Windows.Forms.TrackBar tbThreshold;
        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}