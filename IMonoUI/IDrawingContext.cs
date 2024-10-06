using IMonoUI.Primitives;
using Splat;

namespace IMonoUI;

public interface IDrawingContext
{
    public void DrawLine(MonoPoint a, MonoPoint b, SplatColor color, float thickness = 1.0f);
    public void DrawRectangle(RectangleF rect, SplatColor color, float thickness = 1.0f);
    public void FillRectangle(RectangleF rect, SplatColor color);
    public void DrawEllipse(RectangleF rect, SplatColor color, float thickness = 1.0f);
}