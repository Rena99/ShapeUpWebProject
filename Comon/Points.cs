using System;
using System.Collections.Generic;
using System.Text;

namespace Comon
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Points
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public double X;
        public double Y;

        // Constructors.
        public Points(double x, double y) { X = x; Y = y; }
        public Points() : this(double.NaN, double.NaN) { }
        public static Points operator -(Points v, Points w)
        {
            return new Points(v.X - w.X, v.Y - w.Y);
        }

        public static Points operator +(Points v, Points w)
        {
            return new Points(v.X + w.X, v.Y + w.Y);
        }

        public static double operator *(Points v, Points w)
        {
            return v.X * w.X + v.Y * w.Y;
        }

        public static Points operator *(Points v, double mult)
        {
            return new Points(v.X * mult, v.Y * mult);
        }

        public static Points operator *(double mult, Points v)
        {
            return new Points(v.X * mult, v.Y * mult);
        }

        public double Cross(Points v)
        {
            return X * v.Y - Y * v.X;
        }

        public override bool Equals(object obj)
        {
            var v = (Points)obj;
            return (X - v.X).IsZero() && (Y - v.Y).IsZero();
        }
    }
    public static class Extensions
    {
        private const double Epsilon = 1e-10;

        public static bool IsZero(this double d)
        {
            return Math.Abs(d) < Epsilon;
        }
    }
    public struct Segment2D
    {
        public Points Start { get; }
        public Points End { get; }
        public double Argument => Math.Atan2(End.Y - Start.Y, End.X - Start.X);

        public Segment2D(Points start, Points end)
        {
            Start = start;
            End = end;
        }
    }
    public struct Circle2D
    {
        private const double FullCircleAngle = 2 * Math.PI;
        public Points Center { get; }
        public double Radius { get; }

        public Circle2D(Points center, double radius)
        {
            if (radius <= 0)
                throw new ArgumentOutOfRangeException(nameof(radius));

            Center = center;
            Radius = radius;
        }
    }
}

