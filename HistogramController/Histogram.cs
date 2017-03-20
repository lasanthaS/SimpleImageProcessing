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
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace HistogramController
{
    public class Histogram
    {
        /// <summary>
        /// this method will compute the histogram for given image
        /// </summary>
        /// <param name="src">source Bitmap object to compute histogram</param>
        /// <returns>2 dimensional array (3 x 256) having histogram data for seperate B G R channels</returns>
        public static long[,] ComputeHistogram(Bitmap src)
        {
            // as the technique has pointer access, it should be wrapped with unsafe
            unsafe
            {
                // 2 dimensional array to hold histogram data for seperate B G R channels
                long[,] histData = new long[3, 256];

                // lock bits of the source bitmap
                BitmapData srcData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                // successive 3 bytes hold B G R values for one pixel
                int pixelDepth = 3;

                // traverse the image vertically
                for (int y = 0; y < src.Height; y++)
                {
                    // get a row from the source image
                    byte* srcRow = (byte*)srcData.Scan0 + (y * srcData.Stride);

                    // traverse the image horizontally
                    for (int x = 0; x < src.Width; x++)
                    {
                        histData[0, srcRow[x * pixelDepth]]++;          // increment related B value 
                        histData[1, srcRow[x * pixelDepth + 1]]++;      // increment related G value 
                        histData[2, srcRow[x * pixelDepth + 2]]++;      // increment related R value 
                    }
                }

                // unlock bits of the source image
                src.UnlockBits(srcData);

                // return histogram data
                return histData;
            }
        }
    }
}
