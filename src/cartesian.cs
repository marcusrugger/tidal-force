using System;
using System.Drawing;

public class Cartesian
{
    public double x;
    public double y;

    public Cartesian(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public Cartesian(Polar pc)
    {
        x = pc.r * Math.Cos(pc.a);
        y = pc.r * Math.Sin(pc.a);
    }

    public static Cartesian operator +(Cartesian ls, Cartesian rs)
    {
        return new Cartesian(ls.x + rs.x, ls.y + rs.y);
    }

    public static Cartesian operator -(Cartesian ls, Cartesian rs)
    {
        return new Cartesian(ls.x - rs.x, ls.y - rs.y);
    }

    public Cartesian Negative()
    {
        return new Cartesian(-x, -y);
    }

    public double Distance()
    {
        return Math.Sqrt(x*x + y*y);
    }

    public Cartesian Multiply(double magnitude)
    {
        return new Cartesian(magnitude * x, magnitude * y);
    }

    public Point ToPoint()
    {
        return new Point((int) (x + 0.5), (int) (y + 0.5));
    }
}
