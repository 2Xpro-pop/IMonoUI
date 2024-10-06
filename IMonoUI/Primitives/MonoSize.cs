// This file is ported and adapted from Avalonia (AvaloniaUI / Avalonia)

using System.Globalization;
using System.Numerics;

namespace IMonoUI.Primitives;
public readonly struct MonoSize: IEquatable<MonoSize>
{
    /// <summary>
    /// <summary>
    /// A size representing infinity.
    /// </summary>
    public static readonly MonoSize Infinity = new(double.PositiveInfinity, double.PositiveInfinity);

    /// <summary>
    /// The width.
    /// </summary>
    private readonly double _width;

    /// <summary>
    /// The height.
    /// </summary>
    private readonly double _height;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoSize"/> structure.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public MonoSize(double width, double height)
    {
        _width = width;
        _height = height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoSize"/> structure.
    /// </summary>
    /// <param name="vector2">The vector to take values from.</param>
    public MonoSize(Vector2 vector2) : this(vector2.X, vector2.Y)
    {

    }

    /// <summary>
    /// Gets the aspect ratio of the size.
    /// </summary>
    public double AspectRatio => _width / _height;

    /// <summary>
    /// Gets the width.
    /// </summary>
    public double Width => _width;

    /// <summary>
    /// Gets the height.
    /// </summary>
    public double Height => _height;

    /// <summary>
    /// Checks for equality between two <see cref="MonoSize"/>s.
    /// </summary>
    /// <param name="left">The first size.</param>
    /// <param name="right">The second size.</param>
    /// <returns>True if the sizes are equal; otherwise false.</returns>
    public static bool operator ==(MonoSize left, MonoSize right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Checks for inequality between two <see cref="MonoSize"/>s.
    /// </summary>
    /// <param name="left">The first size.</param>
    /// <param name="right">The second size.</param>
    /// <returns>True if the sizes are unequal; otherwise false.</returns>
    public static bool operator !=(MonoSize left, MonoSize right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Scales a size.
    /// </summary>
    /// <param name="size">The size</param>
    /// <param name="scale">The scaling factor.</param>
    /// <returns>The scaled size.</returns>
    public static MonoSize operator *(MonoSize size, MonoVector scale)
    {
        return new MonoSize(size._width * scale.X, size._height * scale.Y);
    }

    /// <summary>
    /// Scales a size.
    /// </summary>
    /// <param name="size">The size</param>
    /// <param name="scale">The scaling factor.</param>
    /// <returns>The scaled size.</returns>
    public static MonoSize operator /(MonoSize size, MonoVector scale)
    {
        return new MonoSize(size._width / scale.X, size._height / scale.Y);
    }

    /// <summary>
    /// Divides a size by another size to produce a scaling factor.
    /// </summary>
    /// <param name="left">The first size</param>
    /// <param name="right">The second size.</param>
    /// <returns>The scaled size.</returns>
    public static MonoVector operator /(MonoSize left, MonoSize right)
    {
        return new MonoVector(left._width / right._width, left._height / right._height);
    }

    /// <summary>
    /// Scales a size.
    /// </summary>
    /// <param name="size">The size</param>
    /// <param name="scale">The scaling factor.</param>
    /// <returns>The scaled size.</returns>
    public static MonoSize operator *(MonoSize size, double scale)
    {
        return new MonoSize(size._width * scale, size._height * scale);
    }

    /// <summary>
    /// Scales a size.
    /// </summary>
    /// <param name="size">The size</param>
    /// <param name="scale">The scaling factor.</param>
    /// <returns>The scaled size.</returns>
    public static MonoSize operator /(MonoSize size, double scale)
    {
        return new MonoSize(size._width / scale, size._height / scale);
    }

    public static MonoSize operator +(MonoSize size, MonoSize toAdd)
    {
        return new MonoSize(size._width + toAdd._width, size._height + toAdd._height);
    }

    public static MonoSize operator -(MonoSize size, MonoSize toSubtract)
    {
        return new MonoSize(size._width - toSubtract._width, size._height - toSubtract._height);
    }

    /// <summary>
    /// Constrains the size.
    /// </summary>
    /// <param name="constraint">The size to constrain to.</param>
    /// <returns>The constrained size.</returns>
    public MonoSize Constrain(MonoSize constraint)
    {
        return new MonoSize(
            Math.Min(_width, constraint._width),
            Math.Min(_height, constraint._height));
    }

    /// <summary>
    /// Deflates the size by a <see cref="MonoThickness"/>.
    /// </summary>
    /// <param name="thickness">The thickness.</param>
    /// <returns>The deflated size.</returns>
    /// <remarks>The deflated size cannot be less than 0.</remarks>
    public MonoSize Deflate(MonoThickness thickness)
    {
        return new MonoSize(
            Math.Max(0, _width - thickness.Left - thickness.Right),
            Math.Max(0, _height - thickness.Top - thickness.Bottom));
    }

    /// <summary>
    /// Returns a boolean indicating whether the size is equal to the other given size (bitwise).
    /// </summary>
    /// <param name="other">The other size to test equality against.</param>
    /// <returns>True if this size is equal to other; False otherwise.</returns>
    public bool Equals(MonoSize other)
    {
        // ReSharper disable CompareOfFloatsByEqualityOperator
        return _width == other._width &&
               _height == other._height;
        // ReSharper enable CompareOfFloatsByEqualityOperator
    }

    /// <summary>
    /// Returns a boolean indicating whether the size is equal to the other given size (numerically).
    /// </summary>
    /// <param name="other">The other size to test equality against.</param>
    /// <returns>True if this size is equal to other; False otherwise.</returns>
    public bool NearlyEquals(MonoSize other)
    {
        return MathUtilities.AreClose(_width, other._width) &&
               MathUtilities.AreClose(_height, other._height);
    }

    /// <summary>
    /// Checks for equality between a size and an object.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>
    /// True if <paramref name="obj"/> is a size that equals the current size.
    /// </returns>
    public override bool Equals(object? obj) => obj is MonoSize other && Equals(other);

    /// <summary>
    /// Returns a hash code for a <see cref="MonoSize"/>.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = (hash * 23) + Width.GetHashCode();
            hash = (hash * 23) + Height.GetHashCode();
            return hash;
        }
    }

    /// <summary>
    /// Inflates the size by a <see cref="MonoThickness"/>.
    /// </summary>
    /// <param name="thickness">The thickness.</param>
    /// <returns>The inflated size.</returns>
    public MonoSize Inflate(MonoThickness thickness)
    {
        return new MonoSize(
            _width + thickness.Left + thickness.Right,
            _height + thickness.Top + thickness.Bottom);
    }

    /// <summary>
    /// Returns a new <see cref="MonoSize"/> with the same height and the specified width.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <returns>The new <see cref="MonoSize"/>.</returns>
    public MonoSize WithWidth(double width)
    {
        return new MonoSize(width, _height);
    }

    /// <summary>
    /// Returns a new <see cref="MonoSize"/> with the same width and the specified height.
    /// </summary>
    /// <param name="height">The height.</param>
    /// <returns>The new <see cref="MonoSize"/>.</returns>
    public MonoSize WithHeight(double height)
    {
        return new MonoSize(_width, height);
    }

    /// <summary>
    /// Returns the string representation of the size.
    /// </summary>
    /// <returns>The string representation of the size.</returns>
    public override string ToString()
    {
        return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", _width, _height);
    }

    /// <summary>
    /// Deconstructs the size into its Width and Height values.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public void Deconstruct(out double width, out double height)
    {
        width = this._width;
        height = this._height;
    }
}