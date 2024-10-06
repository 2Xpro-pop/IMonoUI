using IMonoUI.Primitives;
using System.Drawing;

namespace IMonoUI;

// Prefer does not use
public interface IDrawingContext
{
    public void DrawLine(IMonoPen pen, MonoPoint a, MonoPoint b);
    public void DrawRectangle(IMonoBrush? brush, IMonoPen? pen, MonoRect rect, double radiusX = 0, double radiusY = 0);
    public void DrawEllipse(IMonoBrush? brush, IMonoPen? pen, MonoPoint center, double radiusX, double radiusY);
}