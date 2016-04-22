using System;

class TidalVectors
{
    public delegate double Function(double a);

    public const int SLICE_DEGREES = 10;
    public const int SLICE_COUNT   = 360 / SLICE_DEGREES;
    public Vector[] points;

    private GravitationalForce gforce;
    private Function fnGforce;

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
        gforce = new GravitationalForce(Constants.Moon.MASS);
        fnGforce = new Function(gforce.compute);

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

    private Cartesian CalculateForce(Cartesian cc)
    {
        var lunarCoord       = new Cartesian(Constants.Moon.MEAN_DISTANCE, 0.0);
        var forceEarthCenter = new Cartesian(fnGforce(Constants.Moon.MEAN_DISTANCE), 0.0);

        var distanceToMoon  = lunarCoord - cc;
        var distanceInPolar = new Polar(distanceToMoon);
        var polarForce      = new Polar(distanceInPolar.a, fnGforce(distanceInPolar.r));
        var coordForce      = new Cartesian(polarForce);
        var relativeForce   = coordForce - forceEarthCenter;
        return relativeForce.Magnify(10e6); // Convert to micronewtons
    }
}
