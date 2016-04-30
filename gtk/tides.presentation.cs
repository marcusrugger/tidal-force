using Cairo;
using System;
using System.Collections.Generic;
using System.Linq;


class DrawObject
{
    protected Cairo.Context context;
    protected DisplayParameters display;

    public DrawObject(Cairo.Context context, DisplayParameters display)
    {
        this.context = context;
        this.display = display;
    }
}


class DrawOrb : DrawObject
{
    Color color;

    public DrawOrb(Cairo.Context context, DisplayParameters display, Color color)
    : base(context, display)
    {
        this.color = color;
    }

    public void Draw(double angle)
    {
        var realPt = new Polar(angle, display.OrbShell).ToCartesian();

        context.LineWidth = 1.0;
        context.SetSourceColor(color);
        context.Arc( display.ToDisplayX(realPt.x), display.ToDisplayY(realPt.y), 10, 0, 2 * Math.PI );
        context.Fill();
    }
}


class DrawSegment : DrawObject
{
    public DrawSegment(Cairo.Context context, DisplayParameters display)
    : base(context, display)
    {}
    
    public void Draw(Cartesian p1, Cartesian p2)
    {
        Draw( ToDisplayPoint(p1), ToDisplayPoint(p2) );
    }

    private void Draw(PointD p1, PointD p2)
    {
        context.LineWidth = 1.0;
        context.SetSourceRGB(0.0, 0.0, 0.0);

        context.MoveTo(p1);
        context.LineTo(p2);

        context.Stroke();

        context.LineWidth = 1.0;
        context.SetSourceRGB(0.0, 0.0, 128.0);
        context.Arc(p1.X, p1.Y, 2, 0, 2*Math.PI);

        context.Stroke();

        context.LineWidth = 1.0;
        context.SetSourceRGB(128.0, 0.0, 0.0);
        context.Arc(p2.X, p2.Y, 2, 0, 2*Math.PI);

        context.Stroke();
    }

    private PointD ToDisplayPoint(Cartesian realPt)
    {
        return new PointD( display.ToDisplayX(realPt.x), display.ToDisplayY(realPt.y) );
    }
}


class DrawEarth : DrawObject
{
    public DrawEarth(Cairo.Context context, DisplayParameters display)
    : base(context, display)
    {}

    public void Draw()
    {
        context.LineWidth = 1.0;
        context.Arc(display.DisplayCenterX, display.DisplayCenterY, display.EarthRadius, 0, 2*Math.PI);
        context.SetSourceRGB(0.0, 1.0, 1.0);
        context.Fill();
    }
}


class VectorTransformer
{
    DisplayParameters display;

    public VectorTransformer(DisplayParameters display)
    {
        this.display = display;
    }

    public Tuple<Cartesian, Cartesian> ToDisplayScale(Tuple<Cartesian, Cartesian> vector)
    {
        var realP1 = vector.Item1.Scale(display.EarthRadius / Constants.Earth.MEAN_RADIUS);
        var realP2 = vector.Item2.Scale(display.VectorScale).Add(realP1);

        return Tuple.Create(realP1, realP2);
    }
}


class TidesPresenter : ITidesPresenter
{
    DrawOrb orbMoon;
    DrawOrb orbSun;
    DrawSegment segment;
    DrawEarth earth;
    
    VectorTransformer transformVector;


    public static TidesPresenter Create(Cairo.Context context, DisplayParameters display)
    {
        return new TidesPresenter(context, display);
    }

    private TidesPresenter(Cairo.Context context, DisplayParameters display)
    {
        orbMoon = new DrawOrb(context, display, new Color(0.5, 0.5, 0.5));
        orbSun  = new DrawOrb(context, display, new Color(1.0, 0.6, 0.1));
        segment = new DrawSegment(context, display);
        earth   = new DrawEarth(context, display);
        
        transformVector = new VectorTransformer(display);
    }

    public void DrawEarth()
    {
        earth.Draw();
    }

    public void Draw(IEnumerable<Tuple<Cartesian, Cartesian>> vectorList)
    {
        foreach (var pair in vectorList.Select(transformVector.ToDisplayScale))
            segment.Draw(pair.Item1, pair.Item2);
    }

    public void DrawSun(double angle)
    {
        orbSun.Draw(angle);
    }

    public void DrawMoon(double angle)
    {
        orbMoon.Draw(angle);
    }
}
