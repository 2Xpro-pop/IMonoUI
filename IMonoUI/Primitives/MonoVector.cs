// This file is ported and adapted from Avalonia (AvaloniaUI / Avalonia)

using System.Globalization;
using System.Numerics;

namespace IMonoUI.Primitives;
public readonly struct MonoVector : IEquatable<MonoVector>
{
    /// <summary>
    /// The X component.
    /// </summary>
    private readonly double _x;

    /// <summary>
    /// The Y component.
    /// </summary>
    private readonly double _y;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoVector"/> structure.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    public MonoVector(double x, double y)
    {
        _x = x;
        _y = y;
    }

    /// <summary>
    /// Gets the X component.
    /// </summary>
    public double X => _x;

    /// <summary>
    /// Gets the Y component.
    /// </summary>
    public double Y => _y;

    /// <summary>
    /// Converts the <see cref="MonoVector"/> to a <see cref="MonoPoint"/>.
    /// </summary>
    /// <param name="a">The vector.</param>
    public static explicit operator MonoPoint(MonoVector a)
    {
        return new MonoPoint(a._x, a._y);
    }

    /// <summary>
    /// Calculates the dot product of two vectors.
    /// </summary>
    /// <param name="a">First vector.</param>
    /// <param name="b">Second vector.</param>
    /// <returns>The dot product.</returns>
    public static double operator *(MonoVector a, MonoVector b)
        => Dot(a, b);

    /// <summary>
    /// Scales a vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="scale">The scaling factor.</param>
    /// <returns>The scaled vector.</returns>
    public static MonoVector operator *(MonoVector vector, double scale)
        => Multiply(vector, scale);

    /// <summary>
    /// Scales a vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="scale">The scaling factor.</param>
    /// <returns>The scaled vector.</returns>
    public static MonoVector operator *(double scale, MonoVector vector)
        => Multiply(vector, scale);

    /// <summary>
    /// Scales a vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="scale">The divisor.</param>
    /// <returns>The scaled vector.</returns>
    public static MonoVector operator /(MonoVector vector, double scale)
        => Divide(vector, scale);

    /// <summary>
    /// Length of the vector.
    /// </summary>
    public double Length => Math.Sqrt(SquaredLength);

    /// <summary>
    /// Squared Length of the vector.
    /// </summary>
    public double SquaredLength => _x * _x + _y * _y;

    /// <summary>
    /// Negates a vector.
    /// </summary>
    /// <param name="a">The vector.</param>
    /// <returns>The negated vector.</returns>
    public static MonoVector operator -(MonoVector a)
        => Negate(a);

    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>A vector that is the result of the addition.</returns>
    public static MonoVector operator +(MonoVector a, MonoVector b)
        => Add(a, b);

    /// <summary>
    /// Subtracts two vectors.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>A vector that is the result of the subtraction.</returns>
    public static MonoVector operator -(MonoVector a, MonoVector b)
        => Subtract(a, b);

    /// <summary>
    /// Check if two vectors are equal (bitwise).
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(MonoVector other)
    {
        // ReSharper disable CompareOfFloatsByEqualityOperator
        return _x == other._x && _y == other._y;
        // ReSharper restore CompareOfFloatsByEqualityOperator
    }

    /// <summary>
    /// Check if two vectors are nearly equal (numerically).
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <returns>True if vectors are nearly equal.</returns>
    public bool NearlyEquals(MonoVector other)
    {
        return MathUtilities.AreClose(_x, other._x) &&
               MathUtilities.AreClose(_y, other._y);
    }

    public override bool Equals(object? obj) => obj is MonoVector other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            return (_x.GetHashCode() * 397) ^ _y.GetHashCode();
        }
    }

    public static bool operator ==(MonoVector left, MonoVector right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MonoVector left, MonoVector right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    /// Returns the string representation of the vector.
    /// </summary>
    /// <returns>The string representation of the vector.</returns>
    public override string ToString()
    {
        return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", _x, _y);
    }

    /// <summary>
    /// Returns a new vector with the specified X component.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <returns>The new vector.</returns>
    public MonoVector WithX(double x)
    {
        return new MonoVector(x, _y);
    }

    /// <summary>
    /// Returns a new vector with the specified Y component.
    /// </summary>
    /// <param name="y">The Y component.</param>
    /// <returns>The new vector.</returns>
    public MonoVector WithY(double y)
    {
        return new MonoVector(_x, y);
    }

    /// <summary>
    /// Returns a normalized version of this vector.
    /// </summary>
    /// <returns>The normalized vector.</returns>
    public MonoVector Normalize()
        => Normalize(this);

    /// <summary>
    /// Returns a negated version of this vector.
    /// </summary>
    /// <returns>The negated vector.</returns>
    public MonoVector Negate()
        => Negate(this);

    /// <summary>
    /// Returns the dot product of two vectors.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>The dot product.</returns>
    public static double Dot(MonoVector a, MonoVector b)
        => a._x * b._x + a._y * b._y;

    /// <summary>
    /// Returns the cross product of two vectors.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>The cross product.</returns>
    public static double Cross(MonoVector a, MonoVector b)
        => a._x * b._y - a._y * b._x;

    /// <summary>
    /// Normalizes the given vector.
    /// </summary>
    /// <param name="vector">The vector</param>
    /// <returns>The normalized vector.</returns>
    public static MonoVector Normalize(MonoVector vector)
        => Divide(vector, vector.Length);

    /// <summary>
    /// Divides the first vector by the second.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>The scaled vector.</returns>
    public static MonoVector Divide(MonoVector a, MonoVector b)
        => new MonoVector(a._x / b._x, a._y / b._y);

    /// <summary>
    /// Divides the vector by the given scalar.
    /// </summary>
    /// <param name="vector">The vector</param>
    /// <param name="scalar">The scalar value</param>
    /// <returns>The scaled vector.</returns>
    public static MonoVector Divide(MonoVector vector, double scalar)
        => new MonoVector(vector._x / scalar, vector._y / scalar);

    /// <summary>
    /// Multiplies the first vector by the second.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>The scaled vector.</returns>
    public static MonoVector Multiply(MonoVector a, MonoVector b)
        => new MonoVector(a._x * b._x, a._y * b._y);

    /// <summary>
    /// Multiplies the vector by the given scalar.
    /// </summary>
    /// <param name="vector">The vector</param>
    /// <param name="scalar">The scalar value</param>
    /// <returns>The scaled vector.</returns>
    public static MonoVector Multiply(MonoVector vector, double scalar)
        => new MonoVector(vector._x * scalar, vector._y * scalar);

    /// <summary>
    /// Adds the second to the first vector
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>The summed vector.</returns>
    public static MonoVector Add(MonoVector a, MonoVector b)
        => new MonoVector(a._x + b._x, a._y + b._y);

    /// <summary>
    /// Subtracts the second from the first vector
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>The difference vector.</returns>
    public static MonoVector Subtract(MonoVector a, MonoVector b)
        => new MonoVector(a._x - b._x, a._y - b._y);

    /// <summary>
    /// Negates the vector
    /// </summary>
    /// <param name="vector">The vector to negate.</param>
    /// <returns>The scaled vector.</returns>
    public static MonoVector Negate(MonoVector vector)
        => new MonoVector(-vector._x, -vector._y);

    /// <summary>
    /// Returns the vector (0.0, 0.0).
    /// </summary>
    public static MonoVector Zero
        => new MonoVector(0, 0);

    /// <summary>
    /// Returns the vector (1.0, 1.0).
    /// </summary>
    public static MonoVector One
        => new MonoVector(1, 1);

    /// <summary>
    /// Returns the vector (1.0, 0.0).
    /// </summary>
    public static MonoVector UnitX
        => new MonoVector(1, 0);

    /// <summary>
    /// Returns the vector (0.0, 1.0).
    /// </summary>
    public static MonoVector UnitY
        => new MonoVector(0, 1);

    /// <summary>
    /// Deconstructs the vector into its X and Y components.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    public void Deconstruct(out double x, out double y)
    {
        x = this._x;
        y = this._y;
    }

    internal Vector2 ToVector2() => new Vector2((float)X, (float)Y);

    internal MonoVector(Vector2 v) : this(v.X, v.Y)
    {

    }

    /// <summary>
    /// Returns a vector whose elements are the absolute values of each of the specified vector's elements.
    /// </summary>
    /// <returns></returns>
    public MonoVector Abs() => new(Math.Abs(X), Math.Abs(Y));

    /// <summary>
    /// Restricts a vector between a minimum and a maximum value.
    /// </summary>
    public static MonoVector Clamp(MonoVector value, MonoVector min, MonoVector max) =>
        Min(Max(value, min), max);

    /// <summary>
    /// Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors
    /// </summary>
    public static MonoVector Max(MonoVector left, MonoVector right) =>
        new(Math.Max(left.X, right.X), Math.Max(left.Y, right.Y));

    /// <summary>
    /// Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors
    /// </summary>
    public static MonoVector Min(MonoVector left, MonoVector right) =>
        new(Math.Min(left.X, right.X), Math.Min(left.Y, right.Y));

    /// <summary>
    /// Computes the Euclidean distance between the two given points.
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The Euclidean distance.</returns>
    public static double Distance(MonoVector value1, MonoVector value2) => Math.Sqrt(DistanceSquared(value1, value2));

    /// <summary>
    /// Returns the Euclidean distance squared between two specified points
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The Euclidean distance squared.</returns>
    public static double DistanceSquared(MonoVector value1, MonoVector value2)
    {
        var difference = value1 - value2;
        return Dot(difference, difference);
    }

    public static implicit operator MonoVector(Vector2 v) => new(v);
}