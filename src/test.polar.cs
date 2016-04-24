using NUnit.Framework;
using System;


[TestFixture]
class PolarTest
{
    [Test]
    public void AngleRadiusConstructor()
    {
        double a = 123.4;
        double r = 234.5;

        Polar p = new Polar(a, r);

        Assert.AreEqual(a, p.a);
        Assert.AreEqual(r, p.r);
    }

    [Test]
    public void ConvertCartesian()
    {
        ConvertCartesian(  0.0,  1.0,   Math.PI/2,  1.0);
        ConvertCartesian(  0.0, -1.0, 3*Math.PI/2,  1.0);
        ConvertCartesian( 30.0, 50.0,   1.0303768, 58.3095189);
        ConvertCartesian(-30.0, 50.0,   2.1112158, 58.3095189);
        ConvertCartesian(-30.0,-50.0,   4.1719695, 58.3095189);
        ConvertCartesian( 30.0,-50.0,   5.2528085, 58.3095189);
    }

    private void ConvertCartesian(double x, double y, double expected_a, double expected_r)
    {
        Cartesian cp = new Cartesian(x, y);
        Polar pp = new Polar(cp);

        Assert.That(Math.Abs(pp.a - expected_a), Is.LessThan(0.000001));        
        Assert.That(Math.Abs(pp.r - expected_r), Is.LessThan(0.000001));
    }
}
