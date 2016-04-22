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

    public Polar Negative()
    {
        double n = a < Math.PI ? a + Math.PI : a - Math.PI;
        return new Polar(n, r);
    }
}
