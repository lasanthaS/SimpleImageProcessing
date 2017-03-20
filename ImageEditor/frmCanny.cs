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
    public partial class frmCanny : Form
    {
        public frmCanny()
        {
            InitializeComponent();
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

        private void frmCanny_Load(object sender, EventArgs e)
        {
            refreshImage();
        }

        private void refreshImage()
        {
            Cursor = Cursors.WaitCursor;
            if (Program.fileOpened != "")
            {
                new Smoothing(Program._srcBitmap).Smooth(SmoothType.GAUSSIAN_SMOOTH);
                EdgeDetection proc = new EdgeDetection(Program._srcBitmap);
                Bitmap dst = proc.Canny((byte)tbT1.Value, (byte)tbT2.Value);
                picDest.Image = dst;
            }
            Cursor = Cursors.Arrow;
        }

        private void tbT1_MouseUp(object sender, MouseEventArgs e)
        {
            refreshImage();
        }

        private void tbT1_KeyUp(object sender, KeyEventArgs e)
        {
            refreshImage();
        }

        private void tbT2_KeyUp(object sender, KeyEventArgs e)
        {
            refreshImage();
        }

        private void tbT2_MouseUp(object sender, MouseEventArgs e)
        {
            refreshImage();
        }
    }
}
