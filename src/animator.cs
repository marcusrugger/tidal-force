using System;
using System.Collections.Generic;
using System.Linq;


interface IAnimator
{
    void nextFrame();
    IEnumerable<Tuple<Cartesian, Cartesian>> computeFrame();
}

abstract class Animator : IAnimator
{
    protected static GravitationalForce solarG  = new GravitationalForce(Constants.Sun.MASS);
    protected static GravitationalForce lunarG  = new GravitationalForce(Constants.Moon.MASS);

    protected ForcePoints pointGenerator;

    public Animator(int vectorCount)
    {
        this.pointGenerator = new ForcePoints(vectorCount);
    }

    public abstract void nextFrame();
    public abstract IEnumerable<Tuple<Cartesian, Cartesian>> computeFrame();

}

class SunAnimator : Animator
{
    private static Cartesian positionEarth = new Cartesian(0.0, 0.0);
    private static Cartesian positionMoon = new Cartesian(Constants.Moon.MEAN_DISTANCE, 0.0);
    private static Func<Cartesian, ForceVectors> fnSolarVectors = p => new ForceVectors(solarG.compute, p, positionEarth, 1e6);
    private static ForceVectors lunarVectors = new ForceVectors(lunarG.compute, positionMoon, positionEarth, 1e6);

    private int angleSun;

    public SunAnimator(int vectorCount) : base(vectorCount)
    {
    }

    public override void nextFrame()
    {
        Func<int, int, int> nextAngle = (angle, step) => (angle + step) % 360;
        angleSun  = nextAngle(angleSun, 15);
    }

    public override IEnumerable<Tuple<Cartesian, Cartesian>> computeFrame()
    {
        var positionSun = new Polar(Algorithms.ToRadians(angleSun), Constants.Sun.MEAN_DISTANCE).ToCartesian();
        var points      = pointGenerator.compute();
        var lunarForces = lunarVectors.compute(points);
        var solarForces = fnSolarVectors(positionSun).compute(points);
        var totalForces = Enumerable.Zip(lunarForces, solarForces, (l, s) => l + s);
        return Enumerable.Zip(points, totalForces, (p, f) => Tuple.Create(p, f));
    }
}

class MoonAnimator : Animator
{
    private static Cartesian positionEarth = new Cartesian(0.0, 0.0);
    private static Cartesian positionSun = new Cartesian(Constants.Sun.MEAN_DISTANCE, 0.0);
    private static Func<Cartesian, ForceVectors> fnMoonVectors = p => new ForceVectors(lunarG.compute, p, positionEarth, 1e6);
    private static ForceVectors solarVectors = new ForceVectors(solarG.compute, positionSun, positionEarth, 1e6);

    private int moonAngle;

    public MoonAnimator(int vectorCount) : base(vectorCount)
    {
    }

    public override void nextFrame()
    {
        Func<int, int, int> nextAngle = (angle, step) => (angle + step) % 360;
        moonAngle  = nextAngle(moonAngle, 5);
    }

    public override IEnumerable<Tuple<Cartesian, Cartesian>> computeFrame()
    {
        var positionMoon = new Polar(Algorithms.ToRadians(moonAngle), Constants.Moon.MEAN_DISTANCE).ToCartesian();
        var points       = pointGenerator.compute();
        var solarForces  = solarVectors.compute(points);
        var moonForces   = fnMoonVectors(positionMoon).compute(points);
        var totalForces  = Enumerable.Zip(solarForces, moonForces, (l, s) => l + s);
        return Enumerable.Zip(points, totalForces, (p, f) => Tuple.Create(p, f));
    }
}
