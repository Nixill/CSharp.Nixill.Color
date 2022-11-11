using static System.Math;

namespace Nixill.Colors;

public delegate float InterpolationFunction(float pos);

public static class InterpolationFunctions
{
  public static InterpolationFunction Linear => x => x;
  public static InterpolationFunction Sawtooth => x => Abs((x + 1) % 2 - 1);
  public static InterpolationFunction Clamped => x => (x < 0) ? 0 : (x > 1) ? 1 : x;
  public static InterpolationFunction Squared => x => x * x;
  public static InterpolationFunction Sqrt => x => (float)Sqrt(Abs(x));
  public static InterpolationFunction Circle => x => (float)(1 - Sqrt(1 - x * x));
  public static InterpolationFunction Sin => x => (float)((Cos((x + 1) * PI) + 1) / 2);
  public static InterpolationFunction InvSin => x => (float)(1 - (Acos(Sawtooth(x) * 2 - 1) / PI));

  public static InterpolationFunction OfPower(float p) => x => (float)Pow(x, p);
  public static InterpolationFunction CircularPower(float p) => x => (float)(1 - Pow(1 - Pow(x, p), p));

  public static InterpolationFunction Flipped(InterpolationFunction func) => x => 1 - func(1 - x);

  public static InterpolationFunction Chain(params InterpolationFunction[] funcs) =>
    (pos) => funcs.Aggregate(pos, (x, f) => f(x));
}