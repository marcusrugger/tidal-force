using System;

public class Polar
{
    public double a;
    public double r;

    public Polar(double a, double r)
    {
        this.a = a;
        this.r = r;
    }

    public Polar(Cartesian p)
    {
        this.a = SafeAtan(p);
        this.r = Math.Sqrt(p.x * p.x + p.y * p.y);
    }

    private double SafeAtan(Cartesian p)
    {
        if (Math.Abs(p.x) > 1e-9)
            return AtanByQuadrant(p);
        else
            return AtanPointOnYAxis(p);
    }

    private double AtanByQuadrant(Cartesian p)
    {
        double result = Math.Atan(p.y / p.x);

        if (p.x < 0.0)
            result += Math.PI;
        else if (p.y < 0.0)
            result += 2*Math.PI;

        return result;
    }

    private double AtanPointOnYAxis(Cartesian p)
    {
        if (p.y >= 0.0)
            return Math.PI / 2.0;
        else
            return 3.0 * Math.PI / 2.0;
    }

    public Cartesian ToCartesian()
    {
        return new Cartesian(this);
    }

    public Polar TransformR(Func<double, double> fn)
    {
        return new Polar(a, fn(r));
    }
}
