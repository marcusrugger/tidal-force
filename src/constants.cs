using System;


public class Constants
{
    public class Geometry
    {
        public const double TAU = 2 * Math.PI;
    }

    public class Earth
    {
        public const double MASS        = 5.972e24;  // kg
        public const double MEAN_RADIUS = 6.371e6;   // m
    }

    public class Moon
    {
        public const double MASS            =  73.42e21;       // kg
        public const double MEAN_RADIUS     =   1.7371e6;       // m
        public const double MEAN_DISTANCE   = 370.310054e6;     // m
        public const double SEMI_MAJOR_AXIS = 384.399e6;        // m
    }

    public class Sun
    {
        public const double MASS            =   1.989e30;       // kg
        public const double MEAN_DISTANCE   = 149.597870700e9;  // m
    }
}
