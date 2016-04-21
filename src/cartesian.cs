using System;

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
}
