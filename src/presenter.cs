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
    private const int DISPLAY_EARTH_RADIUS = 300;

    private readonly Point DISPLAY_CENTER;
    private readonly Size size;

    private Graphics graphics;

    public static Presenter Create(Graphics graphics, Size size)
    {
        return new Presenter(graphics, size);
    }

    private Presenter(Graphics graphics, Size size)
    {
        this.DISPLAY_CENTER = new Point(size.Width / 2, size.Height / 2);
        this.graphics = graphics;
        this.size     = size;
    }

    public void DrawEarth()
    {
        graphics.FillEllipse(Brushes.Aqua,
                             DISPLAY_CENTER.X - DISPLAY_EARTH_RADIUS + 1,
                             DISPLAY_CENTER.Y - DISPLAY_EARTH_RADIUS + 1,
                             2 * DISPLAY_EARTH_RADIUS,
                             2 * DISPLAY_EARTH_RADIUS);
    }

    public void Draw(IEnumerable<Tuple<Cartesian, Cartesian>> vectors)
    {
        var segments = vectors.Select(TransformToDisplayPoints);

        foreach (var pair in segments)
            DrawSegment(pair.Item1, pair.Item2);
    }

    public void DrawSun(double angle)
    {
        DrawPlanet(Brushes.Orange, angle);
    }

    public void DrawMoon(double angle)
    {
        DrawPlanet(Brushes.Gray, angle);
    }

    private void DrawPlanet(Brush brush, double angle)
    {
        var realPt = new Polar(angle, 450).ToCartesian();
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
        var realP1 = vector.Item1.Scale(DISPLAY_EARTH_RADIUS / Constants.Earth.MEAN_RADIUS);
        var realP2 = vector.Item2.Scale(50.0).Add(realP1);

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
