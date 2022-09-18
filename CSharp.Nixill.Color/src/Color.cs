using System;
using static Nixill.Utils.Interpolation;

namespace Nixill
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

    public static implicit operator System.Drawing.Color(Nixill.Color clr)
    {
      return System.Drawing.Color.FromArgb(
        clr.IntAlpha,
        clr.IntRed,
        clr.IntGreen,
        clr.IntBlue
      );
    }

    public static implicit operator Nixill.Color(System.Drawing.Color clr)
    => new Nixill.Color()
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
  }
}