using System;
using System.Drawing;
using System.Linq;

class TidalVectors
{
    public const int SLICE_DEGREES = 10;
    public const int SLICE_COUNT   = 360 / SLICE_DEGREES;
    public Vector[] vectors;

    static public double toRadians(double degrees)
    {
        return Math.PI * degrees / 180.0;
    }

    static public double toDegrees(double radians)
    {
        return 180.0 * radians / Math.PI;
    }

    public TidalVectors()
    {
        GravitationalForce gforce  = new GravitationalForce(Constants.Moon.MASS);
        Cartesian positionMoon     = new Cartesian(Constants.Moon.MEAN_DISTANCE, 0.0);
        Cartesian forceEarthCenter = new Cartesian(gforce.compute(Constants.Moon.MEAN_DISTANCE), 0.0);

        // Create a uniform list of angles from 0 - 360 degrees
        double[] angles = Enumerable.Range(0, 36)
                                    .Select(n => toRadians(10.0 * (double) n))
                                    .ToArray();

        // Tranform angles into points on a circle of Earth radius
        Cartesian[] points2d = angles.Select(a => new Polar(a, Constants.Earth.MEAN_RADIUS).ToCartesian())
                                     .ToArray();

        // Calculate lunar gravitational force at each point
        Cartesian[] force2d = points2d.Select(p => new Polar(positionMoon - p).TransformR(gforce.compute).ToCartesian())
                                      .ToArray();

        // Subtract gravitational force at Earth's center and convert to micronewtons
        Cartesian[] relative2d = force2d.Select(f => (f - forceEarthCenter).Multiply(1e6))
                                        .ToArray();

        // Combine points and forces into vector
        vectors = points2d.Zip(relative2d, (p, f) => new Vector(p, f))
                          .ToArray();
    }

    public void Draw(Graphics graphics)
    {
        foreach (var vector in vectors)
        {
            var pts = TransformToDisplayPoints(vector);
            Draw(graphics, pts.Item1, pts.Item2);
        }
    }

    private Tuple<Point, Point> TransformToDisplayPoints(Vector vector)
    {
        const double DISPLAY_RADIUS = 300;

        var v = vector.p.Multiply(DISPLAY_RADIUS / Constants.Earth.MEAN_RADIUS);
        var p1 = new Point(500 + (int) v.x, 500 + (int) v.y);
        var p2 = new Point((int) (50.0 * vector.f.x), (int) (50.0 * vector.f.y));

        p2.Offset(p1);

        return Tuple.Create(p1, p2);
    }

    private void Draw(Graphics graphics, Point p1, Point p2)
    {
        graphics.DrawLine(Pens.Red, p1, p2);
        graphics.FillEllipse(Brushes.Green, p1.X - 2, p1.Y - 2, 5, 5);
        graphics.FillEllipse(Brushes.Blue , p2.X - 2, p2.Y - 2, 5, 5);
    }
}
