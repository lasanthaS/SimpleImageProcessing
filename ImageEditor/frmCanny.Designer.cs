namespace ImageEditor
{
    partial class frmCanny
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.picDest = new System.Windows.Forms.PictureBox();
            this.tbT1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.tbT2 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.picDest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbT1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbT2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(590, 42);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(590, 13);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 15;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // picDest
            // 
            this.picDest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picDest.Location = new System.Drawing.Point(12, 12);
            this.picDest.Name = "picDest";
            this.picDest.Size = new System.Drawing.Size(557, 538);
            this.picDest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDest.TabIndex = 14;
            this.picDest.TabStop = false;
            // 
            // tbT1
            // 
            this.tbT1.LargeChange = 16;
            this.tbT1.Location = new System.Drawing.Point(590, 95);
            this.tbT1.Maximum = 255;
            this.tbT1.Name = "tbT1";
            this.tbT1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbT1.Size = new System.Drawing.Size(45, 455);
            this.tbT1.SmallChange = 0;
            this.tbT1.TabIndex = 17;
            this.tbT1.TickFrequency = 8;
            this.tbT1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbT1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbT1_KeyUp);
            this.tbT1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbT1_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(575, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Threshold:";
            // 
            // tbT2
            // 
            this.tbT2.LargeChange = 16;
            this.tbT2.Location = new System.Drawing.Point(641, 95);
            this.tbT2.Maximum = 255;
            this.tbT2.Name = "tbT2";
            this.tbT2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbT2.Size = new System.Drawing.Size(45, 455);
            this.tbT2.SmallChange = 0;
            this.tbT2.TabIndex = 19;
            this.tbT2.TickFrequency = 8;
            this.tbT2.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbT2.Value = 255;
            this.tbT2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbT2_KeyUp);
            this.tbT2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbT2_MouseUp);
            // 
            // frmCanny
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 560);
            this.Controls.Add(this.tbT2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbT1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.picDest);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCanny";
            this.Text = "Canny Edge Detection";
            this.Load += new System.EventHandler(this.frmCanny_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbT1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbT2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.PictureBox picDest;
        private System.Windows.Forms.TrackBar tbT1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbT2;
    }
}