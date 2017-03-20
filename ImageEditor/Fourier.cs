using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

using AForge.Imaging;

namespace ImageEditor
{
    class Fourier
    {
        private Bitmap _src;

        public Fourier(Bitmap src)
        {
            this._src = src;
            // re-format image
            AForge.Imaging.Image.FormatImage(ref this._src);
            // convert image to grayscale
            this._src = new AForge.Imaging.Filters.GrayscaleBT709().Apply(this._src);
        }

        public Bitmap IdealPass(byte threshold, bool isLowPass, out Bitmap fft)
        {
            ComplexImage cimage = ComplexImage.FromBitmap(_src);
            cimage.ForwardFourierTransform();
            fft = cimage.ToBitmap();

            if(isLowPass)
                cimage.FrequencyFilter(new AForge.IntRange(0, (int)threshold));
            else
                cimage.FrequencyFilter(new AForge.IntRange((int)threshold, 0));

            cimage.BackwardFourierTransform();
            Bitmap dst = cimage.ToBitmap();

            return dst;
        }
    }
}
