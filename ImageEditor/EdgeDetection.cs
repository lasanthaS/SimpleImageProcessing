using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageEditor
{
    class EdgeDetection
    {
        private Bitmap _src;

        public EdgeDetection(Bitmap src)
        {
            this._src = src;
        }

        public Bitmap Sobel()
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
                        int[] rgb_1 = new int[3] { 0, 0, 0 };
                        int[] rgb_2 = new int[3] { 0, 0, 0 };
                        byte[] pOut = new byte[3];

                        int col1 = (x == 0) ? x : x - 1;
                        int col2 = x;
                        int col3 = (x == _src.Width - 1) ? x : x + 1;

                        for (int i = 0; i < 3; i++)
                        {
                            rgb_1[i] += -_srcRow1[col1 * pixelDepth + i] + -2 * _srcRow1[col2 * pixelDepth + i] + -_srcRow1[col3 * pixelDepth + i] + 0 * _srcRow2[col1 * pixelDepth + i] + 0 * _srcRow2[col2 * pixelDepth + i] + 0 * _srcRow2[col3 * pixelDepth + i] + _srcRow3[col1 * pixelDepth + i] + 2 * _srcRow3[col2 * pixelDepth + i] + _srcRow3[col3 * pixelDepth + i];
                            rgb_2[i] += -_srcRow1[col1 * pixelDepth + i] + 0 * _srcRow1[col2 * pixelDepth + i] + _srcRow1[col3 * pixelDepth + i] + -2 * _srcRow2[col1 * pixelDepth + i] + 0 * _srcRow2[col2 * pixelDepth + i] + 2 * _srcRow2[col3 * pixelDepth + i] + -_srcRow3[col1 * pixelDepth + i] + 0 * _srcRow3[col2 * pixelDepth + i] + _srcRow3[col3 * pixelDepth + i];

                            rgb[i] = (int)Math.Sqrt(Math.Pow(rgb_1[i], 2) + Math.Pow(rgb_2[i], 2));

                            pOut[i] = (byte)rgb[i];
                        }

                        _dstRow[x * pixelDepth] = pOut[0];          // B
                        _dstRow[x * pixelDepth + 1] = pOut[1];      // G
                        _dstRow[x * pixelDepth + 2] = pOut[2];      // R
                    }
                }

                _dst.UnlockBits(_dstData);
                _src.UnlockBits(_srcData);

                return _dst;
            }
        }












        public Bitmap Canny(byte t1, byte t2)
        {
            unsafe
            {
                Bitmap _dst = new Bitmap(_src.Width, _src.Height);

                BitmapData _srcData = _src.LockBits(new Rectangle(0, 0, _src.Width, _src.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                BitmapData _dstData = _dst.LockBits(new Rectangle(0, 0, _src.Width, _src.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                int pixelDepth = 3;

                byte[,] nonMax = new byte[_src.Width, _src.Height];

                for (int y = 0; y < _src.Height; y++)
                {
                    byte* _srcRow1 = (y == 0) ? (byte*)_srcData.Scan0 + (y * _srcData.Stride) : (byte*)_srcData.Scan0 + ((y - 1) * _srcData.Stride);
                    byte* _srcRow2 = (byte*)_srcData.Scan0 + (y * _srcData.Stride);
                    byte* _srcRow3 = (y == _src.Height - 1) ? (byte*)_srcData.Scan0 + (y * _srcData.Stride) : (byte*)_srcData.Scan0 + ((y + 1) * _srcData.Stride);

                    byte* _dstRow = (byte*)_dstData.Scan0 + (y * _dstData.Stride);

                    for (int x = 1; x < _src.Width - 1; x++)
                    {
                        //int[] soble_strength = new int[3] { 0, 0, 0 };
                        //int[] g_x = new int[3] { 0, 0, 0 };
                        //int[] g_y = new int[3] { 0, 0, 0 };
                        byte[] pOut = new byte[3];

                        int col1 = (x == 0) ? x : x - 1;
                        int col2 = x;
                        int col3 = (x == _src.Width - 1) ? x : x + 1;

                        for (int i = 0; i < 3; i++)
                        {
                            // apply gaussian smoothing

                            // apply sobel operator and calculate Gx and Gy
                            int g_x = -_srcRow1[col1 * pixelDepth + i] + 0 * _srcRow1[col2 * pixelDepth + i] + _srcRow1[col3 * pixelDepth + i] + -2 * _srcRow2[col1 * pixelDepth + i] + 0 * _srcRow2[col2 * pixelDepth + i] + 2 * _srcRow2[col3 * pixelDepth + i] + -_srcRow3[col1 * pixelDepth + i] + 0 * _srcRow3[col2 * pixelDepth + i] + _srcRow3[col3 * pixelDepth + i];
                            int g_y = -_srcRow1[col1 * pixelDepth + i] + -2 * _srcRow1[col2 * pixelDepth + i] + -_srcRow1[col3 * pixelDepth + i] + 0 * _srcRow2[col1 * pixelDepth + i] + 0 * _srcRow2[col2 * pixelDepth + i] + 0 * _srcRow2[col3 * pixelDepth + i] + _srcRow3[col1 * pixelDepth + i] + 2 * _srcRow3[col2 * pixelDepth + i] + _srcRow3[col3 * pixelDepth + i];

                            // calculate strength and gradient (rounding for 0, 45, 90, 135 required)
                            int soble_strength = (int)Math.Sqrt(Math.Pow(g_x, 2) + Math.Pow(g_y, 2));
                            int soble_gradient = 0;
                            if (g_x == 0)
                                soble_gradient = GetApproximateGradient(Math.PI / 2);
                            else
                                soble_gradient = GetApproximateGradient(Math.Atan(g_y / g_x));
                            

                            int sector = 0;
                            if (soble_gradient == 0 || soble_gradient == 180)
                                sector = 0;
                            else if (soble_gradient == 45 || soble_gradient == 225)
                                sector = 1;
                            else if (soble_gradient == 90 || soble_gradient == 270)
                                sector = 2;
                            else if (soble_gradient == 135 || soble_gradient == 315)
                                sector = 3;

                            byte[] sectorPixels = new byte[2] { 0, 0 };

                            switch (sector)
                            {
                                case 0:
                                    sectorPixels[0] = _srcRow2[col3 * pixelDepth + i];
                                    sectorPixels[1] = _srcRow2[col1 * pixelDepth + i];
                                    break;
                                case 1:
                                    sectorPixels[0] = _srcRow1[col3 * pixelDepth + i];
                                    sectorPixels[1] = _srcRow3[col1 * pixelDepth + i];
                                    break;
                                case 2:
                                    sectorPixels[0] = _srcRow1[col2 * pixelDepth + i];
                                    sectorPixels[1] = _srcRow3[col2 * pixelDepth + i];
                                    break;
                                case 3:
                                    sectorPixels[0] = _srcRow1[col1 * pixelDepth + i];
                                    sectorPixels[1] = _srcRow3[col3 * pixelDepth + i];
                                    break;
                            }

                            if ((soble_strength < sectorPixels[0]) || (soble_strength < sectorPixels[1]))
                                soble_strength = 0;

                            soble_strength = (soble_strength > t1 && soble_strength < t2) ? 255 : 0;

                            pOut[i] = (byte)soble_strength;
                        }

                        _dstRow[x * pixelDepth] = pOut[0];          // B
                        _dstRow[x * pixelDepth + 1] = pOut[1];      // G
                        _dstRow[x * pixelDepth + 2] = pOut[2];      // R
                    }
                }

                _dst.UnlockBits(_dstData);
                _src.UnlockBits(_srcData);

                return _dst;
            }
        }

        public int GetApproximateGradient(double r)
        {
            int x = 0;
            r = Math.Abs(r);

            if (((r >= 0) && (r < Math.PI / 8)) || ((r >= 15 * Math.PI / 8) && (r < 2 * Math.PI)))
                x = 0;
            else if ((r >= Math.PI / 8) && (r < 3 * Math.PI / 8))
                x = 45;
            else if ((r >= 3 * Math.PI / 8) && (r < 5 * Math.PI / 8))
                x = 90;
            else if ((r >= 5 * Math.PI / 8) && (r < 7 * Math.PI / 8))
                x = 135;
            else if ((r >= 7 * Math.PI / 8) && (r < 9 * Math.PI / 8))
                x = 180;
            else if ((r >= 9 * Math.PI / 8) && (r < 11 * Math.PI / 8))
                x = 225;
            else if ((r >= 11 * Math.PI / 8) && (r < 13 * Math.PI / 8))
                x = 270;
            else if ((r >= 13 * Math.PI / 8) && (r < 15 * Math.PI / 8))
                x = 315;

            return x;
        }
    }
}
