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
    protected TidalVectors vectorGenerator;

    public Animator(int vectorCount)
    {
        this.vectorGenerator = new TidalVectors(vectorCount);
    }

    public abstract void nextFrame();
    public abstract IEnumerable<Tuple<Cartesian, Cartesian>> computeFrame();

}


class SunMoonAnimator : Animator
{
    private double lunarAngle = 0.0;
    private double solarAngle = 0.0;

    public SunMoonAnimator(int vectorCount) : base(vectorCount)
    {}

    public override void nextFrame()
    {
        const double stepEarth = 5.0;
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
        double lunarRadians = Algorithms.ToRadians(lunarAngle);
        double solarRadians = Algorithms.ToRadians(solarAngle);
        return vectorGenerator.compute(lunarRadians, solarRadians);
    }
}


class TidalVectors
{
    protected static Cartesian positionEarth    = new Cartesian(0.0, 0.0);
    protected static GravitationalForce solarG  = new GravitationalForce(Constants.Sun.MASS);
    protected static GravitationalForce lunarG  = new GravitationalForce(Constants.Moon.MASS);

    private Func<Cartesian, ForceVectors> vectorsLunar = p => new ForceVectors(lunarG.compute, p, positionEarth, 1e6);
    private Func<Cartesian, ForceVectors> vectorsSolar = p => new ForceVectors(solarG.compute, p, positionEarth, 1e6);

    protected ForcePoints pointGenerator;

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
