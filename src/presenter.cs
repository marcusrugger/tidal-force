using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


interface IPresenter
{
    void Draw(IEnumerable<Tuple<Cartesian, Cartesian>> vectors);
    void DrawSun(double angle);
    void DrawMoon(double angle);
}


class Presenter : IPresenter
{
    private readonly int EARTH_RADIUS;
    private readonly Point DISPLAY_CENTER;
    private readonly double VECTOR_SCALE;
    private readonly int ORB_SHELL;
    private readonly Size size;

    private Graphics graphics;

    public static Presenter Create(Graphics graphics, Size size)
    {
        return new Presenter(graphics, size);
    }

    private Presenter(Graphics graphics, Size size)
    {
        int short_dimension = Math.Min(size.Width, size.Height);
        this.DISPLAY_CENTER = new Point(size.Width / 2, size.Height / 2);
        this.EARTH_RADIUS = 3 * short_dimension / 10;
        this.VECTOR_SCALE = 0.05 * (double) short_dimension;
        this.ORB_SHELL = 45 * short_dimension / 100;
        this.graphics = graphics;
        this.size     = size;
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
        var segments = vectors.Select(TransformToDisplayPoints);

        foreach (var pair in segments)
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
        var pt     = TransformToDisplayPoint(realPt);
        graphics.FillEllipse(brush, pt.X - 10, pt.Y - 10, 21, 21);
    }

    private void DrawSegment(Point p1, Point p2)
    {
        graphics.DrawLine(Pens.Red, p1, p2);
        graphics.FillEllipse(Brushes.Green, p1.X - 2, p1.Y - 2, 5, 5);
        graphics.FillEllipse(Brushes.Blue , p2.X - 2, p2.Y - 2, 5, 5);
    }

    private Tuple<Point, Point> TransformToDisplayPoints(Tuple<Cartesian, Cartesian> vector)
    {
        var realP1 = vector.Item1.Scale(EARTH_RADIUS / Constants.Earth.MEAN_RADIUS);
        var realP2 = vector.Item2.Scale(VECTOR_SCALE).Add(realP1);

        return Tuple.Create(TransformToDisplayPoint(realP1), TransformToDisplayPoint(realP2));
    }

    private Point TransformToDisplayPoint(Cartesian realPt)
    {
        var displayPt = realPt.ToPoint();
        displayPt.Offset(DISPLAY_CENTER);
        displayPt.Y = size.Height - displayPt.Y;
        return displayPt;
    }

}
