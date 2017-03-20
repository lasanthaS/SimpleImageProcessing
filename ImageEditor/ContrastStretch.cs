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
using System.Diagnostics;

using HistogramController;


namespace ImageEditor
{
    class ContrastStretch
    {
        private Bitmap _src;
        
        /// <summary>
        /// constructor accepts bitmap object
        /// </summary>
        /// <param name="src">Bitmap object holding the image</param>
        public ContrastStretch(Bitmap src) { this._src = src; }
        /// <summary>
        /// construct accepts file path
        /// </summary>
        /// <param name="filename">file path of the image</param>
        public ContrastStretch(string filename) { this._src = new Bitmap(filename); }

        /// <summary>
        /// this method stretches the contrast of given image
        /// </summary>
        /// <returns>contrast stretched image</returns>
        public Bitmap StretchContrast()
        {
            // as the technique has pointer access, it should be wrapped with unsafe
            unsafe
            {
                // create blank image with original image dimensions to hold the final output
                Bitmap _dst = new Bitmap(_src.Width, _src.Height);

                // get C, D values of the source image
                int[,] csVals = calculateCD(_src);

                // lock bits of the source bitmap
                BitmapData _srcData = _src.LockBits(new Rectangle(0, 0, _src.Width, _src.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                // lock bits of the destination bitmap
                BitmapData _dstData = _dst.LockBits(new Rectangle(0, 0, _src.Width, _src.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                // successive 3 bytes hold B G R values for one pixel
                int pixelDepth = 3;

                // traverse the image vertically
                for (int y = 0; y < _src.Height; y++)
                {
                    byte* _srcRow = (byte*)_srcData.Scan0 + (y * _srcData.Stride);      // get a row from the source image
                    byte* _dstRow = (byte*)_dstData.Scan0 + (y * _dstData.Stride);      // get a row from the destination image

                    // traverse the image horizontally
                    for (int x = 0; x < _src.Width; x++)
                    {
                        byte[] pOut = new byte[3];                                      // 3 byte array for pixels B G R values

                        // calculate 3 stretched values
                        for (int i = 0; i < 3; i++)
                        {
                            // ------------ contrast stretching algorithm ---------------
                            // pOut = (pIn - C) * (B - A) / (D - C) + A
                            // ----------------------------------------------------------
                            pOut[i] = (byte)(((_srcRow[x * pixelDepth + i] - csVals[i, 0]) * (255 - 0) / (csVals[i, 1] - csVals[i, 0])) + 0);
                        }

                        // set pixel values for B G R channels
                        _dstRow[x * pixelDepth]     = pOut[0];      // B
                        _dstRow[x * pixelDepth + 1] = pOut[1];      // G
                        _dstRow[x * pixelDepth + 2] = pOut[2];      // R
                    }
                }

                _dst.UnlockBits(_dstData);                          // unlock bits of the destination image
                _src.UnlockBits(_srcData);                          // unlock bits of the source image

                return _dst;                                        // return the resulted image
            }
        }

        /// <summary>
        /// this method will calculate the endpoints of the peak of RGB channels of the given image
        /// </summary>
        /// <param name="b">Bitmap object to calculate the C, D values</param>
        /// <returns>2 dimensional array (3 x 2) having C, D values for seperate B G R channels</returns>
        private int[,] calculateCD(Bitmap b)
        {
            long[,] histData = Histogram.ComputeHistogram(b);       // compute histogram values for seperate B G R channels
            int[,] cd = new int[3, 2];                              // 2 dimensional array (3 x 2) for return data

            // find the lower value
            for (int i = 0; i < 3; i++)                             // iterate through channel-wise (B G R)
            {
                for (int j = 0; j < 256; j++)                       // iterate through the histogram (from 0 to 255)
                {
                    if (histData[i, j] > 0)                         // check whether the point > 0
                    {
                        cd[i, 0] = j;                               // point founded
                        break;                                      // break the loop
                    }
                }
            }

            // find the upper value
            for (int i = 0; i < 3; i++)                             // iterate through channel-wise (B G R)
            {
                for (int j = 255; j >= 0; j--)                      // iterate through the histogram (from 255 to 0)
                {
                    if (histData[i, j] > 0)                         // check whether the point > 0
                    {
                        cd[i, 1] = j;                               // point founded
                        break;                                      // break the loop
                    }
                }
            }

            return cd;                                              // return C D values for 3 channels
        }
    }
}
