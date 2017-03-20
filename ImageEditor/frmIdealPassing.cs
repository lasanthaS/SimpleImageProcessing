using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AForge.Math;
using AForge.Imaging;

namespace ImageEditor
{
    public partial class frmIdealPassing : Form
    {
        Fourier proc = null;
        bool isLowPass = true;

        public frmIdealPassing()
        {
            InitializeComponent();

            cmbMode.SelectedIndex = 0;

            if (Program.fileOpened != "")
            {
                proc = new Fourier(Program._srcBitmap);
            }
        }

        private void frmDFT_Load(object sender, EventArgs e)
        {
            if (Program.fileOpened != "")
            {
                Bitmap fft;
                Bitmap dst = proc.IdealPass((byte)tbThreshold.Value, isLowPass, out fft);
                picFT.Image = fft;
                picDest.Image = dst;
            }
        }

        private void tbThreshold_MouseUp(object sender, MouseEventArgs e)
        {
            calculateFourier();
        }

        private void calculateFourier()
        {
            this.Cursor = Cursors.WaitCursor;

            if (Program.fileOpened != "")
            {
                Bitmap fft;
                Bitmap dst = proc.IdealPass((byte)tbThreshold.Value, isLowPass, out fft);
                picFT.Image = fft;
                picDest.Image = dst;
            }

            this.Cursor = Cursors.Arrow;
        }

        private void tbThreshold_KeyUp(object sender, KeyEventArgs e)
        {
            calculateFourier();
        }

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (proc != null)
            {
                isLowPass = (cmbMode.SelectedIndex == 0) ? true : false;
                calculateFourier();
            }
        }
    }
}
