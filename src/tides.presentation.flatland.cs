using System;
using System.Collections.Generic;
using System.Linq;
using Flatland;

using VectorList = System.Collections.Generic.IEnumerable<System.Tuple<Flatland.Cartesian, Flatland.Cartesian>>;


class DrawObject
{
    protected Flatland.Canvas canvas;

    public DrawObject(Flatland.Canvas canvas)
    {
        this.canvas  = canvas;
    }
}


class DrawOrb : DrawObject
{
    static readonly double ORB_SHELL = 1.50 * Constants.Earth.MEAN_RADIUS;
    static readonly double ORB_SIZE  = 0.10 * Constants.Earth.MEAN_RADIUS;

    readonly Flatland.Wireframe wireframe;

    public DrawOrb(Flatland.Canvas canvas, Flatland.Color color)
    : base(canvas)
    {
        wireframe = canvas.Wireframe().SetLineColor(color);
    }

    public void Draw(double angle)
    {
        var pt = new Polar(angle, ORB_SHELL).ToCartesian();
        wireframe.Circle(pt, ORB_SIZE);
    }
}


class DrawSegment : DrawObject
{
    static readonly double VECTOR_SCALE = 0.20 * Constants.Earth.MEAN_RADIUS;

    readonly Flatland.Turtle    lineSegment;
    readonly Flatland.Wireframe endcapPt1;

    public DrawSegment(Flatland.Canvas canvas)
    : base(canvas)
    {
        lineSegment = canvas.Turtle()   .SetLineColor(Flatland.Colors.Black);
        endcapPt1   = canvas.Wireframe().SetLineColor(Flatland.Colors.Blue);
    }

    public void Draw(Tuple<Cartesian, Cartesian> vector)
    {
        var p1 = vector.Item1;
        var p2 = vector.Item2.Scale(VECTOR_SCALE).ToPolar();
        Draw(p1, p2);
    }

    private void Draw(Cartesian p1, Polar p2)
    {
        endcapPt1.Circle(p1, 200000.0);
        lineSegment.MoveTo(p1)
                   .TurnTo(p2.A)
                   .Line(p2.R)
                   .SetLineColor(Flatland.Colors.Red)
                   .Turn( Algorithms.ToRadians(150.0) )
                   .Line(400000.0)
                   .Turn( Algorithms.ToRadians(120.0) )
                   .Line(400000.0)
                   .Turn( Algorithms.ToRadians(120.0) )
                   .Line(400000.0);
    }
}


class DrawEarth : DrawObject
{
    public DrawEarth(Flatland.Canvas canvas)
    : base(canvas)
    {}

    public void Draw()
    {
        canvas.Wireframe()
              .SetLineColor(Flatland.Colors.Cyan)
              .Circle(0.0, 0.0, Constants.Earth.MEAN_RADIUS);
    }
}


public class TidesFlatlandPresenter : ITidesPresenter
{
    static readonly Flatland.Color colorSun  = new Flatland.ColorB(253, 184, 19);
    static readonly Flatland.Color colorMoon = Flatland.Colors.Gray;

    readonly DrawOrb orbMoon;
    readonly DrawOrb orbSun;
    readonly DrawSegment segment;
    readonly DrawEarth earth;
    

    public static TidesFlatlandPresenter Create(Flatland.Canvas canvas)
    {
        return new TidesFlatlandPresenter(canvas);
    }

    private TidesFlatlandPresenter(Flatland.Canvas canvas)
    {
        orbMoon = new DrawOrb(canvas, colorMoon);
        orbSun  = new DrawOrb(canvas, colorSun);
        segment = new DrawSegment(canvas);
        earth   = new DrawEarth(canvas);
    }

    public void DrawEarth()
    {
        earth.Draw();
    }

    public void Draw(VectorList vectorList)
    {
        foreach (var vector in vectorList)
            segment.Draw(vector);
    }

    public void DrawSun(double angle)
    {
        orbSun.Draw(angle);
    }

    public void DrawMoon(double angle)
    {
        orbMoon.Draw(angle);
    }
}
