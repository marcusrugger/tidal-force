using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


[TestFixture]
class ForceVectorsTest
{
    [Test]
    public void Simple()
    {
        Cartesian source   = new Cartesian(100.0, 0.0);
        Cartesian relative = new Cartesian(  0.0, 0.0);

        Cartesian[] points = new Cartesian[]
        {
            new Cartesian( 0.0,  0.0),
            new Cartesian( 1.0,  0.0),
            new Cartesian( 0.0,  1.0),
            new Cartesian(-1.0,  0.0),
            new Cartesian( 0.0, -1.0),
            new Cartesian( 1.0,  1.0),
            new Cartesian(-1.0,  1.0),
            new Cartesian(-1.0, -1.0),
            new Cartesian( 1.0, -1.0)
        };

        Cartesian[] expected = new Cartesian[]
        {
            new Cartesian( 0.0,  0.0),
            new Cartesian(-6.0,  0.0),
            new Cartesian( 0.0, -6.0),
            new Cartesian( 6.0,  0.0),
            new Cartesian( 0.0,  6.0),
            new Cartesian(-6.0, -6.0),
            new Cartesian( 6.0, -6.0),
            new Cartesian( 6.0,  6.0),
            new Cartesian(-6.0,  6.0)
        };

        ForceVectors fv = new ForceVectors(n => 3*n, source, relative, 2.0);
        IEnumerable<Cartesian> vectors = fv.compute(points);

        Assert.That(vectors.Count(), Is.EqualTo(points.Length));

        var zip = Enumerable.Zip(vectors, expected, (v, e) => Tuple.Create(v, e));
        foreach (var z in zip) ComparePoint(z.Item1, z.Item2);
    }

    private void ComparePoint(Cartesian pt, Cartesian expected)
    {
        Assert.That(Math.Abs(pt.x - expected.x), Is.LessThan(1e-6));
        Assert.That(Math.Abs(pt.y - expected.y), Is.LessThan(1e-6));
    }
}
