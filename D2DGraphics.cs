using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalthasarLib.D2DPainter
{
    public class D2DGraphics
    {
        SharpDX.Direct2D1.WindowRenderTarget D2Drender;
        SharpDX.DirectWrite.Factory DWrender;
        D2DUtils utils;
        internal D2DGraphics(SharpDX.Direct2D1.WindowRenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory FactoryDWrite)
        {
            D2Drender = RenderTarget2D;
            DWrender = FactoryDWrite;
            utils = new D2DUtils(RenderTarget2D, FactoryDWrite);
        }
        public void Clear()
        {
            D2Drender.Clear(null);
        }
        public void Clear(System.Drawing.Color BackgroundColor)
        {
            D2Drender.Clear(utils.Color2Raw(BackgroundColor));
        }
        public void DrawRectangle(System.Drawing.Rectangle retangle,System.Drawing.Color LineColor)
        {
            D2Drender.DrawRectangle(utils.Retangle2Raw(retangle)
                , utils.SoildBrush2Raw(LineColor));
        }
        public void DrawRectangle(System.Drawing.Rectangle retangle, System.Drawing.Color LineColor, float LineWidth)
        {
            D2Drender.DrawRectangle(utils.Retangle2Raw(retangle)
                , utils.SoildBrush2Raw(LineColor), LineWidth);
        }
        public void FillRectangle(System.Drawing.Rectangle retangle, System.Drawing.Color FillColor)
        {
            D2Drender.FillRectangle(utils.Retangle2Raw(retangle)
                , utils.SoildBrush2Raw(FillColor));
        }
        public void DrawLine(System.Drawing.Point p1, System.Drawing.Point p2, System.Drawing.Color LineColor)
        {
            D2Drender.DrawLine(utils.Point2Raw(p1),utils.Point2Raw(p2), utils.SoildBrush2Raw(LineColor));
        }
        public void DrawLine(System.Drawing.Point p1, System.Drawing.Point p2, System.Drawing.Color LineColor,float LineWidth)
        {
            D2Drender.DrawLine(utils.Point2Raw(p1), utils.Point2Raw(p2), utils.SoildBrush2Raw(LineColor),LineWidth);
        }
        //DrawEllipse
        public void DrawEllipse(System.Drawing.Point center, float width, float height, System.Drawing.Color LineColor)
        {
            D2Drender.DrawEllipse(new SharpDX.Direct2D1.Ellipse(utils.Point2Raw(center), width/2, height/2),
                             utils.SoildBrush2Raw(LineColor));
        }
        public void DrawEllipse(System.Drawing.Point center, float width, float height, System.Drawing.Color LineColor, float LineWidth)
        {
            D2Drender.DrawEllipse(new SharpDX.Direct2D1.Ellipse(utils.Point2Raw(center), width / 2, height / 2),
                             utils.SoildBrush2Raw(LineColor),LineWidth);
        }
        public void FillEllipse(System.Drawing.Point center, float width, float height, System.Drawing.Color LineColor)
        {
            D2Drender.FillEllipse(new SharpDX.Direct2D1.Ellipse(utils.Point2Raw(center), width / 2, height / 2),
                             utils.SoildBrush2Raw(LineColor));
        }
        public void DrawText(string Text, System.Drawing.Rectangle drawRectangle, System.Drawing.Color Color,System.Drawing.Font Font)
        {
            D2Drender.DrawText(Text,
                utils.Font2Raw(Font),
                utils.Retangle2Raw(drawRectangle),
                utils.SoildBrush2Raw(Color));
            
        }
    }
}
