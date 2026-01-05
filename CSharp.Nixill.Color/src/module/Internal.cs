using System.Numerics;

namespace Nixill.Internals;

internal static class Internal
{
  public static double Lerp(double x, double yAtX0, double yAtX1)
    => x * (yAtX1 - yAtX0) + yAtX0;

  public static double InvLerp(double y, double yAtX0, double yAtX1)
    => (y - yAtX0) / (yAtX1 - yAtX0);

  /// <summary>
  ///   Inv-lerps the number in the middle of the array from the numbers
  ///   on the outsides.
  /// </summary>
  /// <param name="args">
  ///   The array, which is treated as if it's sorted (lower bound on [0],
  ///   value to invlerp on [1], and upper bound on [2]).
  /// </param>
  /// <returns>
  ///   The percentage from [0] to [2] at which [1] is located.
  /// </returns>
  public static double InvLerp(double[] args)
    => (args[1] - args[0]) / (args[2] - args[0]);

  public static double ReLerp(double y1, double y1AtX0, double y1AtX1, double y2AtX0, double y2AtX1)
    => Lerp(InvLerp(y1, y1AtX0, y1AtX1), y2AtX0, y2AtX1);

  /// <summary>
  ///   Returns the non-negative modulus of division of <c>n</c> by <c>d</c>.
  /// </summary>
  /// <param name="n">The numerator, or dividend.</param>
  /// <param name="d">The denominator, or divisor.</param>
  /// <returns>The non-negative modulus.</returns>
  public static T NNMod<T>(T n, T d) where T : IModulusOperators<T, T, T>, IAdditionOperators<T, T, T>,
    IComparable<T>, IAdditiveIdentity<T, T>
    => (n %= d).CompareTo(T.AdditiveIdentity) < 0 ? n + d : n;
}