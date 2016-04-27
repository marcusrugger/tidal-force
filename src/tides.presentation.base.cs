using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


interface ITidesPresenter
{
    void DrawEarth();
    void Draw(IEnumerable<Tuple<Cartesian, Cartesian>> vectors);
    void DrawSun(double angle);
    void DrawMoon(double angle);
}


class TidesBasePresenter
{
    protected readonly int EARTH_RADIUS;
    protected readonly Point DISPLAY_CENTER;
    protected readonly double VECTOR_SCALE;
    protected readonly int ORB_SHELL;
    protected readonly int width;
    protected readonly int height;

    public TidesBasePresenter(int width, int height)
    {
        int short_dimension = Math.Min(width, height);

        this.width = width;
        this.height = height;
        this.DISPLAY_CENTER = new Point(width / 2, height / 2);
        this.EARTH_RADIUS = 3 * short_dimension / 10;
        this.VECTOR_SCALE = 0.05 * (double) short_dimension;
        this.ORB_SHELL = 45 * short_dimension / 100;
    }

    protected IEnumerable<Tuple<Point, Point>>
    ToDisplayVectors(IEnumerable<Tuple<Cartesian, Cartesian>> vectors)
    {
        return vectors.Select(ToDisplayPoints);
    }

    protected Tuple<Point, Point> ToDisplayPoints(Tuple<Cartesian, Cartesian> vector)
    {
        var realP1 = vector.Item1.Scale(EARTH_RADIUS / Constants.Earth.MEAN_RADIUS);
        var realP2 = vector.Item2.Scale(VECTOR_SCALE).Add(realP1);

        return Tuple.Create(ToDisplayPoint(realP1), ToDisplayPoint(realP2));
    }

    protected Point ToDisplayPoint(Cartesian realPt)
    {
        var displayPt = realPt.ToPoint();
        displayPt.Offset(DISPLAY_CENTER);
        displayPt.Y = height - displayPt.Y;
        return displayPt;
    }
}
