﻿using System;
using System.Collections.Generic;
using Comon;

//test not fit

namespace Services
{
    public class Algorithm
    {
        public MyShapes area { get; set; } //area

        public List<MyShapes> shapes { get; set; } //list of shapes

        public bool Succeeded { get; set; } //true if managed to place all shapes

        public int Length { get; set; } //length of all shapes

        //constructer
        public Algorithm(List<MyShapes> myShapes, MyShapes area)
        {
            shapes = myShapes;
            this.area = area;
            Succeeded = true;
            foreach (var item in shapes)
            {
                Length += item.Width;
            }
            OrderShapes();
            Succeeded = SetShapes();
        }

        //sorts shape from hardest to place to easiest to place
        public void OrderShapes()
        {
            CompareSize s = new CompareSize();
            CompareLength l = new CompareLength();
            CompareWidth w = new CompareWidth();
            CompareArea a = new CompareArea();
            shapes.Sort(s);
            shapes.Sort(a);
            if (area.Width > area.Height)
            {
                shapes.Sort(l);
                shapes.Sort(w);
            }
            else
            {
                shapes.Sort(w);
                shapes.Sort(l);
            }
            shapes.Reverse();
        }

        //places shapes on area
        public bool SetShapes()
        {
            double sum = 0;
            bool success = true;
            foreach (var item in shapes)//checks that every shape fits
            {
                if (item.Height > area.Height || item.Width > area.Width) return false;
                sum += item.AreaOfS;
            }
            if (sum > area.AreaOfS) return false; //checks that total area of shapes is less than area of area
            MyShapes stationary = null;
            List<MyShapes> myShapes = new List<MyShapes>();
            foreach (var item in shapes) //places every shape on area
            {
                success = PlaceShape(item, ref stationary, myShapes);
                if (!success) return false;
                myShapes.Add(item); //adds shape to placed shapes
            }
            ShrinkingAlgorithm(shapes); //shrinks area
            foreach (var item in shapes)//checks if shapes fit in area
            {
                if (item.PointOnArea.X + (item.Width - item.IndexOfPoint.X) > area.Width) return false;
            }
            return success;
        }

        //places shapes
        public bool PlaceShape(MyShapes orbiting, ref MyShapes stationary, List<MyShapes> myShapes)
        {
            //step 1-place first shape
            if (stationary == null)
            {
                int indexOfF = 0;
                for (int p = 1; p < orbiting.Points.Length; p++)
                {
                    if (orbiting.Points[p].X < orbiting.Points[indexOfF].X) indexOfF = p;
                    else if (orbiting.Points[p].X == orbiting.Points[indexOfF].X && orbiting.Points[p].Y < orbiting.Points[indexOfF].Y) indexOfF = p;
                }
                PointClass indexOnA = new PointClass(0, area.Height - (area.Height - orbiting.Points[indexOfF].Y));
                PlaceOnArea(orbiting, indexOfF, indexOnA);
                stationary = orbiting;
                return true;
            }
            //step 2-place next shape
            int i = myShapes.Count-1;
            PointClass OptimalPoint = new PointClass(-1, -1);
            while (i >= 0)
            {
                PointClass pointOnA = NTFFunctions(orbiting, myShapes[i], myShapes, area);
                if (pointOnA != null)
                {
                    if (OptimalPoint.X == -1)
                    {
                        OptimalPoint = new PointClass(pointOnA.X, pointOnA.Y);
                    }
                    else if (pointOnA.X < OptimalPoint.X || pointOnA.X == OptimalPoint.X && pointOnA.Y < OptimalPoint.Y)
                    {
                        OptimalPoint = new PointClass(pointOnA.X, pointOnA.Y);
                    }
                }
                i--;
            }
            if (OptimalPoint.X == -1) return false;
            PlaceOnArea(orbiting, 0, OptimalPoint);
            //stationary = orbiting;
            return true;
        }

