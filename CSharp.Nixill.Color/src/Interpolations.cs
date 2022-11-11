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
      return a + x * (b - a);
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

    internal static (float, float) Decompress(float x1, float x2, float by) => (x1 - (by * (x2 - x1)) / (2 * (1 - by)), ((1 - by / 2) * (x2 - x1)) / (1 - by) + x1);
  }
}