using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.D2DPainter
{
    public class D2DPaintEventArgs : EventArgs
    {
        private D2DGraphics _g;
        private System.Drawing.Rectangle _r;
        private System.Drawing.Point _m;
        public D2DPaintEventArgs(D2DGraphics g, System.Drawing.Rectangle r, System.Drawing.Point m)
            : base()
        {
            _g = g;
            _r = r;
            _m = m;
        }
        public System.Drawing.Rectangle ClipRectangle
        {
            get
            {
                return _r;
            }
        }
        public D2DGraphics D2DGraphics
        {
            get
            {
                return _g;
            }
        }
        public System.Drawing.Point MousePoint
        {
            get
            {
                return _m;
            }
        }
    }
    
}