        private PointClass NTFFunctions(MyShapes orbiting, MyShapes stationary, List<MyShapes> myShapes, MyShapes area)
        {
            List<Pair> pairs = NFPOptions(orbiting, stationary);
            EliminateOptions(pairs, myShapes, area, orbiting, stationary); 
            if (pairs.Count == 0) return null;
            PointClass pointOnA;
            do
            {
                int optimal = GetOptimalLocation(pairs);
                pointOnA = pairs[optimal].PointOnA;
                LeftBottom(myShapes, orbiting, 0, ref pointOnA);
                PointClass[] placedPoints = GetPoints(orbiting, pointOnA, 0);
                if (placedPoints[GetMaxY(orbiting.Points)].Y > area.Height) pointOnA = new PointClass(-1, -1);
                if (pointOnA.X == -1)
                {
                    pairs.RemoveAt(optimal);
                    if (pairs.Count == 0) return null;
                }
            }while (pointOnA.X == -1);
            return pointOnA;
        }

        private int GetMaxY(PointClass[] points)
        {
            int MaxY = 0;
            for (int i = 1; i < points.Length; i++)
            {
                MaxY = points[i].Y > points[MaxY].Y ? i : MaxY;
            }
            return MaxY;

        }

        //shrinks length of area
        private void ShrinkingAlgorithm(List<MyShapes> MyShapes)
        {
            //start from rightmost shape and try to place shape more in left
            CompareYLocation yLocation = new CompareYLocation();
            CompareXLocation xLocation = new CompareXLocation();//sorts shapes by location
            MyShapes.Sort(yLocation);
            MyShapes.Sort(xLocation);
            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int index = MyShapes.Count - 1; index > 0; index--)
                {
                    List<MyShapes> helperList = new List<MyShapes>();
                    for (int k = 0; k < MyShapes.Count; k++)
                        if (k != index) helperList.Add(MyShapes[k]);
                    for (int j = 0; j < helperList.Count; j++)
                    {
                        PointClass pointOnA = NTFFunctions(MyShapes[index], helperList[j], helperList, area);
                        if (pointOnA != null)
                        {
                            if (pointOnA.X < MyShapes[index].PointOnArea.X || (pointOnA.X == MyShapes[index].PointOnArea.X && pointOnA.Y < MyShapes[index].PointOnArea.Y))
                            {
                                PlaceOnArea(MyShapes[index], 0, pointOnA);
                                changed = true;
                            }
                        }
                    }
                }
            }
        }

        //gets NFP locations
        private List<Pair> NFPOptions(MyShapes orbiting, MyShapes statinary)
        {
            List<Pair> pairs = new List<Pair>();
            for (int i = 0; i < statinary.Points.Length; i++)
            {
                for (int j = 0; j < orbiting.Points.Length; j++)
                {
                    pairs.Add(new Pair(i, j));
                }
            }
            for (int i = 0; i < pairs.Count; i++)
            {
                PointClass Trim = TrimTranslation(orbiting, statinary, pairs[i].shape, statinary.PlacedPoints[pairs[i].shape], pairs[i].Orbit);
                pairs[i].PointOnA = statinary.PlacedPoints[pairs[i].shape] - Trim;
                PointClass[] points = GetPoints(orbiting, pairs[i].PointOnA, pairs[i].Orbit);
                pairs[i].PointOnA = points[0];
                foreach (var point in points)
                {
                    PointClass p = new PointClass(Math.Round(point.X, 1), Math.Round(point.Y, 1));
                    if(!LinesDoNotIntersect(statinary.PlacedPoints, points))
                    {
                        pairs.Remove(pairs[i--]);
                        break;
                    }
                }
            }
            return pairs;
        }

        private bool LinesDoNotIntersect(PointClass[] statinaryPoints, PointClass[] orbitingPoints)
        {
            for (int i = 0; i < statinaryPoints.Length; i++)
            {
                PointClass p1 = statinaryPoints[i];
                PointClass q1 = i == statinaryPoints.Length - 1 ? statinaryPoints[0] : statinaryPoints[i + 1];
                for (int j = 0; j < orbitingPoints.Length; j++)
                {
                    PointClass p2 = orbitingPoints[j];
                    PointClass q2 = j == orbitingPoints.Length - 1 ? orbitingPoints[0] : orbitingPoints[j + 1];
                    if (LineSegementsIntersect(p1, q1, p2, q2, out PointClass intersection, false))
                    {
                        if (NotOnVertex(statinaryPoints, orbitingPoints, intersection)) return false;
                        else if (LineIsInside(p1, q1, orbitingPoints) || LineIsInside(p2, q2, statinaryPoints)) return false;
                    }
                }
            }
            for (int i = 0; i < orbitingPoints.Length; i++)
            {
                if (IsInside(statinaryPoints, statinaryPoints.Length, orbitingPoints[i], Length, null, false)) return false;
            }
            return true;
        }

        private bool LineIsInside(PointClass p1, PointClass q1, PointClass[] orbitingPoints)
        {
            double x = (p1.X > q1.X) ? (p1.X - q1.X) / 2 + q1.X : (q1.X - p1.X) / 2 + p1.X;
            double y = ((p1.Y - q1.Y) / (p1.X - q1.X) *
                (x - p1.X)) + p1.Y;
            if (p1.X == q1.X) y = (p1.Y > q1.Y) ? (p1.Y - q1.Y) / 2 + q1.Y : (q1.Y - p1.Y) / 2 + p1.Y;
            PointClass vector = new PointClass(x, y);
            if (IsInside(orbitingPoints, orbitingPoints.Length, vector, Length, null, false)) return true;
            return false;
        }

        //eliminates NFP locations
        private void EliminateOptions(List<Pair> pairs, List<MyShapes> myShapes, MyShapes area, MyShapes orbiting, MyShapes s)
        {
            for (int i = 0; i < pairs.Count; i++)
            {
                PointClass[] points = GetPoints(orbiting, pairs[i].PointOnA, 0);
                bool removed = false;
                foreach (var item in points)
                {
                    if (item.Y < 0 || item.X < 0)
                    {
                        pairs.RemoveAt(i--);
                        removed = true;
                        break;
                    }
                }
                if (!removed && !CanPlace(myShapes, points, -1, Length)) pairs.RemoveAt(i--);
            }
        }

        //gets optimal location of NFP
        private int GetOptimalLocation(List<Pair> pairs)
        {
            int optimal = 0;
            for (int i = 1; i < pairs.Count; i++)
            {
                if (pairs[i].PointOnA.X < pairs[optimal].PointOnA.X) optimal = i;
                else if (pairs[i].PointOnA.X == pairs[optimal].PointOnA.X &&
                    pairs[i].PointOnA.Y < pairs[optimal].PointOnA.Y) optimal = i;
            }
            return optimal;
        }

        private bool NotOnVertex(PointClass[] statinaryPoints, PointClass[] orbitingPoints, PointClass point)
        {
            point = new PointClass(Math.Round(point.X, 1), Math.Round(point.Y, 1));
            foreach (var item in statinaryPoints)
            {
                if (Math.Round(point.X, 0) == Math.Round(item.X, 0) && Math.Round(point.Y, 0) == Math.Round(item.Y, 0)) return false;
            }
            foreach (var item in orbitingPoints)
            {
                if (Math.Round(point.X, 0) == Math.Round(item.X, 0) && Math.Round(point.Y, 0) == Math.Round(item.Y, 0)) return false;
            }
            return true;
        }

        //places shape on area
        private void PlaceOnArea(MyShapes myShape, int indexOfF, PointClass indexOnA)
        {
            myShape.PlacedPoints = GetPoints(myShape, indexOnA, indexOfF);
            myShape.PointOnArea = indexOnA;
            myShape.IndexOfPoint = myShape.Points[indexOfF];
        }

        //moves every vertex of shape to location on which will be placed
        public PointClass[] GetPoints(MyShapes s, PointClass indexOnA, int indexOfF, PointClass trim = null)
        {
            if (trim == null) trim = new PointClass(0, 0);
            double difx = indexOnA.X - (s.Points[indexOfF].X + trim.X), diffy = indexOnA.Y - (s.Points[indexOfF].Y + trim.Y);
            PointClass[] vector = new PointClass[s.Points.Length];
            for (int i = 0; i < vector.Length; i++) vector[i] = new PointClass(s.Points[i].X + difx, s.Points[i].Y + diffy);
            return vector;
        }

        //checks if could places shape on this location
        public bool CanPlace(List<MyShapes> myShapes, PointClass[] Points, int cShape, int INF)
        {
            for (int i = 0; i < myShapes.Count; i++)
            {
                if (myShapes[i].Id == cShape) continue;
                for (int j = 0; j < Points.Length; j++)
                {
                    if (IsInside(myShapes[i].PlacedPoints, myShapes[i].PlacedPoints.Length, Points[j], INF * 2, null, false)) return false;
                    if (!LinesDoNotIntersect(myShapes[i].PlacedPoints, Points)) return false;
                }
            }
            return true;
        }

        // Given three colinear points p, q, r, the function checks if point q lies on line segment 'pr' 
        static bool OnSegment(PointClass p, PointClass q, PointClass r)
        {
            if (q.X <= Math.Max(p.X, r.X) &&
                q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) &&
                q.Y >= Math.Min(p.Y, r.Y))
            {
                return true;
            }
            return false;
        }

        // To find orientation of ordered triplet (p, q, r). 
        // The function returns following values: 0 --> p, q and r are colinear, 1 --> Clockwise, 2 --> Counterclockwise 
        static int Orientation(PointClass p, PointClass q, PointClass r)
        {
            double val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0; // colinear 
            return (val > 0) ? 1 : 2; // clock or counterclock wise 
        }

        // The function that returns true if line segment 'p1q' and 'p2q2' intersect. 
        static bool DoIntersect(PointClass p1, PointClass q1, PointClass p2, PointClass q2)
        {
            // Find the four orientations needed for general and special cases 
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4) return true;

            // Special Cases: 
            //p1, q1 and p2 are colinear and  p2 lies on segment p1q1 
            if (o1 == 0 && OnSegment(p1, p2, q1)) return true;

            // p1, q1 and p2 are colinear and  q2 lies on segment p1q1 
            if (o2 == 0 && OnSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2 
            if (o3 == 0 && OnSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2 
            if (o4 == 0 && OnSegment(p2, q1, q2)) return true;

            // Doesn't fall in any of the above cases 
            return false;
        }

        // Returns true if the point p lies inside the polygon[] with n vertices 
        static bool IsInside(PointClass[] polygon, int n, PointClass p, int INF, PointClass o = null, bool collinear = true)
        {
            PointClass extreme;
            // There must be at least 3 vertices in polygon[] 
            if (n < 3) return false;

            // Create a point for line segment from p to infinite 
            if (o != null && o.Y == p.Y) extreme = new PointClass(p.X, INF);
            else extreme = new PointClass(INF, p.Y);
            foreach (var item in polygon)
            {
                if (item.Y == p.Y && item.X >= p.X)
                {
                    extreme = new PointClass(p.X, INF);
                    break;
                }
            }

            // Count intersections of the above line with sides of polygon 
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;
                // Check if the line segment from 'p' to 'extreme' intersects with the line segment from 'polygon[i]' to 'polygon[next]' 
                if (DoIntersect(polygon[i], polygon[next], p, extreme))
                {
                    if (LineSegementsIntersect(polygon[i], polygon[next], p, extreme, out PointClass intersect, false))
                    {
                        if (intersect.X == polygon[i].X && intersect.Y == polygon[i].Y) count--;
                    }
                    // If the point 'p' is colinear with line segment 'i-next', then check if it lies on segment. If it lies, return true, otherwise false 
                    if (Orientation(polygon[i], p, polygon[next]) == 0)
                    {
                        if (collinear) return OnSegment(polygon[i], p, polygon[next]);
                        else return false;
                    }
                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise 
            return (count % 2 == 1);
        }

        //trims translation vector
        private PointClass TrimTranslation(MyShapes orbiting, MyShapes stationary, int inds, PointClass indexOnA, int indexOfP)
        {
            //moves shape to next point
            PointClass[] Points = GetPoints(orbiting, indexOnA, indexOfP);
            PointClass intersectPoint = new PointClass(0, 0);
            for (int i = 0; i < Points.Length; i++) //checks if at any point orbiting shape overlaps with stationary shape
            {
                PointClass p1 = Points[i];
                PointClass p2 = i == Points.Length - 1 ? Points[0] : Points[i + 1];
                for (int j = 0; j < stationary.Points.Length; j++)
                {
                    PointClass q1 = stationary.PlacedPoints[j];
                    PointClass q2 = j == stationary.Points.Length - 1 ? stationary.PlacedPoints[0] : stationary.PlacedPoints[j + 1];
                    PointClass vector;
                    if (LineSegementsIntersect(q1, q2, p1, p2, out vector, false))
                    {
                        if (!(Math.Round(vector.X) == Math.Round(q1.X) && Math.Round(vector.Y) == Math.Round(q2.Y)) &&
                        !(Math.Round(vector.X) == Math.Round(q2.X) && Math.Round(vector.Y) == Math.Round(q2.Y)) &&
                        !(Math.Round(vector.X) == Math.Round(p1.X) && Math.Round(vector.Y) == Math.Round(p1.Y)) &&
                        !(Math.Round(vector.X) == Math.Round(p2.X) && Math.Round(vector.Y) == Math.Round(p2.Y)))
                        {
                            PointClass realVector = IsInside(stationary.Points, stationary.Points.Length, p2, Length) ?
                                new PointClass(p2.X - vector.X, p2.Y - vector.Y) : new PointClass(q2.X - vector.X, q2.Y - vector.Y);
                            if (intersectPoint.X == 0&&intersectPoint.Y==0)
                            {
                                intersectPoint = realVector;
                                continue;
                            }
                            intersectPoint = Math.Sqrt(Math.Pow(indexOnA.X - (indexOnA.X - realVector.X), 2) + Math.Pow(indexOnA.Y - (indexOnA.Y - realVector.Y), 2)) >
                                Math.Sqrt(Math.Pow(indexOnA.X - (indexOnA.X - intersectPoint.X), 2) + Math.Pow(indexOnA.Y - (indexOnA.Y - intersectPoint.X), 2)) ?
                                intersectPoint : realVector;
                        }
                    }
                }
            }
            return intersectPoint;
        }

        // checks if an intersection point was found
        public static bool LineSegementsIntersect(PointClass p, PointClass p2, PointClass q, PointClass q2, out PointClass intersection, bool considerCollinearOverlapAsIntersect)
        {
            intersection = new PointClass();
            var r = p2 - p;
            var s = q2 - q;
            var rxs = r.Cross(s);
            var qpxr = (q - p).Cross(r);

            // If r x s = 0 and (q - p) x r = 0, then the two lines are collinear.
            if (rxs.IsZero() && qpxr.IsZero())
            {
                // 1. If either  0 <= (q - p) * r <= r * r or 0 <= (p - q) * s <= * s then the two lines are overlapping,
                if (considerCollinearOverlapAsIntersect)
                    if ((0 <= (q - p) * r && (q - p) * r <= r * r) || (0 <= (p - q) * s && (p - q) * s <= s * s)) return true;
                // 2. If neither 0 <= (q - p) * r = r * r nor 0 <= (p - q) * s <= s * s then the two lines are collinear but disjoint.
                return false;
            }

            // 3. If r x s = 0 and (q - p) x r != 0, then the two lines are parallel and non-intersecting.
            if (rxs.IsZero() && !qpxr.IsZero()) return false;

            // t = (q - p) x s / (r x s)
            var t = (q - p).Cross(s) / rxs;

            // u = (q - p) x r / (r x s)
            var u = (q - p).Cross(r) / rxs;

            // 4. If r x s != 0 and 0 <= t <= 1 and 0 <= u <= 1 the two line segments meet at the point p + t r = q + u s.
            if (!rxs.IsZero() && (0 <= t && t <= 1) && (0 <= u && u <= 1))
            {
                // We can calculate the intersection point using either t or u.
                intersection = p + t * r;

                // An intersection was found.
                return true;
            }
            // 5. Otherwise, the two line segments are not parallel but do not intersect.
            return false;
        }

        //moves shape to left-bottom most location
        private void LeftBottom(List<MyShapes> myShapes, MyShapes shape, int indexOfS, ref PointClass vector)
        {
            PointClass helperPoint = new PointClass(vector.X, vector.Y);
            //places shape in current optimal location
            bool changed = true;
            while (changed)
            {
                PointClass[] Points = GetPoints(shape, vector, indexOfS);
                int MaxY = GetMaxY(Points);
                double CountX = 0, CountY = 0; //saves distance that can move shape
                PointClass HelperVector = new PointClass(Points[0].X, Points[0].Y);
                for (int i = 1; i < Points.Length; i++)//gets smallest points of the shape 
                {
                    if (Points[i].X < HelperVector.X) HelperVector.X = Points[i].X;
                    if (Points[i].Y < HelperVector.Y) HelperVector.Y = Points[i].Y;
                }
                MoveLeft(HelperVector, ref CountX, Points, myShapes);
                changed = MoveDown(HelperVector, ref CountY, Points, myShapes);
                helperPoint -= new PointClass(Math.Round(CountX, 1), Math.Round(CountY, 1));
                Points = GetPoints(shape, helperPoint, indexOfS);
                if (Points[MaxY].Y > area.Height)
                {
                    CountY = CountX = 0;
                    Points = GetPoints(shape, vector, indexOfS);
                    changed = MoveDown(HelperVector, ref CountY, Points, myShapes);
                }
                vector -= new PointClass(Math.Round(CountX, 1), Math.Round(CountY, 1));
                vector = new PointClass(Math.Round(vector.X, 1), Math.Round(vector.Y, 1));
            } 
        }

        private bool MoveDown(PointClass helperVector, ref double CountY, PointClass[] Points, List<MyShapes> myShapes)
        {
            bool b = true;
            while (b && helperVector.Y - CountY >= 0)//gets distance that can move y
            {
                CountY += .1;
                Math.Round(CountY, 1);
                for (int i = 0; i < Points.Length; i++) Points[i].Y -= .1;
                if (!CanMove(myShapes, Points))
                {
                    b = false;
                    CountY -= .1;
                    break;
                }
            }
            if (b) CountY -= .1;
            if (CountY != 0) return true;
            return false;
        }

        private void MoveLeft(PointClass helperVector, ref double CountX, PointClass[] Points, List<MyShapes> myShapes)
        {
            bool b = true;
            while (b && helperVector.X - CountX >= 0)  //gets distance that can move left
            {
                CountX += .1;
                Math.Round(CountX, 1);
                for (int i = 0; i < Points.Length; i++) Points[i].X -= .1;
                if (!CanMove(myShapes, Points))
                {
                    b = false;
                    CountX -= .1;
                    break;
                }
            }
            if (b) CountX -= .1;
        }

        //checks if shape can be moved to specific location
        public bool CanMove(List<MyShapes> myShapes, PointClass[] Points)
        {
            // PointClass p1, q1, p2, q2;
            for (int i = 0; i < myShapes.Count; i++)
            {
                if (!LinesDoNotIntersect(myShapes[i].PlacedPoints, Points))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
