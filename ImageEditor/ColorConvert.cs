using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageEditor
{
    class ColorConvert
    {
        private Bitmap _src;

        public ColorConvert(Bitmap src)
        {
            this._src = src;
        }

        public Bitmap ToGrayscale()
        {
            unsafe
            {
                Bitmap _dst = new Bitmap(_src.Width, _src.Height);

                BitmapData _srcData = _src.LockBits(new Rectangle(0, 0, _src.Width, _src.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                BitmapData _dstData = _dst.LockBits(new Rectangle(0, 0, _src.Width, _src.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                int pixelDepth = 3;

                for (int y = 0; y < _src.Height; y++)
                {
                    byte* _srcRow = (byte*)_srcData.Scan0 + (y * _srcData.Stride);

                    byte* _dstRow = (byte*)_dstData.Scan0 + (y * _dstData.Stride);

                    for (int x = 1; x < _src.Width - 1; x++)
                    {
                        byte pOut = (byte)(_srcRow[x * pixelDepth] * 0.3 + _srcRow[x * pixelDepth + 1] * 0.59 + _srcRow[x * pixelDepth + 2] * 0.11);

                        _dstRow[x * pixelDepth] = pOut;          // B
                        _dstRow[x * pixelDepth + 1] = pOut;      // G
                        _dstRow[x * pixelDepth + 2] = pOut;      // R
                    }
                }

                _dst.UnlockBits(_dstData);
                _src.UnlockBits(_srcData);

                return _dst;
            }
        }
    }
}
