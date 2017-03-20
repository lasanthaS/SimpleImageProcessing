using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace ImageEditor
{
    public enum SmoothType
    {
        MEAN_SMOOTH,
        MEDIAN_SMOOTH,
        GAUSSIAN_SMOOTH
    }

    class Smoothing
    {
        private Bitmap _src;

        public Smoothing(Bitmap src)
        {
            this._src = src;
        }

        public Bitmap Smooth(SmoothType st)
        {
            Bitmap ret = new Bitmap(1,1);

            switch (st)
            {
                case SmoothType.MEAN_SMOOTH:
                    ret = meanSmooth();
                    break;
                case SmoothType.MEDIAN_SMOOTH:
                    ret = medianSmooth();
                    break;
                case SmoothType.GAUSSIAN_SMOOTH:
                    ret = gaussianSmooth();
                    break;
            }

            return ret;
        }

        private Bitmap meanSmooth()
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
                            rgb[i] += _srcRow1[col1 * pixelDepth + i] + _srcRow1[col2 * pixelDepth + i] + _srcRow1[col3 * pixelDepth + i] + _srcRow2[col1 * pixelDepth + i] + _srcRow2[col2 * pixelDepth + i] + _srcRow2[col3 * pixelDepth + i] + _srcRow3[col1 * pixelDepth + i] + _srcRow3[col2 * pixelDepth + i] + _srcRow3[col3 * pixelDepth + i];
                            pOut[i] = (byte)(rgb[i] / 9);
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

        private Bitmap medianSmooth()
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
                        //int[] rgb = new int[3] { 0, 0, 0 };
                        byte[] pOut = new byte[3];

                        int col1 = (x == 0) ? x : x - 1;
                        int col2 = x;
                        int col3 = (x == _src.Width - 1) ? x : x + 1;

                        List<byte> r = new List<byte>();
                        List<byte> g = new List<byte>();
                        List<byte> b = new List<byte>();

                        b.Add(_srcRow1[col1 * pixelDepth]);
                        b.Add(_srcRow1[col2 * pixelDepth]);
                        b.Add(_srcRow1[col3 * pixelDepth]);
                        b.Add(_srcRow2[col1 * pixelDepth]);
                        b.Add(_srcRow2[col2 * pixelDepth]);
                        b.Add(_srcRow2[col3 * pixelDepth]);
                        b.Add(_srcRow3[col1 * pixelDepth]);
                        b.Add(_srcRow3[col2 * pixelDepth]);
                        b.Add(_srcRow3[col3 * pixelDepth]);

                        g.Add(_srcRow1[col1 * pixelDepth + 1]);
                        g.Add(_srcRow1[col2 * pixelDepth + 1]);
                        g.Add(_srcRow1[col3 * pixelDepth + 1]);
                        g.Add(_srcRow2[col1 * pixelDepth + 1]);
                        g.Add(_srcRow2[col2 * pixelDepth + 1]);
                        g.Add(_srcRow2[col3 * pixelDepth + 1]);
                        g.Add(_srcRow3[col1 * pixelDepth + 1]);
                        g.Add(_srcRow3[col2 * pixelDepth + 1]);
                        g.Add(_srcRow3[col3 * pixelDepth + 1]);

                        r.Add(_srcRow1[col1 * pixelDepth + 2]);
                        r.Add(_srcRow1[col2 * pixelDepth + 2]);
                        r.Add(_srcRow1[col3 * pixelDepth + 2]);
                        r.Add(_srcRow2[col1 * pixelDepth + 2]);
                        r.Add(_srcRow2[col2 * pixelDepth + 2]);
                        r.Add(_srcRow2[col3 * pixelDepth + 2]);
                        r.Add(_srcRow3[col1 * pixelDepth + 2]);
                        r.Add(_srcRow3[col2 * pixelDepth + 2]);
                        r.Add(_srcRow3[col3 * pixelDepth + 2]);

                        b.Sort();
                        g.Sort();
                        r.Sort();
                        pOut[0] = b[5];
                        pOut[1] = g[5];
                        pOut[2] = r[5];

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

        private Bitmap gaussianSmooth()
        {
            unsafe
            {
                Bitmap _dst = new Bitmap(_src.Width, _src.Height);

                BitmapData _srcData = _src.LockBits(new Rectangle(0, 0, _src.Width, _src.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                BitmapData _dstData = _dst.LockBits(new Rectangle(0, 0, _src.Width, _src.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                int pixelDepth = 3;

                for (int y = 0; y < _src.Height; y++)
                {
                    
                    //byte* _srcRow1 = (y == 0) ? (byte*)_srcData.Scan0 + (y * _srcData.Stride) : (byte*)_srcData.Scan0 + ((y - 1) * _srcData.Stride);
                    //byte* _srcRow2 = (byte*)_srcData.Scan0 + (y * _srcData.Stride);
                    //byte* _srcRow3 = (y == _src.Height - 1) ? (byte*)_srcData.Scan0 + (y * _srcData.Stride) : (byte*)_srcData.Scan0 + ((y + 1) * _srcData.Stride);

                    byte* _srcRow1, _srcRow2, _srcRow4, _srcRow5;

                    if (y == 0)
                    {
                        _srcRow1 = (byte*)_srcData.Scan0 + (y * _srcData.Stride);
                        _srcRow2 = (byte*)_srcData.Scan0 + (y * _srcData.Stride);
                    }
                    else if (y == 1)
                    {
                        _srcRow1 = (byte*)_srcData.Scan0 + ((y - 1) * _srcData.Stride);
                        _srcRow2 = (byte*)_srcData.Scan0 + ((y - 1) * _srcData.Stride);
                    }
                    else
                    {
                        _srcRow1 = (byte*)_srcData.Scan0 + ((y - 2) * _srcData.Stride);
                        _srcRow2 = (byte*)_srcData.Scan0 + ((y - 1) * _srcData.Stride);
                    }

                    if (y == _src.Height - 1)
                    {
                        _srcRow4 = (byte*)_srcData.Scan0 + (y * _srcData.Stride);
                        _srcRow5 = (byte*)_srcData.Scan0 + (y * _srcData.Stride);
                    }
                    else if (y == _src.Height - 2)
                    {
                        _srcRow4 = (byte*)_srcData.Scan0 + ((y + 1) * _srcData.Stride);
                        _srcRow5 = (byte*)_srcData.Scan0 + ((y + 1) * _srcData.Stride);
                    }
                    else
                    {
                        _srcRow4 = (byte*)_srcData.Scan0 + ((y + 1) * _srcData.Stride);
                        _srcRow5 = (byte*)_srcData.Scan0 + ((y + 2) * _srcData.Stride);
                    }

                    byte* _srcRow3 = (byte*)_srcData.Scan0 + (y * _srcData.Stride);

                    byte* _dstRow = (byte*)_dstData.Scan0 + (y * _dstData.Stride);

                    for (int x = 1; x < _src.Width - 1; x++)
                    {
                        int[] rgb = new int[3] { 0, 0, 0 };
                        byte[] pOut = new byte[3];

                        int col1, col2, col4, col5; // = (x == 0) ? x : x - 1;

                        if (x == 0)
                        {
                            col1 = col2 = x;
                        }
                        else if (x == 1)
                        {
                            col1 = x - 1;
                            col2 = x;
                        }
                        else
                        {
                            col1 = x - 2;
                            col2 = x - 1;
                        }

                        if (x == _src.Width - 1)
                        {
                            col4 = col5 = x;
                        }
                        else if (x == _src.Width - 2)
                        {
                            col4 = x + 1;
                            col5 = x + 1;
                        }
                        else
                        {
                            col4 = x + 1;
                            col5 = x + 2;
                        }

                        int col3 = x;

                        for (int i = 0; i < 3; i++)
                        {
                            rgb[i] += 1 * _srcRow1[col1 * pixelDepth + i] + 4 * _srcRow1[col2 * pixelDepth + i] + 7 * _srcRow1[col3 * pixelDepth + i] + 4 * _srcRow1[col4 * pixelDepth + i] + 1 * _srcRow1[col5 * pixelDepth + i];
                            rgb[i] += 4 * _srcRow2[col1 * pixelDepth + i] + 16 * _srcRow2[col2 * pixelDepth + i] + 26 * _srcRow2[col3 * pixelDepth + i] + 16 * _srcRow2[col4 * pixelDepth + i] + 4 * _srcRow2[col5 * pixelDepth + i];
                            rgb[i] += 7 * _srcRow3[col1 * pixelDepth + i] + 26 * _srcRow3[col2 * pixelDepth + i] + 41 * _srcRow3[col3 * pixelDepth + i] + 26 * _srcRow3[col4 * pixelDepth + i] + 7 * _srcRow3[col5 * pixelDepth + i];
                            rgb[i] += 4 * _srcRow4[col1 * pixelDepth + i] + 16 * _srcRow4[col2 * pixelDepth + i] + 26 * _srcRow4[col3 * pixelDepth + i] + 16 * _srcRow4[col4 * pixelDepth + i] + 4 * _srcRow4[col5 * pixelDepth + i];
                            rgb[i] += 1 * _srcRow5[col1 * pixelDepth + i] + 4 * _srcRow5[col2 * pixelDepth + i] + 7 * _srcRow5[col3 * pixelDepth + i] + 4 * _srcRow5[col4 * pixelDepth + i] + 1 * _srcRow5[col5 * pixelDepth + i];
                            pOut[i] = (byte)(rgb[i] / 273);
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
    }
}
