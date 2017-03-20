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
    public partial class frmGrayscale : Form
    {
        public frmGrayscale()
        {
            InitializeComponent();
        }

        private void frmGrayscale_Load(object sender, EventArgs e)
        {
            if (Program.fileOpened != "")
            {
                ColorConvert proc = new ColorConvert(Program._srcBitmap);
                Bitmap dst = proc.ToGrayscale();
                picDest.Image = dst;
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
