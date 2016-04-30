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

        context.SetSourceRGB(0.1, 0.4, 0.6);
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
        var realPt = new Polar(angle, ORB_SHELL).ToCartesian();

        context.LineWidth = 1.0;
        context.SetSourceRGB(0.0, 0.0, 0.0);
        context.Arc( ToDisplayX(realPt.x), ToDisplayY(realPt.y), 10, 0, 2 * Math.PI );

        context.SetSourceRGB(red, green, blue);
        context.Fill();
    }
    
    private int ToDisplayX(double x)
    {
        return DISPLAY_CENTER_X + (int) (x + 0.5);
    }
    
    private int ToDisplayY(double y)
    {
        return height - (DISPLAY_CENTER_Y + (int) (y + 0.5));
    }

    private IEnumerable<Tuple<Point, Point>>
    ToDisplayVectors(IEnumerable<Tuple<Cartesian, Cartesian>> vectors)
    {
        return vectors.Select(ToDisplayPoints);
    }

    private Tuple<Point, Point> ToDisplayPoints(Tuple<Cartesian, Cartesian> vector)
    {
        var realP1 = vector.Item1.Scale(EARTH_RADIUS / Constants.Earth.MEAN_RADIUS);
        var realP2 = vector.Item2.Scale(VECTOR_SCALE).Add(realP1);

        return Tuple.Create(ToDisplayPoint(realP1), ToDisplayPoint(realP2));
    }

    private Point ToDisplayPoint(Cartesian realPt)
    {
        return new Point(ToDisplayX(realPt.x), ToDisplayY(realPt.y));
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
