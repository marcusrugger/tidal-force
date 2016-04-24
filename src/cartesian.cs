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

    public Cartesian Subtract(Cartesian other)
    {
        return new Cartesian(x - other.x, y - other.y);
    }

    public Cartesian Scale(double magnitude)
    {
        return new Cartesian(magnitude * x, magnitude * y);
    }

    public Cartesian Transform(Func<double, double> fn)
    {
        return new Cartesian(fn(x), fn(y));
    }

    public Point ToPoint()
    {
        return new Point((int) (x + 0.5), (int) (y + 0.5));
    }

    public Polar ToPolar()
    {
        return new Polar(this);
    }
}
