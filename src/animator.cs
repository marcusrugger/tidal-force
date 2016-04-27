using System;
using System.Collections.Generic;
using System.Linq;


interface IAnimator
{
    void NextFrame();
    void Draw(IPresenter presenter);

    void Reset();
    bool Fast
    { set; }
}


abstract class Animator : IAnimator
{
    protected int frame_step;
    protected TidalVectors vectorGenerator;

    public Animator(int vectorCount)
    {
        this.frame_step = 1;
        this.vectorGenerator = new TidalVectors(vectorCount);
    }

    public abstract void NextFrame();
    public abstract void Draw(IPresenter presenter);

    public virtual void Reset()
    {
        Fast = false;
    }

    public bool Fast
    {
        set { frame_step = value ? 5 : 1; }
    }

}


class MoonAnimator : Animator
{
    private int lunarAngle = 0;

    public MoonAnimator(int vectorCount) : base(vectorCount)
    {}

    public override void NextFrame()
    {
        lunarAngle = (lunarAngle + frame_step) % 360;
    }

    public override void Draw(IPresenter presenter)
    {
        presenter.Draw    (computeFrame());
        presenter.DrawSun (0.0);
        presenter.DrawMoon(Algorithms.ToRadians(lunarAngle));
    }

    public override void Reset()
    {
        base.Reset();
        lunarAngle = 0;
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

    public override void NextFrame()
    {
        solarAngle = solarAngle - frame_step;
        solarAngle = solarAngle < 0 ? solarAngle + 360 : solarAngle;
    }

    public override void Draw(IPresenter presenter)
    {
        presenter.Draw    (computeFrame());
        presenter.DrawSun (Algorithms.ToRadians(solarAngle));
        presenter.DrawMoon(0.0);
    }

    public override void Reset()
    {
        base.Reset();
        solarAngle = 0;
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

    public override void NextFrame()
    {
        double stepEarth = (double) frame_step;
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
        double lunarRadians = Algorithms.ToRadians(lunarAngle);
        double solarRadians = Algorithms.ToRadians(solarAngle);

        presenter.Draw    (vectorGenerator.compute(lunarRadians, solarRadians));
        presenter.DrawSun (solarRadians);
        presenter.DrawMoon(lunarRadians);
    }

    public override void Reset()
    {
        base.Reset();
        lunarAngle = 0.0;
        solarAngle = 0.0;
    }
}
