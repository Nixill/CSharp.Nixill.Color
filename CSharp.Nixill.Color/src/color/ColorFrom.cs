using System.Text.RegularExpressions;
using Nixill.Internals;

namespace Nixill.Colors;

public readonly partial struct Color
{
  #region RGB, etc.
  /// <summary>
  ///   Creates a color from <see langword="int"/> representations of each
  ///   component on a scale of 0 to 255.
  /// </summary>
  /// <remarks>
  ///   Components need not be constrained to 0..255, but values outside
  ///   these ranges may cause undefined behavior.
  /// </remarks>
  /// <param name="red">The Red component.</param>
  /// <param name="green">The Green component.</param>
  /// <param name="blue">The Blue component.</param>
  /// <param name="alpha">The Alpha (opacity) component.</param>
  /// <returns>The created Color.</returns>
  public static Color FromRGBA(int red, int green, int blue, int alpha = 255)
    => new Color(red / 255d, green / 255d, blue / 255d, alpha / 255d);

  /// <summary>
  ///   Creates a color from <see langword="byte"/> representations of
  ///   each component on a scale of 0 to 255.
  /// </summary>
  /// <param name="red">The Red component.</param>
  /// <param name="green">The Green component.</param>
  /// <param name="blue">The Blue component.</param>
  /// <param name="alpha">The Alpha (opacity) component.</param>
  /// <returns>The created Color.</returns>
  public static Color FromRGBA(byte red, byte green, byte blue, byte alpha = 255)
    => new Color(red / 255d, green / 255d, blue / 255d, alpha / 255d);

  static byte[] GetBytes(int from, out byte[] bytes)
  {
    bytes = BitConverter.GetBytes(from);
    if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
    return bytes;
  }

  static byte[] GetBytes(uint from, out byte[] bytes)
  {
    bytes = BitConverter.GetBytes(from);
    if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
    return bytes;
  }

  /// <summary>
  ///   Creates a color from a 32-bit signed integer, where the bytes from
  ///   most to least significant represent red, green, blue, and alpha,
  ///   respectively.
  /// </summary>
  /// <param name="rgba">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromRGBA(int rgba)
    => Color.FromRGBA(GetBytes(rgba, out byte[] bytes)[0], bytes[1], bytes[2], bytes[3]);

  /// <summary>
  ///   Creates a color from a 32-bit unsigned integer, where the bytes
  ///   from most to least significant represent red, green, blue, and
  ///   alpha, respectively.
  /// </summary>
  /// <param name="rgba">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromRGBA(uint rgba)
    => Color.FromRGBA(GetBytes(rgba, out byte[] bytes)[0], bytes[1], bytes[2], bytes[3]);

  /// <summary>
  ///   Creates a color from a 32-bit signed integer, where the bytes from
  ///   most to least significant represent nothing, red, green, and blue,
  ///   respectively.
  /// </summary>
  /// <param name="rgb">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromRGB(int rgb)
    => Color.FromRGBA(GetBytes(rgb, out byte[] bytes)[0], bytes[1], bytes[2], (byte)255);

  /// <summary>
  ///   Creates a color from a 32-bit unsigned integer, where the bytes
  ///   from most to least significant represent nothing, red, green, and
  ///   blue, respectively.
  /// </summary>
  /// <param name="rgba">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromRGB(uint rgb)
    => Color.FromRGBA(GetBytes(rgb, out byte[] bytes)[0], bytes[1], bytes[2], (byte)255);

  /// <summary>
  ///   Creates a color from a 32-bit signed integer, where the bytes from
  ///   most to least significant represent alpha, red, green, and blue,
  ///   respectively.
  /// </summary>
  /// <param name="argb">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromARGB(int argb)
    => Color.FromRGBA(GetBytes(argb, out byte[] bytes)[1], bytes[2], bytes[3], bytes[0]);

  /// <summary>
  ///   Creates a color from a 32-bit unsigned integer, where the bytes
  ///   from most to least significant represent alpha, red, green, and
  ///   blue, respectively.
  /// </summary>
  /// <param name="argb">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromARGB(uint argb)
    => Color.FromRGBA(GetBytes(argb, out byte[] bytes)[1], bytes[2], bytes[3], bytes[0]);

  /// <summary>
  ///   Creates a color from a 32-bit signed integer, where the bytes from
  ///   most to least significant represent blue, green, red, and alpha,
  ///   respectively.
  /// </summary>
  /// <param name="bgra">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromBGRA(int bgra)
    => Color.FromRGBA(GetBytes(bgra, out byte[] bytes)[2], bytes[1], bytes[0], bytes[3]);

  /// <summary>
  ///   Creates a color from a 32-bit unsigned integer, where the bytes
  ///   from most to least significant represent blue, green, red, and
  ///   alpha, respectively.
  /// </summary>
  /// <param name="bgra">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromBGRA(uint bgra)
    => Color.FromRGBA(GetBytes(bgra, out byte[] bytes)[2], bytes[1], bytes[0], bytes[3]);

  /// <summary>
  ///   Creates a color from a 32-bit signed integer, where the bytes from
  ///   most to least significant represent nothing, blue, green, and red,
  ///   respectively.
  /// </summary>
  /// <param name="bgr">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromBGR(int bgr)
    => Color.FromRGBA(GetBytes(bgr, out byte[] bytes)[2], bytes[1], bytes[0], (byte)255);

  /// <summary>
  ///   Creates a color from a 32-bit unsigned integer, where the bytes
  ///   from most to least significant represent nothing, blue, green, and
  ///   red, respectively.
  /// </summary>
  /// <param name="bgr">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromBGR(uint bgr)
    => Color.FromRGBA(GetBytes(bgr, out byte[] bytes)[2], bytes[1], bytes[0], (byte)255);

  /// <summary>
  ///   Creates a color from a 32-bit signed integer, where the bytes from
  ///   most to least significant represent alpha, blue, green, and red,
  ///   respectively.
  /// </summary>
  /// <param name="abgr">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromABGR(int abgr)
    => Color.FromRGBA(GetBytes(abgr, out byte[] bytes)[3], bytes[2], bytes[1], bytes[0]);

  /// <summary>
  ///   Creates a color from a 32-bit unsigned integer, where the bytes
  ///   from most to least significant represent alpha, blue, green, and
  ///   red, respectively.
  /// </summary>
  /// <param name="abgr">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromABGR(uint abgr)
    => Color.FromRGBA(GetBytes(abgr, out byte[] bytes)[3], bytes[2], bytes[1], bytes[0]);

  [GeneratedRegex(@"^#?((?:[0-9A-Fa-f]{2}){3,4})$")]
  static partial Regex ColorCode { get; }

  /// <summary>
  ///   Creates a color from a hex string, where the bytes from left to
  ///   right in the string represent red, green, blue, and (if present)
  ///   alpha, respectively.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     A six-character hex string without an alpha component may also
  ///     be passed to this method.
  ///   </para>
  ///   <para>
  ///     A leading <c>#</c> is optional. No other prefix is allowed.
  ///   </para>
  /// </remarks>
  /// <param name="rgba">The string.</param>
  /// <returns>The created Color.</returns>
  /// <exception cref="FormatException">
  ///   <paramref name="rgba"/> is not a valid hex code.
  /// </exception>
  public static Color FromRGBA(string rgba)
  {
    Match match = ColorCode.Match(rgba.Trim());
    if (!match.Success) throw new FormatException($"'{rgba}' is not a valid color.");
    uint color = Convert.ToUInt32(match.Groups[1].Value, 16);
    if (match.Groups[1].Length == 6) return FromRGB(color);
    else return FromRGBA(color);
  }

  /// <summary>
  ///   Creates a color from a hex string, where the bytes from left to
  ///   right in the string represent alpha (if present), red, green, and
  ///   blue, respectively.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     A six-character hex string without an alpha component may also
  ///     be passed to this method.
  ///   </para>
  ///   <para>
  ///     A leading <c>#</c> is optional. No other prefix is allowed.
  ///   </para>
  /// </remarks>
  /// <param name="argb">The string.</param>
  /// <returns>The created Color.</returns>
  /// <exception cref="FormatException">
  ///   <paramref name="argb"/> is not a valid hex code.
  /// </exception>
  public static Color FromARGB(string argb)
  {
    Match match = ColorCode.Match(argb.Trim());
    if (!match.Success) throw new FormatException($"'{argb}' is not a valid color.");
    uint color = Convert.ToUInt32(match.Groups[1].Value, 16);
    if (match.Groups[1].Length == 6) return FromRGB(color);
    else return FromARGB(color);
  }
  #endregion

  #region Hue Calculations
  /// <summary>
  ///   Generates a color from a given hue (in degrees) and min and max
  ///   component values.
  /// </summary>
  /// <remarks>
  ///   <paramref name="maxColor"/>, <paramref name="minColor"/>, and
  ///   <paramref name="alpha"/> need not necessarily be constrained to
  ///   the range of [0, 1], but values outside this range may cause
  ///   undefined behavior.
  /// </remarks>
  /// <param name="hueDegrees">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="minColor">
  ///   The value of the lowest component of the Color.
  /// </param>
  /// <param name="maxColor">
  ///   The value of the highest component of the Color.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHMM(double hueDegrees, double minColor, double maxColor, double alpha = 1) =>
    (double.IsNaN(hueDegrees)) ?
    new Color(
      red: Internal.Lerp(0.5, minColor, maxColor),
      green: Internal.Lerp(0.5, minColor, maxColor),
      blue: Internal.Lerp(0.5, minColor, maxColor),
      alpha: alpha
    ) :
    new Color(
      red: Internal.Lerp(GetRedFactor(hueDegrees), minColor, maxColor),
      green: Internal.Lerp(GetGreenFactor(hueDegrees), minColor, maxColor),
      blue: Internal.Lerp(GetBlueFactor(hueDegrees), minColor, maxColor),
      alpha: alpha
    );

  /// <summary>
  ///   Generates a color from a given hue (in degrees) and min and max
  ///   component values.
  /// </summary>
  /// <remarks>
  ///   <paramref name="maxColor"/>, <paramref name="minColor"/>, and
  ///   <paramref name="alpha"/> need not necessarily be constrained to
  ///   the range of [0, 255], but values outside this range may cause
  ///   undefined behavior.
  /// </remarks>
  /// <param name="hueDegrees">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="minColor">
  ///   The value of the lowest component of the Color.
  /// </param>
  /// <param name="maxColor">
  ///   The value of the highest component of the Color.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHMM(int hueDegrees, int minColor, int maxColor, int alpha = 255) => new Color(
      red: Internal.Lerp(GetRedFactor(hueDegrees), minColor / 255.0, maxColor / 255.0),
      green: Internal.Lerp(GetGreenFactor(hueDegrees), minColor / 255.0, maxColor / 255.0),
      blue: Internal.Lerp(GetBlueFactor(hueDegrees), minColor / 255.0, maxColor / 255.0),
      alpha: alpha
    );

  /// <summary>
  ///   Generates a color from a given hue and min and max component
  ///   values.
  /// </summary>
  /// <remarks>
  ///   <paramref name="maxColor"/>, <paramref name="minColor"/>, and
  ///   <paramref name="alpha"/> need not necessarily be constrained to
  ///   the range of [0, 255], but values outside this range may cause
  ///   undefined behavior.
  /// </remarks>
  /// <param name="hueDegrees">
  ///   The Hue of the Color, given in multiples of 1.5 degrees. Is
  ///   automatically normalized to the range of [0, 240).
  /// </param>
  /// <param name="minColor">
  ///   The value of the lowest component of the Color.
  /// </param>
  /// <param name="maxColor">
  ///   The value of the highest component of the Color.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHMM(byte hue0To240, byte minColor, byte maxColor, byte alpha = 255) =>
    new Color(
      red: Internal.Lerp(GetRedFactor(hue0To240 / 2.0 * 3.0), minColor / 255.0, maxColor / 255.0),
      green: Internal.Lerp(GetGreenFactor(hue0To240 / 2.0 * 3.0), minColor / 255.0, maxColor / 255.0),
      blue: Internal.Lerp(GetBlueFactor(hue0To240 / 2.0 * 3.0), minColor / 255.0, maxColor / 255.0),
      alpha: alpha
    );

  static double GetRedFactor(double hueDegrees) => Math.Clamp(Math.Abs(3 - Internal.NNMod(hueDegrees / 60, 6)), 1, 2) - 1;
  static double GetGreenFactor(double hueDegrees) => Math.Clamp(Math.Abs(3 - Internal.NNMod(hueDegrees / 60 + 4, 6)), 1, 2) - 1;
  static double GetBlueFactor(double hueDegrees) => Math.Clamp(Math.Abs(3 - Internal.NNMod(hueDegrees / 60 + 2, 6)), 1, 2) - 1;
  #endregion

  #region Luminosity
  /// <summary>
  ///   Generates a color from a given hue (in degrees), saturation, and
  ///   luminosity.
  /// </summary>
  /// <remarks>
  ///   <paramref name="saturation"/>, <paramref name="luminosity"/>, and
  ///   <paramref name="alpha"/> need not necessarily be constrained to
  ///   the range of [0, 1], but values outside this range may cause
  ///   undefined behavior.
  /// </remarks>
  /// <param name="hue">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="saturation">
  ///   The Luminosity-based Saturation of the Color, generally expressed
  ///   as a value in the range of [0, 1].
  /// </param>
  /// <param name="luminosity">
  ///   The Luminosity of the Color, generally expressed as a value in the
  ///   range of [0, 1].
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color, generally expressed as a value
  ///   in the range of [0, 1].
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHSL(double hueDegrees, double saturation, double luminosity, double alpha = 1)
    => FromHCL(hueDegrees, (1 - Math.Abs(2 * luminosity - 1)) * saturation, luminosity, alpha);

  /// <summary>
  ///   Generates a color from a given hue (in degrees), saturation, and
  ///   luminosity.
  /// </summary>
  /// <remarks>
  ///   <paramref name="saturation"/>, <paramref name="luminosity"/>, and
  ///   <paramref name="alpha"/> need not necessarily be constrained to
  ///   the range of [0, 255], but values outside this range may cause
  ///   undefined behavior.
  /// </remarks>
  /// <param name="hueDegrees">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="saturation">
  ///   The Luminosity-based Saturation of the Color, generally expressed
  ///   as a value in the range of [0, 255].
  /// </param>
  /// <param name="luminosity">
  ///   The Luminosity of the Color, generally expressed as a value in the
  ///   range of [0, 255].
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color, generally expressed as a value
  ///   in the range of [0, 255].
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHSL(int hueDegrees, int saturation, int luminosity, int alpha = 255)
    => FromHSL(hueDegrees, saturation / 255.0, luminosity / 255.0, alpha / 255.0);

  /// <summary>
  ///   Generates a color from a given hue, saturation, and luminosity.
  /// </summary>
  /// <remarks>
  ///   <paramref name="saturation"/>, <paramref name="value"/>, and
  ///   <paramref name="alpha"/> need not necessarily be constrained to
  ///   the range of [0, 240], but values outside this range may cause
  ///   undefined behavior.
  /// </remarks>
  /// <param name="hue">
  ///   The Hue of the Color, given in - and automatically normalized to -
  ///   the range of [0, 240).
  /// </param>
  /// <param name="saturation">
  ///   The Luminosity-based Saturation of the Color, generally expressed
  ///   as a value in the range of [0, 240].
  /// </param>
  /// <param name="value">
  ///   The Luminosity of the Color, generally expressed as a value in the
  ///   range of [0, 240].
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color, generally expressed as a value
  ///   in the range of [0, 240].
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHSL(byte hue, byte saturation, byte luminosity, byte alpha = 240)
    => FromHSL(hue * 1.5, saturation / 240.0, luminosity / 240.0, alpha / 240.0);

  /// <summary>
  ///   Generates a color from a given hue (in degrees), chroma, and
  ///   luminosity.
  /// </summary>
  /// <remarks>
  ///   To produce an in-gamut Color, <paramref name="luminosity"/> must
  ///   be between 0 and 1, <paramref name="chroma"/> must be between 0
  ///   and <c>2 * Math.Abs(0.5 - luminosity)</c>, and <paramref name="alpha"/>
  ///   must be between 0 and 1. Values outside these ranges are allowed,
  ///   but may cause undefined behavior.
  /// </remarks>
  /// <param name="hueDegrees">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="chroma">
  ///   The Chroma of the Color, generally expressed as a value in the
  ///   range of [0, 1].
  /// </param>
  /// <param name="luminosity">
  ///   The Luminosity of the Color, generally expressed as a value in the
  ///   range of [0, 1].
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color, generally expressed as a value
  ///   in the range of [0, 1].
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHCL(double hueDegrees, double chroma, double luminosity, double alpha = 1)
  {
    double minColor = luminosity - (chroma / 2);
    double maxColor = chroma + minColor;
    return FromHMM(hueDegrees, minColor, maxColor, alpha);
  }

  /// <summary>
  ///   Generates a color from a given hue (in degrees), chroma, and
  ///   luminosity.
  /// </summary>
  /// <remarks>
  ///   To produce an in-gamut Color, <paramref name="luminosity"/> must
  ///   be between 0 and 255, <paramref name="chroma"/> must be between 0
  ///   and <c>2 * Math.Abs(127.5 - luminosity)</c>, and <paramref name="alpha"/>
  ///   must be between 0 and 255. Values outside these ranges are
  ///   allowed, but may cause undefined behavior.
  /// </remarks>
  /// <param name="hueDegrees">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="chroma">
  ///   The Chroma of the Color, generally expressed as a value in the
  ///   range of [0, 255].
  /// </param>
  /// <param name="luminosity">
  ///   The Luminosity of the Color, generally expressed as a value in the
  ///   range of [0, 255].
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color, generally expressed as a value
  ///   in the range of [0, 255].
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHCL(int hueDegrees, int chroma, int luminosity, int alpha = 255)
    => FromHCL(hueDegrees / 1.0, chroma / 255.0, luminosity / 255.0, alpha / 255.0);

  /// <summary>
  ///   Generates a color from a given hue, chroma, and luminosity.
  /// </summary>
  /// <remarks>
  ///   To produce an in-gamut Color, <paramref name="luminosity"/> must
  ///   be between 0 and 240, <paramref name="chroma"/> must be between 0
  ///   and <c>2 * Math.Abs(120 - luminosity)</c>, and <paramref name="alpha"/>
  ///   must be between 0 and 240. Values outside these ranges are
  ///   allowed, but may cause undefined behavior.
  /// </remarks>
  /// <param name="hueDegrees">
  ///   The Hue of the Color, given in (and automatically normalized to)
  ///   the range of [0, 240).
  /// </param>
  /// <param name="chroma">
  ///   The Chroma of the Color, generally expressed as a value in the
  ///   range of [0, 240].
  /// </param>
  /// <param name="luminosity">
  ///   The Luminosity of the Color, generally expressed as a value in the
  ///   range of [0, 240].
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color, generally expressed as a value in the range
  ///   of [0, 240].
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHCL(byte hueDegrees, byte chroma, byte luminosity, byte alpha = 240)
    => FromHCL(hueDegrees / 240.0, chroma / 240.0, luminosity / 240.0, alpha / 240.0);
  #endregion

  #region Value
  /// <summary>
  ///   Generates a color from a given hue (in degrees), saturation, and value.
  /// </summary>
  /// <remarks>
  ///   <paramref name="saturation"/>, <paramref name="value"/>, and <paramref name="alpha"/>
  ///   need not necessarily be constrained to the range of [0, 1], but
  ///   values outside this range may cause undefined behavior.
  /// </remarks>
  /// <param name="hue">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="saturation">
  ///   The Value-based Saturation of the Color, generally expressed as a
  ///   value in the range of [0, 1].
  /// </param>
  /// <param name="value">
  ///   The Value of the Color, generally expressed as a value in the
  ///   range of [0, 1].
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color, generally expressed as a value
  ///   in the range of [0, 1].
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHSV(double hueDegrees, double saturation, double value, double alpha = 1)
    => FromHCV(hueDegrees, value * saturation, value, alpha);

  /// <summary>
  ///   Generates a color from a given hue (in degrees), saturation, and value.
  /// </summary>
  /// <remarks>
  ///   <paramref name="saturation"/>, <paramref name="value"/>, and <paramref name="alpha"/>
  ///   need not necessarily be constrained to the range of [0, 255], but
  ///   values outside this range may cause undefined behavior.
  /// </remarks>
  /// <param name="hueDegrees">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="saturation">
  ///   The Value-based Saturation of the Color, generally expressed as a
  ///   value in the range of [0, 255].
  /// </param>
  /// <param name="value">
  ///   The Value of the Color, generally expressed as a value in the
  ///   range of [0, 255].
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color, generally expressed as a value
  ///   in the range of [0, 255].
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHSV(int hueDegrees, int saturation, int value, int alpha = 255)
    => FromHSV(hueDegrees, saturation / 255.0, value / 255.0, alpha / 255.0);

  /// <summary>
  ///   Generates a color from a given hue, saturation, and value.
  /// </summary>
  /// <remarks>
  ///   <paramref name="saturation"/>, <paramref name="value"/>, and <paramref name="alpha"/>
  ///   need not necessarily be constrained to the range of [0, 240], but
  ///   values outside this range may cause undefined behavior.
  /// </remarks>
  /// <param name="hue">
  ///   The Hue of the Color, given in - and automatically normalized to -
  ///   the range of [0, 240).
  /// </param>
  /// <param name="saturation">
  ///   The Value-based Saturation of the Color, generally expressed as a
  ///   value in the range of [0, 240].
  /// </param>
  /// <param name="value">
  ///   The Value of the Color, generally expressed as a value in the
  ///   range of [0, 240].
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color, generally expressed as a value
  ///   in the range of [0, 240].
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHSV(byte hue, byte saturation, byte value, byte alpha = 240)
    => FromHSV(hue * 1.5, saturation / 240.0, value / 240.0, alpha / 240.0);

  /// <summary>
  ///   Generates a color from a given hue (in degrees), chroma, and value.
  /// </summary>
  /// <remarks>
  ///   To produce an in-gamut Color, <paramref name="value"/> must be
  ///   between 0 and 1, <paramref name="chroma"/> must be between 0
  ///   and <paramref name="value"/>, and <paramref name="alpha"/> must be
  ///   between 0 and 1. Values outside these ranges are allowed, but may
  ///   cause undefined behavior.
  /// </remarks>
  /// <param name="hueDegrees">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="chroma">
  ///   The Chroma of the Color, generally expressed as a value in the
  ///   range of [0, 1].
  /// </param>
  /// <param name="value">
  ///   The Value of the Color, generally expressed as a value in the
  ///   range of [0, 1].
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color, generally expressed as a value
  ///   in the range of [0, 1].
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHCV(double hueDegrees, double chroma, double value, double alpha = 1)
  {
    double maxColor = value;
    double minColor = maxColor - chroma;
    return FromHMM(hueDegrees, minColor, maxColor, alpha);
  }

  /// <summary>
  ///   Generates a color from a given hue (in degrees), chroma, and value.
  /// </summary>
  /// <remarks>
  ///   To produce an in-gamut Color, <paramref name="value"/> must be
  ///   between 0 and 255, <paramref name="chroma"/> must be between 0
  ///   and <paramref name="value"/>, and <paramref name="alpha"/> must be
  ///   between 0 and 255. Values outside these ranges are allowed, but
  ///   may cause undefined behavior.
  /// </remarks>
  /// <param name="hueDegrees">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="chroma">
  ///   The Chroma of the Color, generally expressed as a value in the
  ///   range of [0, 255].
  /// </param>
  /// <param name="value">
  ///   The Luminosity of the Color, generally expressed as a value in the
  ///   range of [0, 255].
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color, generally expressed as a value
  ///   in the range of [0, 255].
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHCV(int hueDegrees, int chroma, int value, int alpha = 255)
    => FromHCV(hueDegrees / 1.0, chroma / 255.0, value / 255.0, alpha / 255.0);

  /// <summary>
  ///   Generates a color from a given hue, chroma, and value.
  /// </summary>
  /// <remarks>
  ///   To produce an in-gamut Color, <paramref name="value"/> must be
  ///   between 0 and 240, <paramref name="chroma"/> must be between 0
  ///   and <paramref name="value"/>, and <paramref name="alpha"/> must be
  ///   between 0 and 240. Values outside these ranges are allowed, but
  ///   may cause undefined behavior.
  /// </remarks>
  /// <param name="hueDegrees">
  ///   The Hue of the Color, given in (and automatically normalized to)
  ///   the range of [0, 240).
  /// </param>
  /// <param name="chroma">
  ///   The Chroma of the Color, generally expressed as a value in the
  ///   range of [0, 240].
  /// </param>
  /// <param name="value">
  ///   The Value of the Color, generally expressed as a value in the
  ///   range of [0, 240].
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color, generally expressed as a value
  ///   in the range of [0, 240].
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHCV(byte hueDegrees, byte chroma, byte value, byte alpha = 240)
    => FromHCV(hueDegrees / 240.0, chroma / 240.0, value / 240.0, alpha / 240.0);
  #endregion
}