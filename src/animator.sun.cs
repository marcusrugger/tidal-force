using System;
using System.Collections.Generic;
using System.Linq;
using Flatland;


class SunAnimator : Animator
{
    private int solarAngle = 0;

    public override void NextFrame()
    {
        solarAngle = solarAngle - frame_step;
        solarAngle = solarAngle < 0 ? solarAngle + 360 : solarAngle;
    }

    public override void Draw(ITidesPresenter presenter)
    {
        double solarRadians = Algorithms.ToRadians(solarAngle);

        presenter.Draw    (vectorGenerator.compute(0.0, solarRadians));
        presenter.DrawSun (solarRadians);
        presenter.DrawMoon(0.0);
    }

    protected override void DoReset()
    {
        solarAngle = 0;
    }
}
