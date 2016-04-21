using System;

public class Polar
{
    public const double pi = 3.1415926536;
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

    public Polar Negative()
    {
        double n = a + pi;
        if (n > 2 * pi) n -= 2 * pi;
        return new Polar(n, r);
    }
}
