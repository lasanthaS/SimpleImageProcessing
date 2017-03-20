using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace ImageEditor
{
    public class InfoViewer
    {
        private Size _size;
        private string _filename;

        public void Refresh()
        {
            if (Program.fileOpened != "")
            {
                _size = new Size(Program._srcBitmap.Width, Program._srcBitmap.Height);
                _filename = Program.fileOpened;
                Program.frmMain.picSrc.Image = Program._srcBitmap;
                Program.frmMain.RefreshHistogram();
                Program.bitmapSize = _size.Height * _size.Width;
            }
            else
            {
                _size = new Size();
                _filename = "";
                Program.bitmapSize = 0;
            }
        }

        [Category("General"), Description("Image dimension in pixels")]
        public Size ImageSize
        {
            get { return _size; }
        }

        [Category("General"), Description("Full path of the selected image")]
        public string FilePath
        {
            get { return _filename; }
        }

        [Category("General"), Description("File name of the selected image")]
        public string FileName
        {
            get { return Path.GetFileName(_filename); }
        }

        [Category("Viewer"), Description("Controls how the Picture box displays the image")]
        public PictureBoxSizeMode SizeMode
        {
            get { return Program.frmMain.picSrc.SizeMode; }
            set { Program.frmMain.picSrc.SizeMode = (value != PictureBoxSizeMode.AutoSize) ? value : PictureBoxSizeMode.CenterImage; }
        }
    }
}
