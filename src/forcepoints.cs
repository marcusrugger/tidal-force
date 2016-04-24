using System;
using System.Collections.Generic;
using System.Linq;

class ForcePoints
{
    private readonly double radius;

    private static double toRadians(double degrees)
    {
        return Math.PI * degrees / 180.0;
    }

    public ForcePoints(double radius = Constants.Earth.MEAN_RADIUS)
    {
        this.radius = radius;
    }

    public IEnumerable<Cartesian> compute(int count)
    {
        Func<int, double> fn = n => toRadians(((double) n) * 360.0 / ((double) count));
        return Enumerable.Range(0, count).Select(n => CalculatePoint( fn(n) ));
    }

    // For a given angle in radians, calculate a point on a circle
    private Cartesian CalculatePoint(double angle)
    {
        return new Polar(angle, radius).ToCartesian();
    }
}
