using System;
using System.Collections.Generic;
using System.Linq;
using Flatland;


class TidalVectors
{
    protected static readonly Cartesian positionEarth    = new Cartesian(0.0, 0.0);
    protected static readonly GravitationalForce solarG  = new GravitationalForce(Constants.Sun.MASS);
    protected static readonly GravitationalForce lunarG  = new GravitationalForce(Constants.Moon.MASS);

    readonly Func<Cartesian, ForceVectors> vectorsLunar = p => new ForceVectors(lunarG.compute, p, positionEarth, 1e6);
    readonly Func<Cartesian, ForceVectors> vectorsSolar = p => new ForceVectors(solarG.compute, p, positionEarth, 1e6);

    protected readonly ForcePoints pointGenerator;

    public TidalVectors(int vectorCount)
    {
        this.pointGenerator = new ForcePoints(vectorCount);
    }

    public IEnumerable<Tuple<Cartesian, Cartesian>> compute(double lunarAngle, double solarAngle)
    {
        var points = pointGenerator.compute();

        var lunarPosition = new Polar(lunarAngle, Constants.Moon.MEAN_DISTANCE).ToCartesian();
        var lunarForces   = vectorsLunar(lunarPosition).compute(points);

        var solarPosition = new Polar(solarAngle, Constants.Sun.MEAN_DISTANCE).ToCartesian();
        var solarForces   = vectorsSolar(solarPosition).compute(points);

        var totalForces = Enumerable.Zip(lunarForces, solarForces, (l, s) => l + s);
        return Enumerable.Zip(points, totalForces, (p, f) => Tuple.Create(p, f));
    }
}
