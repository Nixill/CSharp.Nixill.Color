using Nixill.Internals;

namespace Nixill.Colors;

/// <summary>
///   Represents a single color expressed as red/green/blue/alpha doubles
///   in sRGB color space.
/// </summary>
/// <param name="red">The red component of the color.</param>
/// <param name="green">The green component of the color.</param>
/// <param name="blue">The blue component of the color.</param>
/// <param name="alpha">
///   The alpha (opacity) component of the color.
/// </param>
public readonly partial struct Color(double red, double green, double blue, double alpha = 1)
{
  /// <summary>
  ///   Get: The red component of this Color, normally expressed as a
  ///   value from 0 to 1.
  /// </summary>
  /// <remarks>
  ///   This component need not necessarily be clamped to 0..1, but values
  ///   outside this range may cause undefined behavior.
  /// </remarks>
  public readonly double Red = red;

  /// <summary>
  ///   Get: The green component of this Color, normally expressed as a
  ///   value from 0 to 1.
  /// </summary>
  /// <remarks>
  ///   This component need not necessarily be clamped to 0..1, but values
  ///   outside this range may cause undefined behavior.
  /// </remarks>
  public readonly double Green = green;

  /// <summary>
  ///   Get: The blue component of this Color, normally expressed as a
  ///   value from 0 to 1.
  /// </summary>
  /// <remarks>
  ///   This component need not necessarily be clamped to 0..1, but values
  ///   outside this range may cause undefined behavior.
  /// </remarks>
  public readonly double Blue = blue;

  /// <summary>
  ///   Get: The <i>transparency</i> component of this Color (the inverse
  ///   Alpha), normally expressed as a value from 0 to 1.
  /// </summary>
  /// <remarks>
  ///   This component need not necessarily be clamped to 0..1, but values
  ///   outside this range may cause undefined behavior.
  /// </remarks>
  readonly double InverseAlpha = 1 - alpha;

  /// <summary>
  ///   Get: The alpha (opaticy) component of this Color, normally
  ///   expressed as a value from 0 to 1.
  /// </summary>
  /// <remarks>
  ///   This component need not necessarily be clamped to 0..1, but values
  ///   outside this range may cause undefined behavior.
  /// </remarks>
  public readonly double Alpha => 1 - InverseAlpha;

  /// <summary>
  ///   Get: The red component of this Color, normally expressed as a
  ///   value from 0 to 255.
  /// </summary>
  /// <remarks>
  ///   This component need not necessarily be clamped to 0..255, but
  ///   values outside this range may cause undefined behavior.
  /// </remarks>
  public int IntRed => (int)Math.Round(Red * 255, MidpointRounding.ToEven);

  /// <summary>
  ///   Get: The green component of this Color, normally expressed as a
  ///   value from 0 to 255.
  /// </summary>
  /// <remarks>
  ///   This component need not necessarily be clamped to 0..255, but
  ///   values outside this range may cause undefined behavior.
  /// </remarks>
  public int IntGreen => (int)Math.Round(Green * 255, MidpointRounding.ToEven);

  /// <summary>
  ///   Get: The blue component of this Color, normally expressed as a
  ///   value from 0 to 255.
  /// </summary>
  /// <remarks>
  ///   This component need not necessarily be clamped to 0..255, but
  ///   values outside this range may cause undefined behavior.
  /// </remarks>
  public int IntBlue => (int)Math.Round(Blue * 255, MidpointRounding.ToEven);

  /// <summary>
  ///   Get: The alpha (opacity) component of this Color, normally
  ///   expressed as a value from 0 to 255.
  /// </summary>
  /// <remarks>
  ///   This component need not necessarily be clamped to 0..255, but
  ///   values outside this range may cause undefined behavior.
  /// </remarks>
  public int IntAlpha => (int)Math.Round(Alpha * 255, MidpointRounding.ToEven);

  /// <summary>
  ///   Get: The red component of this Color as a value between 0 and 255.
  /// </summary>
  /// <remarks>
  ///   If this component's decimal value is less than 0 or greater than
  ///   1, it is clamped to that range.
  /// </remarks>
  public byte ByteRed => (byte)Math.Round(Math.Clamp(Red, 0, 1) * 255, MidpointRounding.ToEven);

  /// <summary>
  ///   Get: The green component of this Color as a value between 0 and 255.
  /// </summary>
  /// <remarks>
  ///   If this component's decimal value is less than 0 or greater than
  ///   1, it is clamped to that range.
  /// </remarks>
  public byte ByteGreen => (byte)Math.Round(Math.Clamp(Green, 0, 1) * 255, MidpointRounding.ToEven);

  /// <summary>
  ///   Get: The blue component of this Color as a value between 0 and 255.
  /// </summary>
  /// <remarks>
  ///   If this component's decimal value is less than 0 or greater than
  ///   1, it is clamped to that range.
  /// </remarks>
  public byte ByteBlue => (byte)Math.Round(Math.Clamp(Blue, 0, 1) * 255, MidpointRounding.ToEven);

  /// <summary>
  ///   Get: The alpha (opacity) component of this Color as a value
  ///   between 0 and 255.
  /// </summary>
  /// <remarks>
  ///   If this component's decimal value is less than 0 or greater than
  ///   1, it is clamped to that range.
  /// </remarks>
  public byte ByteAlpha => (byte)Math.Round(Math.Clamp(Alpha, 0, 1) * 255, MidpointRounding.ToEven);

  /// <summary>
  ///   Get: The hue of this Color, on a scale spanning 0 (red), 2
  ///   (green), 4 (blue), and 6 (red). If this Color is fully
  ///   desaturated, 0 is returned.
  /// </summary>
  public double Hue0To6
  {
    get
    {
      double[] sortedColors = [.. Enumerable.Order([Red, Green, Blue])];

      double hueScale = Internal.InvLerp(sortedColors);

      return (Red == sortedColors[2]) ? ((Green == sortedColors[0]) ? (6 - hueScale) : hueScale)
        : (Green == sortedColors[2]) ? 2 + ((Blue == sortedColors[0]) ? -hueScale : hueScale)
        : (Blue == sortedColors[2]) ? 4 + ((Red == sortedColors[0]) ? -hueScale : hueScale)
        : throw new InvalidOperationException("How did you cause this error?");
    }
  }

  /// <summary>
  ///   Get: The hue of this Color, on a scale spanning 0 (red), 120
  ///   (green), 240 (blue), and 360 (red). If this Color is fully
  ///   desaturated, 0 is returned.
  /// </summary>
  public double HueDegrees => Hue0To6 * 60;

  /// <summary>
  ///   Get: The hue of this Color, on a scale spanning 0 (red), 2π/3
  ///   (green), 4π/3 (blue), and 2π (red). If this Color is fully
  ///   desaturated, 0 is returned.
  /// </summary>
  public double HueRadians => Hue0To6 / 30 * Math.PI;

  /// <summary>
  ///   Get: Either this Color's <see cref="Red"/>, its <see cref="Green"/>,
  ///   or its <see cref="Blue"/>, whichever is highest.
  /// </summary>
  public double MaxColor => double.Max(double.Max(Red, Green), Blue);

  /// <summary>
  ///   Get: Either this Color's <see cref="Red"/>, its <see cref="Green"/>,
  ///   or its <see cref="Blue"/>, whichever is lowest.
  /// </summary>
  public double MinColor => double.Min(double.Min(Red, Green), Blue);

  /// <summary>
  ///   Get: This Color's <see cref="Red"/>, <see cref="Green"/>, and
  ///   <see cref="Blue"/>, in order from lowest to highest.
  /// </summary>
  public double[] SortedColors => [.. Enumerable.Order([Red, Green, Blue])];

  /// <summary>
  ///   Get: The Chroma of this Color, defined as the difference between
  ///   its <see cref="MaxColor"/> and its <see cref="MinColor"/>.
  /// </summary>
  public double Chroma
  {
    get
    {
      double[] sort = SortedColors;
      return sort[2] - sort[0];
    }
  }

  /// <summary>
  ///   Get: The Value of this Color, defined simply as its <see cref="MaxColor"/>.
  /// </summary>
  public double Value => MaxColor;

  /// <summary>
  ///   Get: The Luminosity of this Color, defined as the midpoint between
  ///   its <see cref="MinColor"/> and its <see cref="MaxColor"/>.
  /// </summary>
  public double Luminosity
  {
    get
    {
      double[] sort = SortedColors;
      return (sort[2] + sort[0]) / 2;
    }
  }

  /// <summary>
  ///   Get: The Value-based Saturation of this Color, defined as the
  ///   ratio between its <see cref="Chroma"/> and its <see cref="Value"/>,
  ///   special-cased to 0 if the Value is 0.
  /// </summary>
  public double VSaturation
  {
    get
    {
      double[] sort = SortedColors;
      if (sort[2] == 0) return 0;
      return (sort[2] - sort[0]) / sort[2];
    }
  }

  /// <summary>
  ///   Get: The Luminosity-based Saturation of this Color, defined as the
  ///   ratio between its <see cref="Chroma"/> and the proximity of its
  ///   <see cref="Luminosity"/> to 0.5 (<c>1 - |2L - 1|</c>),
  ///   special-cased to 0 if the Luminosity is either 0 or 1.
  /// </summary>
  public double LSaturation
  {
    get
    {
      double[] sort = SortedColors;
      double lum = (sort[2] + sort[0]) / 2;
      if (lum == 1 || lum == 0) return 0;
      return (sort[2] - sort[0]) / (1 - Math.Abs(2 * lum - 1));
    }
  }
}