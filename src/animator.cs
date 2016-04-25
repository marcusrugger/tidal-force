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
    protected static Cartesian positionEarth    = new Cartesian(0.0, 0.0);
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

class OneBodyAnimator : Animator
{
    private Cartesian positionStationary;
    private ForceVectors vectorsStationary;
    private Func<Cartesian, ForceVectors> fnMoverVectors;

    private int angleMover;
    private double distanceMover;

    public OneBodyAnimator(int vectorCount,
                           double distanceMover,
                           Func<double, double> fnMoverG,
                           Cartesian positionStationary,
                           ForceVectors vectorsStationary)
    : base(vectorCount)
    {
        this.distanceMover = distanceMover;
        this.fnMoverVectors = p => new ForceVectors(fnMoverG, p, positionEarth, 1e6);
        this.positionStationary = positionStationary;
        this.vectorsStationary = vectorsStationary;
    }

    public override void nextFrame()
    {
        angleMover = (angleMover + 15) % 360;
    }

    public override IEnumerable<Tuple<Cartesian, Cartesian>> computeFrame()
    {
        var points = pointGenerator.compute();

        var stationaryForces = vectorsStationary.compute(points);

        var positionMover = new Polar(Algorithms.ToRadians(angleMover), distanceMover).ToCartesian();
        var moverForces   = fnMoverVectors(positionMover).compute(points);

        var totalForces = Enumerable.Zip(stationaryForces, moverForces, (l, s) => l + s);
        return Enumerable.Zip(points, totalForces, (p, f) => Tuple.Create(p, f));
    }
}

class SunAnimator : OneBodyAnimator
{
    private static Cartesian positionMoon = new Cartesian(Constants.Moon.MEAN_DISTANCE, 0.0);
    private static ForceVectors lunarVectors = new ForceVectors(lunarG.compute, positionMoon, positionEarth, 1e6);

    public SunAnimator(int vectorCount)
    : base(vectorCount,
           Constants.Sun.MEAN_DISTANCE,
           solarG.compute,
           positionMoon,
           lunarVectors)
    {}
}

class MoonAnimator : OneBodyAnimator
{
    private static Cartesian positionSun = new Cartesian(Constants.Sun.MEAN_DISTANCE, 0.0);
    private static ForceVectors solarVectors = new ForceVectors(solarG.compute, positionSun, positionEarth, 1e6);

    public MoonAnimator(int vectorCount)
    : base(vectorCount,
           Constants.Moon.MEAN_DISTANCE,
           lunarG.compute,
           positionSun,
           solarVectors)
    {
    }
}


class SunMoonAnimator : Animator
{
    private Func<Cartesian, ForceVectors> vectorsLunar = p => new ForceVectors(lunarG.compute, p, positionEarth, 1e6);
    private Func<Cartesian, ForceVectors> vectorsSolar = p => new ForceVectors(solarG.compute, p, positionEarth, 1e6);

    private double lunarAngle = 0.0;
    private double solarAngle = 0.0;

    public SunMoonAnimator(int vectorCount) : base(vectorCount)
    {}

    public override void nextFrame()
    {
        const double stepEarth = 15.0;
        lunarAngle = nextAngle(lunarAngle, (stepEarth /  28.0) - stepEarth);
        solarAngle = nextAngle(solarAngle, (stepEarth / 365.0) - stepEarth);
    }

    private double nextAngle(double angle, double step)
    {
        angle += step;
        if (angle >= 360.0) angle -= 360.0;
        if (angle <    0.0) angle += 360.0;
        return angle;
    }

    public override IEnumerable<Tuple<Cartesian, Cartesian>> computeFrame()
    {
        var points = pointGenerator.compute();

        var lunarPosition = new Polar(Algorithms.ToRadians(lunarAngle), Constants.Moon.MEAN_DISTANCE).ToCartesian();
        var lunarForces   = vectorsLunar(lunarPosition).compute(points);

        var solarPosition = new Polar(Algorithms.ToRadians(solarAngle), Constants.Sun.MEAN_DISTANCE).ToCartesian();
        var solarForces   = vectorsSolar(solarPosition).compute(points);

        var totalForces = Enumerable.Zip(lunarForces, solarForces, (l, s) => l + s);
        return Enumerable.Zip(points, totalForces, (p, f) => Tuple.Create(p, f));
    }
}