using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.D2DPainter
{
    internal class D2DUtils
    {
        SharpDX.Direct2D1.WindowRenderTarget D2Drender;
        SharpDX.DirectWrite.Factory DWrender;
        internal D2DUtils(SharpDX.Direct2D1.WindowRenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory FactoryDWrite)
        {
            D2Drender = RenderTarget2D;
            DWrender = FactoryDWrite;
        }
        public SharpDX.Mathematics.Interop.RawColor4 Color2Raw(System.Drawing.Color Color)
        {
            float rr, rg, rb, ra;
            rr = Color.R / 255.0f;
            rg = Color.G / 255.0f;
            rb = Color.B / 255.0f;
            ra = Color.A / 255.0f;
            return new SharpDX.Mathematics.Interop.RawColor4(rr, rg, rb, ra);
        }
        public SharpDX.Mathematics.Interop.RawRectangleF Retangle2Raw(System.Drawing.Rectangle retangle)
        {
            int w = retangle.Width;
            int l = retangle.Left;
            int h = retangle.Height;
            int t = retangle.Top;
            return new SharpDX.Mathematics.Interop.RawRectangleF(l, t, l+w, t+h);
        }
        public SharpDX.Mathematics.Interop.RawVector2 Point2Raw(System.Drawing.Point point)
        {
            int x = point.X;
            int y = point.Y;
            return new SharpDX.Mathematics.Interop.RawVector2(x, y);
        }
        public SharpDX.Direct2D1.SolidColorBrush SoildBrush2Raw(System.Drawing.Color Color)
        {
            return new SharpDX.Direct2D1.SolidColorBrush(D2Drender, Color2Raw(Color));
        }
        public SharpDX.DirectWrite.TextFormat Font2Raw(System.Drawing.Font Font)
        {
            SharpDX.DirectWrite.TextFormat ret;
            //new SharpDX.DirectWrite.TextFormat(FactoryDWrite, "微软雅黑", 12)
            ret = new SharpDX.DirectWrite.TextFormat(DWrender,
                Font.FontFamily.Name,
                Font.Bold ? SharpDX.DirectWrite.FontWeight.Bold : SharpDX.DirectWrite.FontWeight.Normal,
                Font.Italic ? SharpDX.DirectWrite.FontStyle.Italic : SharpDX.DirectWrite.FontStyle.Normal,
                Font.Size);
            return ret;
        }
    }
    
}
