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
    }

    public void DrawMoon(double angle)
    {
    }
}
