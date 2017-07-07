using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpDX.Direct2D1;

namespace BalthasarLib.D2DPainter
{
    public partial class D2DPainterBox : UserControl
    {
        SharpDX.DirectWrite.Factory FactoryDWrite;
        WindowRenderTarget RenderTarget2D;
        BackgroundWorker renderworker = new BackgroundWorker();
        D2DGraphics g;
        Point MousePoint;
        // 创建一个委托，返回类型为void，两个参数
        public delegate void OnD2DPaintHandler(object sender, D2DPaintEventArgs e);
        // 将创建的委托和特定事件关联,在这里特定的事件为KeyDown
        public event OnD2DPaintHandler D2DPaint;

        private bool _antialias = false;
        public bool Antialias {
            get { return _antialias; }
            set {
                if (RenderTarget2D != null)
                {
                    _antialias = value;
                    RenderTarget2D.AntialiasMode = AntialiasMode.Aliased;
                    if (_antialias) RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;
                }
            }
        }

        public D2DPainterBox()
        {
            InitializeComponent();
            Initialize();
            renderworker.DoWork += renderworker_DoWork;
        }

        void renderworker_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Initialize()
        {
            // 创建 Direct2D 单线程工厂。
            Factory factory = new Factory(FactoryType.MultiThreaded);
            FactoryDWrite = new SharpDX.DirectWrite.Factory();

            // 渲染参数。

            var d2PixelFormat = new PixelFormat(SharpDX.DXGI.Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied);
            RenderTargetProperties renderProps = new RenderTargetProperties
            {
                PixelFormat = d2PixelFormat,
                Usage = RenderTargetUsage.None,
                Type = RenderTargetType.Default
            };
            // 渲染目标属性。
            HwndRenderTargetProperties hwndProps = new HwndRenderTargetProperties()
            {
                // 承载控件的句柄。
                Hwnd = this.Handle,
                // 控件的尺寸。
                PixelSize = new SharpDX.Size2(this.ClientSize.Width, this.ClientSize.Height),
                PresentOptions = PresentOptions.None
            };
            // 渲染目标。
            
            RenderTarget2D = new WindowRenderTarget(factory, renderProps, hwndProps);
            RenderTarget2D.AntialiasMode = AntialiasMode.Aliased;
            if (Antialias) RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;

            g = new D2DGraphics(RenderTarget2D, FactoryDWrite);
        }

        private void D2DPainterBox_Load(object sender, EventArgs e)
        {

        }
        public override void Refresh()
        {
            if (RenderTarget2D != null)
            {
                OnPaint(null);
            }
            else
            {
                base.Refresh();
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            try
            {
                if (RenderTarget2D != null)
                {
                    RenderTarget2D.Resize(new SharpDX.Size2(this.ClientSize.Width, this.ClientSize.Height));
                }
            }
            catch { ;}
        }
        public void ReflectedResize()
        {
            try
            {
                if (RenderTarget2D != null)
                {
                    RenderTarget2D.Resize(new SharpDX.Size2(this.ClientSize.Width, this.ClientSize.Height));
                    OnPaint(null);
                }
            }
            catch { ;}
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (RenderTarget2D != null)
                {
                    RenderTarget2D.BeginDraw();
                    g.Clear();
                    if (D2DPaint != null)
                    {
                        D2DPaintEventArgs d2de = new D2DPaintEventArgs(g, new Rectangle(0, 0, this.Width, this.Height),MousePoint);
                        D2DPaint(this, d2de);
                    }
                    RenderTarget2D.EndDraw();
                }
            }
            catch { ;}
        }

        private void D2DPainterBox_MouseMove(object sender, MouseEventArgs e)
        {
            MousePoint = e.Location;// this.PointToClient(e.Location);
        }   
    }
}
