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
  public static Color FromIntRGBA(int red, int green, int blue, int alpha = 255)
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
  public static Color FromIntRGBA(int rgba)
    => Color.FromIntRGBA(GetBytes(rgba, out byte[] bytes)[0], bytes[1], bytes[2], bytes[3]);

  /// <summary>
  ///   Creates a color from a 32-bit unsigned integer, where the bytes
  ///   from most to least significant represent red, green, blue, and
  ///   alpha, respectively.
  /// </summary>
  /// <param name="rgba">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromUIntRGBA(uint rgba)
    => Color.FromIntRGBA(GetBytes(rgba, out byte[] bytes)[0], bytes[1], bytes[2], bytes[3]);

  /// <summary>
  ///   Creates a color from a 32-bit signed integer, where the bytes from
  ///   most to least significant represent nothing, red, green, and blue,
  ///   respectively.
  /// </summary>
  /// <param name="rgb">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromIntRGB(int rgb)
    => Color.FromIntRGBA(GetBytes(rgb, out byte[] bytes)[1], bytes[2], bytes[3], (byte)255);

  /// <summary>
  ///   Creates a color from a 32-bit unsigned integer, where the bytes
  ///   from most to least significant represent nothing, red, green, and
  ///   blue, respectively.
  /// </summary>
  /// <param name="rgba">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromUIntRGB(uint rgb)
    => Color.FromIntRGBA(GetBytes(rgb, out byte[] bytes)[1], bytes[2], bytes[3], (byte)255);

  /// <summary>
  ///   Creates a color from a 32-bit signed integer, where the bytes from
  ///   most to least significant represent alpha, red, green, and blue,
  ///   respectively.
  /// </summary>
  /// <param name="argb">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromIntARGB(int argb)
    => Color.FromIntRGBA(GetBytes(argb, out byte[] bytes)[1], bytes[2], bytes[3], bytes[0]);

  /// <summary>
  ///   Creates a color from a 32-bit unsigned integer, where the bytes
  ///   from most to least significant represent alpha, red, green, and
  ///   blue, respectively.
  /// </summary>
  /// <param name="argb">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromUIntARGB(uint argb)
    => Color.FromIntRGBA(GetBytes(argb, out byte[] bytes)[1], bytes[2], bytes[3], bytes[0]);

  /// <summary>
  ///   Creates a color from a 32-bit signed integer, where the bytes from
  ///   most to least significant represent blue, green, red, and alpha,
  ///   respectively.
  /// </summary>
  /// <param name="bgra">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromIntBGRA(int bgra)
    => Color.FromIntRGBA(GetBytes(bgra, out byte[] bytes)[2], bytes[1], bytes[0], bytes[3]);

  /// <summary>
  ///   Creates a color from a 32-bit unsigned integer, where the bytes
  ///   from most to least significant represent blue, green, red, and
  ///   alpha, respectively.
  /// </summary>
  /// <param name="bgra">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromUIntBGRA(uint bgra)
    => Color.FromIntRGBA(GetBytes(bgra, out byte[] bytes)[2], bytes[1], bytes[0], bytes[3]);

  /// <summary>
  ///   Creates a color from a 32-bit signed integer, where the bytes from
  ///   most to least significant represent nothing, blue, green, and red,
  ///   respectively.
  /// </summary>
  /// <param name="bgr">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromIntBGR(int bgr)
    => Color.FromIntRGBA(GetBytes(bgr, out byte[] bytes)[3], bytes[2], bytes[1], (byte)255);

  /// <summary>
  ///   Creates a color from a 32-bit unsigned integer, where the bytes
  ///   from most to least significant represent nothing, blue, green, and
  ///   red, respectively.
  /// </summary>
  /// <param name="bgr">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromUIntBGR(uint bgr)
    => Color.FromIntRGBA(GetBytes(bgr, out byte[] bytes)[3], bytes[2], bytes[1], (byte)255);

  /// <summary>
  ///   Creates a color from a 32-bit signed integer, where the bytes from
  ///   most to least significant represent alpha, blue, green, and red,
  ///   respectively.
  /// </summary>
  /// <param name="abgr">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromIntABGR(int abgr)
    => Color.FromIntRGBA(GetBytes(abgr, out byte[] bytes)[3], bytes[2], bytes[1], bytes[0]);

  /// <summary>
  ///   Creates a color from a 32-bit unsigned integer, where the bytes
  ///   from most to least significant represent alpha, blue, green, and
  ///   red, respectively.
  /// </summary>
  /// <param name="abgr">The integer.</param>
  /// <returns>The corresponding Color.</returns>
  public static Color FromUIntABGR(uint abgr)
    => Color.FromIntRGBA(GetBytes(abgr, out byte[] bytes)[3], bytes[2], bytes[1], bytes[0]);

  [GeneratedRegex(@"^(?:#|0[Xx]|\$)?([0-9A-Fa-f]{3,8})$")]
  static partial Regex ColorCode { get; }

  static string ExpandHex(string hex)
  {
    Match match = ColorCode.Match(hex.Trim());
    if (!match.Success) throw new FormatException($"'{hex}' is not a valid color.");
    string s = match.Groups[1].Value;
    return s.Length switch
    {
      3 or 4 => new string([.. s.SelectMany<char, char>(c => [c, c])]),
      6 or 8 => s,
      // it should always be 5 or 7 if reaching this point, but catchall
      // works just as well.
      _ => throw new FormatException($"'{hex}' is not a valid color.")
    };
  }

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
  ///     A three- or four-character hex string may be passed to this
  ///     method, in which case each character is doubled to represent a
  ///     full byte.
  ///   </para>
  ///   <para>
  ///     The hex code may be prefixed with <c>#</c>, <c>$</c>, <c>0x</c>
  ///     (case insensitive), or nothing. No other prefix is allowed.
  ///   </para>
  /// </remarks>
  /// <param name="hex">The string.</param>
  /// <param name="alphaFirst">
  ///   If true, the first byte represents alpha, not the last. Ignored if
  ///   the input is a three-byte hex string.
  /// </param>
  /// <returns>The created Color.</returns>
  /// <exception cref="FormatException">
  ///   <paramref name="hex"/> is not a valid hex code.
  /// </exception>
  public static Color FromHex(string hex, bool alphaFirst = false)
  {
    hex = ExpandHex(hex);
    uint color = Convert.ToUInt32(hex, 16);
    if (hex.Length == 6) return FromUIntRGB(color);
    else if (!alphaFirst) return FromUIntRGBA(color);
    else return FromUIntARGB(color);
  }

  /// <summary>
  ///   Attempts to create a color from a hex string, where the bytes from
  ///   left to right in the string represent red, green, blue, and (if
  ///   present) alpha, respectively.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     A six-character hex string without an alpha component may also
  ///     be passed to this method.
  ///   </para>
  ///   <para>
  ///     A three- or four-character hex string may be passed to this
  ///     method, in which case each character is doubled to represent a
  ///     full byte.
  ///   </para>
  ///   <para>
  ///     The hex code may be prefixed with <c>#</c>, <c>$</c>, <c>0x</c>
  ///     (case insensitive), or nothing. No other prefix is allowed.
  ///   </para>
  /// </remarks>
  /// <param name="hex">The string.</param>
  /// <param name="result">
  ///   If this method returns <see langword="true"/>, this parameter is
  ///   set to the created color. Otherwise, this parameter is set to
  ///   <see langword="default"/>(<see cref="Color"/>), which is opaque black.
  /// </param>
  /// <param name="alphaFirst">
  ///   If true, the first byte represents alpha, not the last. Ignored if
  ///   the input is a three-byte hex string.
  /// </param>
  /// <returns>
  ///   Whether or not the input <paramref name="hex"/> is a valid color
  ///   hex string.
  /// </returns>
  public static bool TryFromHex(string hex, out Color result, bool alphaFirst = false)
  {
    try
    {
      result = Color.FromHex(hex, alphaFirst);
      return true;
    }
    catch (Exception)
    {
      result = default!;
      return false;
    }
  }
  #endregion

  #region Grayscale
  /// <summary>
  ///   Generates a shade of gray with a given value.
  /// </summary>
  /// <remarks>
  ///   <paramref name="value"/> and <paramref name="alpha"/> need not
  ///   necessarily be constrained to the range of [0, 1], but values
  ///   outside this range may cause undefined behavior.
  /// </remarks>
  /// <param name="value">
  ///   The Value of the Color. Also matches its Luminosity and Intensity.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The generated shade of gray.</returns>
  public static Color FromGray(double value, double alpha = 1)
    => new Color(value, value, value, alpha);

  /// <summary>
  ///   Generates a shade of gray with a given value in the range 0 to
  ///   255.
  /// </summary>
  /// <remarks>
  ///   <paramref name="value"/> and <paramref name="alpha"/> need not
  ///   necessarily be constrained to the range of [0, 255], but values
  ///   outside this range may cause undefined behavior.
  /// </remarks>
  /// <param name="value">
  ///   The Value of the Color. Also matches its Luminosity and Intensity.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The generated shade of gray.</returns>
  public static Color FromIntGray(int value, int alpha = 255)
    => new Color(value, value, value, alpha);
  #endregion

  #region Hue Calculations
  /// <summary>
  ///   Generates a color from a given hue (in degrees) and min and max
  ///   component values.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     <paramref name="maxColor"/>, <paramref name="minColor"/>, and
  ///     <paramref name="alpha"/> need not necessarily be constrained to
  ///     the range of [0, 1], but values outside this range may cause
  ///     undefined behavior.
  ///   </para>
  ///   <para>
  ///     If <paramref name="maxColor"/> is specified as lower than
  ///     <paramref name="minColor"/>, the resulting color will have its
  ///     hue flipped 180°.
  ///   </para>
  /// </remarks>
  /// <param name="hue">
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
  public static Color FromHMM(double hue, double minColor, double maxColor, double alpha = 1) =>
    (double.IsNaN(hue)) ?
    new Color(
      red: Internal.Lerp(0.5, minColor, maxColor),
      green: Internal.Lerp(0.5, minColor, maxColor),
      blue: Internal.Lerp(0.5, minColor, maxColor),
      alpha: alpha
    ) :
    new Color(
      red: Internal.Lerp(GetRedFactor(hue), minColor, maxColor),
      green: Internal.Lerp(GetGreenFactor(hue), minColor, maxColor),
      blue: Internal.Lerp(GetBlueFactor(hue), minColor, maxColor),
      alpha: alpha
    );

  /// <summary>
  ///   Generates a color from a given hue (in degrees) and min and max
  ///   component values.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     <paramref name="maxColor"/>, <paramref name="minColor"/>, and
  ///     <paramref name="alpha"/> need not necessarily be constrained to
  ///     the range of [0, 255], but values outside this range may cause
  ///     undefined behavior.
  ///   </para>
  ///   <para>
  ///     If <paramref name="maxColor"/> is specified as lower than
  ///     <paramref name="minColor"/>, the resulting color will have its
  ///     hue flipped 180°.
  ///   </para>
  /// </remarks>
  /// <param name="hue">
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
  public static Color FromIntHMM(int hue, int minColor, int maxColor, int alpha = 255) => new Color(
      red: Internal.Lerp(GetRedFactor(hue), minColor / 255.0, maxColor / 255.0),
      green: Internal.Lerp(GetGreenFactor(hue), minColor / 255.0, maxColor / 255.0),
      blue: Internal.Lerp(GetBlueFactor(hue), minColor / 255.0, maxColor / 255.0),
      alpha: alpha
    );

  static double GetRedFactor(double hue) => Math.Clamp(Math.Abs(3 - Internal.NNMod(hue / 60, 6)), 1, 2) - 1;
  static double GetGreenFactor(double hue) => Math.Clamp(Math.Abs(3 - Internal.NNMod(hue / 60 + 4, 6)), 1, 2) - 1;
  static double GetBlueFactor(double hue) => Math.Clamp(Math.Abs(3 - Internal.NNMod(hue / 60 + 2, 6)), 1, 2) - 1;
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
  ///   The Luminosity-based Saturation of the Color.
  /// </param>
  /// <param name="luminosity">
  ///   The Luminosity of the Color.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHSL(double hue, double saturation, double luminosity, double alpha = 1)
    => FromHCL(hue, (1 - Math.Abs(2 * luminosity - 1)) * saturation, luminosity, alpha);

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
  /// <param name="hue">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="saturation">
  ///   The Luminosity-based Saturation of the Color.
  /// </param>
  /// <param name="luminosity">
  ///   The Luminosity of the Color.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromIntHSL(int hue, int saturation, int luminosity, int alpha = 255)
    => FromHSL(hue, saturation / 255.0, luminosity / 255.0, alpha / 255.0);

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
  /// <param name="hue">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="chroma">
  ///   The Chroma of the Color.
  /// </param>
  /// <param name="luminosity">
  ///   The Luminosity of the Color.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHCL(double hue, double chroma, double luminosity, double alpha = 1)
  {
    double minColor = luminosity - (chroma / 2);
    double maxColor = chroma + minColor;
    return FromHMM(hue, minColor, maxColor, alpha);
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
  /// <param name="hue">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="chroma">
  ///   The Chroma of the Color.
  /// </param>
  /// <param name="luminosity">
  ///   The Luminosity of the Color.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromIntHCL(int hue, int chroma, int luminosity, int alpha = 255)
    => FromHCL(hue / 1.0, chroma / 255.0, luminosity / 255.0, alpha / 255.0);
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
  ///   The Value-based Saturation of the Color.
  /// </param>
  /// <param name="value">
  ///   The Value of the Color.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHSV(double hue, double saturation, double value, double alpha = 1)
    => FromHCV(hue, value * saturation, value, alpha);

  /// <summary>
  ///   Generates a color from a given hue (in degrees), saturation, and value.
  /// </summary>
  /// <remarks>
  ///   <paramref name="saturation"/>, <paramref name="value"/>, and <paramref name="alpha"/>
  ///   need not necessarily be constrained to the range of [0, 255], but
  ///   values outside this range may cause undefined behavior.
  /// </remarks>
  /// <param name="hue">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="saturation">
  ///   The Value-based Saturation of the Color.
  /// </param>
  /// <param name="value">
  ///   The Value of the Color.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromIntHSV(int hue, int saturation, int value, int alpha = 255)
    => FromHSV(hue, saturation / 255.0, value / 255.0, alpha / 255.0);

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
  /// <param name="hue">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="chroma">
  ///   The Chroma of the Color.
  /// </param>
  /// <param name="value">
  ///   The Value of the Color.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromHCV(double hue, double chroma, double value, double alpha = 1)
  {
    double maxColor = value;
    double minColor = maxColor - chroma;
    return FromHMM(hue, minColor, maxColor, alpha);
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
  /// <param name="hue">
  ///   The Hue of the Color, given in degrees. Is automatically
  ///   normalized to the range of [0, 360).
  /// </param>
  /// <param name="chroma">
  ///   The Chroma of the Color.
  /// </param>
  /// <param name="value">
  ///   The Luminosity of the Color.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) of the Color.
  /// </param>
  /// <returns>The created Color.</returns>
  public static Color FromIntHCV(int hue, int chroma, int value, int alpha = 255)
    => FromHCV(hue / 1.0, chroma / 255.0, value / 255.0, alpha / 255.0);
  #endregion
}