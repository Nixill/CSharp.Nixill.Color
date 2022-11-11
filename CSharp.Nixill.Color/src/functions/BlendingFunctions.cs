using static Nixill.Utils.Interpolation;

namespace Nixill.Colors;

public delegate Color BlendingFunction(Color left, Color right, float pos);

public static class BlendingFunctions
{
  public static BlendingFunction sRGB => (left, right, pos) => new Color(
    Lerp(left.Red, right.Red, pos),
    Lerp(left.Green, right.Green, pos),
    Lerp(left.Blue, right.Blue, pos),
    Lerp(left.Alpha, right.Alpha, pos)
  );

  public static BlendingFunction CustomsRGB(InterpolationFunction all) => CustomsRGB(all, all, all, all);

  public static BlendingFunction CustomsRGB(InterpolationFunction red, InterpolationFunction green, InterpolationFunction blue, InterpolationFunction alpha = null)
  {
    if (alpha == null) alpha = InterpolationFunctions.Linear;

    return (left, right, pos) => new Color(
      Lerp(left.Red, right.Red, red(pos)),
      Lerp(left.Green, right.Green, green(pos)),
      Lerp(left.Blue, right.Blue, blue(pos)),
      Lerp(left.Alpha, right.Alpha, alpha(pos))
    );
  }

  public static BlendingFunction LinearRGB => (left, right, pos) => new Color()
  {
    LinearRed = Lerp(left.LinearRed, right.LinearRed, pos),
    LinearGreen = Lerp(left.LinearGreen, right.LinearGreen, pos),
    LinearBlue = Lerp(left.LinearBlue, right.LinearBlue, pos),
    Alpha = Lerp(left.Alpha, right.Alpha, pos)
  };

  public static BlendingFunction CustomLinearRGB(InterpolationFunction all) => CustomLinearRGB(all, all, all, all);

  public static BlendingFunction CustomLinearRGB(InterpolationFunction red, InterpolationFunction green, InterpolationFunction blue, InterpolationFunction alpha = null)
  {
    if (alpha == null) alpha = InterpolationFunctions.Linear;

    return (left, right, pos) => new Color()
    {
      LinearRed = Lerp(left.LinearRed, right.LinearRed, red(pos)),
      LinearGreen = Lerp(left.LinearGreen, right.LinearGreen, green(pos)),
      LinearBlue = Lerp(left.LinearBlue, right.LinearBlue, blue(pos)),
      Alpha = Lerp(left.Alpha, right.Alpha, alpha(pos))
    };
  }

  public static BlendingFunction HSV => CustomHSV(InterpolationFunctions.Linear);

  public static BlendingFunction CustomHSV(InterpolationFunction all, NaNOverride hueOverride = null,
    NaNOverride satOverride = null, bool isIncreasing = true, bool equalFullCircle = false) => CustomHSV(all, all, all,
      all, hueOverride, satOverride, isIncreasing, equalFullCircle);

  public static BlendingFunction CustomHSV(InterpolationFunction hue, InterpolationFunction sat,
    InterpolationFunction val, InterpolationFunction alpha = null, NaNOverride hueOverride = null,
    NaNOverride satOverride = null, bool isIncreasing = true, bool equalFullCircle = false) => (left, right, pos) =>
  {
    if (alpha == null) alpha = InterpolationFunctions.Linear;
    if (hueOverride == null) hueOverride = NaNOverrides.EqualHue(0);
    if (satOverride == null) satOverride = NaNOverrides.Zero;

    (float L, float R) hues = (left.Hue, right.Hue);
    (float L, float R) sats = (left.VSaturation, right.VSaturation);
    (float L, float R) vals = (left.Value, right.Value);
    (float L, float R) alphas = (left.Alpha, right.Alpha);

    if (float.IsNaN(hues.L)) hues.L = hueOverride(left, right);
    if (float.IsNaN(hues.R)) hues.R = hueOverride(right, left);
    if (float.IsNaN(sats.L)) sats.L = satOverride(left, right);
    if (float.IsNaN(sats.R)) sats.R = satOverride(right, left);

    if (hues.L > hues.R && isIncreasing) hues.R += 360;
    else if (hues.R > hues.L && !isIncreasing) hues.L += 360;

    return Color.FromHSV(
      Lerp(hues.L, hues.R, hue(pos)),
      Lerp(sats.L, sats.R, sat(pos)),
      Lerp(vals.L, vals.R, val(pos)),
      Lerp(alphas.L, alphas.R, alpha(pos))
    );
  };
}