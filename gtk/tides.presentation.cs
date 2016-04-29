using Cairo;
using System;
using System.Collections.Generic;
using System.Linq;


class TidesPresenter : TidesBasePresenter, ITidesPresenter
{
    Cairo.Context context;

    public static TidesPresenter Create(Cairo.Context context)
    {
        return new TidesPresenter(context);
    }

    private TidesPresenter(Cairo.Context context) : base(1000, 1000)
    {
        this.context = context;
    }

    public void DrawEarth()
    {
        context.LineWidth = 1.0;
        context.SetSourceRGB(0.0, 0.0, 0.0);

        context.Arc(DISPLAY_CENTER_X, DISPLAY_CENTER_Y, EARTH_RADIUS, 0, 2*Math.PI);
        context.StrokePreserve();

        context.SetSourceRGB(0.1, 0.4, 0.6);
        context.Fill();
    }

    public void Draw(IEnumerable<Tuple<Cartesian, Cartesian>> vectors)
    {
    }

    public void DrawSun(double angle)
    {
        DrawOrb(angle, 1.0, 0.6, 0.1);
    }

    public void DrawMoon(double angle)
    {
        DrawOrb(angle, 0.5, 0.5, 0.5);
    }

    private void DrawOrb(double angle, double red, double green, double blue)
    {
        var realPt = new Polar(angle, ORB_SHELL).ToCartesian();

        context.LineWidth = 1.0;
        context.SetSourceRGB(0.0, 0.0, 0.0);
        
        context.Arc(ScaleX(realPt.x), ScaleY(realPt.y), 10, 0, 2*Math.PI);
        context.StrokePreserve();

        context.SetSourceRGB(red, green, blue);
        context.Fill();
    }
    
    private int ScaleX(double x)
    {
        return DISPLAY_CENTER_X + (int) (x + 0.5);
    }
    
    private int ScaleY(double y)
    {
        return height - (DISPLAY_CENTER_Y + (int) (y + 0.5));
    }
}
