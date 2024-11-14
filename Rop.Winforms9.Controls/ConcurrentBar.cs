using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rop.Winforms9.GraphicsEx.Colors;
using Rop.Winforms9.GraphicsEx.Geom;
using Timer = System.Windows.Forms.Timer;

namespace Rop.Winforms9.Controls
{
    [DesignerCategory("Code")]
    public class ConcurrentBar:Control
    {
        private readonly Timer _timer;
        private int _maximum=-1;
        private int _value=0;
        private string _textFormat="";
        private int _barHeight = 12;
        private Color _borderColor = Color.Gray;
        private Color _barColor = Color.CornflowerBlue;
        private Color _barBackground = Color.Empty;
        private int _radius = 0;
        private bool _isMarquee;

        private bool _usePercent;

        [DefaultValue(-1)]
        public int Maximum
        {
            get => _maximum;
            set => _setProperty(ref _maximum,value,v=>
            {
                if (_value > v) _value = _maximum;
                _timer.Enabled=_maximum>= 0;
            });
        }

        [DefaultValue(0)]
        public int Value
        {
            get => _value;
            set=>_setProperty(ref _value,value);
        }

        [DefaultValue(false)]
        public bool UsePercent
        {
            get => _usePercent;
            set => _setProperty(ref _usePercent, value);
        }

        public int PercentValue=>Maximum>0 ? Math.Clamp((100 * Value) / Maximum,0,100) : 0;


        [DefaultValue("")]
        public string TextFormat
        {
            get => _textFormat;
            set=>_setProperty(ref _textFormat, value);
        }


        [DefaultValue(12)]
        public int BarHeight
        {
            get => _barHeight;
            set=> _setProperty(ref _barHeight, value);
        }

