using Cairo;
using System;
using System.Collections.Generic;
using System.Linq;


class TidesPresenter : TidesBasePresenter, ITidesPresenter
{
    public static TidesPresenter Create()
    {
        return new TidesPresenter();
    }

    private TidesPresenter() : base(1000, 1000)
    {
    }

    public void DrawEarth()
    {
    }

    public void Draw(IEnumerable<Tuple<Cartesian, Cartesian>> vectors)
    {
    }

    public void DrawSun(double angle)
    {
    }

    public void DrawMoon(double angle)
    {
    }
}
