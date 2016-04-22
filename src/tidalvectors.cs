using System;
using System.Drawing;

class TidalVectors
{
    public const int SLICE_DEGREES = 5;
    public const int SLICE_COUNT   = 360 / SLICE_DEGREES;
    public Vector[] points;

    private GravitationalForce gforce;
    private Cartesian positionMoon;
    private Cartesian forceEarthCenter;

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
        gforce           = new GravitationalForce(Constants.Moon.MASS);
        positionMoon     = new Cartesian(Constants.Moon.MEAN_DISTANCE, 0.0);
        forceEarthCenter = new Cartesian(gforce.compute(Constants.Moon.MEAN_DISTANCE), 0.0);
        PopulatePoints();
    }

    private void PopulatePoints()
    {
        points = new Vector[SLICE_COUNT];
        for (int a = 0; a < SLICE_COUNT; a++)
            points[a] = CalculateVector(toRadians(SLICE_DEGREES * a));
    }

    private Vector CalculateVector(double angle)
    {
        var position = CalculatePosition(angle);
        var force    = CalculateForce(position);

        Console.WriteLine("Force: x = {0,6:0.0}, y = {1,6:0.0}", force.x, force.y);
        return new Vector(position, force);
    }

    private Cartesian CalculatePosition(double angle)
    {
        var pc = new Polar(angle, Constants.Earth.MEAN_RADIUS);
        return new Cartesian(pc);
    }

    private Cartesian CalculateForce(Cartesian positionPoint)
    {
        var forcePolar  = new Polar(positionMoon - positionPoint).TranslateR(gforce.compute);
        var forceVector = new Cartesian(forcePolar);

        return (forceVector - forceEarthCenter).Multiply(10e6); // Convert to micronewtons
    }

    public void Draw(Graphics graphics)
    {
        const double DISPLAY_RADIUS = 300;
        foreach (var vector in points)
        {
            var v = vector.p.Multiply(DISPLAY_RADIUS / Constants.Earth.MEAN_RADIUS);
            var p1 = new Point(500 + (int) v.x, 500 + (int) v.y);
            var p2 = new Point((int) (10.0 * vector.f.x), (int) (10.0 * vector.f.y));
            p2.X = p1.X + p2.X;
            p2.Y = p1.Y + p2.Y;
            graphics.DrawLine(Pens.Red, p1, p2);
            graphics.FillEllipse(Brushes.Green, p1.X - 2, p1.Y - 2, 5, 5);
            graphics.FillEllipse(Brushes.Blue , p2.X - 2, p2.Y - 2, 5, 5);
        }
    }
}
