using System;

public class GravitationalForce
{
    private const double G = 6.67408e-11;
    private readonly double n;

    public GravitationalForce(double M, double m)
    {
        n = G * M * m;
    }

    public GravitationalForce(double m) : this(1.0, m) {}

    public double compute(double r)
    {
        return n / (r * r);
    }
}
