using NUnit.Framework;
using System;


[TestFixture]
class GravitationalForceTest
{
    private const double G = 6.67408e-11;

    [Test]
    public void UnitForce()
    {
        GravitationalForce g = new GravitationalForce(1.0, 1.0);

        Assert.That(g.compute(1.0), Is.EqualTo(G));
    }

    [Test]
    public void EarthForce()
    {
        GravitationalForce g = new GravitationalForce(Constants.Earth.MASS);

        Assert.That(g.compute(1.0), Is.EqualTo(G * Constants.Earth.MASS));
        Assert.That(g.compute(1e6), Is.EqualTo(G * Constants.Earth.MASS / 1e12));
    }

    [Test]
    public void EarthMoonForce()
    {
        GravitationalForce g = new GravitationalForce(Constants.Earth.MASS, Constants.Moon.MASS);

        double d = 370.149120e6;
        double f = G * Constants.Earth.MASS * Constants.Moon.MASS / (d*d);
        Assert.That(g.compute(d), Is.EqualTo(f));
    }
}
