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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace HistogramController
{
    public partial class HistogramController : UserControl
    {
        private int _offset         = 10;                   // offset (padding of the histogram
        private PointF _unit        = new PointF(0, 0);     // horizontal and vertical unit value
        private bool _isDrawing     = false;                // status
        private Color _color        = Color.Black;          // color (default is black)
        private long[] _histData;                           // histogram data holder

        public HistogramController()
        {
            InitializeComponent();

            _isDrawing  = false;                            // status
            _color      = Color.Black;                      // color (default is black)
            _offset     = 10;                               // offset (padding of the histogram
        }

        /// <summary>
        /// offset (padding) property of the histogram
        /// </summary>
        [Category("Histogram"), Description("Horizontal offset of the graph")]
        public int Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        /// <summary>
        /// histogram color property
        /// </summary>
        [Category("Histogram"), Description("Color of the histogram")]
        public Color HistColor
        {
            get { return _color; }
            set { _color = value; }
        }

        private void HistogramController_Paint(object sender, PaintEventArgs e)
        {
            // draw only in the drawing mode
            if (_isDrawing)
            {
                // get current Graphics object
                Graphics g = e.Graphics;
                // define the drawing pen
                Pen p = new Pen(new SolidBrush(_color), _unit.X);

                for (int i = 0; i < _histData.Length; i++)
                {
                    /*
                    // draw using a lines
                    g.DrawLine(
                        p,
                        new PointF(
                            _offset + i * _unit.X, 
                            this.Height - _offset - _unit.Y * _histData[i]),
                        new PointF(
                            _offset + i * _unit.X, 
                            this.Height - _offset));
                    */

                    // draw using rectangles
                    g.FillRectangle(new SolidBrush(_color),                     // brush color
                        new RectangleF(                                         // histogram bar
                            _offset + i * _unit.X,                              // starting X point                            
                            this.Height - _offset - _histData[i] * _unit.Y,     // starting Y point
                            _unit.X,                                            // width of the bar
                            _unit.Y * _histData[i]                              // height of the bar
                        ));
                }

                // draw the bottom line (base)
                g.DrawLine(p, new PointF(_offset, this.Height - _offset), new PointF(this.Width-_offset, this.Height-_offset));
            }
        }

        /// <summary>
        /// calling this method will initiate the generation of histogram
        /// </summary>
        /// <param name="histData">long array having histogram data from 0 - 255</param>
        public void DrawHist(long[] histData)
        {
            _histData = new long[histData.Length];      // allocate elements of the array
            histData.CopyTo(_histData, 0);              // copy data

            this._isDrawing = true;                     // set the mode to drawing 
            computeUnits();                             // calculate horizontal/vertical units

            this.Refresh();                             // refresh the control UI. this will draw the histogram because the status was changed to true
        }

        /// <summary>
        /// compute horizontal/vertical unit length of the histogram
        /// </summary>
        private void computeUnits()
        {
            _unit.X = (float)(this.Width - 2 * _offset) / _histData.Length;         // calculate X value
            _unit.Y = (float)(this.Height - 2 * _offset) / getMax(_histData);       // calculate Y value
        }

        /// <summary>
        /// get maximum frequency of the histogram
        /// </summary>
        /// <param name="histData">histogram data array</param>
        /// <returns>maximum frequency of the histogram data</returns>
        private long getMax(long[] histData)
        {
            // calculate only in the drawing mode
            if (_isDrawing)
            {
                long max = 0;                                   // maximum value holder
                for (int i = 0; i < histData.Length; i++)       // traverse through the histogram data
                    if (histData[i] > max)                      // check for maximum value
                        max = histData[i];                      // extract max

                return max;                                     // return max
            }
            // if the control is not in the drawing method return 1 for max
            return 1;
        }

        public void ClearHistogram()
        {
            _isDrawing = false;
            this.Refresh();
        }
    }
}
