using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


class Presenter
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

    public void DrawSegment(Cartesian realP1, Cartesian realP2)
    {
        var pt1 = TransformToDisplayPoint(realP1);
        var pt2 = TransformToDisplayPoint(realP2);
        DrawSegment(pt1, pt2);
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