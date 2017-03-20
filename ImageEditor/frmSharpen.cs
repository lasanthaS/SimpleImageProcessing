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
    public partial class frmSharpen : Form
    {
        public frmSharpen()
        {
            InitializeComponent();
        }

        private void frmSharpen_Load(object sender, EventArgs e)
        {
            if (Program.fileOpened != "")
            {
                Sharpening proc = new Sharpening(Program._srcBitmap);
                Bitmap _dstImage = proc.Sharpen();
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
