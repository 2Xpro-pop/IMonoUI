namespace IMonoUI;

public interface IRendererContext
{
    public IServiceProvider Services
    {
        get;
    }

    public IDrawingContext DrawingContext
    {
        get;
    }
}
