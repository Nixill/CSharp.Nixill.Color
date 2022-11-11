using Nixill.Collections;
using static Nixill.Utils.Interpolation;

namespace Nixill.Colors;

public class Gradient : AVLTreeDictionary<float, GradientStop>
{
  public BlendingFunction LeftBlending { get; private set; }

  #region Constructors
  public Gradient(BlendingFunction func) : base()
  {
    LeftBlending = func;
  }

  public Gradient(BlendingFunction func, GradientStop stop) : this(func)
  {
    this[0] = stop;
  }

  public Gradient(BlendingFunction func, GradientStop left, GradientStop right) : this(func)
  {
    this[0] = left;
    this[1] = right;
  }

  public Gradient(BlendingFunction func, IEnumerable<GradientStop> stops) : this(func)
  {
    // Ensure single enumeration
    GradientStop[] array = stops.ToArray();

    // Single color handling to prevent NaN fun
    if (array.Length == 1)
    {
      this[0] = array[0];
      return;
    }

    // Otherwise:
    foreach ((float pos, GradientStop stop) entry in array.Select((c, i) => ((float)i, c)))
    {
      float pos = entry.pos / (array.Length - 1);
      this[pos] = entry.stop;
    }
  }

  public Gradient(BlendingFunction func, IEnumerable<(float, GradientStop)> stops) : this(func)
  {
    foreach ((float pos, GradientStop stop) entry in stops)
    {
      this[entry.pos] = entry.stop;
    }
  }

  public Gradient(GradientStop stop) : this(stop.Function, stop) { }
  public Gradient(BlendingFunction func, Color color) : this(func, (color, func)) { }
  public Gradient(BlendingFunction func, Color left, Color right) : this(func, (left, func), (right, func)) { }
  public Gradient(BlendingFunction func, IEnumerable<Color> colors) : this(func, colors.Select(c => (GradientStop)(c, func))) { }
  public Gradient(BlendingFunction func, IEnumerable<(float p, Color c)> colors) : this(func, colors.Select(e => (e.p, (GradientStop)(e.c, func)))) { }
  public Gradient(Gradient existing) : this(existing.LeftBlending, existing.Select(k => (k.Key, k.Value))) { }
  #endregion

  public Color ColorAt(float pos)
  {
    // No keys: Error
    if (Count == 0) throw new InvalidOperationException("This gradient contains no stops.");

    // One key: Just return that color
    if (Count == 1) return LowestEntry().Value.StopColor;

    // Exactly matches a key: Return that color
    if (ContainsKey(pos)) return this[pos].StopColor;

    // Ugh fine we have to actually do math
    KeyValuePair<float, GradientStop> leftEntry;
    KeyValuePair<float, GradientStop> rightEntry;
    BlendingFunction blend;

    // First we have to figure out if we're below, between, or above keys
    if (ContainsLower(pos))
    {
      if (ContainsHigher(pos))
      {
        leftEntry = LowerEntry(pos);
        rightEntry = HigherEntry(pos);
        blend = leftEntry.Value.Function;
      }
      else
      {
        rightEntry = HighestEntry();
        leftEntry = LowerEntry(rightEntry.Key);
        blend = rightEntry.Value.Function;
      }
    }
    else
    {
      leftEntry = LowestEntry();
      rightEntry = HigherEntry(leftEntry.Key);
      blend = LeftBlending;
    }

    // Now get the colors and position
    Color left = leftEntry.Value.StopColor;
    Color right = rightEntry.Value.StopColor;
    float inv = InvLerp(leftEntry.Key, rightEntry.Key, pos);

    // And finally blend them
    return blend(left, right, inv);
  }

  public IEnumerable<Color> EvenlySpacedColors(int count)
  {
    if (Count == 0) throw new InvalidOperationException("This Gradient contains no stops.");
    if (Count == 1) return Enumerable.Repeat(LowestEntry().Value.StopColor, count);

    float low = LowestKey();
    float high = HighestKey();
    return Enumerable.Range(0, count)
      .Select(x => Lerp(0, low, count - 1, high, x))
      .Select(pos => ColorAt(pos));
  }

  public IEnumerable<Color> EvenlySpacedPaddedColors(int count, float padding)
  {
    if (Count == 0) throw new InvalidOperationException("This Gradient contains no stops.");
    if (Count == 1) return Enumerable.Repeat(LowestEntry().Value.StopColor, count);

    float low = LowestKey();
    float high = HighestKey();
    (low, high) = Decompress(low, high, padding);
    return Enumerable.Range(0, count)
      .Select(x => Lerp(0, low, count - 1, high, x))
      .Select(pos => ColorAt(pos));
  }
}