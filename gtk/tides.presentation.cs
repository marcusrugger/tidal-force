using Cairo;
using System;
using System.Collections.Generic;
using System.Linq;


class DrawObject
{
    protected Context context;
    protected DisplayParameters display;

    public DrawObject(Context context, DisplayParameters display)
    {
        this.context = context;
        this.display = display;
    }
}


class DrawOrb : DrawObject
{
    Color color;

    public DrawOrb(Context context, DisplayParameters display, Color color)
    : base(context, display)
    {
        this.color = color;
    }

    public void Draw(double angle)
    {
        var realPt = new Polar(angle, display.OrbShell).ToCartesian();

        context.LineWidth = 1.0;
        context.SetSourceColor(color);
        context.Arc( display.ToDisplayX(realPt.x), display.ToDisplayY(realPt.y), 10, 0, 2 * Math.PI );
        context.Fill();
    }
}


class DrawSegment : DrawObject
{
    private Color colorLine = new Color(   0.0,   0.0,   0.0 );
    private Color colorPt1  = new Color(   0.0,   0.0, 256.0 );
    private Color colorPt2  = new Color( 256.0,   0.0,   0.0 );

    public DrawSegment(Context context, DisplayParameters display)
    : base(context, display)
    {}
    
    public void Draw(Cartesian p1, Cartesian p2)
    {
        Draw( ToDisplayPoint(p1), ToDisplayPoint(p2) );
    }

    private void Draw(PointD p1, PointD p2)
    {
        DrawLine(colorLine, p1, p2);
        DrawEndpoint(colorPt1, p1);
        DrawEndpoint(colorPt2, p2);
    }
    
    private void DrawLine(Color color, PointD p1, PointD p2)
    {
        context.LineWidth = 1.0;
        context.SetSourceColor(colorLine);
        context.MoveTo(p1);
        context.LineTo(p2);
        context.Stroke();
    }
    
    private void DrawEndpoint(Color color, PointD p)
    {
        context.LineWidth = 1.0;
        context.SetSourceColor(color);
        context.Arc(p.X, p.Y, 2, 0, 2*Math.PI);
        context.Stroke();
    }

    private PointD ToDisplayPoint(Cartesian realPt)
    {
        return new PointD( display.ToDisplayX(realPt.x), display.ToDisplayY(realPt.y) );
    }
}


class DrawEarth : DrawObject
{
    public DrawEarth(Context context, DisplayParameters display)
    : base(context, display)
    {}

    public void Draw()
    {
        context.LineWidth = 1.0;
        context.Arc(display.DisplayCenterX, display.DisplayCenterY, display.EarthRadius, 0, 2*Math.PI);
        context.SetSourceRGB(0.0, 1.0, 1.0);
        context.Fill();
    }
}


class TidesPresenter : ITidesPresenter
{
    DisplayParameters display;
    
    Color colorSun  = new Color(1.0, 0.6, 0.1);
    Color colorMoon = new Color(0.5, 0.5, 0.5);

    DrawOrb orbMoon;
    DrawOrb orbSun;
    DrawSegment segment;
    DrawEarth earth;
    

    public static TidesPresenter Create(Context context, DisplayParameters display)
    {
        return new TidesPresenter(context, display);
    }

    private TidesPresenter(Context context, DisplayParameters display)
    {
        this.display = display;

        orbMoon = new DrawOrb(context, display, colorMoon);
        orbSun  = new DrawOrb(context, display, colorSun);
        segment = new DrawSegment(context, display);
        earth   = new DrawEarth(context, display);
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
