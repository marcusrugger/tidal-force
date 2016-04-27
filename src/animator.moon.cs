using System;
using System.Collections.Generic;
using System.Linq;


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
        double lunarRadians = Algorithms.ToRadians(lunarAngle);

        presenter.Draw    (vectorGenerator.compute(lunarRadians, 0.0));
        presenter.DrawSun (0.0);
        presenter.DrawMoon(lunarRadians);
    }

    protected override void DoReset()
    {
        lunarAngle = 0;
    }
}
