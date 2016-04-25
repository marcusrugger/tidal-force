using System;
using System.Collections.Generic;
using System.Linq;

class ForceVectors
{
    private readonly Func<double, double> fn;
    private readonly Cartesian centerOfGravity;
    private readonly Cartesian relativeForce;
    private readonly double scale;

    public ForceVectors(Func<double, double> fn, Cartesian centerOfGravity, Cartesian relativePt, double scale)
    {
        this.fn                 = fn;
        this.centerOfGravity    = centerOfGravity;
        this.relativeForce      = CalculateForce(relativePt);
        this.scale              = scale;
    }

    public IEnumerable<Cartesian> compute(IEnumerable<Cartesian> points)
    {
        return points.Select(CalculateRelativeForce);
    }

    private Cartesian CalculateRelativeForce(Cartesian p)
    {
        return CalculateForce(p).Subtract(relativeForce)    // Make the force relative
                                .Scale(scale);              // Scale to requested units
    }

    private Cartesian CalculateForce(Cartesian p)
    {
        return centerOfGravity.Subtract(p)      // Distance from point to the center of gravity
                              .ToPolar()        // Convert to polar coordinates
                              .TransformR(fn)   // Calculate gravitational force at point
                              .ToCartesian();   // Convert back to cartesian coordinates
    }
}
