using System;
using System.Collections.Generic;
using System.Linq;

class TidalVectors
{
    private static GravitationalForce lunarG  = new GravitationalForce(Constants.Moon.MASS);
    private static GravitationalForce solarG  = new GravitationalForce(Constants.Sun.MASS);
    // private static Cartesian positionSun      = new Cartesian(0.0, Constants.Sun.MEAN_DISTANCE);
    private static Cartesian positionMoon     = new Cartesian(Constants.Moon.MEAN_DISTANCE, 0.0);
    private static Cartesian positionEarth    = new Cartesian(0.0, 0.0);
    // private static ForceVectors solarVectors  = new ForceVectors(solarG.compute, positionSun , positionEarth, 1e6);
    private static ForceVectors lunarVectors  = new ForceVectors(lunarG.compute, positionMoon, positionEarth, 1e6);

    private static Func<Cartesian, ForceVectors> fnSolarVectors = p => new ForceVectors(solarG.compute, p, positionEarth, 1e6);

    public static IEnumerable<Tuple<Cartesian, Cartesian>> Create(Cartesian positionSun)
    {
        var points      = new ForcePoints(32).compute();
        var lunarForces = lunarVectors.compute(points);
        var solarForces = fnSolarVectors(positionSun).compute(points);//solarVectors.compute(points);
        var totalForces = Enumerable.Zip(lunarForces, solarForces, (l, s) => l + s);
        return Enumerable.Zip(points, totalForces, (p, f) => Tuple.Create(p, f));
    }
}
