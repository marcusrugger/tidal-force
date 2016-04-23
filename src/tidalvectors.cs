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

    public static IEnumerable<Vector> Create()
    {
        // Create a uniform list of angles from 0 - 360 degrees
        IEnumerable<double> angles = Enumerable.Range(0, SLICE_COUNT)
                                               .Select(n => toRadians(SLICE_DEGREES * (double) n));

        // Tranform angles into points on a circle of Earth radius
        IEnumerable<Cartesian> points2d = angles.Select(a => new Polar(a, Constants.Earth.MEAN_RADIUS).ToCartesian());

        // Calculate lunar gravitational force at each point
        IEnumerable<Cartesian> force2d = points2d.Select(p => new Polar(positionMoon - p).TransformR(gforce.compute).ToCartesian());

        // Subtract gravitational force at Earth's center and convert to micronewtons
        IEnumerable<Cartesian> relative2d = force2d.Select(f => (f - forceEarthCenter).Multiply(1e6));

        // Combine points and forces into vector
        return points2d.Zip(relative2d, (p, f) => new Vector(p, f));
    }
}
