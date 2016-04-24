using NUnit.Framework;
using System;


[TestFixture]
class AlgorithmsTest
{
    [Test]
    public void Zero()
    {
        Assert.That(Algorithms.ToRadians(0.0), Is.EqualTo(0.0));
        Assert.That(Algorithms.ToDegrees(0.0), Is.EqualTo(0.0));
    }

    [Test]
    public void PositiveToRadians()
    {
        IsApproximatelyEqual( Algorithms.ToRadians( 30.0), 1.0 * Math.PI / 6.0 );
        IsApproximatelyEqual( Algorithms.ToRadians( 45.0), 1.0 * Math.PI / 4.0 );
        IsApproximatelyEqual( Algorithms.ToRadians( 90.0), 1.0 * Math.PI / 2.0 );
        IsApproximatelyEqual( Algorithms.ToRadians(135.0), 3.0 * Math.PI / 4.0 );
        IsApproximatelyEqual( Algorithms.ToRadians(180.0), 1.0 * Math.PI / 1.0 );
        IsApproximatelyEqual( Algorithms.ToRadians(225.0), 5.0 * Math.PI / 4.0 );
        IsApproximatelyEqual( Algorithms.ToRadians(270.0), 3.0 * Math.PI / 2.0 );
        IsApproximatelyEqual( Algorithms.ToRadians(360.0), 2.0 * Math.PI / 1.0 );
    }

    [Test]
    public void PositiveToDegrees()
    {
        IsApproximatelyEqual( Algorithms.ToDegrees(1.0 * Math.PI / 6.0),  30.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(1.0 * Math.PI / 4.0),  45.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(1.0 * Math.PI / 2.0),  90.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(3.0 * Math.PI / 4.0), 135.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(1.0 * Math.PI / 1.0), 180.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(5.0 * Math.PI / 4.0), 225.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(3.0 * Math.PI / 2.0), 270.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(2.0 * Math.PI / 1.0), 360.0 );
    }

    [Test]
    public void NegativeToRadians()
    {
        IsApproximatelyEqual( Algorithms.ToRadians(- 30.0), -1.0 * Math.PI / 6.0 );
        IsApproximatelyEqual( Algorithms.ToRadians(- 45.0), -1.0 * Math.PI / 4.0 );
        IsApproximatelyEqual( Algorithms.ToRadians(- 90.0), -1.0 * Math.PI / 2.0 );
        IsApproximatelyEqual( Algorithms.ToRadians(-135.0), -3.0 * Math.PI / 4.0 );
        IsApproximatelyEqual( Algorithms.ToRadians(-180.0), -1.0 * Math.PI / 1.0 );
        IsApproximatelyEqual( Algorithms.ToRadians(-225.0), -5.0 * Math.PI / 4.0 );
        IsApproximatelyEqual( Algorithms.ToRadians(-270.0), -3.0 * Math.PI / 2.0 );
        IsApproximatelyEqual( Algorithms.ToRadians(-360.0), -2.0 * Math.PI / 1.0 );
    }

    [Test]
    public void NegativeToDegrees()
    {
        IsApproximatelyEqual( Algorithms.ToDegrees(-1.0 * Math.PI / 6.0), - 30.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(-1.0 * Math.PI / 4.0), - 45.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(-1.0 * Math.PI / 2.0), - 90.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(-3.0 * Math.PI / 4.0), -135.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(-1.0 * Math.PI / 1.0), -180.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(-5.0 * Math.PI / 4.0), -225.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(-3.0 * Math.PI / 2.0), -270.0 );
        IsApproximatelyEqual( Algorithms.ToDegrees(-2.0 * Math.PI / 1.0), -360.0 );
    }

    public bool IsApproximatelyEqual(double a, double b, double fudge = 0.000001)
    {
        return Math.Abs(a - b) < fudge;
    }
}
