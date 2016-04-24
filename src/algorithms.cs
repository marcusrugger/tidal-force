using System;


class Algorithms
{
    public static double ToRadians(double degrees)
    {
        return Math.PI * degrees / 180.0;
    }

    public static double ToDegrees(double radians)
    {
        return 180.0 * radians / Math.PI;
    }
}