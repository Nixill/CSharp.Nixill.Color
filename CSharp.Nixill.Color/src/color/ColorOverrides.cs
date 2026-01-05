using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Nixill.Internals;

namespace Nixill.Colors;

public readonly partial struct Color : IEquatable<Color>
{
  /// <summary>
  ///   Tests whether this Color equals another object.
  /// </summary>
  /// <remarks>
  ///   This Color equals another object if that object is also a Color
  ///   and both colors have exactly the same <see cref="Red"/>, <see cref="Green"/>,
  ///   <see cref="Blue"/>, and <see cref="InverseAlpha"/>.
  /// </remarks>
  /// <param name="obj">The other object to test.</param>
  /// <returns>
  ///   <see langword="true"/> iff this Color equals the other object (see
  ///   Remarks); <see cref="false"/> otherwise.
  /// </returns>
  public override bool Equals([NotNullWhen(true)] object? obj)
  {
    return obj is Color other && this.Equals(other);
  }

  /// <summary>
  ///   Tests whether this Color equals another Color.
  /// </summary>
  /// <remarks>
  ///   Two Colors are equal iff both colors have exactly the same <see cref="Red"/>,
  ///   <see cref="Green"/>, <see cref="Blue"/>, and <see cref="InverseAlpha"/>.
  /// </remarks>
  /// <param name="other">The other color to test.</param>
  /// <returns>
  ///   <see langword="true"/> iff the two Colors are equal (see Remarks),
  ///   <see langword="false"/> otherwise.
  /// </returns>
  public bool Equals(Color other)
  {
    return this.Red == other.Red && this.Green == other.Green
      && this.Blue == other.Blue && this.InverseAlpha == other.InverseAlpha;
  }

  /// <summary>
  ///   Tests whether this Color approximately equals another Color.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     Two Colors are approximately equal iff their <see cref="Red"/>,
  ///     <see cref="Green"/>, <see cref="Blue"/>, and <see cref="InverseAlpha"/>
  ///     are each within just less than 1/255 of each other.
  ///   </para>
  ///   <para>
  ///     Specifically, the difference between each component must be less
  ///     than <c>1 / 255.00006</c>.
  ///   </para>
  /// </remarks>
  /// <param name="other">The other Color to compare.</param>
  /// <returns>
  ///   <see langword="true"/> iff the two Colors are approximately equal
  ///   (see Remarks above); <see langword="false"/> otherwise.
  /// </returns>
  public bool ApproxEquals(Color other)
  {
    double factor = 1 / 255.00006;
    return Math.Abs(this.Red - other.Red) < factor
      && Math.Abs(this.Green - other.Green) < factor
      && Math.Abs(this.Blue - other.Blue) < factor
      && Math.Abs(this.InverseAlpha - other.InverseAlpha) < factor;
  }

  /// <summary>
  ///   Returns the hash code for this Color instance.
  /// </summary>
  /// <returns>The hash code.</returns>
  public override int GetHashCode()
  {
    return Red.GetHashCode() ^ Green.GetHashCode() ^ Blue.GetHashCode() ^ InverseAlpha.GetHashCode();
  }

  /// <summary>
  ///   Returns a string representation of this instance.
  /// </summary>
  /// <returns>The string representation.</returns>
  public override string ToString()
  {
    return $"{{ Red: {Red:f4}, Green: {Green:f4}, Blue: {Blue:f4}, Alpha: {Alpha:f4} }}";
  }
}