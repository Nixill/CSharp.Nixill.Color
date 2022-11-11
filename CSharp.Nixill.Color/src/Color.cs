using System.Text.RegularExpressions;
using Nixill.Utils;
using static Nixill.Utils.Interpolation;

namespace Nixill.Colors
{
  public struct Color
  {
    public Color(float red, float green, float blue)
    {
      Red = red;
      Green = green;
      Blue = blue;
      Alpha = 1;
    }

    public Color(float red, float green, float blue, float alpha)
    {
      Red = red;
      Green = green;
      Blue = blue;
      Alpha = alpha;
    }

    public float Red;
    public float Green;
    public float Blue;
    public float Alpha;

    // INTEGER COMPONENTS
    public int IntRed
    {
      get => (int)(Red * 255);
      set => Red = ((float)value) / 255f;
    }

    public int IntGreen
    {
      get => (int)(Green * 255);
      set => Green = ((float)value) / 255f;
    }

    public int IntBlue
    {
      get => (int)(Blue * 255);
      set => Blue = ((float)value) / 255f;
    }

    public int IntAlpha
    {
      get => (int)(Alpha * 255);
      set => Alpha = ((float)value) / 255f;
    }

    // LINEAR RGB COMPONENTS
    public float LinearRed
    {
      get => (float)((Red <= 0.04045) ?
        Red / 12.92 :
        Math.Pow((Red + 0.055) / 1.055, 2.4));
      set => Red = (float)((value <= 0.0031308) ?
        value * 12.92 :
        1.055 * Math.Pow(value, 1.0 / 2.4) - 0.055);
    }

    public float LinearGreen
    {
      get => (float)((Green <= 0.04045) ?
        Green / 12.92 :
        Math.Pow((Green + 0.055) / 1.055, 2.4));
      set => Green = (float)((value <= 0.0031308) ?
        value * 12.92 :
        1.055 * Math.Pow(value, 1.0 / 2.4) - 0.055);
    }

    public float LinearBlue
    {
      get => (float)((Blue <= 0.04045) ?
        Blue / 12.92 :
        Math.Pow((Blue + 0.055) / 1.055, 2.4));
      set => Blue = (float)((value <= 0.0031308) ?
        value * 12.92 :
        1.055 * Math.Pow(value, 1.0 / 2.4) - 0.055);
    }

    // HSV COMPONENTS
    // Note: Hue is also used in HSL
    public float Hue
    {
      get
      {
        if (Red == Green && Green == Blue) return float.NaN;

        if (Green > Red)
        {
          if (Blue > Green) /* > Red */ return Lerp(180, 240, InvLerp(Blue, Red, Green));
          if /* Green > */ (Red > Blue) return Lerp(60, 120, InvLerp(Green, Blue, Red));
          else /* Green > Blue ≥ Red */ return Lerp(180, 120, InvLerp(Green, Red, Blue));
        }
        else
        {
          if (Blue > Red) /* ≥ Green */ return Lerp(300, 240, InvLerp(Blue, Green, Red));
          if /* Red ≥ */ (Green >= Blue) return Lerp(60, 0, InvLerp(Red, Blue, Green));
          else /* Red ≥ Blue ≥ Green */ return Lerp(300, 360, InvLerp(Red, Green, Blue));
        }
      }

      set
      {
        float val = Value;

        // First let's get the NaN case out of the way.
        if (float.IsNaN(value))
        {
          Red = val;
          Green = val;
          Blue = val;
          return;
        }

        // And fully saturate the color if there's no saturation.
        float sat = VSaturation;

        if (sat == 0) VSaturation = 1;

        // Now determine the three values...
        float hue = value % 360;

        float low = (1 - sat) * val;
        float high = val;

        float hueIndex = 1 - Math.Abs((value % 120f) - 60f) / 60f;

        float mid = Lerp(low, high, hueIndex);

        // ... and their order.
        if (hue < 60) { Red = high; Green = mid; Blue = low; }
        else if (hue < 120) { Green = high; Red = mid; Blue = low; }
        else if (hue < 180) { Green = high; Blue = mid; Red = low; }
        else if (hue < 240) { Blue = high; Green = mid; Red = low; }
        else if (hue < 300) { Blue = high; Red = mid; Green = low; }
        else { Red = high; Blue = mid; Green = low; }
      }
    }

    public float Value
    {
      get => Math.Max(Red, Math.Max(Green, Blue));
      set
      {
        float sat = VSaturation;

        if (sat == 0)
        {
          Red = value;
          Green = value;
          Blue = value;
        }
        else
        {
          float pVal = Value;
          Red = Lerp(0, 0, pVal, Red, value);
          Green = Lerp(0, 0, pVal, Green, value);
          Blue = Lerp(0, 0, pVal, Blue, value);
        }
      }
    }

    public float VSaturation
    {
      get
      {
        float low = Math.Min(Red, Math.Min(Green, Blue));
        float high = Math.Max(Red, Math.Max(Green, Blue));

        return Lerp(high, 0, 0, 1, low);
      }

      set
      {
        float pSat = VSaturation;
        float val = Value;

        if (pSat == 0)
        {
          Red = val;
          Green = Lerp(0, val, 1, 0, value);
          Blue = Lerp(0, val, 1, 0, value);
          return;
        }

        Red = Lerp(0, val, pSat, Red, value);
        Green = Lerp(0, val, pSat, Green, value);
        Blue = Lerp(0, val, pSat, Blue, value);
      }
    }

