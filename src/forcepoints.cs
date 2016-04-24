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

    public IEnumerable<Cartesian> compute(int sections)
    {
        return Enumerable.Range(0, sections).Select(n => CalculatePoint((double) n * 360.0 / (double) sections));
    }

    // For a given angle in degrees, calculate a point on a circle of Earth's radius
    private Cartesian CalculatePoint(double angle)
    {
        return new Polar(toRadians(angle), radius).ToCartesian();
    }
}
