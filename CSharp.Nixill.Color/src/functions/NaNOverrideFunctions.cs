using Nixill.Colors;

public delegate float NaNOverride(Color near, Color far);

public static class NaNOverrides
{
  public static NaNOverride RedHue => (near, far) => 0f;
  public static NaNOverride YellowHue => (near, far) => 60f;
  public static NaNOverride GreenHue => (near, far) => 120f;
  public static NaNOverride CyanHue => (near, far) => 180f;
  public static NaNOverride BlueHue => (near, far) => 240f;
  public static NaNOverride MagentaHue => (near, far) => 300f;

  public static NaNOverride Zero => (near, far) => 0f;
  public static NaNOverride One => (near, far) => 1f;
  public static NaNOverride Half => (near, far) => 0.5f;

  public static NaNOverride Exactly(float val) => (near, far) => val;

  public static NaNOverride EqualHue(float backup) => (near, far) => (float.IsNaN(far.Hue)) ? backup : far.Hue;
  public static NaNOverride OppositeHue(float backup) => (near, far) => (float.IsNaN(far.Hue)) ? backup : (far.Hue + 180) % 360;

  public static NaNOverride EqualVSat(float backup) => (near, far) => (float.IsNaN(far.VSaturation)) ? backup : far.VSaturation;
  public static NaNOverride EqualLSat(float backup) => (near, far) => (float.IsNaN(far.LSaturation)) ? backup : far.LSaturation;
}