    // HSL COMPONENTS
    // Note: Since HSL Hue == HSV Hue, Hue is defined above.
    public float Luminosity
    {
      get
      {
        float min = Math.Min(Red, Math.Min(Green, Blue));
        float max = Value;

        return (min + max) / 2f;
      }

      set
      {
        float sat = LSaturation;
        float lum = value;

        // Shortcut 1: 0 val is always black
        if (lum == 0)
        {
          Red = 0;
          Green = 0;
          Blue = 0;
          return;
        }

        // Shortcut 2: 1 val is always white
        if (lum == 1)
        {
          Red = 1;
          Green = 1;
          Blue = 1;
          return;
        }

        // Shortcut 3: 0 saturation means equal colors
        // (if we're starting from black or white, assume 0 saturation)
        // Note that this condition also checks for hue being NaN°.
        if (sat == 0 || float.IsNaN(sat))
        {
          Red = lum;
          Green = lum;
          Blue = lum;
          return;
        }

        // Otherwise, I'll just pull HSL to HSV formulas off Wikipedia
        Value = lum + sat * Math.Min(lum, 1 - lum);
        VSaturation = 2 * (1 - lum / Value);
      }
    }

    public float LSaturation
    {
      get
      {
        float min = Math.Min(Red, Math.Min(Green, Blue));
        float max = Value;

        return (max - min) / (1 - Math.Abs(2 * Luminosity - 1));
      }

      set
      {
        float sat = value;
        float lum = Luminosity;
        float pSat = LSaturation;

        // Shortcut 1: NaN saturation becomes black or white.
        if (float.IsNaN(sat))
        {
          if (lum <= 0.5)
          {
            Red = 0;
            Green = 0;
            Blue = 0;
          }
          else
          {
            Red = 1;
            Green = 1;
            Blue = 1;
          }
          return;
        }

        // Shortcut 2: 0 saturation becomes greyscale.
        if (sat == 0)
        {
          Red = lum;
          Green = lum;
          Blue = lum;
          return;
        }

        // Not really a shortcut, but if we get here, we should make sure
        // hue has a defined value.
        if (float.IsNaN(Hue))
        {
          Red = 1;
          Green = 0;
          Blue = 0;
        }

        // Otherwise, I'll just pull HSL to HSV formulas off Wikipedia
        Value = lum + sat * Math.Min(lum, 1 - lum);
        VSaturation = 2 * (1 - lum / Value);
      }
    }

    // CONVERSIONS
    public static implicit operator System.Drawing.Color(Nixill.Colors.Color clr)
    {
      return System.Drawing.Color.FromArgb(
        clr.IntAlpha,
        clr.IntRed,
        clr.IntGreen,
        clr.IntBlue
      );
    }

    public static implicit operator Nixill.Colors.Color(System.Drawing.Color clr)
    => new Nixill.Colors.Color()
    {
      IntAlpha = clr.A,
      IntRed = clr.R,
      IntGreen = clr.G,
      IntBlue = clr.B
    };

    public static Color FromHSV(float hue, float saturation, float value, float alpha = 1)
    => new Color(1, 0, 0, alpha)
    {
      Value = value,
      VSaturation = saturation,
      Hue = hue
    };

    public static Color FromHSL(float hue, float saturation, float luminosity, float alpha = 1)
    => new Color(1, 0, 0, alpha)
    {
      Luminosity = luminosity,
      LSaturation = saturation,
      Hue = hue
    };

    private const string HexCode = @"^(#|0x)?([0-9a-f]{2})([0-9a-f]{2})([0-9a-f]{2})([0-9a-f]{2})?$";
    private static readonly Regex HexRegex = new Regex(HexCode, RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static Color FromRGBA(string code)
    {
      if (!HexRegex.TryMatch(code, out Match hexMatch)) throw new ArgumentException("FromRGBA(string) requires a valid six- or eight-digit hex code.");

      code = code.ToLower();
      if (code.StartsWith("#")) code = code[1..^0];
      if (code.StartsWith("0x")) code = code[2..^0];

      int red = NumberUtils.StringToInt(hexMatch.Groups[2].Value, 16);
      int green = NumberUtils.StringToInt(hexMatch.Groups[3].Value, 16);
      int blue = NumberUtils.StringToInt(hexMatch.Groups[4].Value, 16);
      int alpha = 255;

      if (hexMatch.Groups[5].Success)
      {
        alpha = NumberUtils.StringToInt(hexMatch.Groups[5].Value, 16);
      }

      return new Color()
      {
        IntRed = red,
        IntGreen = green,
        IntBlue = blue,
        IntAlpha = alpha
      };
    }

    public string ToRGBHex() =>
      (new int[] { IntRed, IntGreen, IntBlue })
        .Select(x => string.Format("{0:X2}", Math.Max(0, Math.Min(255, x))))
        .Aggregate((x, y) => x + y);

    public string ToARGBHex() =>
      (new int[] { IntAlpha, IntRed, IntGreen, IntBlue })
        .Select(x => string.Format("{0:X2}", Math.Max(0, Math.Min(255, x))))
        .Aggregate((x, y) => x + y);

    public string ToRGBAHex() =>
      (new int[] { IntRed, IntGreen, IntBlue, IntAlpha })
        .Select(x => string.Format("{0:X2}", Math.Max(0, Math.Min(255, x))))
        .Aggregate((x, y) => x + y);
  }
}