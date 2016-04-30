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


public class DisplayParameters
{
    private readonly int EARTH_RADIUS;
    private readonly int DISPLAY_CENTER_X;
    private readonly int DISPLAY_CENTER_Y;
    private readonly double VECTOR_SCALE;
    private readonly int ORB_SHELL;
    private readonly int width;
    private readonly int height;

    public DisplayParameters(int width, int height)
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

    public int DisplayCenterX
    { get { return DISPLAY_CENTER_X;} }

    public int DisplayCenterY
    { get { return DISPLAY_CENTER_Y;} }

    public int EarthRadius
    { get { return EARTH_RADIUS;} }

    public double VectorScale
    { get { return VECTOR_SCALE;} }

    public int OrbShell
    { get { return ORB_SHELL;} }

    public int Width
    { get { return width;} }

    public int Height
    { get { return height;} }


    public int ToDisplayX(double x)
    {
        return DisplayCenterX + (int) (x + 0.5);
    }

    public int ToDisplayY(double y)
    {
        return Height - (DisplayCenterY + (int) (y + 0.5));
    }
}
