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
        if (Math.Abs(p.x) > 0.000001)
            this.a = Math.Atan(p.y / p.x);
        else if (p.y >= 0.0)
            this.a = Math.PI / 2.0;
        else
            this.a = 3.0 * Math.PI / 2.0;

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
