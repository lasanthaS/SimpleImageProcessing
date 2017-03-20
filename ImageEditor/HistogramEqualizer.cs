using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;

using HistogramController;

namespace ImageEditor
{
    class HistogramEqualizer
    {
        private Bitmap _src;

        public HistogramEqualizer(Bitmap src)
        {
            this._src = src;
        }

        public Bitmap Equalize()
        {
            // 1. calculate the current source histogram
            long[,] srcHistVal = Histogram.ComputeHistogram(this._src);
            long[,] map = generateMap(srcHistVal);

            return assignPixels(map);
        }

        private long[,] generateMap(long[,] curHistVal) 
        {
            // 2. init constants: m = 255; 0 <= k <= m; r_k = k / m; P(r_k) = n_k / n; 
            int m = 255;                                                        // number of gray levels
            //float rk = 0.0f;                                                    // rk = k / m
            long n = Program._srcBitmap.Width * Program._srcBitmap.Height;      // number of pixels in the image
            float[,] sk = new float[3, 256];                                   // sk
            long[,] map = new long[3, 256];                              // new color map

            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k <= m; k++)
                {
                    float p_rk = (float)curHistVal[i, k] / n;

                    if (k == 0)
                        sk[i, k] = p_rk;
                    else
                        sk[i, k] = sk[i, k - 1] + p_rk;

                    map[i, k] = (long)Math.Round(sk[i, k] * m);
                }
            }

            return map;
        }

        private Bitmap assignPixels(long[,] map)
        {
            unsafe
            {
                // create blank image with original image dimensions to hold the final output
                Bitmap _dst = new Bitmap(_src.Width, _src.Height);

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
                            pOut[i] = (byte)map[i, _srcRow[x * pixelDepth + i]];
                        }

                        // set pixel values for B G R channels
                        _dstRow[x * pixelDepth] = pOut[0];          // B
                        _dstRow[x * pixelDepth + 1] = pOut[1];      // G
                        _dstRow[x * pixelDepth + 2] = pOut[2];      // R
                    }
                }

                _dst.UnlockBits(_dstData);                          // unlock bits of the destination image
                _src.UnlockBits(_srcData);                          // unlock bits of the source image

                return _dst;                                        // return the resulted image
            }
        }
    }
}
