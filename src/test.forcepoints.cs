using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


[TestFixture]
class ForcePointsTest
{
    [Test]
    public void Simple()
    {
        ForcePoints generator = new ForcePoints(1.0);
        var pts = generator.compute(4);

        Cartesian[] expected = new Cartesian[]
        {
            new Cartesian( 1.0,  0.0),
            new Cartesian( 0.0,  1.0),
            new Cartesian(-1.0,  0.0),
            new Cartesian( 0.0, -1.0)
        };

        Assert.That(pts.Count(), Is.EqualTo(expected.Length));
        var zip = Enumerable.Zip(pts, expected, (p, e) => Tuple.Create(p, e));
        foreach (var z in zip)
        {
            Assert.That(Math.Abs(z.Item1.x - z.Item2.x), Is.LessThan(0.000001));
            Assert.That(Math.Abs(z.Item1.y - z.Item2.y), Is.LessThan(0.000001));
        }
    }
}
