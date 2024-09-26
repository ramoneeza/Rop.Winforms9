using System.Drawing;

namespace Rop.Winforms9.GraphicsEx.Geom;

/// <summary>
/// Rectangle With Diagonal and Scale
/// </summary>
public record RectangleD
{
    public event EventHandler? Changed;
    private float _x;
    public float X
    {
        get => _x;
        set
        {
            if (_x!=value) return;
            _x = value;
            OnChanged();
        }
    }
    private float _y;
    public float Y
    {
        get => _y;
        set
        {
            if (_y==value) return;
            _y = value;
            OnChanged();
        }
    }
    public float Left
    {
        get => X; 
        set=>X=value; 
    }
    public float Top { 
        get=>Y;
        set => Y = value;
    }
    public float Width
    {
        get { return _unitywidth * Scale; }
        set
        {
            Scale = value / _unitywidth;
        }
    }
    public float Height
    {
        get { return _unityheight * Scale; }
        set
        {
            Scale = value / _unityheight;
        }
    }
    public float Right
    {
        get { return Left + Width; }
        set=> Left = value - Width;
    }
    public float Bottom
    {
        get { return Top + Height; }
        set => Top = value - Height;
    }
    public PointF Location
    {
        get => new PointF(X, Y);
        set
        {
            if (Location == value) return;
            _x = value.X;
            _y = value.Y;
            OnChanged();
        }
    }

    public PointF LeftTop
    {
        get => Location;
        set => Location = value;
    }
    public PointF RightTop
    {
        get =>this[1];
        set => this[1] = value;
    }
    public PointF LeftBottom
    {
        get => this[2];
        set => this[2] = value;
    }
    public PointF RightBottom
    {
        get => this[3];
        set => this[3] = value;
    }
    public PointF Center
    {
        get => this[4];
        set=> this[4] = value;
    }

    public float _initdiagonal;
    public readonly float _unitywidth;
    public readonly float _unityheight;
    public RectangleD(float x, float y, float width, float height)
    {
        _x = x;
        _y = y;
        var sz= new SizeF(width, height);
        sz.Proportion(out float w, out float h, out float d);
        _initdiagonal =d;
        _unitywidth = w;
        _unityheight = h;
        _scale = 1;
    }
    public RectangleF Rectangle => new RectangleF(Left, Top, Width, Height);
    public float Diagonal { 
        get=>_initdiagonal*Scale;
        set => Scale = value / _initdiagonal;
    }
    private float _scale;

    /// <summary>
    /// Get or Set Scale of Rectangle
    /// </summary>
    public float Scale
    {
        get => _scale;
        set
        {
            if (_scale == value) return;
            _scale = value;
            OnChanged();
        }
    }
    /// <summary>
    /// Set Scale without change anchor position
    /// </summary>
    /// <param name="anchor"></param>
    /// <param name="scale"></param>
    public void SetScale(int anchor, float scale)
    {
        if (_scale==scale) return;
        var p = this[anchor];
        _scale = scale;
        _setAnchor(anchor,p);
        OnChanged();
    }

    /// <summary>
    /// Set Diagonal Size without change anchor position
    /// </summary>
    /// <param name="anchor"></param>
    /// <param name="diagonal"></param>
    public void SetDiagonal(int anchor, float diagonal) => SetScale(anchor, diagonal / _initdiagonal);
        
    /// <summary>
    /// Get Size
    /// </summary>
    public SizeF Size=>new SizeF(Width, Height);
        
    /// <summary>
    /// Constructor Rectangle D
    /// </summary>
    /// <param name="location"></param>
    /// <param name="sz"></param>
    public RectangleD(PointF location, SizeF sz):this(location.X, location.Y, sz.Width, sz.Height)
    {
    }
    /// <summary>
    /// Constructor RectangleD
    /// </summary>
    /// <param name="anchor"></param>
    /// <param name="punto"></param>
    /// <param name="sz"></param>
    public RectangleD(int anchor, PointF punto, SizeF sz):this(punto,sz)
    {
        _setAnchor(anchor, punto);
    }
    public RectangleD(RectangleF rectangle):this(rectangle.Location,rectangle.Size)
    {
    }
        
        
    /// <summary>
    /// Current Diagonal as Scale=1
    /// </summary>
    public void ResetScale()
    {
        if (_initdiagonal==Diagonal) return;
        _initdiagonal = Diagonal;
        OnChanged();
    }

    /// <summary>
    /// Get Anchor Point of a Rectangle
    /// 0---1
    /// | 4 |
    /// 2---3
    /// </summary>
    /// <param name="r"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    public PointF GetAnchor(int a)
    {
        return a switch
        {
            0 => new PointF(Left,Top),
            1 => new PointF(Right, Top),
            2 => new PointF(Left, Bottom),
            3 => new PointF(Right, Bottom),
            4 => Location.Add(Size.Multiply(0.5f)),
            _ => Location
        };
    }
    /// <summary>
    /// Get Anchors of a Rectangle
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public PointF[] GetAnchors()
    {
        return [GetAnchor(0), GetAnchor(1), GetAnchor(2), GetAnchor(3), GetAnchor(4)];
    }
    /// <summary>
    /// Set Anchor position of a Rectangle (No Resize)
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="a">Anchor Index</param>
    /// <param name="p">New Position of Anchor</param>
    /// <returns></returns>
        
    public void SetAnchor(int a, PointF p)
    {
        var ap = GetAnchor(a);
        if (ap==p) return;
        _setAnchor(a,p);
        OnChanged();
    }
    private void _setAnchor(int a, PointF p)
    {
        var sz = Size;
        var l = a switch
        {
            0 => p,
            1 => new PointF(p.X - Width, p.Y),
            2 => new PointF(p.X, p.Y - Height),
            3 => new PointF(p.X - Width, p.Y - Height),
            4 => p.Add(Size.Multiply(-0.5f)),
            _ => Location
        };
        _x = l.X;
        _y = l.Y;
    }
        
        
        
    /// <summary>
    /// Get Point of Anchors
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public PointF this[int i]
    {
        get =>GetAnchor(i);
        set=>SetAnchor(i, value);
    }
    /// <summary>
    /// OnChanged Event
    /// </summary>
    protected virtual void OnChanged()
    {
        Changed?.Invoke(this, EventArgs.Empty);
    }
}