using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


class TidesPresenter : ITidesPresenter
{
    private Graphics graphics;
    private DisplayParameters display;

    public static TidesPresenter Create(Graphics graphics, DisplayParameters display)
    {
        return new TidesPresenter(graphics, display);
    }

    private TidesPresenter(Graphics graphics, DisplayParameters display)
    {
        this.graphics = graphics;
        this.display  = display;
    }

    public void DrawEarth()
    {
        graphics.FillEllipse(Brushes.Aqua,
                             display.DisplayCenterX - display.EarthRadius + 1,
                             display.DisplayCenterY - display.EarthRadius + 1,
                             2 * display.EarthRadius,
                             2 * display.EarthRadius);
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
        var realPt = new Polar(angle, display.OrbShell).ToCartesian();
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
        var realP1 = vector.Item1.Scale(display.EarthRadius / Constants.Earth.MEAN_RADIUS);
        var realP2 = vector.Item2.Scale(display.VectorScale).Add(realP1);

        return Tuple.Create(ToDisplayPoint(realP1), ToDisplayPoint(realP2));
    }

    private Point ToDisplayPoint(Cartesian realPt)
    {
        var displayPt = realPt.ToPoint();
        displayPt.Offset(display.DisplayCenterX, display.DisplayCenterY);
        displayPt.Y = display.Height - displayPt.Y;
        return displayPt;
    }
}
