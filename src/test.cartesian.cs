using NUnit.Framework;
using System;


[TestFixture]
class CartesianTest
{
    [Test]
    public void XYConstructor()
    {
        double x = 123.4;
        double y = 234.5;
        Cartesian p = new Cartesian(x, y);

        Assert.That(p.x, Is.EqualTo(x));
        Assert.That(p.y, Is.EqualTo(y));
    }

    [Test]
    public void ConvertPolar()
    {
        double a = Math.PI / 6.0;
        double r = 5.0;

        Polar     p = new Polar(a, r);
        Cartesian c = new Cartesian(p);

        Assert.That(c.x, Is.EqualTo(r * Math.Cos(a)));
        Assert.That(c.y, Is.EqualTo(r * Math.Sin(a)));
    }

    [Test]
    public void Add()
    {
        Cartesian p1 = new Cartesian(123.4, 234.5);
        Cartesian p2 = new Cartesian(345.6, 456.7);
        Cartesian p3 = p2.Add(p1);

        Assert.That(p1.x, Is.EqualTo(123.4));
        Assert.That(p1.y, Is.EqualTo(234.5));
        Assert.That(p2.x, Is.EqualTo(345.6));
        Assert.That(p2.y, Is.EqualTo(456.7));

        Assert.That(p3.x, Is.EqualTo(123.4 + 345.6));
        Assert.That(p3.y, Is.EqualTo(234.5 + 456.7));
    }

    [Test]
    public void Subtract()
    {
        Cartesian p1 = new Cartesian(123.4, 234.5);
        Cartesian p2 = new Cartesian(345.6, 456.7);
        Cartesian p3 = p2.Subtract(p1);

        Assert.That(p1.x, Is.EqualTo(123.4));
        Assert.That(p1.y, Is.EqualTo(234.5));
        Assert.That(p2.x, Is.EqualTo(345.6));
        Assert.That(p2.y, Is.EqualTo(456.7));

        Assert.That(p3.x, Is.EqualTo(345.6 - 123.4));
        Assert.That(p3.y, Is.EqualTo(456.7 - 234.5));
    }

    [Test]
    public void Scale()
    {
        Cartesian p1 = new Cartesian(123.4, 234.5);
        Cartesian p2 = p1.Scale(2.0);

        Assert.That(Math.Abs(p2.x - 246.8), Is.LessThan(1e-9));
        Assert.That(Math.Abs(p2.y - 469.0), Is.LessThan(1e-9));
    }

    [Test]
    public void Transform()
    {
        Cartesian p1 = new Cartesian(123.4, 234.5);
        Cartesian p2 = p1.Transform(n => 2*n);

        Assert.That(Math.Abs(p2.x - 246.8), Is.LessThan(1e-9));
        Assert.That(Math.Abs(p2.y - 469.0), Is.LessThan(1e-9));
    }

    [Test]
    public void ToPoint()
    {
        Cartesian cp = new Cartesian(123.4, 234.5);
        System.Drawing.Point dp = cp.ToPoint();

        Assert.That(dp.X, Is.EqualTo(123));
        Assert.That(dp.Y, Is.EqualTo(235));
    }

    [Test]
    public void ToPolar()
    {
        Cartesian c = new Cartesian(123.4, 234.5);
        Polar     p = c.ToPolar();

        Assert.That(p, Is.InstanceOf(typeof(Polar)));
        Assert.That(Math.Abs(p.a -   1.0863887), Is.LessThan(1e-6));        
        Assert.That(Math.Abs(p.r - 264.9864336), Is.LessThan(1e-6));
    }
}
