using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using HistogramController;

namespace ImageEditor
{
    public partial class frmHistEqualize : Form
    {
        public frmHistEqualize()
        {
            InitializeComponent();
        }

        private void frmHistEqualize_Load(object sender, EventArgs e)
        {
            if (Program.fileOpened != "")
            {
                HistogramEqualizer proc = new HistogramEqualizer(Program._srcBitmap);
                Bitmap _dstBitmap = proc.Equalize();
                picDest.Image = _dstBitmap;

                Program.displayHistogram(_dstBitmap, ref histDest);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Program._srcBitmap = (Bitmap)picDest.Image;
            Program.infoViewer.Refresh();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
