using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


class DotnetPresenter : BasePresenter, IPresenter
{
    private Graphics graphics;

    public static DotnetPresenter Create(Graphics graphics, Size size)
    {
        return new DotnetPresenter(graphics, size);
    }

    private DotnetPresenter(Graphics graphics, Size size) : base(size.Width, size.Height)
    {
        this.graphics = graphics;
    }

    public void DrawEarth()
    {
        graphics.FillEllipse(Brushes.Aqua,
                             DISPLAY_CENTER.X - EARTH_RADIUS + 1,
                             DISPLAY_CENTER.Y - EARTH_RADIUS + 1,
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
}
