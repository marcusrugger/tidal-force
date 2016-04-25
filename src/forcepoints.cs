using System;
using System.Collections.Generic;
using System.Linq;

class ForcePoints
{
    private readonly double radius;
    private readonly int count;

    public ForcePoints(int count, double radius = Constants.Earth.MEAN_RADIUS)
    {
        this.count  = count;
        this.radius = radius;
    }

    public IEnumerable<Cartesian> compute()
    {
        Func<int, double> fn = n => Algorithms.ToRadians( (double) n * 360.0 / (double) count );
        return Enumerable.Range(0, count).Select(n => CalculatePoint( fn(n) ));
    }

    // For a given angle in radians, calculate a point on a circle
    private Cartesian CalculatePoint(double angle)
    {
        return new Polar(angle, radius).ToCartesian();
    }
}
