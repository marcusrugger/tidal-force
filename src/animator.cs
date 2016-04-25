using System;
using System.Collections.Generic;
using System.Linq;


interface IAnimator
{
    void nextFrame();
    void Draw(IPresenter presenter);
}


abstract class Animator : IAnimator
{
    protected TidalVectors vectorGenerator;

    public Animator(int vectorCount)
    {
        this.vectorGenerator = new TidalVectors(vectorCount);
    }

    public abstract void nextFrame();
    public abstract void Draw(IPresenter presenter);

}


class MoonAnimator : Animator
{
    private int lunarAngle = 0;

    public MoonAnimator(int vectorCount) : base(vectorCount)
    {}

    public override void nextFrame()
    {
        lunarAngle = (lunarAngle + 5) % 360;
    }

    public override void Draw(IPresenter presenter)
    {
        presenter.Draw    (computeFrame());
        presenter.DrawSun (0.0);
        presenter.DrawMoon(Algorithms.ToRadians(lunarAngle));
    }

    private IEnumerable<Tuple<Cartesian, Cartesian>> computeFrame()
    {
        double lunarRadians = Algorithms.ToRadians(lunarAngle);
        return vectorGenerator.compute(lunarRadians, 0.0);
    }
}


class SunAnimator : Animator
{
    private int solarAngle = 0;

    public SunAnimator(int vectorCount) : base(vectorCount)
    {}

    public override void nextFrame()
    {
        solarAngle = solarAngle - 5;
        solarAngle = solarAngle < 0 ? solarAngle + 360 : solarAngle;
    }

    public override void Draw(IPresenter presenter)
    {
        presenter.Draw    (computeFrame());
        presenter.DrawSun (Algorithms.ToRadians(solarAngle));
        presenter.DrawMoon(0.0);
    }

    private IEnumerable<Tuple<Cartesian, Cartesian>> computeFrame()
    {
        double solarRadians = Algorithms.ToRadians(solarAngle);
        return vectorGenerator.compute(0.0, solarRadians);
    }
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

    public override void Draw(IPresenter presenter)
    {
        presenter.Draw    (computeFrame());
        presenter.DrawSun (Algorithms.ToRadians(solarAngle));
        presenter.DrawMoon(Algorithms.ToRadians(lunarAngle));
    }

    private IEnumerable<Tuple<Cartesian, Cartesian>> computeFrame()
    {
        double lunarRadians = Algorithms.ToRadians(lunarAngle);
        double solarRadians = Algorithms.ToRadians(solarAngle);
        return vectorGenerator.compute(lunarRadians, solarRadians);
    }
}
