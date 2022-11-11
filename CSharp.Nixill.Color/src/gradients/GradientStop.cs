namespace Nixill.Colors;

public struct GradientStop
{
  public Color StopColor;
  public BlendingFunction Function;

  public static implicit operator GradientStop((Color sc, BlendingFunction func) input) => new GradientStop()
  {
    StopColor = input.sc,
    Function = input.func
  };

  public static implicit operator (Color sc, BlendingFunction func)(GradientStop input) => (
    input.StopColor,
    input.Function
  );
}