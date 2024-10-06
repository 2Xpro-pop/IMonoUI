using Splat;
using System.Drawing;

namespace IMonoUI;

public interface IDrawingContext
{
    public void DrawLine(PointF a, PointF b, SplatColor color, float thickness = 1.0f);
    public void DrawRectangle(RectangleF rect, SplatColor color, float thickness = 1.0f);
    public void FillRectangle(RectangleF rect, SplatColor color);
    public void DrawEllipse(RectangleF rect, SplatColor color, float thickness = 1.0f);
}