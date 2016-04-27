using System;
using System.Collections.Generic;
using System.Linq;


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

    public override void Draw(ITidesPresenter presenter)
    {
        double lunarRadians = Algorithms.ToRadians(lunarAngle);
        double solarRadians = Algorithms.ToRadians(solarAngle);

        presenter.Draw    (vectorGenerator.compute(lunarRadians, solarRadians));
        presenter.DrawSun (solarRadians);
        presenter.DrawMoon(lunarRadians);
    }

    protected override void DoReset()
    {
        lunarAngle = 0.0;
        solarAngle = 0.0;
    }
}
