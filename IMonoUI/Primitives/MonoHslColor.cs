// This file is ported and adapted from Avalonia (AvaloniaUI / Avalonia)

using IMonoUI.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMonoUI.Primitives;
public readonly struct MonoHslColor : IEquatable<MonoHslColor>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MonoHslColor"/> struct.
    /// </summary>
    /// <param name="alpha">The Alpha (transparency) component in the range from 0..1.</param>
    /// <param name="hue">The Hue component in the range from 0..360.
    /// Note that 360 is equivalent to 0 and will be adjusted automatically.</param>
    /// <param name="saturation">The Saturation component in the range from 0..1.</param>
    /// <param name="lightness">The Lightness component in the range from 0..1.</param>
    public MonoHslColor(
        double alpha,
        double hue,
        double saturation,
        double lightness)
    {
        A = MathUtilities.Clamp(alpha, 0.0, 1.0);
        H = MathUtilities.Clamp(hue, 0.0, 360.0);
        S = MathUtilities.Clamp(saturation, 0.0, 1.0);
        L = MathUtilities.Clamp(lightness, 0.0, 1.0);

        // The maximum value of Hue is technically 360 minus epsilon (just below 360).
        // This is because, in a color circle, 360 degrees is equivalent to 0 degrees.
        // However, that is too tricky to work with in code and isn't as intuitive.
        // Therefore, since 360 == 0, just wrap 360 if needed back to 0.
        H = (H == 360.0 ? 0 : H);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoHslColor"/> struct.
    /// </summary>
    /// <remarks>
    /// This constructor exists only for internal use where performance is critical.
    /// Whether or not the component values are in the correct ranges must be known.
    /// </remarks>
    /// <param name="alpha">The Alpha (transparency) component in the range from 0..1.</param>
    /// <param name="hue">The Hue component in the range from 0..360.
    /// Note that 360 is equivalent to 0 and will be adjusted automatically.</param>
    /// <param name="saturation">The Saturation component in the range from 0..1.</param>
    /// <param name="lightness">The Lightness component in the range from 0..1.</param>
    /// <param name="clampValues">Whether to clamp component values to their required ranges.</param>
    internal MonoHslColor(
        double alpha,
        double hue,
        double saturation,
        double lightness,
        bool clampValues)
    {
        if (clampValues)
        {
            A = MathUtilities.Clamp(alpha, 0.0, 1.0);
            H = MathUtilities.Clamp(hue, 0.0, 360.0);
            S = MathUtilities.Clamp(saturation, 0.0, 1.0);
            L = MathUtilities.Clamp(lightness, 0.0, 1.0);

            // See comments in constructor above
            H = (H == 360.0 ? 0 : H);
        }
        else
        {
            A = alpha;
            H = hue;
            S = saturation;
            L = lightness;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoHslColor"/> struct.
    /// </summary>
    /// <param name="color">The RGB color to convert to HSL.</param>
    public MonoHslColor(MonoColor color)
    {
        var hsl = color.ToHsl();

        A = hsl.A;
        H = hsl.H;
        S = hsl.S;
        L = hsl.L;
    }

    /// <inheritdoc cref="MonoHsvColor.A"/>
    public double A { get; }

    /// <inheritdoc cref="MonoHsvColor.H"/>
    public double H { get; }

    /// <inheritdoc cref="MonoHsvColor.S"/>
    public double S { get; }

    /// <summary>
    /// Gets the Lightness component in the range from 0..1 (percentage).
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    ///   <item>0 is fully black.</item>
    ///   <item>1 is fully white.</item>
    /// </list>
    /// </remarks>
    public double L { get; }

    /// <inheritdoc/>
    public bool Equals(MonoHslColor other)
    {
        return other.A == A &&
               other.H == H &&
               other.S == S &&
               other.L == L;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is MonoHslColor hslColor)
        {
            return Equals(hslColor);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Gets a hashcode for this object.
    /// Hashcode is not guaranteed to be unique.
    /// </summary>
    /// <returns>The hashcode for this object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(A, H, S, L);
    }

    /// <summary>
    /// Returns the RGB color model equivalent of this HSL color.
    /// </summary>
    /// <returns>The RGB equivalent color.</returns>
    public MonoColor ToRgb()
    {
        return MonoHslColor.ToRgb(H, S, L, A);
    }

    /// <summary>
    /// Returns the HSV color model equivalent of this HSL color.
    /// </summary>
    /// <returns>The HSV equivalent color.</returns>
    public MonoHsvColor ToHsv()
    {
        return MonoHslColor.ToHsv(H, S, L, A);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var sb = StringBuilderCache.Acquire();

        // Use a format similar to CSS. However:
        //   - To ensure precision is never lost, allow decimal places.
        //     This is especially important for round-trip serialization.
        //   - To maintain numerical consistency, do not use percent.
        //
        // Example:
        //
        // hsla(hue, saturation, lightness, alpha)
        // hsla(230, 1.0, 0.5, 1.0)
        //

        sb.Append("hsva(");
        sb.Append(H.ToString(CultureInfo.InvariantCulture));
        sb.Append(", ");
        sb.Append(S.ToString(CultureInfo.InvariantCulture));
        sb.Append(", ");
        sb.Append(L.ToString(CultureInfo.InvariantCulture));
        sb.Append(", ");
        sb.Append(A.ToString(CultureInfo.InvariantCulture));
        sb.Append(')');

        return StringBuilderCache.GetStringAndRelease(sb);
    }

    /// <summary>
    /// Parses an HSL color string.
    /// </summary>
    /// <param name="s">The HSL color string to parse.</param>
    /// <returns>The parsed <see cref="MonoHslColor"/>.</returns>
    public static MonoHslColor Parse(string s)
    {
        ArgumentNullException.ThrowIfNull(s);

        if (TryParse(s, out MonoHslColor hslColor))
        {
            return hslColor;
        }

        throw new FormatException($"Invalid HSL color string: '{s}'.");
    }

    /// <summary>
    /// Parses an HSL color string.
    /// </summary>
    /// <param name="s">The HSL color string to parse.</param>
    /// <param name="hslColor">The parsed <see cref="MonoHslColor"/>.</param>
    /// <returns>True if parsing was successful; otherwise, false.</returns>
    public static bool TryParse(string? s, out MonoHslColor hslColor)
    {
        bool prefixMatched = false;

        hslColor = default;

        if (s is null)
        {
            return false;
        }

        string workingString = s.Trim();

        if (workingString.Length == 0 ||
            workingString.IndexOf(',') < 0)
        {
            return false;
        }

        // Note: The length checks are also an important optimization.
        // The shortest possible format is "hsl(0,0,0)", Length = 10.

        if (workingString.Length >= 11 &&
            workingString.StartsWith("hsla(", StringComparison.OrdinalIgnoreCase) &&
            workingString.EndsWith(')'))
        {
            workingString = workingString[5..^1];
            prefixMatched = true;
        }

        if (prefixMatched == false &&
            workingString.Length >= 10 &&
            workingString.StartsWith("hsl(", StringComparison.OrdinalIgnoreCase) &&
            workingString.EndsWith(')'))
        {
            workingString = workingString[4..^1];
            prefixMatched = true;
        }

        if (prefixMatched == false)
        {
            return false;
        }

        string[] components = workingString.Split(',');

        if (components.Length == 3) // HSL
        {
            if (components[0].AsSpan().TryParseDouble(NumberStyles.Number, CultureInfo.InvariantCulture, out double hue) &&
                TryInternalParse(components[1].AsSpan(), out double saturation) &&
                TryInternalParse(components[2].AsSpan(), out double lightness))
            {
                hslColor = new MonoHslColor(1.0, hue, saturation, lightness);
                return true;
            }
        }
        else if (components.Length == 4) // HSLA
        {
            if (components[0].AsSpan().TryParseDouble(NumberStyles.Number, CultureInfo.InvariantCulture, out double hue) &&
                TryInternalParse(components[1].AsSpan(), out double saturation) &&
                TryInternalParse(components[2].AsSpan(), out double lightness) &&
                TryInternalParse(components[3].AsSpan(), out double alpha))
            {
                hslColor = new MonoHslColor(alpha, hue, saturation, lightness);
                return true;
            }
        }

        // Local function to specially parse a double value with an optional percentage sign
        static bool TryInternalParse(ReadOnlySpan<char> inString, out double outDouble)
        {
            // The percent sign, if it exists, must be at the end of the number
            int percentIndex = inString.IndexOf("%".AsSpan(), StringComparison.Ordinal);

            if (percentIndex >= 0)
            {
                var result = inString[..percentIndex].TryParseDouble(NumberStyles.Number, CultureInfo.InvariantCulture,
                     out double percentage);

                outDouble = percentage / 100.0;
                return result;
            }
            else
            {
                return inString.TryParseDouble(NumberStyles.Number, CultureInfo.InvariantCulture,
                    out outDouble);
            }
        }

        return false;
    }

    /// <summary>
    /// Creates a new <see cref="MonoHslColor"/> from individual color component values.
    /// </summary>
    /// <remarks>
    /// This exists for symmetry with the <see cref="MonoColor"/> struct; however, the
    /// appropriate constructor should commonly be used instead.
    /// </remarks>
    /// <param name="a">The Alpha (transparency) component in the range from 0..1.</param>
    /// <param name="h">The Hue component in the range from 0..360.</param>
    /// <param name="s">The Saturation component in the range from 0..1.</param>
    /// <param name="l">The Lightness component in the range from 0..1.</param>
    /// <returns>A new <see cref="MonoHslColor"/> built from the individual color component values.</returns>
    public static MonoHslColor FromAhsl(double a, double h, double s, double l)
    {
        return new MonoHslColor(a, h, s, l);
    }

    /// <summary>
    /// Creates a new <see cref="MonoHslColor"/> from individual color component values.
    /// </summary>
    /// <remarks>
    /// This exists for symmetry with the <see cref="MonoColor"/> struct; however, the
    /// appropriate constructor should commonly be used instead.
    /// </remarks>
    /// <param name="h">The Hue component in the range from 0..360.</param>
    /// <param name="s">The Saturation component in the range from 0..1.</param>
    /// <param name="l">The Lightness component in the range from 0..1.</param>
    /// <returns>A new <see cref="MonoHslColor"/> built from the individual color component values.</returns>
    public static MonoHslColor FromHsl(double h, double s, double l)
    {
        return new MonoHslColor(1.0, h, s, l);
    }

    /// <summary>
    /// Converts the given HSLA color component values to their RGB color equivalent.
    /// </summary>
    /// <param name="hue">The Hue component in the HSL color model in the range from 0..360.</param>
    /// <param name="saturation">The Saturation component in the HSL color model in the range from 0..1.</param>
    /// <param name="lightness">The Lightness component in the HSL color model in the range from 0..1.</param>
    /// <param name="alpha">The Alpha component in the range from 0..1.</param>
    /// <returns>A new RGB <see cref="MonoColor"/> equivalent to the given HSLA values.</returns>
    public static MonoColor ToRgb(
        double hue,
        double saturation,
        double lightness,
        double alpha = 1.0)
    {
        // Note: Conversion code is originally based on ColorHelper in the Windows Community Toolkit (licensed MIT)
        // https://github.com/CommunityToolkit/WindowsCommunityToolkit/blob/main/Microsoft.Toolkit.Uwp/Helpers/ColorHelper.cs
        // It has been modified to ensure input ranges and not throw exceptions.

        // We want the hue to be between 0 and 359,
        // so we first ensure that that's the case.
        while (hue >= 360.0)
        {
            hue -= 360.0;
        }

        while (hue < 0.0)
        {
            hue += 360.0;
        }

        // We similarly clamp saturation, lightness and alpha between 0 and 1.
        saturation = saturation < 0.0 ? 0.0 : saturation;
        saturation = saturation > 1.0 ? 1.0 : saturation;

        lightness = lightness < 0.0 ? 0.0 : lightness;
        lightness = lightness > 1.0 ? 1.0 : lightness;

        alpha = alpha < 0.0 ? 0.0 : alpha;
        alpha = alpha > 1.0 ? 1.0 : alpha;

        double chroma = (1 - Math.Abs((2 * lightness) - 1)) * saturation;
        double h1 = hue / 60;
        double x = chroma * (1 - Math.Abs((h1 % 2) - 1));
        double m = lightness - (0.5 * chroma);
        double r1, g1, b1;

        if (h1 < 1)
        {
            r1 = chroma;
            g1 = x;
            b1 = 0;
        }
        else if (h1 < 2)
        {
            r1 = x;
            g1 = chroma;
            b1 = 0;
        }
        else if (h1 < 3)
        {
            r1 = 0;
            g1 = chroma;
            b1 = x;
        }
        else if (h1 < 4)
        {
            r1 = 0;
            g1 = x;
            b1 = chroma;
        }
        else if (h1 < 5)
        {
            r1 = x;
            g1 = 0;
            b1 = chroma;
        }
        else
        {
            r1 = chroma;
            g1 = 0;
            b1 = x;
        }

        return new MonoColor(
            (byte)Math.Round(255 * alpha),
            (byte)Math.Round(255 * (r1 + m)),
            (byte)Math.Round(255 * (g1 + m)),
            (byte)Math.Round(255 * (b1 + m)));
    }

    /// <summary>
    /// Converts the given HSLA color component values to their HSV color equivalent.
    /// </summary>
    /// <param name="hue">The Hue component in the HSL color model in the range from 0..360.</param>
    /// <param name="saturation">The Saturation component in the HSL color model in the range from 0..1.</param>
    /// <param name="lightness">The Lightness component in the HSL color model in the range from 0..1.</param>
    /// <param name="alpha">The Alpha component in the range from 0..1.</param>
    /// <returns>A new <see cref="MonoHsvColor"/> equivalent to the given HSLA values.</returns>
    public static MonoHsvColor ToHsv(
        double hue,
        double saturation,
        double lightness,
        double alpha = 1.0)
    {
        // We want the hue to be between 0 and 359,
        // so we first ensure that that's the case.
        while (hue >= 360.0)
        {
            hue -= 360.0;
        }

        while (hue < 0.0)
        {
            hue += 360.0;
        }

        // We similarly clamp saturation, lightness and alpha between 0 and 1.
        saturation = saturation < 0.0 ? 0.0 : saturation;
        saturation = saturation > 1.0 ? 1.0 : saturation;

        lightness = lightness < 0.0 ? 0.0 : lightness;
        lightness = lightness > 1.0 ? 1.0 : lightness;

        alpha = alpha < 0.0 ? 0.0 : alpha;
        alpha = alpha > 1.0 ? 1.0 : alpha;

        // The conversion algorithm is from the below link
        // https://en.wikipedia.org/wiki/HSL_and_HSV#Interconversion

        double s;
        double v = lightness + (saturation * Math.Min(lightness, 1.0 - lightness));

        if (v <= 0)
        {
            s = 0;
        }
        else
        {
            s = 2.0 * (1.0 - (lightness / v));
        }

        return new MonoHsvColor(alpha, hue, s, v);
    }

    /// <summary>
    /// Indicates whether the values of two specified <see cref="MonoHslColor"/> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>True if left and right are equal; otherwise, false.</returns>
    public static bool operator ==(MonoHslColor left, MonoHslColor right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Indicates whether the values of two specified <see cref="MonoHslColor"/> objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>True if left and right are not equal; otherwise, false.</returns>
    public static bool operator !=(MonoHslColor left, MonoHslColor right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Explicit conversion from an <see cref="MonoHslColor"/> to a <see cref="MonoColor"/>.
    /// </summary>
    /// <param name="hslColor">The <see cref="MonoHslColor"/> to convert.</param>
    public static explicit operator MonoColor(MonoHslColor hslColor)
    {
        return hslColor.ToRgb();
    }
}