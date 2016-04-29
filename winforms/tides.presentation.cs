using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


class TidesPresenter : TidesBasePresenter, ITidesPresenter
{
    private Graphics graphics;

    public static TidesPresenter Create(Graphics graphics, Size size)
    {
        return new TidesPresenter(graphics, size);
    }

    private TidesPresenter(Graphics graphics, Size size) : base(size.Width, size.Height)
    {
        this.graphics = graphics;
    }

    public void DrawEarth()
    {
        graphics.FillEllipse(Brushes.Aqua,
                             DISPLAY_CENTER_X - EARTH_RADIUS + 1,
                             DISPLAY_CENTER_Y - EARTH_RADIUS + 1,
                             2 * EARTH_RADIUS,
                             2 * EARTH_RADIUS);
    }

    public void Draw(IEnumerable<Tuple<Cartesian, Cartesian>> vectors)
    {
        foreach (var pair in ToDisplayVectors(vectors))
            DrawSegment(pair.Item1, pair.Item2);
    }

    public void DrawSun(double angle)
    {
        DrawOrb(Brushes.Orange, angle);
    }

    public void DrawMoon(double angle)
    {
        DrawOrb(Brushes.Gray, angle);
    }

    private void DrawOrb(Brush brush, double angle)
    {
        var realPt = new Polar(angle, ORB_SHELL).ToCartesian();
        var pt     = ToDisplayPoint(realPt);
        graphics.FillEllipse(brush, pt.X - 10, pt.Y - 10, 21, 21);
    }

    private void DrawSegment(Point p1, Point p2)
    {
        graphics.DrawLine(Pens.Red, p1, p2);
        graphics.FillEllipse(Brushes.Green, p1.X - 2, p1.Y - 2, 5, 5);
        graphics.FillEllipse(Brushes.Blue , p2.X - 2, p2.Y - 2, 5, 5);
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
        var displayPt = realPt.ToPoint();
        displayPt.Offset(DISPLAY_CENTER_X, DISPLAY_CENTER_Y);
        displayPt.Y = height - displayPt.Y;
        return displayPt;
    }
}
