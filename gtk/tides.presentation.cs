using Cairo;
using System;
using System.Collections.Generic;
using System.Linq;


class Point
{
    public int x;
    public int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}


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

    public void Draw(Point p1, Point p2)
    {
        context.LineWidth = 1.0;
        context.SetSourceRGB(0.0, 0.0, 0.0);

        context.MoveTo(p1.x, p1.y);
        context.LineTo(p2.x, p2.y);

        context.Stroke();

        context.LineWidth = 1.0;
        context.SetSourceRGB(0.0, 0.0, 128.0);
        context.Arc(p1.x, p1.y, 2, 0, 2*Math.PI);

        context.Stroke();

        context.LineWidth = 1.0;
        context.SetSourceRGB(128.0, 0.0, 0.0);
        context.Arc(p2.x, p2.y, 2, 0, 2*Math.PI);

        context.Stroke();
    }
}


class DrawVectors : DrawObject
{
    DrawSegment segment;

    public DrawVectors(Cairo.Context context, DisplayParameters display)
    : base(context, display)
    {
        segment = new DrawSegment(context, display);
    }

    public void Draw(IEnumerable<Tuple<Cartesian, Cartesian>> vectors)
    {
        foreach (var pair in ToDisplayVectors(vectors))
            segment.Draw(pair.Item1, pair.Item2);
    }

    private IEnumerable<Tuple<Point, Point>>
    ToDisplayVectors(IEnumerable<Tuple<Cartesian, Cartesian>> vectors)
    {
        return vectors.Select(ToDisplayPoints);
    }

    private Tuple<Point, Point> ToDisplayPoints(Tuple<Cartesian, Cartesian> vector)
    {
        var realP1 = vector.Item1.Scale(display.EarthRadius / Constants.Earth.MEAN_RADIUS);
        var realP2 = vector.Item2.Scale(display.VectorScale).Add(realP1);

        return Tuple.Create(ToDisplayPoint(realP1), ToDisplayPoint(realP2));
    }

    protected Point ToDisplayPoint(Cartesian realPt)
    {
        return new Point(display.ToDisplayX(realPt.x), display.ToDisplayY(realPt.y));
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
        context.SetSourceRGB(0.0, 0.0, 0.0);

        context.Arc(display.DisplayCenterX, display.DisplayCenterY, display.EarthRadius, 0, 2*Math.PI);

        context.SetSourceRGB(0.0, 1.0, 1.0);
        context.Fill();
    }
}


class TidesPresenter : ITidesPresenter
{
    DrawOrb orbMoon;
    DrawOrb orbSun;
    DrawVectors vectors;
    DrawEarth earth;

    public static TidesPresenter Create(Cairo.Context context, DisplayParameters display)
    {
        return new TidesPresenter(context, display);
    }

    private TidesPresenter(Cairo.Context context, DisplayParameters display)
    {
        orbMoon = new DrawOrb(context, display, new Color(0.5, 0.5, 0.5));
        orbSun  = new DrawOrb(context, display, new Color(1.0, 0.6, 0.1));
        vectors = new DrawVectors(context, display);
        earth   = new DrawEarth(context, display);
    }

    public void DrawEarth()
    {
        earth.Draw();
    }

    public void Draw(IEnumerable<Tuple<Cartesian, Cartesian>> vectorList)
    {
        vectors.Draw(vectorList);
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
