/*********************************************************************
 * 
 *      Project: Contrast Stretching for Images
 *      --------------------------------------------------------------
 *      Author: T M L P Samarakoon
 *      Index No.: 08020574
 *      Reg. No. : 2008/ICT/057
 *      --------------------------------------------------------------
 *      Date: Wednesday, September 22, 2010
 *
 ********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using HistogramController;

namespace ImageEditor
{
    public partial class frmImageEditor : Form
    {
        public frmImageEditor()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open dialog box to choose file
            dlgSrc.ShowDialog();                                                // display the Open File dialog box
            Program.fileOpened = dlgSrc.FileName;

            // display the image/histogram
            if (Program.fileOpened != "" && File.Exists(Program.fileOpened))                // check the file name is valid
            {
                // load the file to the 1st picture box
                Program._srcBitmap = new Bitmap(@Program.fileOpened);

                Program.infoViewer.Refresh();

                propertyGrid1.SelectedObject = Program.infoViewer;
            }
        }

        public void RefreshHistogram()
        {
            // display the histogram
            Program.displayHistogram(Program._srcBitmap, ref histSrc);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog(this);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            dlgSave.ShowDialog();
            string filename = dlgSave.FileName;

            try
            {
                Program._srcBitmap.Save(filename);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to rewrite file. Please verify that the file is not opened by any other application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void contrastStretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmContrastStretch().ShowDialog(this);
        }

        private void equalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmHistEqualize().ShowDialog(this);
        }

        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmMeanSmoothing().ShowDialog(this);
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmMedianSmoothing().ShowDialog(this);
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSharpen().ShowDialog(this);
        }

        private void gaussianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmGaussianSmoothing().ShowDialog(this);
        }

        private void dFTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmIdealPassing().ShowDialog(this);
        }

        private void spatialToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSobel().ShowDialog(this);
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmGrayscale().ShowDialog(this);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Program.fileOpened = "";
            picSrc.Image = null;
            histSrc.ClearHistogram();
            Program.infoViewer.Refresh();
        }

        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmCanny().ShowDialog(this);
        }

        private void btnGrad_Click(object sender, EventArgs e)
        {
        }
    }
}
