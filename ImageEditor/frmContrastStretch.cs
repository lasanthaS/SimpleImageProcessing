using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class frmContrastStretch : Form
    {
        public frmContrastStretch()
        {
            InitializeComponent();
        }

        private void frmContrastStretch_Load(object sender, EventArgs e)
        {
            if (Program.fileOpened != "")
            {
                // process and display the image
                ContrastStretch proc = new ContrastStretch(Program._srcBitmap);       // create ImageProcessor object
                Bitmap _dstBitmap = proc.StretchContrast();                         // stretch the contrast
                picDest.Image = _dstBitmap;                                         // display the resulted image

                // display the histogram
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
