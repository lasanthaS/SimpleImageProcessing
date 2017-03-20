using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using HistogramController;

namespace ImageEditor
{
    static class Program
    {
        public static frmImageEditor frmMain;
        public static string fileOpened = "";
        public static Bitmap _srcBitmap;                                        // this is to hold the source bitmap object
        public static InfoViewer infoViewer;
        public static int bitmapSize;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmMain = new frmImageEditor();
            infoViewer = new InfoViewer();
            Application.Run(frmMain);
        }

        /// <summary>
        /// this method will display calculate histogram values and display it
        /// </summary>
        /// <param name="image">Bitmap object to calculate the histogram</param>
        /// <param name="histController">HistogramController object to display the histogram</param>
        public static void displayHistogram(Bitmap image, ref HistogramController.HistogramController histController)
        {
            // draw the original histogram
            long[] histData = new long[256];                                    // array to hold the histogram data
            long[,] temp = Histogram.ComputeHistogram(image);                   // array to hold channel-wise histogram data
            for (int i = 0; i < 256; i++)
                histData[i] = (temp[0, i] + temp[1, i] + temp[2, i]) / 3;       // calculate mean histogram value

            histController.DrawHist(histData);                                  // display the histogram
        }
    }
}
