// This file is ported and adapted from Avalonia (AvaloniaUI / Avalonia)

using System.Globalization;
using System.Runtime.CompilerServices;

namespace IMonoUI.Utilities;
public static class SpanHelpers
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseUInt(this ReadOnlySpan<char> span, NumberStyles style, IFormatProvider provider, out uint value)
    {
        return uint.TryParse(span, style, provider, out value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseInt(this ReadOnlySpan<char> span, out int value)
    {
        return int.TryParse(span, out value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseDouble(this ReadOnlySpan<char> span, NumberStyles style, IFormatProvider provider, out double value)
    {
        return double.TryParse(span, style, provider, out value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseByte(this ReadOnlySpan<char> span, NumberStyles style, IFormatProvider provider, out byte value)
    {
        return byte.TryParse(span, style, provider, out value);
    }
}