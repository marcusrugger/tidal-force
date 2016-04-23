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
        this.a = Math.Atan(p.y / p.x);
        this.r = Math.Sqrt(p.x * p.x + p.y * p.y);
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
