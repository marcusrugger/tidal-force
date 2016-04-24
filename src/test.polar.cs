using NUnit.Framework;


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
}
