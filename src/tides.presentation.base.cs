using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


public interface ITidesPresenter
{
    void DrawEarth();
    void Draw(IEnumerable<Tuple<Cartesian, Cartesian>> vectors);
    void DrawSun(double angle);
    void DrawMoon(double angle);
}


public class TidesBasePresenter
{
    protected readonly int EARTH_RADIUS;
    protected readonly int DISPLAY_CENTER_X;
    protected readonly int DISPLAY_CENTER_Y;
    protected readonly double VECTOR_SCALE;
    protected readonly int ORB_SHELL;
    protected readonly int width;
    protected readonly int height;

    public TidesBasePresenter(int width, int height)
    {
        int short_dimension = Math.Min(width, height);

        this.width = width;
        this.height = height;
        this.DISPLAY_CENTER_X = width / 2;
        this.DISPLAY_CENTER_Y = height / 2;
        this.EARTH_RADIUS = 3 * short_dimension / 10;
        this.VECTOR_SCALE = 0.05 * (double) short_dimension;
        this.ORB_SHELL = 45 * short_dimension / 100;
    }
}
