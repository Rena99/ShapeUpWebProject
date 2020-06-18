using System.Collections.Generic;
using System;

namespace Comon
{
    public class MyShapes
    {
        public MyShapes()
        {

        }
        public MyShapes(int id, int unit, ICollection<PointDTO> points)
        {
            Id = id;
            Unit = unit;
            Width = SetWidth(points);
            Height = SetHeight(points);
            Points = SetPoints(points);
            MinX = SetMinX(points);
            MinY = SetMinY(points);
            PlacedPoints = new Points[Points.Length];
        }
        public int Id { get; set; }
        public int Unit { get; set; }
        public double Measure { get
            {
                switch (Unit)
                {
                    case 1: return 1 * 1000000;//kilometre
                    case 2: return 1 * 10;//centimetre
                    case 3: return 1 * 1000;//metre
                    case 4: return 1 * 1609344;//mile
                    case 5: return 1 * 914.4000;//yard
                    case 6: return 1 * 304.8000;//foot
                    case 7: return 1 * 25.40000;//inch
                    default: return 1; //millimeter
                }
            }
        }
        public Points[] Points { get; set; }
        public Points[] PlacedPoints { get; set; }
        public Points[] SetPoints(ICollection<PointDTO> points)
        {
            int i = 0;
            Points[] Points = new Points[points.Count];
            foreach (var item in points)
            {
                Points[i++] = new Points((double)(item.X) * Measure - MinX, (double)(item.Y) * Measure - MinY);
            }
            return Points;
        }
        
        public double MinX { get; set; }
        public double SetMinX(ICollection<PointDTO> points)
        {
            decimal minx = -1;
            foreach (var item in points)
            {
                if (minx == -1) { minx = item.X; }
                else if (item.X < minx) minx = item.X;
            }
            return (double)minx * Measure;

        }
        public double MinY { get; set; }
        public double SetMinY(ICollection<PointDTO> points)
        {
            decimal miny = -1;
            foreach (var item in points)
            {
                if (miny == -1) { miny = item.Y; }
                else if (item.Y < miny) miny = item.Y;
            }
            return (double)miny * Measure;
        }
        public double Size
        {
            get
            {
                return Width * Height;
            }
        }
        public int Width { get; set; }
        public int SetWidth(ICollection<PointDTO> points)
        {
            decimal x = Int16.MaxValue, x2 = 0;
            foreach (var item in points)
            {
                if (item.X < x) x = item.X;
                if (item.X > x2) x2 = item.X;
            }
            return (int)(x2-x) * (int)Measure;
        }
        public int Height { get; set; }
        public int SetHeight(ICollection<PointDTO> points)
        {
            decimal y = Int16.MaxValue, y2 = 0;
            foreach (var item in points)
            {
                if (item.Y < y) y = item.Y;
                if (item.Y > y2) y2 = item.Y;
            }
            return (int)(y2-y) * (int)Measure;
        }
        public double AreaOfS
        {
            get
            {
                double one = 0, two = 0;
                for (int i = 0; i < Points.Length; i++)
                {
                    if (i + 1 < Points.Length)
                    {
                        one += Points[i].X * Points[i + 1].Y;
                        two += Points[i].Y * Points[i + 1].X;
                    }
                    else
                    {
                        one += Points[i].X * Points[0].Y;
                        two += Points[i].Y * Points[0].X;
                    }
                }
                return (one - two) / 2;
            }
        }
        public Points PointOnArea { get; set; }
        public Points IndexOfPoint { get; set; }

        //public VectorClass[] Vectors { get; set; }

        //public VectorClass[] setVectors(PointClass[] points)
        //{
        //    VectorClass[] vectors = new VectorClass[points.Length];
        //    for (int i = 0; i < points.Length; i++)
        //    {
        //        int j = i == points.Length - 1 ? 0 : i + 1;
        //        vectors[i] = new VectorClass(points[i], points[j]);
        //    }
        //    return vectors;
        //}
    }
}



    

