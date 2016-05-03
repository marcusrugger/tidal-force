using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Flatland;


[TestFixture]
class ForcePointsTest
{
    [Test]
    public void Simple()
    {
        ForcePoints generator = new ForcePoints(4, 1.0);
        var pts = generator.compute();

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
            Assert.That(Math.Abs(z.Item1.X - z.Item2.X), Is.LessThan(0.000001));
            Assert.That(Math.Abs(z.Item1.Y - z.Item2.Y), Is.LessThan(0.000001));
        }
    }
}
