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
        public void DrawLine(System.Drawing.Point p1, System.Drawing.Point p2, System.Drawing.Color LineColor, float LineWidth,System.Drawing.Drawing2D.DashStyle dashStyle)
        {
            SharpDX.Direct2D1.StrokeStyleProperties ssProps = new SharpDX.Direct2D1.StrokeStyleProperties();
            ssProps.StartCap = SharpDX.Direct2D1.CapStyle.Flat;
            ssProps.EndCap = SharpDX.Direct2D1.CapStyle.Flat;
            ssProps.DashOffset = 0.1f;
            switch (dashStyle)
            {
                case System.Drawing.Drawing2D.DashStyle.Dash: ssProps.DashStyle = SharpDX.Direct2D1.DashStyle.Dash; break;
                case System.Drawing.Drawing2D.DashStyle.DashDot: ssProps.DashStyle = SharpDX.Direct2D1.DashStyle.DashDot; break;
                case System.Drawing.Drawing2D.DashStyle.DashDotDot: ssProps.DashStyle = SharpDX.Direct2D1.DashStyle.DashDotDot; break;
                case System.Drawing.Drawing2D.DashStyle.Dot: ssProps.DashStyle = SharpDX.Direct2D1.DashStyle.Dot; break;
                case System.Drawing.Drawing2D.DashStyle.Solid: ssProps.DashStyle = SharpDX.Direct2D1.DashStyle.Solid; break;
            }
            SharpDX.Direct2D1.StrokeStyle LineStyle = new SharpDX.Direct2D1.StrokeStyle(D2Drender.Factory, ssProps);
            D2Drender.DrawLine(utils.Point2Raw(p1), utils.Point2Raw(p2), utils.SoildBrush2Raw(LineColor), LineWidth, LineStyle);
        }

        //DrawPathGeometry
        public void DrawPathGeometrySink(List<System.Drawing.Point> Points, System.Drawing.Color LineColor, bool CloseGeometry = false)
        {
            DrawPathGeometrySink(Points, LineColor, 1, CloseGeometry);
        }
        public void DrawPathGeometrySink(List<System.Drawing.Point> Points, System.Drawing.Color LineColor, float LineWidth, bool CloseGeometry = false)
        {
            SharpDX.Direct2D1.PathGeometry pg = new SharpDX.Direct2D1.PathGeometry(D2Drender.Factory);
            SharpDX.Direct2D1.GeometrySink sink = pg.Open();
            for (int i = 0; i < Points.Count; i++)
            {
                if (i == 0)
                {
                    sink.BeginFigure(utils.Point2Raw(Points[0]), SharpDX.Direct2D1.FigureBegin.Hollow);
                }
                else
                {
                    sink.AddLine(utils.Point2Raw(Points[i]));
                }
            }
            if (Points.Count > 0) sink.EndFigure(CloseGeometry ? SharpDX.Direct2D1.FigureEnd.Closed : SharpDX.Direct2D1.FigureEnd.Open);
            sink.Close();
            
            D2Drender.DrawGeometry(pg, utils.SoildBrush2Raw(LineColor), LineWidth);
        }
        public void DrawPathGeometrySink(List<System.Drawing.Point> Points, System.Drawing.Color LineColor,float LineWidth, System.Drawing.Drawing2D.DashStyle dashStyle,bool CloseGeometry=false)
        {
            SharpDX.Direct2D1.PathGeometry pg = new SharpDX.Direct2D1.PathGeometry(D2Drender.Factory);
            SharpDX.Direct2D1.GeometrySink sink=pg.Open();
            for (int i = 0; i < Points.Count; i++)
            {
                if (i == 0)
                {
                    sink.BeginFigure(utils.Point2Raw(Points[0]), SharpDX.Direct2D1.FigureBegin.Hollow);
                }
                else
                {
                    sink.AddLine(utils.Point2Raw(Points[i]));
                }
            }
            if(Points.Count>0)sink.EndFigure(CloseGeometry?SharpDX.Direct2D1.FigureEnd.Closed:SharpDX.Direct2D1.FigureEnd.Open);
            sink.Close();
            SharpDX.Direct2D1.StrokeStyleProperties ssProps = new SharpDX.Direct2D1.StrokeStyleProperties();
            ssProps.StartCap = SharpDX.Direct2D1.CapStyle.Flat;
            ssProps.EndCap = SharpDX.Direct2D1.CapStyle.Flat;
            ssProps.DashOffset = 0.1f;
            switch (dashStyle)
            {
                case System.Drawing.Drawing2D.DashStyle.Dash: ssProps.DashStyle = SharpDX.Direct2D1.DashStyle.Dash; break;
                case System.Drawing.Drawing2D.DashStyle.DashDot: ssProps.DashStyle = SharpDX.Direct2D1.DashStyle.DashDot; break;
                case System.Drawing.Drawing2D.DashStyle.DashDotDot: ssProps.DashStyle = SharpDX.Direct2D1.DashStyle.DashDotDot; break;
                case System.Drawing.Drawing2D.DashStyle.Dot: ssProps.DashStyle = SharpDX.Direct2D1.DashStyle.Dot; break;
                case System.Drawing.Drawing2D.DashStyle.Solid: ssProps.DashStyle = SharpDX.Direct2D1.DashStyle.Solid; break;
            }
            SharpDX.Direct2D1.StrokeStyle LineStyle = new SharpDX.Direct2D1.StrokeStyle(D2Drender.Factory, ssProps);
            D2Drender.DrawGeometry(pg, utils.SoildBrush2Raw(LineColor),LineWidth,LineStyle);
        }
        public void FillPathGeometrySink(List<System.Drawing.Point> Points, System.Drawing.Color FillColor)
        {
            SharpDX.Direct2D1.PathGeometry pg = new SharpDX.Direct2D1.PathGeometry(D2Drender.Factory);
            SharpDX.Direct2D1.GeometrySink sink = pg.Open();
            for (int i = 0; i < Points.Count; i++)
            {
                if (i == 0)
                {
                    sink.BeginFigure(utils.Point2Raw(Points[0]), SharpDX.Direct2D1.FigureBegin.Filled);
                }
                else
                {
                    sink.AddLine(utils.Point2Raw(Points[i]));
                }
            }
            if (Points.Count > 0) sink.EndFigure(SharpDX.Direct2D1.FigureEnd.Closed);
            sink.Close();

            D2Drender.FillGeometry(pg, utils.SoildBrush2Raw(FillColor));
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
        public void DrawText(string Text, System.Drawing.Rectangle drawRectangle, System.Drawing.Color Color,System.Drawing.Font Font,bool overflowHidden=true)
        {
            float width = drawRectangle.Width;
            float height = drawRectangle.Height;
            if (overflowHidden)
            {
                SharpDX.DirectWrite.TextLayout pTextLayout = new SharpDX.DirectWrite.TextLayout(
                    DWrender,
                    Text,
                    utils.Font2Raw(Font),
                    drawRectangle.Width,
                    drawRectangle.Height);
                width = pTextLayout.Metrics.WidthIncludingTrailingWhitespace;
                height = pTextLayout.Metrics.Height;
            }
            if(width<drawRectangle.Width && height<drawRectangle.Height)
            D2Drender.DrawText(Text,
                utils.Font2Raw(Font),
                utils.Retangle2Raw(drawRectangle),
                utils.SoildBrush2Raw(Color));
            
        }
    }
}
