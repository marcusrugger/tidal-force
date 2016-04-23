using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

class TidalVectors
{
    private static int SLICE_DEGREES = 15;
    private static int SLICE_COUNT   = 360 / SLICE_DEGREES;

    private static GravitationalForce gforce  = new GravitationalForce(Constants.Moon.MASS);
    private static Cartesian positionMoon     = new Cartesian(Constants.Moon.MEAN_DISTANCE, 0.0);
    private static Cartesian forceEarthCenter = new Cartesian(gforce.compute(Constants.Moon.MEAN_DISTANCE), 0.0);

    private static double toRadians(double degrees)
    {
        return Math.PI * degrees / 180.0;
    }

    private static double toDegrees(double radians)
    {
        return 180.0 * radians / Math.PI;
    }

    public static IEnumerable<Tuple<Cartesian, Cartesian>> Create()
    {
        // Create a uniform set of points along a circle of Earth radius
        var points = Enumerable.Range(0, SLICE_COUNT)
                               .Select(n => toRadians( (double) (SLICE_DEGREES * n) ))
                               .Select(a => new Polar(a, Constants.Earth.MEAN_RADIUS).ToCartesian());

        // Calculate the lunar gravitational force at each point, relative to Earth's center, and convert to micronewtons
        var forces = points.Select(p => new Polar(positionMoon - p).TransformR(gforce.compute).ToCartesian())
                           .Select(f => (f - forceEarthCenter).Multiply(1e6));

        // Combine points and forces into Tuple and return them to caller
        return points.Zip(forces, (p, f) => Tuple.Create(p, f));
    }
}
