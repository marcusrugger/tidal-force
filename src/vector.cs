using System;

public class Vector
{
    public Cartesian p;
    public Cartesian f;

    public Vector(Cartesian p, Cartesian f)
    {
        this.p = p;
        this.f = f;
    }

    public Vector(Polar p, Polar f)
    {
        this.p = new Cartesian(p);
        this.f = new Cartesian(f);
    }

    public Vector Translate(Cartesian o)
    {
        return new Vector(this.p + o, this.f);
    }

    public Vector TranslateForce(Cartesian f)
    {
        return new Vector(this.p, this.f + f);
    }

    public Vector ReverseForce()
    {
        return new Vector(p, f.Negative());
    }
}
