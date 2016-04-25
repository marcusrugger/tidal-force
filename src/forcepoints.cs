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
        Func<int, double>       fnRadian = n => Algorithms.ToRadians( (double) n * 360.0 / (double) count );
        Func<double, Cartesian> fnPoint  = n => new Polar(n, radius).ToCartesian();
        return Enumerable.Range(0, count).Select(n => fnPoint( fnRadian(n) ));
    }
}
