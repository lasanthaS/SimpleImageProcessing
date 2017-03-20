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
    public partial class frmSobel : Form
    {
        public frmSobel()
        {
            InitializeComponent();
        }

        private void frmSobel_Load(object sender, EventArgs e)
        {
            if (Program.fileOpened != "")
            {
                EdgeDetection proc = new EdgeDetection(Program._srcBitmap);
                Bitmap dst = proc.Sobel();
                picDest.Image = dst;
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
