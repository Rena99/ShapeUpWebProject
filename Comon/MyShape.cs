﻿using System.Collections.Generic;
using System;
using Repositories.Models;

namespace Comon
{
    public class MyShapes
    {
        public MyShapes()
        {

        }
        public MyShapes(int id, int unit, ICollection<Point> points)
        {
            Id = id;
            Unit = unit;
            Width = SetWidth(points);
            Height = SetHeight(points);
            Points = SetPoints(points);
            //Vectors = setVectors(Points);
            MinX = SetMinX(points);
            MinY = SetMinY(points);
            //LinearEquation = setEquations();
            PlacedPoints = new PointClass[Points.Length];
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
        public PointClass[] Points { get; set; }
        public PointClass[] PlacedPoints { get; set; }
        public PointClass[] SetPoints(ICollection<Point> points)
        {
            int i = 0;
            PointClass[] Points = new PointClass[points.Count];
            foreach (var item in points)
            {
                Points[i++] = new PointClass((double)(item.X) * Measure - MinX, (double)(item.Y) * Measure - MinY);
            }
            return Points;
        }
        //public double[][] LinearEquation { get; set; }
        //public double[][] setEquations()
        //{
        //    //y=mx+b
        //    //0-m, 1=b, 2=vertical
        //    double[][] equations = new double[Points.Length][];
        //    int j = 0;
        //    for (int i = 1; i < Points.Length; i++)
        //    {
        //        equations[j] = new double[3];
        //        if (Points[i - 1].X == Points[i].X)
        //        {
        //            equations[j][0] = Points[i].X;
        //            equations[j][1] = 0;
        //            equations[j++][2] = 1;
        //            continue;
        //        }
        //        equations[j][0] = (Points[i - 1].Y - Points[i].Y) / (Points[i - 1].X - Points[i].X);
        //        equations[j][1] = -(equations[j][0] * Points[i].X) + Points[i].Y;
        //        equations[j++][2] = 0;
        //    }
        //    equations[j] = new double[3];
        //    if (Points[Points.Length - 1].X == Points[0].X)
        //    {
        //        equations[j][0] = Points[0].X;
        //        equations[j][1] = 0;
        //        equations[j++][2] = 1;
        //    }
        //    else
        //    {
        //        equations[j][0] = (Points[Points.Length - 1].Y - Points[0].Y) / (Points[Points.Length - 1].X - Points[0].X);
        //        equations[j][1] =- (equations[j][0] * Points[0].X) + Points[0].Y;
        //        equations[j++][2] = 0;
        //    }
        //    return equations;
        //}
        public double MinX { get; set; }
        public double SetMinX(ICollection<Point> points)
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
        public double SetMinY(ICollection<Point> points)
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
        public int SetWidth(ICollection<Point> points)
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
        public int SetHeight(ICollection<Point> points)
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
        public PointClass PointOnArea { get; set; }
        public PointClass IndexOfPoint { get; set; }

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



    

