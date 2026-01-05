namespace Nixill.Colors;

public readonly partial struct Color
{
  byte[] GetBytes(int redIndex, int greenIndex, int blueIndex, int alphaIndex)
  {
    byte[] output = [0, 0, 0, 0];
    output[redIndex] = ByteRed;
    output[greenIndex] = ByteGreen;
    output[blueIndex] = ByteBlue;
    if (alphaIndex >= 0) output[alphaIndex] = ByteAlpha;

    if (BitConverter.IsLittleEndian) Array.Reverse(output);

    return output;
  }

  int GetIntFromBytes(int redIndex, int greenIndex, int blueIndex, int alphaIndex)
    => BitConverter.ToInt32(GetBytes(redIndex, greenIndex, blueIndex, alphaIndex));

  uint GetUIntFromBytes(int redIndex, int greenIndex, int blueIndex, int alphaIndex)
    => BitConverter.ToUInt32(GetBytes(redIndex, greenIndex, blueIndex, alphaIndex));

  /// <summary>
  ///   Returns this Color as a signed 32-bit integer, such that the
  ///   bytes (from most to least significant) represent red, green, blue,
  ///   and alpha, each on a scale from 0 to 255, respectively.
  /// </summary>
  /// <returns>As above.</returns>
  public int ToIntRGBA() => GetIntFromBytes(0, 1, 2, 3);

  /// <summary>
  ///   Returns this Color as an unsigned 32-bit integer, such that the
  ///   bytes (from most to least significant) represent red, green, blue,
  ///   and alpha, each on a scale from 0 to 255, respectively.
  /// </summary>
  /// <returns>As above.</returns>
  public uint ToUIntRGBA() => GetUIntFromBytes(0, 1, 2, 3);

  /// <summary>
  ///   Returns this Color as a signed 32-bit integer, such that the
  ///   bytes (from most to least significant) represent nothing, red,
  ///   green, and blue, each on a scale from 0 to 255, respectively.
  /// </summary>
  /// <returns>As above.</returns>
  public int ToIntRGB() => GetIntFromBytes(1, 2, 3, -1);

  /// <summary>
  ///   Returns this Color as an unsigned 32-bit integer, such that the
  ///   bytes (from most to least significant) represent nothing, red,
  ///   green, and blue, each on a scale from 0 to 255, respectively.
  /// </summary>
  /// <returns>As above.</returns>
  public uint ToUIntRGB() => GetUIntFromBytes(1, 2, 3, -1);

  /// <summary>
  ///   Returns this Color as a signed 32-bit integer, such that the
  ///   bytes (from most to least significant) represent alpha, red,
  ///   green, and blue, each on a scale from 0 to 255, respectively.
  /// </summary>
  /// <returns>As above.</returns>
  public int ToIntARGB() => GetIntFromBytes(1, 2, 3, 0);

  /// <summary>
  ///   Returns this Color as an unsigned 32-bit integer, such that the
  ///   bytes (from most to least significant) represent alpha, red,
  ///   green, and blue, each on a scale from 0 to 255, respectively.
  /// </summary>
  /// <returns>As above.</returns>
  public uint ToUIntARGB() => GetUIntFromBytes(1, 2, 3, 0);

  /// <summary>
  ///   Returns this Color as a signed 32-bit integer, such that the
  ///   bytes (from most to least significant) represent blue, green, red,
  ///   and alpha, each on a scale from 0 to 255, respectively.
  /// </summary>
  /// <returns>As above.</returns>
  public int ToIntBGRA() => GetIntFromBytes(2, 1, 0, 3);

  /// <summary>
  ///   Returns this Color as an unsigned 32-bit integer, such that the
  ///   bytes (from most to least significant) represent blue, green, red,
  ///   and alpha, each on a scale from 0 to 255, respectively.
  /// </summary>
  /// <returns>As above.</returns>
  public uint ToUIntBGRA() => GetUIntFromBytes(2, 1, 0, 3);

  /// <summary>
  ///   Returns this Color as a signed 32-bit integer, such that the
  ///   bytes (from most to least significant) represent nothing, blue,
  ///   green, and red, each on a scale from 0 to 255, respectively.
  /// </summary>
  /// <returns>As above.</returns>
  public int ToIntBGR() => GetIntFromBytes(3, 2, 1, -1);

  /// <summary>
  ///   Returns this Color as an unsigned 32-bit integer, such that the
  ///   bytes (from most to least significant) represent nothing, blue,
  ///   green, and red, each on a scale from 0 to 255, respectively.
  /// </summary>
  /// <returns>As above.</returns>
  public uint ToUIntBGR() => GetUIntFromBytes(3, 2, 1, -1);

  /// <summary>
  ///   Returns this Color as a signed 32-bit integer, such that the
  ///   bytes (from most to least significant) represent alpha, blue,
  ///   green, and red, each on a scale from 0 to 255, respectively.
  /// </summary>
  /// <returns>As above.</returns>
  public int ToIntABGR() => GetIntFromBytes(3, 2, 1, 0);

  /// <summary>
  ///   Returns this Color as an unsigned 32-bit integer, such that the
  ///   bytes (from most to least significant) represent alpha, blue,
  ///   green, and red, each on a scale from 0 to 255, respectively.
  /// </summary>
  /// <returns>As above.</returns>
  public uint ToUIntABGR() => GetUIntFromBytes(3, 2, 1, 0);

  /// <summary>
  ///   Returns this Color as a hexcode string, six characters long, such
  ///   that the bytes (from left to right) represent red, green, and
  ///   blue, each on a scale from 00 to FF, respectively.
  /// </summary>
  /// <param name="condense">
  ///   If <see langword="true"/> and each byte is composed of the same
  ///   two hex digits, the return value should be condensed to three
  ///   digits instead of six.
  /// </param>
  /// <param name="prefix">
  ///   The prefix to use at the start of the color output. <c>#</c>,
  ///   <c>$</c>, <c>0x</c>, and <c>0X</c> can be round-tripped into
  ///   <see cref="FromHex(string, bool)"/>, but any prefix is allowed.
  /// </param>
  /// <returns>As above.</returns>
  public string ToHexRGB(bool condense = false, string prefix = "")
  {
    string h = $"{ToUIntRGB():x6}";

    if (condense && h[0] == h[1] && h[2] == h[3] && h[4] == h[5])
      return $"{prefix}{h[0]}{h[2]}{h[4]}";
    else return $"{prefix}{h}";
  }

  /// <summary>
  ///   Returns this Color as a hexcode string, eight characters long,
  ///   such that the bytes (from left to right) represent alpha, red,
  ///   green, and blue, each on a scale from 00 to FF, respectively.
  /// </summary>
  /// <param name="condense">
  ///   If <see langword="true"/> and each byte is composed of the same
  ///   two hex digits, the return value should be condensed to four
  ///   digits instead of eight.
  /// </param>
  /// <param name="prefix">
  ///   The prefix to use at the start of the color output. <c>#</c>,
  ///   <c>$</c>, <c>0x</c>, and <c>0X</c> can be round-tripped into
  ///   <see cref="FromHex(string, bool)"/>, but any prefix is allowed.
  /// </param>
  /// <returns>As above.</returns>
  public string ToHexARGB(bool condense = false, string prefix = "")
  {
    string h = $"{ToUIntARGB():x8}";

    if (condense && h[0] == h[1] && h[2] == h[3] && h[4] == h[5] && h[6] == h[7])
      return $"{prefix}{h[0]}{h[2]}{h[4]}{h[6]}";
    else return $"{prefix}{h}";
  }

  /// <summary>
  ///   Returns this Color as a hexcode string, eight characters long,
  ///   such that the bytes (from left to right) represent red, green,
  ///   blue, and alpha, each on a scale from 00 to FF, respectively.
  /// </summary>
  /// <param name="condense">
  ///   If <see langword="true"/> and each byte is composed of the same
  ///   two hex digits, the return value should be condensed to four
  ///   digits instead of eight.
  /// </param>
  /// <param name="prefix">
  ///   The prefix to use at the start of the color output. <c>#</c>,
  ///   <c>$</c>, <c>0x</c>, and <c>0X</c> can be round-tripped into
  ///   <see cref="FromHex(string, bool)"/>, but any prefix is allowed.
  /// </param>
  /// <returns>As above.</returns>
  public string ToHexRGBA(bool condense = false, string prefix = "")
  {
    string h = $"{ToUIntRGBA():x8}";

    if (condense && h[0] == h[1] && h[2] == h[3] && h[4] == h[5] && h[6] == h[7])
      return $"{prefix}{h[0]}{h[2]}{h[4]}{h[6]}";
    else return $"{prefix}{h}";
  }
}