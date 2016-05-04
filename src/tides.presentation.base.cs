using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Flatland;

using VectorList = System.Collections.Generic.IEnumerable<System.Tuple<Flatland.Cartesian, Flatland.Cartesian>>;


public interface ITidesPresenter
{
    void DrawEarth();
    void Draw(VectorList vectors);
    void DrawSun(double angle);
    void DrawMoon(double angle);
}
