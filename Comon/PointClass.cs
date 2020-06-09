using System;
using System.Collections.Generic;
using System.Text;

namespace Comon
{
    public class PointClass
    {
        public double X;
        public double Y;

        // Constructors.
        public PointClass(double x, double y) { X = x; Y = y; }
        public PointClass() : this(double.NaN, double.NaN) { }

        //public static bool operator ==(PointClass v, PointClass w)
        //{
        //    return v.X == w.X&& v.Y == w.Y;
        //}
        //public static bool operator !=(PointClass v, PointClass w)
        //{
        //    return v.X != w.X || v.Y != w.Y;
        //}
        public static PointClass operator -(PointClass v, PointClass w)
        {
            return new PointClass(v.X - w.X, v.Y - w.Y);
        }

        public static PointClass operator +(PointClass v, PointClass w)
        {
            return new PointClass(v.X + w.X, v.Y + w.Y);
        }

        public static double operator *(PointClass v, PointClass w)
        {
            return v.X * w.X + v.Y * w.Y;
        }

        public static PointClass operator *(PointClass v, double mult)
        {
            return new PointClass(v.X * mult, v.Y * mult);
        }

        public static PointClass operator *(double mult, PointClass v)
        {
            return new PointClass(v.X * mult, v.Y * mult);
        }

        public double Cross(PointClass v)
        {
            return X * v.Y - Y * v.X;
        }

        public override bool Equals(object obj)
        {
            var v = (PointClass)obj;
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
        public PointClass Start { get; }
        public PointClass End { get; }
        public double Argument => Math.Atan2(End.Y - Start.Y, End.X - Start.X);

        public Segment2D(PointClass start, PointClass end)
        {
            Start = start;
            End = end;
        }
    }
    public struct Circle2D
    {
        private const double FullCircleAngle = 2 * Math.PI;
        public PointClass Center { get; }
        public double Radius { get; }

        public Circle2D(PointClass center, double radius)
        {
            if (radius <= 0)
                throw new ArgumentOutOfRangeException(nameof(radius));

            Center = center;
            Radius = radius;
        }
    }
}

