using System;

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
  }
}