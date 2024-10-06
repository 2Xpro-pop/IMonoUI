// This file is ported and adapted from Avalonia (AvaloniaUI / Avalonia)

using System.Globalization;
using System.Numerics;

namespace IMonoUI.Primitives;

public readonly struct MonoPoint : IEquatable<MonoPoint>
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
    /// Initializes a new instance of the <see cref="MonoPoint"/> structure.
    /// </summary>
    /// <param name="x">The X position.</param>
    /// <param name="y">The Y position.</param>
    public MonoPoint(double x, double y)
    {
        _x = x;
        _y = y;
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
    /// Converts the <see cref="MonoPoint"/> to a <see cref="MonoVector"/>.
    /// </summary>
    /// <param name="p">The point.</param>
    public static implicit operator MonoVector(MonoPoint p)
    {
        return new MonoVector(p._x, p._y);
    }

    /// <summary>
    /// Negates a point.
    /// </summary>
    /// <param name="a">The point.</param>
    /// <returns>The negated point.</returns>
    public static MonoPoint operator -(MonoPoint a)
    {
        return new MonoPoint(-a._x, -a._y);
    }

    /// <summary>
    /// Checks for equality between two <see cref="MonoPoint"/>s.
    /// </summary>
    /// <param name="left">The first point.</param>
    /// <param name="right">The second point.</param>
    /// <returns>True if the points are equal; otherwise false.</returns>
    public static bool operator ==(MonoPoint left, MonoPoint right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Checks for inequality between two <see cref="MonoPoint"/>s.
    /// </summary>
    /// <param name="left">The first point.</param>
    /// <param name="right">The second point.</param>
    /// <returns>True if the points are unequal; otherwise false.</returns>
    public static bool operator !=(MonoPoint left, MonoPoint right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Adds two points.
    /// </summary>
    /// <param name="a">The first point.</param>
    /// <param name="b">The second point.</param>
    /// <returns>A point that is the result of the addition.</returns>
    public static MonoPoint operator +(MonoPoint a, MonoPoint b)
    {
        return new MonoPoint(a._x + b._x, a._y + b._y);
    }

    /// <summary>
    /// Adds a vector to a point.
    /// </summary>
    /// <param name="a">The point.</param>
    /// <param name="b">The vector.</param>
    /// <returns>A point that is the result of the addition.</returns>
    public static MonoPoint operator +(MonoPoint a, MonoVector b)
    {
        return new MonoPoint(a._x + b.X, a._y + b.Y);
    }

    /// <summary>
    /// Subtracts two points.
    /// </summary>
    /// <param name="a">The first point.</param>
    /// <param name="b">The second point.</param>
    /// <returns>A point that is the result of the subtraction.</returns>
    public static MonoPoint operator -(MonoPoint a, MonoPoint b)
    {
        return new MonoPoint(a._x - b._x, a._y - b._y);
    }

    /// <summary>
    /// Subtracts a vector from a point.
    /// </summary>
    /// <param name="a">The point.</param>
    /// <param name="b">The vector.</param>
    /// <returns>A point that is the result of the subtraction.</returns>
    public static MonoPoint operator -(MonoPoint a, MonoVector b)
    {
        return new MonoPoint(a._x - b.X, a._y - b.Y);
    }

    /// <summary>
    /// Multiplies a point by a factor coordinate-wise
    /// </summary>
    /// <param name="p">MonoPoint to multiply</param>
    /// <param name="k">Factor</param>
    /// <returns>Points having its coordinates multiplied</returns>
    public static MonoPoint operator *(MonoPoint p, double k) => new MonoPoint(p.X * k, p.Y * k);

    /// <summary>
    /// Multiplies a point by a factor coordinate-wise
    /// </summary>
    /// <param name="p">MonoPoint to multiply</param>
    /// <param name="k">Factor</param>
    /// <returns>Points having its coordinates multiplied</returns>
    public static MonoPoint operator *(double k, MonoPoint p) => new MonoPoint(p.X * k, p.Y * k);

    /// <summary>
    /// Divides a point by a factor coordinate-wise
    /// </summary>
    /// <param name="p">MonoPoint to divide by</param>
    /// <param name="k">Factor</param>
    /// <returns>Points having its coordinates divided</returns>
    public static MonoPoint operator /(MonoPoint p, double k) => new MonoPoint(p.X / k, p.Y / k);

    /// <summary>
    /// Applies a matrix to a point.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="matrix">The matrix.</param>
    /// <returns>The resulting point.</returns>
    public static MonoPoint operator *(MonoPoint point, MonoMatrix matrix) => matrix.Transform(point);

    /// <summary>
    /// Computes the Euclidean distance between the two given points.
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The Euclidean distance.</returns>
    public static double Distance(MonoPoint value1, MonoPoint value2)
    {
        double distanceSquared = ((value2.X - value1.X) * (value2.X - value1.X)) +
                                 ((value2.Y - value1.Y) * (value2.Y - value1.Y));
        return Math.Sqrt(distanceSquared);
    }

    /// <summary>
    /// Returns a boolean indicating whether the point is equal to the other given point (bitwise).
    /// </summary>
    /// <param name="other">The other point to test equality against.</param>
    /// <returns>True if this point is equal to other; False otherwise.</returns>
    public bool Equals(MonoPoint other)
    {
        // ReSharper disable CompareOfFloatsByEqualityOperator
        return _x == other._x &&
               _y == other._y;
        // ReSharper enable CompareOfFloatsByEqualityOperator
    }

    /// <summary>
    /// Returns a boolean indicating whether the point is equal to the other given point
    /// (numerically).
    /// </summary>
    /// <param name="other">The other point to test equality against.</param>
    /// <returns>True if this point is equal to other; False otherwise.</returns>
    public bool NearlyEquals(MonoPoint other)
    {
        return MathUtilities.AreClose(_x, other._x) &&
               MathUtilities.AreClose(_y, other._y);
    }

    /// <summary>
    /// Checks for equality between a point and an object.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>
    /// True if <paramref name="obj"/> is a point that equals the current point.
    /// </returns>
    public override bool Equals(object? obj) => obj is MonoPoint other && Equals(other);

    /// <summary>
    /// Returns a hash code for a <see cref="MonoPoint"/>.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = (hash * 23) + _x.GetHashCode();
            hash = (hash * 23) + _y.GetHashCode();
            return hash;
        }
    }

    /// <summary>
    /// Returns the string representation of the point.
    /// </summary>
    /// <returns>The string representation of the point.</returns>
    public override string ToString()
    {
        return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", _x, _y);
    }

    /// <summary>
    /// Transforms the point by a matrix.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <returns>The transformed point.</returns>
    public MonoPoint Transform(MonoMatrix transform) => transform.Transform(this);

    internal MonoPoint Transform(Matrix4x4 matrix)
    {
        var vec = Vector2.Transform(new Vector2((float)X, (float)Y), matrix);
        return new MonoPoint(vec.X, vec.Y);
    }

    /// <summary>
    /// Returns a new point with the specified X coordinate.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <returns>The new point.</returns>
    public MonoPoint WithX(double x)
    {
        return new MonoPoint(x, _y);
    }

    /// <summary>
    /// Returns a new point with the specified Y coordinate.
    /// </summary>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>The new point.</returns>
    public MonoPoint WithY(double y)
    {
        return new MonoPoint(_x, y);
    }

    /// <summary>
    /// Deconstructs the point into its X and Y coordinates.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    public void Deconstruct(out double x, out double y)
    {
        x = this._x;
        y = this._y;
    }
}