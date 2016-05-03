using System;
using System.Collections.Generic;
using System.Linq;
using Flatland;

using VectorList = System.Collections.Generic.IEnumerable<System.Tuple<Flatland.Cartesian, Flatland.Cartesian>>;


class DrawObject
{
    protected Flatland.Canvas canvas;
    protected DisplayParameters display;

    public DrawObject(Flatland.Canvas canvas, DisplayParameters display)
    {
        this.canvas  = canvas;
        this.display = display;
    }

    protected Flatland.Cartesian ToDisplayPoint(Cartesian realPt)
    {
        return new Flatland.Cartesian( display.ToDisplayX(realPt.X), display.ToDisplayY(realPt.Y) );
    }
}


class DrawOrb : DrawObject
{
    readonly Flatland.Wireframe wireframe;

    public DrawOrb(Flatland.Canvas canvas, DisplayParameters display, Flatland.Color color)
    : base(canvas, display)
    {
        wireframe = canvas.Wireframe().SetLineColor(color);
    }

    public void Draw(double angle)
    {
        var realPt = new Polar(angle, display.OrbShell).ToCartesian();
        var pt     = ToDisplayPoint(realPt);
        wireframe.Circle(pt, 10.0);
    }
}


class DrawSegment : DrawObject
{
    readonly Flatland.Wireframe lineSegment;
    readonly Flatland.Wireframe endcapPt1;
    readonly Flatland.Wireframe endcapPt2;

    public DrawSegment(Flatland.Canvas canvas, DisplayParameters display)
    : base(canvas, display)
    {
        lineSegment = canvas.Wireframe().SetLineColor(Flatland.Colors.Black);
        endcapPt1   = canvas.Wireframe().SetLineColor(Flatland.Colors.Blue);
        endcapPt2   = canvas.Wireframe().SetLineColor(Flatland.Colors.Red);
    }
    
    public void Draw(Cartesian p1, Cartesian p2)
    {
        InternalDraw( ToDisplayPoint(p1), ToDisplayPoint(p2) );
    }

    private void InternalDraw(Cartesian p1, Cartesian p2)
    {
        lineSegment.Line(p1, p2);
        endcapPt1.Circle(p1, 2.0);
        endcapPt2.Circle(p2, 2.0);
    }
}


class DrawEarth : DrawObject
{
    public DrawEarth(Flatland.Canvas canvas, DisplayParameters display)
    : base(canvas, display)
    {}

    public void Draw()
    {
        canvas.Wireframe()
              .SetLineColor(Flatland.Colors.Cyan)
              .Circle(display.DisplayCenterX, display.DisplayCenterY, display.EarthRadius);
    }
}


public class TidesFlatlandPresenter : ITidesPresenter
{
    DisplayParameters display;
    
    Flatland.Color colorSun  = new Flatland.ColorB(253, 184, 19);
    Flatland.Color colorMoon = Flatland.Colors.Gray;

    DrawOrb orbMoon;
    DrawOrb orbSun;
    DrawSegment segment;
    DrawEarth earth;
    

    public static TidesFlatlandPresenter Create(Flatland.Canvas canvas, DisplayParameters display)
    {
        return new TidesFlatlandPresenter(canvas, display);
    }

    private TidesFlatlandPresenter(Flatland.Canvas canvas, DisplayParameters display)
    {
        this.display = display;

        orbMoon = new DrawOrb(canvas, display, colorMoon);
        orbSun  = new DrawOrb(canvas, display, colorSun);
        segment = new DrawSegment(canvas, display);
        earth   = new DrawEarth(canvas, display);
    }

    public void DrawEarth()
    {
        earth.Draw();
    }

    public void Draw(VectorList vectorList)
    {
        foreach (var pair in vectorList.Select(display.ToDisplayScale))
            segment.Draw(pair.Item1, pair.Item2);
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
