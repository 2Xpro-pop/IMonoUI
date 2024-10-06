using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IMonoUI.Primitives;
public readonly struct MonoRect : IEquatable<MonoRect>
{
    /// <summary>
    /// The X position.
    /// </summary>
    private readonly double _x;

    /// <summary>
    /// The Y position.
    /// </summary>
    private readonly double _y;

    /// <summary>
    /// The width.
    /// </summary>
    private readonly double _width;

    /// <summary>
    /// The height.
    /// </summary>
    private readonly double _height;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoRect"/> structure.
    /// </summary>
    /// <param name="x">The X position.</param>
    /// <param name="y">The Y position.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public MonoRect(double x, double y, double width, double height)
    {
        _x = x;
        _y = y;
        _width = width;
        _height = height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoRect"/> structure.
    /// </summary>
    /// <param name="size">The size of the rectangle.</param>
    public MonoRect(MonoSize size)
    {
        _x = 0;
        _y = 0;
        _width = size.Width;
        _height = size.Height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoRect"/> structure.
    /// </summary>
    /// <param name="position">The position of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    public MonoRect(MonoPoint position, MonoSize size)
    {
        _x = position.X;
        _y = position.Y;
        _width = size.Width;
        _height = size.Height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoRect"/> structure.
    /// </summary>
    /// <param name="topLeft">The top left position of the rectangle.</param>
    /// <param name="bottomRight">The bottom right position of the rectangle.</param>
    public MonoRect(MonoPoint topLeft, MonoPoint bottomRight)
    {
        _x = topLeft.X;
        _y = topLeft.Y;
        _width = bottomRight.X - topLeft.X;
        _height = bottomRight.Y - topLeft.Y;
    }

    /// <summary>
    /// Gets the X position.
    /// </summary>
    public double X => _x;

    /// <summary>
    /// Gets the Y position.
    /// </summary>
    public double Y => _y;

    /// <summary>
    /// Gets the width.
    /// </summary>
    public double Width => _width;

    /// <summary>
    /// Gets the height.
    /// </summary>
    public double Height => _height;

    /// <summary>
    /// Gets the position of the rectangle.
    /// </summary>
    public MonoPoint Position => new(_x, _y);

    /// <summary>
    /// Gets the size of the rectangle.
    /// </summary>
    public MonoSize MonoSize => new(_width, _height);

    /// <summary>
    /// Gets the right position of the rectangle.
    /// </summary>
    public double Right => _x + _width;

    /// <summary>
    /// Gets the bottom position of the rectangle.
    /// </summary>
    public double Bottom => _y + _height;

    /// <summary>
    /// Gets the left position.
    /// </summary>
    public double Left => _x;

    /// <summary>
    /// Gets the top position.
    /// </summary>
    public double Top => _y;

    /// <summary>
    /// Gets the top left MonoPoint of the rectangle.
    /// </summary>
    public MonoPoint TopLeft => new(_x, _y);

    /// <summary>
    /// Gets the top right MonoPoint of the rectangle.
    /// </summary>
    public MonoPoint TopRight => new(Right, _y);

    /// <summary>
    /// Gets the bottom left MonoPoint of the rectangle.
    /// </summary>
    public MonoPoint BottomLeft => new(_x, Bottom);

    /// <summary>
    /// Gets the bottom right MonoPoint of the rectangle.
    /// </summary>
    public MonoPoint BottomRight => new(Right, Bottom);

    /// <summary>
    /// Gets the center MonoPoint of the rectangle.
    /// </summary>
    public MonoPoint Center => new(_x + (_width / 2), _y + (_height / 2));

    /// <summary>
    /// Checks for equality between two <see cref="MonoRect"/>s.
    /// </summary>
    /// <param name="left">The first rect.</param>
    /// <param name="right">The second rect.</param>
    /// <returns>True if the rects are equal; otherwise false.</returns>
    public static bool operator ==(MonoRect left, MonoRect right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Checks for inequality between two <see cref="MonoRect"/>s.
    /// </summary>
    /// <param name="left">The first rect.</param>
    /// <param name="right">The second rect.</param>
    /// <returns>True if the rects are unequal; otherwise false.</returns>
    public static bool operator !=(MonoRect left, MonoRect right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Multiplies a rectangle by a scaling vector.
    /// </summary>
    /// <param name="rect">The rectangle.</param>
    /// <param name="scale">The vector scale.</param>
    /// <returns>The scaled rectangle.</returns>
    public static MonoRect operator *(MonoRect rect, MonoVector scale)
    {
        return new MonoRect(
            rect.X * scale.X,
            rect.Y * scale.Y,
            rect.Width * scale.X,
            rect.Height * scale.Y);
    }

    /// <summary>
    /// Multiplies a rectangle by a scale.
    /// </summary>
    /// <param name="rect">The rectangle.</param>
    /// <param name="scale">The scale.</param>
    /// <returns>The scaled rectangle.</returns>
    public static MonoRect operator *(MonoRect rect, double scale)
    {
        return new MonoRect(
            rect.X * scale,
            rect.Y * scale,
            rect.Width * scale,
            rect.Height * scale);
    }

    /// <summary>
    /// Divides a rectangle by a vector.
    /// </summary>
    /// <param name="rect">The rectangle.</param>
    /// <param name="scale">The vector scale.</param>
    /// <returns>The scaled rectangle.</returns>
    public static MonoRect operator /(MonoRect rect, MonoVector scale)
    {
        return new MonoRect(
            rect.X / scale.X,
            rect.Y / scale.Y,
            rect.Width / scale.X,
            rect.Height / scale.Y);
    }

    /// <summary>
    /// Determines whether a MonoPoint is in the bounds of the rectangle.
    /// </summary>
    /// <param name="p">The MonoPoint.</param>
    /// <returns>true if the MonoPoint is in the bounds of the rectangle; otherwise false.</returns>
    public bool Contains(MonoPoint p)
    {
        return p.X >= _x && p.X <= _x + _width &&
               p.Y >= _y && p.Y <= _y + _height;
    }

    /// <summary>
    /// Determines whether a MonoPoint is in the bounds of the rectangle, exclusive of the
    /// rectangle's bottom/right edge.
    /// </summary>
    /// <param name="p">The MonoPoint.</param>
    /// <returns>true if the MonoPoint is in the bounds of the rectangle; otherwise false.</returns>    
    public bool ContainsExclusive(MonoPoint p)
    {
        return p.X >= _x && p.X < _x + _width &&
               p.Y >= _y && p.Y < _y + _height;
    }

    /// <summary>
    /// Determines whether the rectangle fully contains another rectangle.
    /// </summary>
    /// <param name="r">The rectangle.</param>
    /// <returns>true if the rectangle is fully contained; otherwise false.</returns>
    public bool Contains(MonoRect r)
    {
        return Contains(r.TopLeft) && Contains(r.BottomRight);
    }

    /// <summary>
    /// Centers another rectangle in this rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to center.</param>
    /// <returns>The centered rectangle.</returns>
    public MonoRect CenterRect(MonoRect rect)
    {
        return new MonoRect(
            _x + ((_width - rect._width) / 2),
            _y + ((_height - rect._height) / 2),
            rect._width,
            rect._height);
    }

    /// <summary>
    /// Inflates the rectangle.
    /// </summary>
    /// <param name="thickness">The thickness to be subtracted for each side of the rectangle.</param>
    /// <returns>The inflated rectangle.</returns>
    public MonoRect Inflate(double thickness)
    {
        return Inflate(new MonoThickness(thickness));
    }

    /// <summary>
    /// Inflates the rectangle.
    /// </summary>
    /// <param name="thickness">The thickness to be subtracted for each side of the rectangle.</param>
    /// <returns>The inflated rectangle.</returns>
    public MonoRect Inflate(MonoThickness thickness)
    {
        return new MonoRect(
            new MonoPoint(_x - thickness.Left, _y - thickness.Top),
            MonoSize.Inflate(thickness));
    }

    /// <summary>
    /// Deflates the rectangle.
    /// </summary>
    /// <param name="thickness">The thickness to be subtracted for each side of the rectangle.</param>
    /// <returns>The deflated rectangle.</returns>
    public MonoRect Deflate(double thickness)
    {
        return Deflate(new MonoThickness(thickness));
    }

    /// <summary>
    /// Deflates the rectangle by a <see cref="MonoThickness"/>.
    /// </summary>
    /// <param name="thickness">The thickness to be subtracted for each side of the rectangle.</param>
    /// <returns>The deflated rectangle.</returns>
    public MonoRect Deflate(MonoThickness thickness)
    {
        return new MonoRect(
            new MonoPoint(_x + thickness.Left, _y + thickness.Top),
            MonoSize.Deflate(thickness));
    }

    /// <summary>
    /// Returns a boolean indicating whether the rect is equal to the other given rect.
    /// </summary>
    /// <param name="other">The other rect to test equality against.</param>
    /// <returns>True if this rect is equal to other; False otherwise.</returns>
    public bool Equals(MonoRect other)
    {
        // ReSharper disable CompareOfFloatsByEqualityOperator
        return _x == other._x &&
               _y == other._y &&
               _width == other._width &&
               _height == other._height;
        // ReSharper enable CompareOfFloatsByEqualityOperator
    }

    /// <summary>
    /// Returns a boolean indicating whether the given object is equal to this rectangle.
    /// </summary>
    /// <param name="obj">The object to compare against.</param>
    /// <returns>True if the object is equal to this rectangle; false otherwise.</returns>
    public override bool Equals(object? obj) => obj is MonoRect other && Equals(other);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Width, Height);
    }

    /// <summary>
    /// Gets the intersection of two rectangles.
    /// </summary>
    /// <param name="rect">The other rectangle.</param>
    /// <returns>The intersection.</returns>
    public MonoRect Intersect(MonoRect rect)
    {
        var newLeft = (rect.X > X) ? rect.X : X;
        var newTop = (rect.Y > Y) ? rect.Y : Y;
        var newRight = (rect.Right < Right) ? rect.Right : Right;
        var newBottom = (rect.Bottom < Bottom) ? rect.Bottom : Bottom;

        if ((newRight > newLeft) && (newBottom > newTop))
        {
            return new MonoRect(newLeft, newTop, newRight - newLeft, newBottom - newTop);
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// Determines whether a rectangle intersects with this rectangle.
    /// </summary>
    /// <param name="rect">The other rectangle.</param>
    /// <returns>
    /// True if the specified rectangle intersects with this one; otherwise false.
    /// </returns>
    public bool Intersects(MonoRect rect)
    {
        return (rect.X < Right) && (X < rect.Right) && (rect.Y < Bottom) && (Y < rect.Bottom);
    }

    /// <summary>
    /// Returns the axis-aligned bounding box of a transformed rectangle.
    /// </summary>
    /// <param name="matrix">The transform.</param>
    /// <returns>The bounding box</returns>
    public MonoRect TransformToAABB(MonoMatrix matrix)
    {
        ReadOnlySpan<MonoPoint> MonoPoints =
        [
            TopLeft.Transform(matrix),
            TopRight.Transform(matrix),
            BottomRight.Transform(matrix),
            BottomLeft.Transform(matrix)
        ];

        var left = double.MaxValue;
        var right = double.MinValue;
        var top = double.MaxValue;
        var bottom = double.MinValue;

        foreach (var p in MonoPoints)
        {
            if (p.X < left) left = p.X;
            if (p.X > right) right = p.X;
            if (p.Y < top) top = p.Y;
            if (p.Y > bottom) bottom = p.Y;
        }

        return new MonoRect(new MonoPoint(left, top), new MonoPoint(right, bottom));
    }

    internal MonoRect TransformToAABB(Matrix4x4 matrix)
    {
        ReadOnlySpan<MonoPoint> MonoPoints =
        [
            TopLeft.Transform(matrix),
            TopRight.Transform(matrix),
            BottomRight.Transform(matrix),
            BottomLeft.Transform(matrix)
        ];

        var left = double.MaxValue;
        var right = double.MinValue;
        var top = double.MaxValue;
        var bottom = double.MinValue;

        foreach (var p in MonoPoints)
        {
            if (p.X < left) left = p.X;
            if (p.X > right) right = p.X;
            if (p.Y < top) top = p.Y;
            if (p.Y > bottom) bottom = p.Y;
        }

        return new MonoRect(new MonoPoint(left, top), new MonoPoint(right, bottom));
    }

    /// <summary>
    /// Translates the rectangle by an offset.
    /// </summary>
    /// <param name="offset">The offset.</param>
    /// <returns>The translated rectangle.</returns>
    public MonoRect Translate(MonoVector offset)
    {
        return new MonoRect(Position + offset, MonoSize);
    }

    /// <summary>
    /// Normalizes the rectangle so both the <see cref="Width"/> and <see 
    /// cref="Height"/> are positive, without changing the location of the rectangle
    /// </summary>
    /// <returns>Normalized Rect</returns>
    /// <remarks>
    /// Empty rect will be return when Rect contains invalid values. Like NaN.
    /// </remarks>
    public MonoRect Normalize()
    {
        MonoRect rect = this;

        if (double.IsNaN(rect.Right) || double.IsNaN(rect.Bottom) ||
            double.IsNaN(rect.X) || double.IsNaN(rect.Y) ||
            double.IsNaN(Height) || double.IsNaN(Width))
        {
            return default;
        }

        if (rect.Width < 0)
        {
            var x = X + Width;
            var width = X - x;

            rect = rect.WithX(x).WithWidth(width);
        }

        if (rect.Height < 0)
        {
            var y = Y + Height;
            var height = Y - y;

            rect = rect.WithY(y).WithHeight(height);
        }

        return rect;
    }

    /// <summary>
    /// Gets the union of two rectangles.
    /// </summary>
    /// <param name="rect">The other rectangle.</param>
    /// <returns>The union.</returns>
    public MonoRect Union(MonoRect rect)
    {
        if (Width == 0 && Height == 0)
        {
            return rect;
        }
        else if (rect.Width == 0 && rect.Height == 0)
        {
            return this;
        }
        else
        {
            var x1 = Math.Min(X, rect.X);
            var x2 = Math.Max(Right, rect.Right);
            var y1 = Math.Min(Y, rect.Y);
            var y2 = Math.Max(Bottom, rect.Bottom);

            return new MonoRect(new MonoPoint(x1, y1), new MonoPoint(x2, y2));
        }
    }

    internal static MonoRect? Union(MonoRect? left, MonoRect? right)
    {
        if (left == null)
            return right;
        if (right == null)
            return left;
        return left.Value.Union(right.Value);
    }

    /// <summary>
    /// Returns a new <see cref="MonoRect"/> with the specified X position.
    /// </summary>
    /// <param name="x">The x position.</param>
    /// <returns>The new <see cref="MonoRect"/>.</returns>
    public MonoRect WithX(double x)
    {
        return new MonoRect(x, _y, _width, _height);
    }

    /// <summary>
    /// Returns a new <see cref="MonoRect"/> with the specified Y position.
    /// </summary>
    /// <param name="y">The y position.</param>
    /// <returns>The new <see cref="MonoRect"/>.</returns>
    public MonoRect WithY(double y)
    {
        return new MonoRect(_x, y, _width, _height);
    }

    /// <summary>
    /// Returns a new <see cref="MonoRect"/> with the specified width.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <returns>The new <see cref="MonoRect"/>.</returns>
    public MonoRect WithWidth(double width)
    {
        return new MonoRect(_x, _y, width, _height);
    }

    /// <summary>
    /// Returns a new <see cref="MonoRect"/> with the specified height.
    /// </summary>
    /// <param name="height">The height.</param>
    /// <returns>The new <see cref="MonoRect"/>.</returns>
    public MonoRect WithHeight(double height)
    {
        return new MonoRect(_x, _y, _width, height);
    }

    /// <summary>
    /// Returns the string representation of the rectangle.
    /// </summary>
    /// <returns>The string representation of the rectangle.</returns>
    public override string ToString()
    {
        return string.Format(
            CultureInfo.InvariantCulture,
            "{0}, {1}, {2}, {3}",
            _x,
            _y,
            _width,
            _height);
    }

    /// <summary>
    /// This method should be used internally to check for the rect emptiness
    /// Once we add support for WPF-like empty rects, there will be an actual implementation
    /// For now it's internal to keep some loud community members happy about the API being pretty 
    /// </summary>
    internal bool IsEmpty() => this == default;
}