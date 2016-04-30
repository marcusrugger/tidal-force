using Cairo;
using System;
using System.Collections.Generic;
using System.Linq;


class TidesPresenter : ITidesPresenter
{
    Cairo.Context context;
    DisplayParameters display;

    public static TidesPresenter Create(Cairo.Context context, DisplayParameters display)
    {
        return new TidesPresenter(context, display);
    }

    private TidesPresenter(Cairo.Context context, DisplayParameters display)
    {
        this.context = context;
        this.display = display;
    }

    public void DrawEarth()
    {
        context.LineWidth = 1.0;
        context.SetSourceRGB(0.0, 0.0, 0.0);

        context.Arc(display.DisplayCenterX, display.DisplayCenterY, display.EarthRadius, 0, 2*Math.PI);

        context.SetSourceRGB(0.0, 1.0, 1.0);
        context.Fill();
    }

    public void Draw(IEnumerable<Tuple<Cartesian, Cartesian>> vectors)
    {
        foreach (var pair in ToDisplayVectors(vectors))
            DrawSegment(pair.Item1, pair.Item2);
    }

    public void DrawSun(double angle)
    {
        DrawOrb(angle, 1.0, 0.6, 0.1);
    }

    public void DrawMoon(double angle)
    {
        DrawOrb(angle, 0.5, 0.5, 0.5);
    }

    private void DrawSegment(Point p1, Point p2)
    {
        context.SetSourceRGB(0.0, 128.0, 0.0);
        context.Arc(p1.x, p1.y, 2, 0, 2*Math.PI);

        context.LineWidth = 1.0;
        context.SetSourceRGB(0.0, 0.0, 0.0);

        context.MoveTo(p1.x, p1.y);
        context.LineTo(p2.x, p2.y);

        context.Stroke();

        context.SetSourceRGB(128.0, 0.0, 128.0);
        context.Arc(p2.x, p2.y, 2, 0, 2*Math.PI);

        context.Stroke();
    }

    private void DrawOrb(double angle, double red, double green, double blue)
    {
        var realPt = new Polar(angle, display.OrbShell).ToCartesian();

        context.LineWidth = 1.0;
        context.SetSourceRGB(0.0, 0.0, 0.0);
        context.Arc( display.ToDisplayX(realPt.x), display.ToDisplayY(realPt.y), 10, 0, 2 * Math.PI );

        context.SetSourceRGB(red, green, blue);
        context.Fill();
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

    private Point ToDisplayPoint(Cartesian realPt)
    {
        return new Point(display.ToDisplayX(realPt.x), display.ToDisplayY(realPt.y));
    }

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
}
