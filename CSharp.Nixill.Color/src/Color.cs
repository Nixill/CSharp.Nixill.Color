using System;

namespace Nixill
{
  public class Color
  {
    public float Red { get; internal set; }
    public float Green { get; internal set; }
    public float Blue { get; internal set; }

    public float LinearRed
    {
      get => (float)((Red <= 0.04045) ?
        Red / 12.92 :
        Math.Pow((Red + 0.055) / 1.055, 2.4));
      internal set => Red = (float)((value <= 0.0031308) ?
        value * 12.92 :
        1.055 * Math.Pow(value, 1.0 / 2.4) - 0.055);
    }

    public float LinearGreen
    {
      get => (float)((Green <= 0.04045) ?
        Green / 12.92 :
        Math.Pow((Green + 0.055) / 1.055, 2.4));
      internal set => Green = (float)((value <= 0.0031308) ?
        value * 12.92 :
        1.055 * Math.Pow(value, 1.0 / 2.4) - 0.055);
    }

    public float LinearBlue
    {
      get => (float)((Blue <= 0.04045) ?
        Blue / 12.92 :
        Math.Pow((Blue + 0.055) / 1.055, 2.4));
      internal set => Blue = (float)((value <= 0.0031308) ?
        value * 12.92 :
        1.055 * Math.Pow(value, 1.0 / 2.4) - 0.055);
    }
  }
}