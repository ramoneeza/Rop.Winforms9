namespace Rop.Winforms9.GraphicsEx.Geom;

/// <summary>
/// Corners enumeration
/// </summary>
[Flags]
public enum RectangleCorners
{
    /// <summary>
    /// None 
    /// </summary>
    None = 0,
    /// <summary>
    /// TopLeft
    /// </summary>
    TopLeft = 1,
    /// <summary>
    /// TopRight
    /// </summary>
    TopRight = 2,
    /// <summary>
    /// BottomLeft
    /// </summary>
    BottomLeft = 4,
    /// <summary>
    /// BottomRight
    /// </summary>
    BottomRight = 8,
    /// <summary>
    /// AllCorners
    /// </summary>
    All = TopLeft | TopRight | BottomLeft | BottomRight
}