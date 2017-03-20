using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

using System.Diagnostics;

namespace ImageEditor
{
    class Sharpening
    {
        private Bitmap _src;

        public Sharpening(Bitmap src)
        {
            this._src = src;
        }

        public Bitmap Sharpen()
        {
            unsafe
            {
                Bitmap _dst = new Bitmap(_src.Width, _src.Height);

                BitmapData _srcData = _src.LockBits(new Rectangle(0, 0, _src.Width, _src.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                BitmapData _dstData = _dst.LockBits(new Rectangle(0, 0, _src.Width, _src.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                int pixelDepth = 3;

                for (int y = 0; y < _src.Height; y++)
                {
                    byte* _srcRow1 = (y == 0) ? (byte*)_srcData.Scan0 + (y * _srcData.Stride) : (byte*)_srcData.Scan0 + ((y - 1) * _srcData.Stride);
                    byte* _srcRow2 = (byte*)_srcData.Scan0 + (y * _srcData.Stride);
                    byte* _srcRow3 = (y == _src.Height - 1) ? (byte*)_srcData.Scan0 + (y * _srcData.Stride) : (byte*)_srcData.Scan0 + ((y + 1) * _srcData.Stride);

                    byte* _dstRow = (byte*)_dstData.Scan0 + (y * _dstData.Stride);

                    for (int x = 1; x < _src.Width - 1; x++)
                    {
                        int[] rgb = new int[3] { 0, 0, 0 };
                        byte[] pOut = new byte[3];

                        int col1 = (x == 0) ? x : x - 1;
                        int col2 = x;
                        int col3 = (x == _src.Width - 1) ? x : x + 1;

                        for (int i = 0; i < 3; i++)
                        {
                            rgb[i] += (_srcRow2[col2 * pixelDepth + i] * 9) - (_srcRow1[col1 * pixelDepth + i] + _srcRow1[col2 * pixelDepth + i] + _srcRow1[col3 * pixelDepth + i] + _srcRow2[col1 * pixelDepth + i] + _srcRow2[col3 * pixelDepth + i] + _srcRow3[col1 * pixelDepth + i] + _srcRow3[col2 * pixelDepth + i] + _srcRow3[col3 * pixelDepth + i]);

                            rgb[i] = Math.Min(Math.Max(rgb[i], 0), 255);

                            pOut[i] = (byte)(rgb[i]);
                        }

                        _dstRow[x * pixelDepth] = pOut[0];          // B
                        _dstRow[x * pixelDepth + 1] = pOut[1];      // G
                        _dstRow[x * pixelDepth + 2] = pOut[2];      // R
                    }
                }

                _src.UnlockBits(_srcData);
                _dst.UnlockBits(_dstData);

                return _dst;
            }
        }
    }
}