        protected override Size DefaultSize=> new Size(100, 15);


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get => _borderColor;
            set=> _setProperty(ref _borderColor, value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BarColor
        {
            get => _barColor;
            set => _setProperty(ref _barColor, value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BarBackground
        {
            get => _barBackground;
            set => _setProperty(ref _barBackground, value);
        }

        [DefaultValue(0)]
        public int Radius
        {
            get => _radius;
            set=> _setProperty(ref _radius, value);
        }

        private Rectangle? _barBoundsCache;
        private GraphicsPath? _slotcache;


        private Rectangle _barBounds()
        {
            if (_barBoundsCache == null)
            {
                var y = (Height - BarHeight) / 2;
                _barBoundsCache = new Rectangle(0, y, Width, BarHeight);
            }
            return _barBoundsCache.Value;
        }

        private GraphicsPath _barSlot()
        {
            if (_slotcache == null)
            {
                var rect = _barBounds().DeltaSize(-1, -1);
                var r = Radius;
                if (r > rect.Height / 2)
                {
                    r = rect.Height / 2;
                }

                if (r > 0)
                {
                    _slotcache= RoundedRectangle.Create(rect, r);
                }
                else
                {
                    var path = new GraphicsPath();
                    path.AddRectangle(rect);
                    _slotcache=path;
                }
            }
            return _slotcache;
        }

        [DefaultValue(false)]
        public bool IsMarquee
        {
            get => _isMarquee;
            set => _setProperty(ref _isMarquee, value);
        }
        private void _setProperty<T>(ref T field, T value, Action<T>? setter = null)
        {
            if (Equals(field, value)) return;
            field = value;
            setter?.Invoke(value);
            Invalidate();
        }
        public ConcurrentBar()
        {
            _timer = new Timer()
            {
                Interval = 50, Enabled = false
            };
            _timer.Tick += (s, e) =>Invalidate(_barBounds());
            // Habilitar doble búfer
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            // ReSharper disable once VirtualMemberCallInConstructor
            DoubleBuffered = true;
        }

        private readonly long _speed = 100000;

        protected override void OnResize(EventArgs e)
        {
            _slotcache?.Dispose();
            _slotcache = null;
            _barBoundsCache= null;
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(BackColor);
            var p = PercentValue;
            var v = Value;
            if (Maximum < 0)
            {
                if (!DesignMode) return;
                p = 50;
                v = 50;
            }
            var w = this.Width;
            var slot= _barSlot();
            if (!BarBackground.IsTOrEmpty()) e.Graphics.FillPath(new SolidBrush(BarBackground), slot);

            if (IsMarquee)
                DrawMarquee();
            else
                DrawBar();
            // draw the border
            if (!BorderColor.IsTOrEmpty())
            {
                var pen = new Pen(BorderColor);
                e.Graphics.DrawPath(pen,slot);
            }
            return;
            // Local functions
            
            void DrawBar()
            {
                var w0=(w * p) / 100;
                var w1 = w - w0;
                // create two regions for the two parts
                var r0 = new Rectangle(0, 0, w0, this.Height);
                var r1 = new Rectangle(w0, 0, w1, this.Height);
                e.Graphics.SetClip(slot);
                e.Graphics.FillRectangle(new SolidBrush(BarColor), r0);
                e.Graphics.ResetClip();
// draw the text
                var text = string.IsNullOrEmpty(TextFormat)?Value.ToString(): (UsePercent?string.Format(TextFormat,p):string.Format(TextFormat, v));
                var size = e.Graphics.MeasureString(text, Font);
                var x = (w - size.Width) / 2;
                var y = (this.Height - size.Height) / 2;
                e.Graphics.SetClip(r0);
                e.Graphics.DrawString(text, Font, new SolidBrush(BarBackground.IfTOrEmpty(BackColor)), x, y);
                e.Graphics.ResetClip();
                e.Graphics.SetClip(r1);
                e.Graphics.DrawString(text, Font, new SolidBrush(BarColor), x, y);
                e.Graphics.ResetClip();
            }
            

            void DrawMarquee()
            {
                var s = w / 3;
                var x1 =(int)((DateTime.Now.TimeOfDay.Ticks/_speed)%(w+2*s))-s;
                var x2 = x1 + s;
                if (x2> w) x2 = w;
                var r0 = new Rectangle(0, 0,x1 , this.Height);
                var r1 = new Rectangle(x1, 0, s, this.Height);
                var r2 = new Rectangle(x2, 0, w-x2, this.Height);
                e.Graphics.SetClip(slot);
                e.Graphics.FillRectangle(new SolidBrush(BarColor), r1);
                e.Graphics.ResetClip();
                var text = TextFormat.Replace("{0}", "");
                var size = e.Graphics.MeasureString(text, Font);
                var x = (w - size.Width) / 2;
                var y = (this.Height - size.Height) / 2;
                e.Graphics.SetClip(r0);
                e.Graphics.DrawString(text, Font, new SolidBrush(BarColor), x, y);
                e.Graphics.ResetClip();
                e.Graphics.SetClip(r1);
                e.Graphics.DrawString(text, Font, new SolidBrush(BarBackground.IfTOrEmpty(BackColor)), x, y);
                e.Graphics.ResetClip();
                e.Graphics.SetClip(r2);
                e.Graphics.DrawString(text, Font, new SolidBrush(BarColor), x, y);
                e.Graphics.ResetClip();
            }
        }

        public IProgress<int> Start(int maxvalue, string textformat = "")
        {
            IsMarquee = false;
            Maximum = maxvalue;
            TextFormat = textformat;
            Value = 0;
            return new Progress<int>(v => Value = v);
        }
        public IProgress<int> StartDelta(int maxvalue, string textformat = "")
        {
            IsMarquee = false;
            Maximum = maxvalue;
            TextFormat = textformat;
            Value = 0;
            return new Progress<int>(v => Value += v);
        }
        
        public void StartMarquee(string textformat = "")
        {
            IsMarquee = true;
            Maximum = 0;
            TextFormat = textformat;
            Value = 0;
        }
        
        public void End()
        {
            Maximum = -1;
        }
    }
}
