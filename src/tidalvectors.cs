using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

class TidalVectors
{
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

    public static IEnumerable<Tuple<Cartesian, Cartesian>> Create(int angle)
    {
        var points = Enumerable.Range(0, 360 / angle).Select(n => CalculatePoint(n * angle));
        var forces = points.Select(CalculateForce);
        return Enumerable.Zip(points, forces, (p, f) => Tuple.Create(p, f));
    }

    // For a given angle in degrees, calculate a point on a circle of Earth's radius
    private static Cartesian CalculatePoint(int angle)
    {
        return new Polar(toRadians((double) angle), Constants.Earth.MEAN_RADIUS).ToCartesian();
    }

    // For a given point, calculate the lunar gravitational force in micronewtons relative to Earth's center
    private static Cartesian CalculateForce(Cartesian p)
    {
        return positionMoon.Subtract(p)                 // Distance from point to the Moon
                           .ToPolar()                   // Convert to polar coordinates
                           .TransformR(gforce.compute)  // Calculate lunar gravitational force at point
                           .ToCartesian()               // Convert back to cartesian coordinates
                           .Subtract(forceEarthCenter)  // Make force relative to the center of Earth
                           .Scale(1e6);                 // Convert to micronewtons
    }
}
