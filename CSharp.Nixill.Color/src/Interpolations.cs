namespace Nixill.Utils
{
  internal static class Interpolation
  {
    internal static float Lerp(float x1, float y1, float x2, float y2, float x, bool clamp = false)
    {
      return Lerp(y1, y2, InvLerp(x1, x2, x, clamp));
    }

    internal static float Lerp(float a, float b, float x, bool clamp = false)
    {
      if (clamp)
      {
        if (x < 0) x = 0;
        if (x > 1) x = 1;
      }
      return (b * x) + (a * (1 - x));
    }

    internal static float InvLerp(float a, float b, float p, bool clamp = false)
    {
      float x = (p - a) / (b - a);
      if (clamp)
      {
        if (x < 0) return 0;
        if (x > 1) return 1;
      }
      return x;
    }
  }
}