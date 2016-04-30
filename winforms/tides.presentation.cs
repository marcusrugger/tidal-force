using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


class DrawObject
{
    protected Graphics graphics;
    protected DisplayParameters display;

    public DrawObject(Graphics graphics, DisplayParameters display)
    {
        this.graphics = graphics;
        this.display = display;
    }

    protected Point ToDisplayPoint(Cartesian realPt)
    {
        return new Point( display.ToDisplayX(realPt.x), display.ToDisplayY(realPt.y) );
    }
}


class DrawOrb : DrawObject
{
    Brush brush;

    public DrawOrb(Graphics graphics, DisplayParameters display, Brush brush)
    : base(graphics, display)
    {
        this.brush = brush;
    }

    public void Draw(double angle)
    {
        var realPt = new Polar(angle, display.OrbShell).ToCartesian();
        var pt     = ToDisplayPoint(realPt);
        graphics.FillEllipse(brush, pt.X - 10, pt.Y - 10, 21, 21);
    }
}


class DrawSegment : DrawObject
{
    private Pen   colorLine = Pens.Black;
    private Brush colorPt1  = Brushes.Blue;
    private Brush colorPt2  = Brushes.Red;

    public DrawSegment(Graphics graphics, DisplayParameters display)
    : base(graphics, display)
    {}
    
    public void Draw(Cartesian p1, Cartesian p2)
    {
        Draw( ToDisplayPoint(p1), ToDisplayPoint(p2) );
    }

    private void Draw(Point p1, Point p2)
    {
        DrawLine(colorLine, p1, p2);
        DrawEndpoint(colorPt1, p1);
        DrawEndpoint(colorPt2, p2);
    }
    
    private void DrawLine(Pen pen, Point p1, Point p2)
    {
        graphics.DrawLine(pen, p1, p2);
    }
    
    private void DrawEndpoint(Brush brush, Point p)
    {
        graphics.FillEllipse(brush, p.X - 2, p.Y - 2, 5, 5);
    }
}


class DrawEarth : DrawObject
{
    public DrawEarth(Graphics graphics, DisplayParameters display)
    : base(graphics, display)
    {}

    public void Draw()
    {
        graphics.FillEllipse(Brushes.Aqua,
                             display.DisplayCenterX - display.EarthRadius + 1,
                             display.DisplayCenterY - display.EarthRadius + 1,
                             2 * display.EarthRadius,
                             2 * display.EarthRadius);
    }
}


class TidesPresenter : ITidesPresenter
{
    private DisplayParameters display;
    
    Brush colorSun  = Brushes.Orange;
    Brush colorMoon = Brushes.Gray;

    DrawOrb orbMoon;
    DrawOrb orbSun;
    DrawSegment segment;
    DrawEarth earth;

    public static TidesPresenter Create(Graphics graphics, DisplayParameters display)
    {
        return new TidesPresenter(graphics, display);
    }

    private TidesPresenter(Graphics graphics, DisplayParameters display)
    {
        this.display = display;

        orbMoon = new DrawOrb(graphics, display, colorMoon);
        orbSun  = new DrawOrb(graphics, display, colorSun);
        segment = new DrawSegment(graphics, display);
        earth   = new DrawEarth(graphics, display);
    }

    public void DrawEarth()
    {
        earth.Draw();
    }

    public void Draw(IEnumerable<Tuple<Cartesian, Cartesian>> vectorList)
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
