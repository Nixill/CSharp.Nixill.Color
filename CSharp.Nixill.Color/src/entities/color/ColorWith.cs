namespace Nixill.Colors;

public readonly partial struct Color
{
  /// <summary>
  ///   Creates a new Color with the same <see cref="Red"/>, <see cref="Green"/>,
  ///   and <see cref="Blue"/> values as this Color, and the specified
  ///   <see cref="Alpha"/>.
  /// </summary>
  /// <param name="alpha">
  ///   The Alpha (opacity) which the new Color should have.
  /// </param>
  /// <returns>The modified Color.</returns>
  public Color WithAlpha(double alpha)
    => new Color(Red, Green, Blue, alpha);

  /// <summary>
  ///   Creates a new Color with the same <see cref="Red"/>, <see cref="Green"/>,
  ///   and <see cref="Blue"/> values as this Color, and the specified
  ///   <see cref="Alpha"/>.
  /// </summary>
  /// <param name="alpha">
  ///   The Alpha (opacity) which the new Color should have.
  /// </param>
  /// <returns>The modified Color.</returns>
  public Color WithIntAlpha(int alpha)
    => new Color(Red, Green, Blue, alpha / 255.0);

  #region RGB, etc
  /// <summary>
  ///   Copies this color, changing the copy's <see cref="Red"/>, <see cref="Green"/>,
  ///   <see cref="Blue"/>, and/or <see cref="Alpha"/> values as specified.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     Components need not necessarily be constrained to the range of
  ///     [0, 1], but values outside this range may cause undefined behavior.
  ///   </para>
  ///   <para>
  ///     Any component not specified is copied unchanged.
  ///   </para>
  /// </remarks>
  /// <param name="red">
  ///   The Red component of the new Color.
  /// </param>
  /// <param name="green">
  ///   The Green component of the new Color.
  /// </param>
  /// <param name="blue">
  ///   The Blue component of the new Color.
  /// </param>
  /// <param name="alpha">
  ///   The Alpha (opacity) component of the new Color.
  /// </param>
  /// <returns>The modified Color.</returns>
  public Color WithRGBA(double? red = null, double? green = null, double? blue = null, double? alpha = null)
    => new Color(red ?? Red, green ?? Green, blue ?? Blue, alpha ?? Alpha);

  /// <summary>
  ///   Copies this color, changing its <see cref="Red"/>, <see cref="Green"/>,
  ///   <see cref="Blue"/>, and/or <see cref="Alpha"/> values as specified.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     Components need not necessarily be constrained to the range of
  ///     [0, 1], but values outside this range may cause undefined behavior.
  ///   </para>
  ///   <para>
  ///     Any component not specified is copied unchanged.
  ///   </para>
  /// </remarks>
  /// <param name="red">
  ///   The IntRed component of the new Color.
  /// </param>
  /// <param name="green">
  ///   The IntGreen component of the new Color.
  /// </param>
  /// <param name="blue">
  ///   The IntBlue component of the new Color.
  /// </param>
  /// <param name="alpha">
  ///   The IntAlpha component of the new Color.
  /// </param>
  /// <returns>The modified Color.</returns>
  public Color WithIntRGBA(int? red = null, int? green = null, int? blue = null, int? alpha = null)
    => new Color(red / 255.0 ?? Red, green / 255.0 ?? Green, blue / 255.0 ?? Blue, alpha / 255.0 ?? Alpha);
  #endregion

  #region Hue
  /// <summary>
  ///   Copies this Color, changing its Hue to the value specified.
  /// </summary>
  /// <param name="hue">The new Hue in degrees.</param>
  /// <returns>The modified Color.</returns>
  public Color WithHue(double hue)
    => Color.FromHMM(hue, MinColor, MaxColor, Alpha);

  /// <summary>
  ///   Copies this Color, changing its <see cref="Hue"/>,
  ///   <see cref="MaxColor"/>, <see cref="MinColor"/>, and <see cref="Alpha"/>
  ///   as specified.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     <paramref name="maxColor"/>, <paramref name="minColor"/>, and
  ///     <paramref name="alpha"/> need not necessarily be constrained to
  ///     the range of [0, 1], but values outside this range may cause
  ///     undefined behavior.
  ///   </para>
  ///   <para>
  ///     Any component not specified is copied unchanged, except that
  ///     setting min color higher than max color will flip the hue 180°,
  ///     and setting them equal to each other will set hue to 0.
  ///   </para>
  /// </remarks>
  /// <param name="hue">
  ///   The Hue of the new Color in degrees.
  /// </param>
  /// <param name="minColor">
  ///   The smallest value of a component of the new Color.
  /// </param>
  /// <param name="maxColor">
  ///   The largest value of a component of the new Color.
  /// </param>
  /// <param name="alpha">The alpha (opacity) of the new Color.</param>
  /// <returns>The modified Color.</returns>
  public Color WithHMM(double? hue = null, double? minColor = null, double? maxColor = null, double? alpha = null)
    => Color.FromHMM(hue ?? Hue, minColor ?? MinColor, maxColor ?? MaxColor, alpha ?? Alpha);

  /// <summary>
  ///   Copies this Color, changing its <see cref="Hue"/>,
  ///   <see cref="MaxColor"/>, <see cref="MinColor"/>, and <see cref="Alpha"/>
  ///   as specified.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     <paramref name="maxColor"/>, <paramref name="minColor"/>, and
  ///     <paramref name="alpha"/> need not necessarily be constrained to
  ///     the range of [0, 255], but values outside this range may cause
  ///     undefined behavior.
  ///   </para>
  ///   <para>
  ///     If <paramref name="maxColor"/> (or the existing <see cref="MaxColor"/>)
  ///     is specified as lower than <paramref name="minColor"/> (or the
  ///     existing <see cref="MinColor"/>), the resulting color will have
  ///     its hue flipped 180° from either the specified or existing value.
  ///   </para>
  ///   <para>
  ///     Any component not specified is copied unchanged, except that
  ///     setting min color higher than max color will flip the hue 180°,
  ///     and setting them equal to each other will set hue to 0.
  ///   </para>
  /// </remarks>
  /// <param name="hue">
  ///   The Hue of the new Color in degrees.
  /// </param>
  /// <param name="minColor">
  ///   The smallest value of a component of the new Color.
  /// </param>
  /// <param name="maxColor">
  ///   The largest value of a component of the new Color.
  /// </param>
  /// <param name="alpha">The alpha (opacity) of the new Color.</param>
  /// <returns>The modified Color.</returns>
  public Color WithIntHMM(int? hue = null, int? min = null, int? max = null, int? alpha = null)
    => Color.FromHMM(hue ?? Hue, min / 255.0 ?? MinColor, max / 255.0 ?? MaxColor, alpha / 255.0 ?? Alpha);
  #endregion

  #region HSL
  /// <summary>
  ///   Copies this Color, changing its <see cref="Hue"/>,
  ///   <see cref="LSaturation"/>, <see cref="Luminosity"/>, and <see cref="Alpha"/>
  ///   as specified.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     <paramref name="saturation"/>, <paramref name="luminosity"/>,
  ///     and <paramref name="alpha"/> need not necessarily be constrained
  ///     to the range of [0, 1], but values outside this range may cause
  ///     undefined behavior.
  ///   </para>
  ///   <para>
  ///     Any component not specified is copied unchanged, except that
  ///     setting luminosity to 1 or 0 will set hue and saturation to 0,
  ///     and setting saturation to 0 will also set hue to 0.
  ///   </para>
  /// </remarks>
  /// <param name="hue">
  ///   The hue of the new Color in degrees.
  /// </param>
  /// <param name="saturation">
  ///   The luminosity-based saturation of the new Color.
  /// </param>
  /// <param name="luminosity">
  ///   The luminosity of the new Color.
  /// </param>
  /// <param name="alpha">The alpha (opacity) of the new Color.</param>
  /// <returns>The modified Color.</returns>
  public Color WithHSL(double? hue = null, double? saturation = null, double? luminosity = null, double? alpha = null)
    => Color.FromHSL(hue ?? Hue, saturation ?? LSaturation, luminosity ?? Luminosity, alpha ?? Alpha);

  /// <summary>
  ///   Copies this Color, changing its <see cref="Hue"/>,
  ///   <see cref="LSaturation"/>, <see cref="Luminosity"/>, and <see cref="Alpha"/>
  ///   as specified.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     <paramref name="saturation"/>, <paramref name="luminosity"/>,
  ///     and <paramref name="alpha"/> need not necessarily be constrained
  ///     to the range of [0, 255], but values outside this range may
  ///     cause undefined behavior.
  ///   </para>
  ///   <para>
  ///     Any component not specified is copied unchanged, except that
  ///     setting luminosity to 255 or 0 will set hue and saturation to 0,
  ///     and setting saturation to 0 will also set hue to 0.
  ///   </para>
  /// </remarks>
  /// <param name="hue">
  ///   The hue of the new Color in degrees.
  /// </param>
  /// <param name="saturation">
  ///   The luminosity-based saturation of the new Color.
  /// </param>
  /// <param name="luminosity">
  ///   The luminosity of the new Color.
  /// </param>
  /// <param name="alpha">The alpha (opacity) of the new Color.</param>
  /// <returns>The modified Color.</returns>
  public Color WithIntHSL(int? hue = null, int? saturation = null, int? luminosity = null, int? alpha = null)
    => Color.FromHSL(hue ?? Hue, saturation / 255.0 ?? LSaturation, luminosity / 255.0 ?? Luminosity, alpha / 255.0 ?? Alpha);

  /// <summary>
  ///   Copies this Color, changing its <see cref="Hue"/>,
  ///   <see cref="Chroma"/>, <see cref="Luminosity"/>, and <see cref="Alpha"/>
  ///   as specified.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     To produce an in-gamut Color, <paramref name="luminosity"/> (or
  ///     the existing <see cref="Luminosity"/>) and <paramref name="alpha"/>
  ///     (or the existing <see cref="Alpha"/>) must be between 0 and 1,
  ///     and <paramref name="chroma"/> (or the existing <see cref="Chroma"/>)
  ///     must be between 0 and <c>2 * Math.Abs(0.5 - luminosity)</c>.
  ///     Values outside these ranges are allowed, but may cause undefined
  ///     behavior.
  ///   </para>
  ///   <para>
  ///     Any component not specified is copied unchanged, except that
  ///     setting chroma to 0 will also set hue to 0.
  ///   </para>
  /// </remarks>
  /// <param name="hue">
  ///   The hue of the new Color in degrees.
  /// </param>
  /// <param name="chroma">The chroma of the new Color.</param>
  /// <param name="luminosity">The luminosity of the new Color.</param>
  /// <param name="alpha">The alpha (opacity) of the new Color.</param>
  /// <returns>The modified Color.</returns>
  public Color WithHCL(double? hue = null, double? chroma = null, double? luminosity = null, double? alpha = null)
    => Color.FromHCL(hue ?? Hue, chroma ?? Chroma, luminosity ?? Luminosity, alpha ?? Alpha);

  /// <summary>
  ///   Copies this Color, changing its <see cref="Hue"/>,
  ///   <see cref="Chroma"/>, <see cref="Luminosity"/>, and <see cref="Alpha"/>
  ///   as specified.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     To produce an in-gamut Color, <paramref name="luminosity"/> (or
  ///     the existing <see cref="Luminosity"/>) and <paramref name="alpha"/>
  ///     (or the existing <see cref="Alpha"/>) must be between 0 and 255,
  ///     and <paramref name="chroma"/> (or the existing <see cref="Chroma"/>)
  ///     must be between 0 and <c>2 * Math.Abs(127.5 - luminosity)</c>.
  ///     Values outside these ranges are allowed, but may cause undefined
  ///     behavior.
  ///   </para>
  ///   <para>
  ///     Any component not specified is copied unchanged, except that
  ///     setting chroma to 0 will also set hue to 0.
  ///   </para>
  /// </remarks>
  /// <param name="hue">
  ///   The hue of the new Color in degrees.
  /// </param>
  /// <param name="chroma">The chroma of the new Color.</param>
  /// <param name="luminosity">The luminosity of the new Color.</param>
  /// <param name="alpha">The alpha (opacity) of the new Color.</param>
  /// <returns>The modified Color.</returns>
  public Color WithIntHCL(int? hue = null, int? chroma = null, int? luminosity = null, int? alpha = null)
    => Color.FromHCL(hue ?? Hue, chroma / 255.0 ?? Chroma, luminosity / 255.0 ?? Luminosity, alpha / 255.0 ?? Alpha);
  #endregion

  #region HSV
  /// <summary>
  ///   Copies this Color, changing its <see cref="Hue"/>,
  ///   <see cref="VSaturation"/>, <see cref="Value"/>, and <see cref="Alpha"/>
  ///   as specified.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     <paramref name="saturation"/>, <paramref name="value"/>, and
  ///     <paramref name="alpha"/> need not necessarily be constrained to
  ///     the range of [0, 1], but values outside this range may cause
  ///     undefined behavior.
  ///   </para>
  ///   <para>
  ///     Any component not specified is copied unchanged, except that
  ///     setting value to 0 will set hue and saturation to 0, and setting
  ///     saturation to 0 will also set hue to 0.
  ///   </para>
  /// </remarks>
  /// <param name="hue">
  ///   The hue of the new Color in degrees.
  /// </param>
  /// <param name="saturation">
  ///   The value-based saturation of the new Color.
  /// </param>
  /// <param name="value">The value of the new Color.</param>
  /// <param name="alpha">The alpha (opacity) of the new Color.</param>
  /// <returns>The modified Color.</returns>
  public Color WithHSV(double? hue = null, double? saturation = null, double? value = null, double? alpha = null)
    => Color.FromHSV(hue ?? Hue, saturation ?? VSaturation, value ?? Value, alpha ?? Alpha);

  /// <summary>
  ///   Copies this Color, changing its <see cref="Hue"/>,
  ///   <see cref="VSaturation"/>, <see cref="Value"/>, and <see cref="Alpha"/>
  ///   as specified.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     <paramref name="saturation"/>, <paramref name="value"/>, and
  ///     <paramref name="alpha"/> need not necessarily be constrained to
  ///     the range of [0, 255], but values outside this range may cause
  ///     undefined behavior.
  ///   </para>
  ///   <para>
  ///     Any component not specified is copied unchanged, except that
  ///     setting value to 0 will set hue and saturation to 0, and setting
  ///     saturation to 0 will also set hue to 0.
  ///   </para>
  /// </remarks>
  /// <param name="hue">
  ///   The hue of the new Color in degrees.
  /// </param>
  /// <param name="saturation">
  ///   The value-based saturation of the new Color.
  /// </param>
  /// <param name="value">The value of the new Color.</param>
  /// <param name="alpha">The alpha (opacity) of the new Color.</param>
  /// <returns>The modified Color.</returns>
  public Color WithIntHSV(int? hue = null, int? saturation = null, int? value = null, int? alpha = null)
    => Color.FromHSV(hue ?? Hue, saturation / 255.0 ?? VSaturation, value / 255.0 ?? Value, alpha / 255.0 ?? Alpha);

  /// <summary>
  ///   Copies this Color, changing its <see cref="Hue"/>, <see cref="Chroma"/>
  ///   <see cref="Value"/>, and <see cref="Alpha"/> as specified.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     To produce an in-gamut Color, <paramref name="value"/> (or
  ///     the existing <see cref="Value"/>) and <paramref name="alpha"/>
  ///     (or the existing <see cref="Alpha"/>) must be between 0 and 1,
  ///     and <paramref name="chroma"/> (or the existing <see cref="Chroma"/>)
  ///     must be between 0 and the Value. Values outside these ranges are
  ///     allowed, but may cause undefined behavior.
  ///   </para>
  ///   <para>
  ///     Any component not specified is copied unchanged, except that
  ///     setting chroma to 0 will also set hue to 0.
  ///   </para>
  /// </remarks>
  /// <param name="hue">
  ///   The hue of the new Color in degrees.
  /// </param>
  /// <param name="chroma">The chroma of the new Color.</param>
  /// <param name="value">The value of the new Color.</param>
  /// <param name="alpha">The alpha (opacity) of the new Color.</param>
  /// <returns>The modified Color.</returns>
  public Color WithHCV(double? hue = null, double? chroma = null, double? value = null, double? alpha = null)
    => Color.FromHCV(hue ?? Hue, chroma ?? Chroma, value ?? Value, alpha ?? Alpha);

  /// <summary>
  ///   Copies this Color, changing its <see cref="Hue"/>, <see cref="Chroma"/>
  ///   <see cref="Value"/>, and <see cref="Alpha"/> as specified.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     To produce an in-gamut Color, <paramref name="value"/> (or
  ///     the existing <see cref="Value"/>) and <paramref name="alpha"/>
  ///     (or the existing <see cref="Alpha"/>) must be between 0 and 255,
  ///     and <paramref name="chroma"/> (or the existing <see cref="Chroma"/>)
  ///     must be between 0 and the Value. Values outside these ranges are
  ///     allowed, but may cause undefined behavior.
  ///   </para>
  ///   <para>
  ///     Any component not specified is copied unchanged, except that
  ///     setting chroma to 0 will also set hue to 0.
  ///   </para>
  /// </remarks>
  /// <param name="hue">
  ///   The hue of the new Color in degrees.
  /// </param>
  /// <param name="chroma">The chroma of the new Color.</param>
  /// <param name="value">The value of the new Color.</param>
  /// <param name="alpha">The alpha (opacity) of the new Color.</param>
  /// <returns>The modified Color.</returns>
  public Color WithIntHCV(int? hue = null, int? chroma = null, int? value = null, int? alpha = null)
    => Color.FromHCV(hue ?? Hue, chroma / 255.0 ?? Chroma, value / 255.0 ?? Value, alpha / 255.0 ?? Alpha);
  #endregion
}