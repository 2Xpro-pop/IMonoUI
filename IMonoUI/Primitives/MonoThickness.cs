﻿// This file is ported and adapted from Avalonia (AvaloniaUI / Avalonia)

using System.Drawing;
using System.Globalization;

namespace IMonoUI.Primitives;
public readonly struct MonoThickness : IEquatable<MonoThickness>
{
    /// <summary>
    /// The thickness on the left.
    /// </summary>
    private readonly double _left;

    /// <summary>
    /// The thickness on the top.
    /// </summary>
    private readonly double _top;

    /// <summary>
    /// The thickness on the right.
    /// </summary>
    private readonly double _right;

    /// <summary>
    /// The thickness on the bottom.
    /// </summary>
    private readonly double _bottom;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoThickness"/> structure.
    /// </summary>
    /// <param name="uniformLength">The length that should be applied to all sides.</param>
    public MonoThickness(double uniformLength)
    {
        _left = _top = _right = _bottom = uniformLength;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoThickness"/> structure.
    /// </summary>
    /// <param name="horizontal">The thickness on the left and right.</param>
    /// <param name="vertical">The thickness on the top and bottom.</param>
    public MonoThickness(double horizontal, double vertical)
    {
        _left = _right = horizontal;
        _top = _bottom = vertical;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoThickness"/> structure.
    /// </summary>
    /// <param name="left">The thickness on the left.</param>
    /// <param name="top">The thickness on the top.</param>
    /// <param name="right">The thickness on the right.</param>
    /// <param name="bottom">The thickness on the bottom.</param>
    public MonoThickness(double left, double top, double right, double bottom)
    {
        _left = left;
        _top = top;
        _right = right;
        _bottom = bottom;
    }

    /// <summary>
    /// Gets the thickness on the left.
    /// </summary>
    public double Left => _left;

    /// <summary>
    /// Gets the thickness on the top.
    /// </summary>
    public double Top => _top;

    /// <summary>
    /// Gets the thickness on the right.
    /// </summary>
    public double Right => _right;

    /// <summary>
    /// Gets the thickness on the bottom.
    /// </summary>
    public double Bottom => _bottom;

    /// <summary>
    /// Gets a value indicating whether all sides are equal.
    /// </summary>
    public bool IsUniform => Left.Equals(Right) && Top.Equals(Bottom) && Right.Equals(Bottom);

    /// <summary>
    /// Compares two Thicknesses.
    /// </summary>
    /// <param name="a">The first thickness.</param>
    /// <param name="b">The second thickness.</param>
    /// <returns>The equality.</returns>
    public static bool operator ==(MonoThickness a, MonoThickness b)
    {
        return a.Equals(b);
    }

    /// <summary>
    /// Compares two Thicknesses.
    /// </summary>
    /// <param name="a">The first thickness.</param>
    /// <param name="b">The second thickness.</param>
    /// <returns>The inequality.</returns>
    public static bool operator !=(MonoThickness a, MonoThickness b)
    {
        return !a.Equals(b);
    }

    /// <summary>
    /// Adds two Thicknesses.
    /// </summary>
    /// <param name="a">The first thickness.</param>
    /// <param name="b">The second thickness.</param>
    /// <returns>The equality.</returns>
    public static MonoThickness operator +(MonoThickness a, MonoThickness b)
    {
        return new MonoThickness(
            a.Left + b.Left,
            a.Top + b.Top,
            a.Right + b.Right,
            a.Bottom + b.Bottom);
    }

    /// <summary>
    /// Subtracts two Thicknesses.
    /// </summary>
    /// <param name="a">The first thickness.</param>
    /// <param name="b">The second thickness.</param>
    /// <returns>The equality.</returns>
    public static MonoThickness operator -(MonoThickness a, MonoThickness b)
    {
        return new MonoThickness(
            a.Left - b.Left,
            a.Top - b.Top,
            a.Right - b.Right,
            a.Bottom - b.Bottom);
    }

    /// <summary>
    /// Multiplies a MonoThickness to a scalar.
    /// </summary>
    /// <param name="a">The thickness.</param>
    /// <param name="b">The scalar.</param>
    /// <returns>The equality.</returns>
    public static MonoThickness operator *(MonoThickness a, double b)
    {
        return new MonoThickness(
            a.Left * b,
            a.Top * b,
            a.Right * b,
            a.Bottom * b);
    }

    /// <summary>
    /// Adds a MonoThickness to a Size.
    /// </summary>
    /// <param name="size">The size.</param>
    /// <param name="thickness">The thickness.</param>
    /// <returns>The equality.</returns>
    public static MonoSize operator +(MonoSize size, MonoThickness thickness)
    {
        return new MonoSize(
            size.Width + thickness.Left + thickness.Right,
            size.Height + thickness.Top + thickness.Bottom);
    }

    /// <summary>
    /// Subtracts a MonoThickness from a Size.
    /// </summary>
    /// <param name="size">The size.</param>
    /// <param name="thickness">The thickness.</param>
    /// <returns>The equality.</returns>
    public static MonoSize operator -(MonoSize size, MonoThickness thickness)
    {
        return new MonoSize(
            size.Width - (thickness.Left + thickness.Right),
            size.Height - (thickness.Top + thickness.Bottom));
    }

    /// <summary>
    /// Returns a boolean indicating whether the thickness is equal to the other given point.
    /// </summary>
    /// <param name="other">The other thickness to test equality against.</param>
    /// <returns>True if this thickness is equal to other; False otherwise.</returns>
    public bool Equals(MonoThickness other)
    {
        // ReSharper disable CompareOfFloatsByEqualityOperator
        return _left == other._left &&
               _top == other._top &&
               _right == other._right &&
               _bottom == other._bottom;
        // ReSharper restore CompareOfFloatsByEqualityOperator
    }

    /// <summary>
    /// Checks for equality between a thickness and an object.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>
    /// True if <paramref name="obj"/> is a size that equals the current size.
    /// </returns>
    public override bool Equals(object? obj) => obj is MonoThickness other && Equals(other);

    /// <summary>
    /// Returns a hash code for a <see cref="MonoThickness"/>.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = (hash * 23) + Left.GetHashCode();
            hash = (hash * 23) + Top.GetHashCode();
            hash = (hash * 23) + Right.GetHashCode();
            hash = (hash * 23) + Bottom.GetHashCode();
            return hash;
        }
    }

    /// <summary>
    /// Returns the string representation of the thickness.
    /// </summary>
    /// <returns>The string representation of the thickness.</returns>
    public override string ToString()
    {
        return FormattableString.Invariant($"{_left},{_top},{_right},{_bottom}");
    }

    /// <summary>
    /// Deconstructor the thickness into its left, top, right and bottom thickness values.
    /// </summary>
    /// <param name="left">The thickness on the left.</param>
    /// <param name="top">The thickness on the top.</param>
    /// <param name="right">The thickness on the right.</param>
    /// <param name="bottom">The thickness on the bottom.</param>
    public void Deconstruct(out double left, out double top, out double right, out double bottom)
    {
        left = _left;
        top = _top;
        right = _right;
        bottom = _bottom;
    }
}