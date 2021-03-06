﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class frmGaussianSmoothing : Form
    {
        public frmGaussianSmoothing()
        {
            InitializeComponent();
        }

        private void frmGaussianSmoothing_Load(object sender, EventArgs e)
        {
            if (Program.fileOpened != "")
            {
                Smoothing proc = new Smoothing(Program._srcBitmap);
                Bitmap _dstImage = proc.Smooth(SmoothType.GAUSSIAN_SMOOTH);
                picDest.Image = _dstImage;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Program._srcBitmap = (Bitmap)picDest.Image;
            Program.infoViewer.Refresh();
            this.Close();
        }
    }
}
