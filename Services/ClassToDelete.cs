using Comon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Services
{
    public class Pair
    {
        public int shape { get; set; }
        public int Orbit { get; set; }
        public PointClass PointOnA { get; set; }
        public Pair()
        {

        }
        public Pair(int a, int b)
        {
            shape = a;
            Orbit = b;
        }
    }
    class ClassToDelete
    {
        public ClassToDelete(MyShapes shape, MyShapes orbiting)
        {
           // List<PointClass> result = MinkowskiSum(shape.Points, orbiting.Points);
        }

        ////gets correct translation vector
        //private int GetCorrectVector(int[][] PossibleMoves, int PCorrectVector, int POCorrectVector, MyShapes s, MyShapes shap)
        //{
        //    int c = 0, i, corr = 0;
        //    for (i = 0; i < 4; i++) //checks if there are possibilities
        //    {
        //        if (PossibleMoves[i][3] != -1)
        //        {
        //            c++;
        //            corr = i;
        //        }
        //    }
        //    if (c == 0) return -1;
        //    else if (c > 1)
        //    {
        //        c = -1;
        //        for (i = 0; i < 4; i++)
        //        {
        //            if (PossibleMoves[i][3] != -1 && c == -1) //compares all Points to get optimal one
        //            {
        //                c = i;
        //                if (PCorrectVector == -1) break;
        //                continue;
        //            }
        //            int dp = 0; //distance from previous vector 
        //            int dn = 0; //distance from current vector 
        //            int comp = 0; //helper variable
        //            if (PossibleMoves[i][3] == 0) //stationary translation
        //            {
        //                int j = PossibleMoves[i][7];
        //                while (j != PCorrectVector)
        //                {
        //                    if (j == s.Points.Length)
        //                    {
        //                        j = 0;
        //                        continue;
        //                    }
        //                    dn++;
        //                    j++;
        //                }
        //                j = PossibleMoves[c][7];
        //                while (j != PCorrectVector)
        //                {
        //                    if (j == 0)
        //                    {
        //                        j = s.Points.Length - 1;
        //                        continue;
        //                    }
        //                    comp++;
        //                    j--;
        //                }
        //                dn = comp > dn ? dn : comp;
        //                j = PossibleMoves[i][7];
        //                while (j != POCorrectVector)
        //                {
        //                    if (j == shap.Points.Length)
        //                    {
        //                        j = 0;
        //                        continue;
        //                    }
        //                    dp++;
        //                    j++;
        //                }
        //                j = PossibleMoves[c][7];
        //                while (j != POCorrectVector)
        //                {
        //                    if (j == 0)
        //                    {
        //                        j = shap.Points.Length - 1;
        //                        continue;
        //                    }
        //                    comp++;
        //                    j--;
        //                }
        //                dp = comp > dn ? dn : comp;
        //            }
        //            if (PossibleMoves[i][3] == 1) //orbiting translation
        //            {
        //                int j = PossibleMoves[i][7];
        //                while (j != PCorrectVector)
        //                {
        //                    if (j == s.Points.Length)
        //                    {
        //                        j = 0;
        //                        continue;
        //                    }
        //                    dn++;
        //                    j++;
        //                }
        //                j = PossibleMoves[c][8];
        //                while (j != PCorrectVector)
        //                {
        //                    if (j == 0)
        //                    {
        //                        j = s.Points.Length - 1;
        //                        continue;
        //                    }
        //                    comp++;
        //                    j--;
        //                }
        //                dn = comp > dn ? dn : comp;
        //                j = PossibleMoves[i][8];
        //                while (j != POCorrectVector)
        //                {
        //                    if (j == shap.Points.Length)
        //                    {
        //                        j = 0;
        //                        break;
        //                    }
        //                    dp++;
        //                    j++;
        //                }
        //                j = PossibleMoves[c][8];
        //                while (j != POCorrectVector)
        //                {
        //                    if (j == 0)
        //                    {
        //                        j = shap.Points.Length - 1;
        //                        continue;
        //                    }
        //                    comp++;
        //                    j--;
        //                }
        //                dp = comp > dn ? dn : comp;
        //                c = dp > dn ? c : i;
        //                dp = 0;
        //            }
        //        }
        //        corr = c;
        //    }
        //    return corr;
        //}
        //saves optimal location of shape
        //private void SaveOptimalLocation(MyShapes statinary, PointClass SE, PointClass OE, MyShapes shape, int indexOfO, int indexOfS, ref PointClass OptimalA, ref int OptimalO, List<MyShapes> myShapes)
        //{
        //    PointClass staticp = SE.X == -1 ? statinary.PlacedPoints[indexOfS] :
        //           new PointClass(statinary.PlacedPoints[indexOfS].X + SE.X, statinary.PlacedPoints[indexOfS].Y + SE.Y); //gets location to place shape
        //    double below, above, left;
        //    PointClass orbitp = new PointClass(0, 0);
        //    if (OE.X == -1)
        //    {
        //        below = shape.Points[indexOfO].Y;
        //        above = shape.Height - shape.Points[indexOfO].Y;
        //        left = shape.Points[indexOfO].X;
        //    }
        //    else
        //    {
        //        below = OE.Y;
        //        above = shape.Height - OE.Y;
        //        left = OE.X;
        //        orbitp = OE;
        //    }
        //    PointClass[] Points = GetPoints(shape, staticp, indexOfO, orbitp);
        //    //checks that doesn't overlap with any shape or doesn't sit completely on the designated area
        //    if (CanPlace(myShapes, Points, statinary.Id, Length) && staticp.Y - below >= 0 && staticp.Y + above < area.Height && staticp.X - left >= 0)
        //    {
        //        if (staticp.X < OptimalA.X)
        //        {
        //            OptimalA = staticp;
        //            OptimalO = indexOfO;
        //        }
        //        else if (staticp.X == OptimalA.X && staticp.Y < OptimalA.Y)
        //        {
        //            OptimalA = staticp;
        //            OptimalO = indexOfO;
        //        }
        //        else if (OptimalO != -1)
        //        {
        //            if (staticp.X - (staticp.X + Points[OptimalO].X) < OptimalA.X)
        //            {
        //                OptimalA = staticp;
        //                OptimalO = indexOfO;
        //            }
        //            else if (staticp.X == OptimalA.X && staticp.Y + (staticp.Y - Points[OptimalO].Y) < OptimalA.Y)
        //            {
        //                OptimalA = staticp;
        //                OptimalO = indexOfO;
        //            }
        //        }
        //    }
        //}


        //NFP



        ////NFP
        //public void EliminatesTransPoints(ref int[][] PossibleMoves, MyShapes s, MyShapes shap, int indexOfO, int indexOfS)
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        int p = PossibleMoves[i][3];
        //        if (PossibleMoves[i][3] == -1) continue;
        //        else
        //        {
        //            PointClass[] Points = GetPoints(shap, s.PlacedPoints[indexOfS], indexOfO);
        //            PointClass distanceTrans = Points[PossibleMoves[i][4]] - Points[indexOfO];
        //            PointClass vector = new PointClass();
        //            int si;
        //            if (PossibleMoves[i][0] == 0)
        //            {
        //                si = indexOfS == s.Points.Length - 1 ? 0 : indexOfS + 1;
        //            }
        //            else
        //            {
        //                si = indexOfS == 0 ? s.Points.Length - 1 : indexOfS - 1;
        //            }
        //            int oi;
        //            if (PossibleMoves[i][1] == 0)
        //            {
        //                oi = indexOfO == shap.Points.Length - 1 ? 0 : indexOfO + 1;
        //            }
        //            else
        //            {
        //                oi = indexOfO == 0 ? shap.Points.Length - 1 : indexOfO - 1;
        //            }
        //            if (CheckBasicVector(Points, s, shap, PossibleMoves[i][3], indexOfS, si, indexOfO, oi) &&
        //               CheckVectorAgain(Points, s, PossibleMoves[i][3], indexOfS, indexOfO))
        //            {
        //                PossibleMoves[i][3] = -1;
        //                continue;
        //            }
        //            Points = GetPoints(shap, s.PlacedPoints[PossibleMoves[i][5]], indexOfO);
        //            for (int j = 0; j < Points.Length; j++) Points[j] -= distanceTrans;
        //            if (PossibleMoves[i][3] == 1)
        //            {
        //                if ((PossibleMoves[i][4] < indexOfO && indexOfO != Points.Length - 1) || (PossibleMoves[i][4] == Points.Length - 1 && indexOfO == 0)) PossibleMoves[i][3] = -1;
        //                else if (IsInside(s.PlacedPoints, s.PlacedPoints.Length, Points[indexOfO], Length + area.Width, Points[PossibleMoves[i][4]]))
        //                {
        //                    int j = 0;
        //                    for (; j < s.PlacedPoints.Length; j++)
        //                    {
        //                        if (Points[indexOfO].X == s.PlacedPoints[j].X &&
        //                            Points[indexOfO].Y == s.PlacedPoints[j].Y) break;
        //                    }
        //                    if (j == s.PlacedPoints.Length) PossibleMoves[i][3] = -1;
        //                }
        //                else
        //                {
        //                    for (int j = 0; j < Points.Length; j++)
        //                    {
        //                        int k = j == Points.Length - 1 ? 0 : j + 1;
        //                        if (LineSegementsIntersect(Points[j], Points[k], s.PlacedPoints[indexOfS], s.PlacedPoints[si], out vector, false) &&
        //                            vector.X != s.PlacedPoints[indexOfS].X && vector.Y != s.PlacedPoints[indexOfS].Y &&
        //                            vector.X != s.PlacedPoints[si].X && vector.Y != s.PlacedPoints[si].Y)
        //                        {
        //                            PossibleMoves[i][3] = -1;
        //                            break;
        //                        }
        //                    }
        //                    for (int j = 0; j < s.PlacedPoints.Length; j++)
        //                    {
        //                        int k = j == s.PlacedPoints.Length - 1 ? 0 : j + 1;
        //                        if (LineSegementsIntersect(s.PlacedPoints[j], s.PlacedPoints[k], Points[indexOfO], Points[oi], out vector, false) &&
        //                            vector.X != Points[indexOfO].X && vector.Y != Points[indexOfO].Y &&
        //                            vector.X != Points[oi].X && vector.Y != Points[oi].Y)
        //                        {
        //                            PossibleMoves[i][3] = -1;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            else if (PossibleMoves[i][3] == 0)
        //            {
        //                if ((PossibleMoves[i][5] < indexOfS && indexOfS != s.Points.Length - 1) || (PossibleMoves[i][5] == s.Points.Length - 1 && indexOfS == 0)) PossibleMoves[i][3] = -1;
        //                if (IsInside(Points, Points.Length, s.PlacedPoints[indexOfS], Length, s.PlacedPoints[PossibleMoves[i][5]]))
        //                {
        //                    int j = 0;
        //                    for (; j < Points.Length; j++)
        //                    {
        //                        if (Points[j].X == s.PlacedPoints[indexOfS].X &&
        //                            Points[j].Y == s.PlacedPoints[indexOfS].Y) break;
        //                    }
        //                    if (j == Points.Length) PossibleMoves[i][3] = -1;
        //                }
        //                else
        //                {
        //                    for (int j = 0; j < s.PlacedPoints.Length; j++)
        //                    {
        //                        int k = j == s.PlacedPoints.Length - 1 ? 0 : j + 1;
        //                        if (LineSegementsIntersect(s.PlacedPoints[j], s.PlacedPoints[k], Points[indexOfO], Points[oi], out vector, false) &&
        //                            vector.X != Points[indexOfO].X && vector.Y != Points[indexOfO].Y &&
        //                            vector.X != Points[oi].X && vector.Y != Points[oi].Y)
        //                        {
        //                            PossibleMoves[i][3] = -1;
        //                            break;
        //                        }
        //                    }
        //                    for (int j = 0; j < Points.Length; j++)
        //                    {
        //                        int k = j == Points.Length - 1 ? 0 : j + 1;
        //                        if (LineSegementsIntersect(Points[j], Points[k], s.PlacedPoints[indexOfS], s.PlacedPoints[si], out vector, false) &&
        //                            vector.X != s.PlacedPoints[indexOfS].X && vector.Y != s.PlacedPoints[indexOfS].Y &&
        //                            vector.X != s.PlacedPoints[si].X && vector.Y != s.PlacedPoints[si].Y)
        //                        {
        //                            PossibleMoves[i][3] = -1;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            if (PossibleMoves[i][3] != -1)
        //            {
        //                int c = 0;
        //                for (int j = 0; j < Points.Length; j++)
        //                {
        //                    if (IsInside(s.PlacedPoints, s.PlacedPoints.Length, Points[j], Length + area.Width)) c++;
        //                }
        //                if (c == Points.Length) PossibleMoves[i][3] = -1;
        //            }
        //            if (PossibleMoves[i][3] == -1)
        //            {
        //                for (int j = 0; j < 4; j++)
        //                {
        //                    if (PossibleMoves[j][3] == p && p == 1 && PossibleMoves[i][1] == PossibleMoves[j][1]) PossibleMoves[j][3] = -1;
        //                    else if (PossibleMoves[j][3] == p && p == 0 && PossibleMoves[i][0] == PossibleMoves[j][0]) PossibleMoves[j][3] = -1;
        //                }
        //            }
        //        }
        //    }

        //}

        ////NFP
        //private bool CheckVectorAgain(PointClass[] Points, MyShapes s, int v, int indexOfS, int indexOfO)
        //{
        //    int nextS, nextO, prevS, prevO;
        //    nextS = indexOfS == s.Points.Length - 1 ? 0 : indexOfS + 1;
        //    nextO = indexOfO == Points.Length - 1 ? 0 : indexOfO + 1;
        //    prevS = indexOfS == 0 ? s.Points.Length - 1 : indexOfS - 1;
        //    prevO = indexOfO == 0 ? Points.Length - 1 : indexOfO - 1;
        //    bool b = true;
        //    if (v == 1)
        //    {
        //        b = ((s.Points[nextS].X == s.Points[indexOfS].X ||
        //       s.Points[prevS].X == s.Points[indexOfS].X) && Points[nextO].X == Points[indexOfO].X) ? false : b;
        //        b = ((s.Points[nextS].Y == s.Points[indexOfS].Y || s.Points[prevS].Y == s.Points[indexOfS].Y) &&
        //       Points[nextO].Y == Points[indexOfO].Y) ? false : b;
        //        b = (Math.Round((s.Points[nextS].Y - s.Points[indexOfS].Y) / (s.Points[nextS].X - s.Points[indexOfS].X), 1)) ==
        //            (Math.Round((Points[nextO].Y - Points[indexOfO].Y) / (Points[nextO].X - Points[indexOfO].X), 1)) ? false : b;
        //        b = (Math.Round((s.Points[prevS].Y - s.Points[indexOfS].Y) / (s.Points[prevS].X - s.Points[indexOfS].X), 1)) ==
        //            (Math.Round((Points[nextO].Y - Points[indexOfO].Y) / (Points[nextO].X - Points[indexOfO].X), 1)) ? false : b;
        //    }
        //    else
        //    {
        //        b = (s.Points[nextS].Y == s.Points[indexOfS].Y &&
        //               (Points[nextO].Y == Points[indexOfO].Y || Points[prevO].Y == Points[indexOfO].Y)) ? false : b;
        //        b = (s.Points[nextS].X == s.Points[indexOfS].X &&
        //               (Points[nextO].X == Points[indexOfO].X || Points[prevO].X == Points[indexOfO].X)) ? false : b;
        //        b = (Math.Round((Points[nextO].Y - Points[indexOfO].Y) / (Points[nextO].X - Points[indexOfO].X), 1)) ==
        //           (Math.Round((s.Points[nextS].Y - s.Points[indexOfS].Y) / (s.Points[nextS].X - s.Points[indexOfS].X), 1)) ? false : b;
        //        b = (Math.Round((Points[prevO].Y - Points[indexOfO].Y) / (Points[prevO].X - Points[indexOfO].X), 1)) ==
        //            (Math.Round((s.Points[nextS].Y - s.Points[indexOfS].Y) / (s.Points[nextS].X - s.Points[indexOfS].X), 1)) ? false : b;
        //    }
        //    return b;
        //}

        ////NFP
        //private bool CheckBasicVector(PointClass[] Points, MyShapes s, MyShapes shap, int v, int indexOfS, int si, int indexOfO, int oi)
        //{
        //    double x = 0;
        //    double y = 0;
        //    List<MyShapes> shapes = new List<MyShapes>();
        //    shapes.Add(s);
        //    if (v == 1)
        //    {
        //        if (Points[oi].X > Points[indexOfO].X) x = .1;
        //        else if (Points[oi].X < Points[indexOfO].X) x = -.1;
        //        y = (Points[oi].Y - Points[indexOfO].Y) / (Points[oi].X - Points[indexOfO].X) *
        //            (Points[indexOfO].X + x - Points[oi].X) + Points[oi].Y;
        //        y = Points[indexOfO].Y - y;
        //        if (x == 0)
        //        {
        //            if (Points[oi].Y < Points[indexOfO].Y) y = .1;
        //            else y = -.1;
        //        }
        //    }
        //    else
        //    {
        //        if (s.PlacedPoints[si].X > s.PlacedPoints[indexOfS].X) x = -.1;
        //        else if (s.PlacedPoints[si].X < s.PlacedPoints[indexOfS].X) x = .1;
        //        y = (s.PlacedPoints[si].Y - s.PlacedPoints[indexOfS].Y) / (s.PlacedPoints[si].X - s.PlacedPoints[indexOfS].X) *
        //           (s.PlacedPoints[indexOfS].X + x - s.PlacedPoints[si].X) + s.PlacedPoints[si].Y;
        //        y = s.PlacedPoints[indexOfS].Y - y;
        //        if (x == 0)
        //        {
        //            if (s.PlacedPoints[si].Y > s.PlacedPoints[indexOfS].Y) y = .1;
        //            else y = -.1;
        //        }
        //    }
        //    PointClass vector = new PointClass(x, y * -1);
        //    for (int i = 0; i < Points.Length; i++) Points[i] -= vector;
        //    if (!CanMove(shapes, Points, v != 1, v == 1)) return true;
        //    for (int i = 0; i < Points.Length; i++) Points[i] += vector;
        //    return false;
        //}
        //gets linear equation of line
        //public double[] GetEquation(PointClass a, PointClass b)
        //{
        //    double[] res = new double[3];
        //    if (a.X == b.X)
        //    {
        //        res[0] = a.X;
        //        res[1] = 0;
        //        res[2] = 1;
        //    }
        //    else
        //    {
        //        res[0] = (b.Y-a.Y) / (b.X-a.X);
        //        res[1] = -(res[0] * a.X) + a.Y;
        //        res[2] = 0;
        //    }
        //    return res;
        //}
        //for (int j = 0; j < myShapes[i].PlacedPoints.Length; j++)
        //{
        //    p1 = myShapes[i].PlacedPoints[j];
        //    q1 = j == myShapes[i].PlacedPoints.Length - 1 ?
        //        myShapes[i].PlacedPoints[0] : myShapes[i].PlacedPoints[j + 1];
        //    for (int k = 0; k < Points.Length; k++)
        //    {
        //        p2 = Points[k];
        //        q2 = k == Points.Length - 1 ?
        //            Points[0] : Points[k + 1];
        //        if (LineSegementsIntersect(p1, q1, p2, q2, out PointClass intersect, false))
        //        {
        //            if (s && o && !(intersect.X == p1.X && intersect.Y == p1.Y) &&
        //                !(Math.Round(intersect.X, 1) == Math.Round(q1.X, 1) &&
        //                Math.Round(intersect.Y, 1) == Math.Round(q1.Y, 1)) &&
        //                !(Math.Round(intersect.X, 1) == Math.Round(p1.X, 1) &&
        //                Math.Round(intersect.Y, 1) == Math.Round(p1.Y, 1)) &&
        //                !(Math.Round(intersect.X, 1) == Math.Round(q2.X, 1) &&
        //                Math.Round(intersect.Y, 1) == Math.Round(q2.Y, 1)) &&
        //                !(intersect.X == p2.X && intersect.Y == p2.Y) &&
        //                !(Math.Round(intersect.X, 1) == Math.Round(p2.X, 1) &&
        //                Math.Round(intersect.Y, 1) == Math.Round(p2.Y, 1)))
        //            {
        //                return false;
        //            }
        //            else if (o && !s && !(intersect.X == p1.X && intersect.Y == p1.Y) &&
        //                !(Math.Round(intersect.X, 1) == Math.Round(q1.X, 1) &&
        //                Math.Round(intersect.Y, 1) == Math.Round(q1.Y, 1)) &&
        //                !(Math.Round(intersect.X, 1) == Math.Round(p1.X, 1) &&
        //                Math.Round(intersect.Y, 1) == Math.Round(p1.Y, 1)))
        //                return false;
        //            else if (s && !o && !(Math.Round(intersect.X, 1) == Math.Round(q2.X, 1) &&
        //                Math.Round(intersect.Y, 1) == Math.Round(q2.Y, 1)) &&
        //                !(intersect.X == p2.X && intersect.Y == p2.Y) &&
        //                !(Math.Round(intersect.X, 1) == Math.Round(p2.X, 1) &&
        //                Math.Round(intersect.Y, 1) == Math.Round(p2.Y, 1)))
        //                return false;
        //        }
        //        if(IsInside(myShapes[i].PlacedPoints, myShapes[i].PlacedPoints.Length, Points[k], Length, null, false))
        //        {
        //            return false;
        //        }
        //    }
    //    //}
   // }

    //private void BuildTable(ref int[][] PossibleMoves, MyShapes shap, int indexOfO, MyShapes s, int indexOfS, PointClass SE, PointClass OE)
    //{
    //    int i = 0;
    //    PointClass[] ShapMovedVector = GetPoints(shap, s.PlacedPoints[indexOfS], indexOfO);
    //    int previousO, nextO, previousS, nextS;
    //    previousS = indexOfS == 0 ? s.Points.Length - 1 : indexOfS - 1;
    //    nextS = indexOfS == s.Points.Length - 1 ? 0 : indexOfS + 1;
    //    previousO = indexOfO == 0 ? shap.Points.Length - 1 : indexOfO - 1;
    //    nextO = indexOfO == shap.Points.Length - 1 ? 0 : indexOfO + 1;
    //    if (SE.X == -1 && OE.X == -1) //get translation options
    //    {

    //        PossibleMoves[0][0] = PossibleMoves[0][1] = 1;//e, e
    //        PossibleMoves[0][2] = ShapMovedVector[previousO].X <= s.PlacedPoints[indexOfS].X ? 1 : 0;
    //        PossibleMoves[1][0] = 1;//e
    //        PossibleMoves[1][1] = 0;//s
    //        PossibleMoves[1][2] = ShapMovedVector[nextO].X <= s.PlacedPoints[indexOfS].X ? 1 : 0;
    //        PossibleMoves[2][0] = PossibleMoves[2][1] = 0; //s, s
    //        PossibleMoves[2][2] = ShapMovedVector[nextO].X <= s.PlacedPoints[indexOfS].X ? 1 : 0;
    //        PossibleMoves[3][0] = 0;//s
    //        PossibleMoves[3][1] = 1;//e
    //        PossibleMoves[3][2] = ShapMovedVector[previousO].X <= s.PlacedPoints[indexOfS].X ? 1 : 0;
    //        for (i = 0; i < 4; i++)//get vector derivision and eliminate Points
    //        {
    //            if ((PossibleMoves[i][0] == 1 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 0) ||
    //                (PossibleMoves[i][0] == 1 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 1))
    //            {
    //                PossibleMoves[i][3] = 1;
    //                PossibleMoves[i][7] = indexOfS == 0 ? s.LinearEquation.Length - 1 : indexOfS - 1;
    //                PossibleMoves[i][8] = indexOfO == shap.LinearEquation.Length - 1 ? 0 : indexOfO + 1;
    //            }
    //            else if (PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 1)
    //            {
    //                PossibleMoves[i][3] = 1;
    //                PossibleMoves[i][7] = indexOfS == s.LinearEquation.Length - 1 ? 0 : indexOfS + 1;
    //                PossibleMoves[i][8] = indexOfO == shap.LinearEquation.Length - 1 ? 0 : indexOfO + 1;
    //            }
    //            else if (PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 0)
    //            {
    //                PossibleMoves[i][3] = 0;
    //                PossibleMoves[i][7] = indexOfS == s.LinearEquation.Length - 1 ? 0 : indexOfS + 1;
    //                PossibleMoves[i][8] = indexOfO == shap.LinearEquation.Length - 1 ? 0 : indexOfO + 1;
    //            }
    //            else if ((PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 1 && PossibleMoves[i][2] == 0) ||
    //                (PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 1 && PossibleMoves[i][2] == 1))
    //            {
    //                PossibleMoves[i][3] = 0;
    //                PossibleMoves[i][7] = indexOfS == s.LinearEquation.Length - 1 ? 0 : indexOfS + 1;
    //                PossibleMoves[i][8] = indexOfO == 0 ? shap.LinearEquation.Length - 1 : indexOfO - 1;
    //            }
    //            else PossibleMoves[i][3] = PossibleMoves[i][7] = PossibleMoves[i][8] = -1;
    //        }
    //        for (i = 0; i < 4; i++)//eliminate incorrect translation vector
    //        {
    //            if (PossibleMoves[i][3] == -1) PossibleMoves[i][4] = PossibleMoves[i][5] = -1;
    //            else if ((PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 1) ||
    //                (PossibleMoves[i][0] == 1 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 0) ||
    //                (PossibleMoves[i][0] == 1 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 1))
    //            {
    //                PossibleMoves[i][4] = nextO;
    //                PossibleMoves[i][5] = indexOfS;
    //                PossibleMoves[i][6] = indexOfO;
    //            }
    //            else if ((PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 0) ||
    //                 (PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 1 && PossibleMoves[i][2] == 0) ||
    //                 (PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 1 && PossibleMoves[i][2] == 1))
    //            {
    //                PossibleMoves[i][4] = indexOfO;
    //                PossibleMoves[i][5] = nextS;
    //                PossibleMoves[i][6] = indexOfS;
    //            }
    //        }
    //    }
    //    else if (SE.X != -1)
    //    {
    //        for (int j = 0; j < 4; j++)
    //        {
    //            for (int j2 = 0; j2 < 7; j2++)
    //            {
    //                PossibleMoves[j][j2] = -1;
    //            }
    //        }
    //        PossibleMoves[0][4] = indexOfO;
    //        PossibleMoves[0][5] = nextS;
    //    }
    //    else
    //    {
    //        for (int j = 0; j < 4; j++)
    //        {
    //            for (int j2 = 0; j2 < 6; j2++)
    //            {
    //                PossibleMoves[j][j2] = -1;
    //            }
    //        }
    //        PossibleMoves[0][4] = nextO;
    //        PossibleMoves[0][5] = indexOfS;
    //    }
    //}


    //NTF-get location on which to place shape
    //private void GetLocation(MyShapes shap, MyShapes s, ref int OptimalO, ref PointClass OptimalA, List<MyShapes> myShapes)
    //{
    //    int i = 1, SIndexOfS = 0;
    //    for (; i < s.Points.Length; i++)
    //    {
    //        if (s.Points[i].Y < s.Points[SIndexOfS].Y) SIndexOfS = i;
    //    }
    //    int indexOfS = SIndexOfS, SindexOfO = 0;
    //    for (i = 1; i < shap.Points.Length; i++)
    //    {
    //        if (shap.Points[i].Y > shap.Points[SindexOfO].Y) SindexOfO = i;
    //    }
    //    int indexOfO = SindexOfO;
    //    PointClass OE = new PointClass(-1, -1); //if sliding stopped before vertex of orbiting shape
    //    PointClass SE = new PointClass(-1, -1); //if sliding stopped before vertex of stationary shape
    //    int PCorrectVector = -1;
    //    int POCorrectVector = -1;
    //    int[][] PossibleMoves = new int[4][]; //saves all translations
    //    for (i = 0; i < 4; i++) PossibleMoves[i] = new int[9];
    //    //0-statinary direction, 1-orbiting direction, 2-orbiting position, 3-translation derived vector
    //    //4-orbiting vertex change, 5-stationary vertex change, 6-translation vector, 7-stationary line, 8-orbiting line
    //    do
    //    {
    //        //save optimal location
    //        SaveOptimalLocation(s, SE, OE, shap, indexOfO, indexOfS, ref OptimalA, ref OptimalO, myShapes);

    //        //start=0, end=1, right=0, left=1, stationary=0, orbiting=1
    //        BuildTable(ref PossibleMoves, shap, indexOfO, s, indexOfS, SE, OE);
    //        int correctTVector;
    //        if (SE.X != -1 || OE.X != -1)
    //        {
    //            correctTVector = 0;
    //            SE = OE = new PointClass(-1, -1);
    //        }
    //        else
    //        {
    //            EliminatesTransPoints(ref PossibleMoves, s, shap, indexOfO, indexOfS);
    //            correctTVector = GetCorrectVector(PossibleMoves, PCorrectVector, POCorrectVector, s, shap);
    //            if (correctTVector == -1) break; //no translation possible
    //            PCorrectVector = PossibleMoves[correctTVector][7];
    //            POCorrectVector = PossibleMoves[correctTVector][8];
    //        }
    //        PointClass Trim = TrimTranslation(shap, s, indexOfS, s.PlacedPoints[PossibleMoves[correctTVector][5]], PossibleMoves[correctTVector][4]);
    //        if (Trim.X != -1)
    //        {
    //            int j = 0;
    //            for (; j < 4; j++)
    //            {
    //                if (PossibleMoves[j][3] != -1 && PossibleMoves[j][3] != PossibleMoves[correctTVector][3])
    //                {
    //                    correctTVector = j;
    //                    break;
    //                }
    //            }
    //            if (j != 4)
    //            {
    //                Trim = TrimTranslation(shap, s, indexOfS, s.PlacedPoints[PossibleMoves[correctTVector][5]], PossibleMoves[correctTVector][4]);
    //            }
    //            if (Trim.X != -1)
    //            {
    //                if (PossibleMoves[correctTVector][3] == 1) OE = new PointClass(shap.Points[PossibleMoves[correctTVector][4]].X - Trim.X,
    //                shap.Points[PossibleMoves[correctTVector][4]].Y - Trim.Y);
    //                else SE = new PointClass(s.Points[PossibleMoves[correctTVector][5]].X - Trim.X,
    //                    s.Points[PossibleMoves[correctTVector][5]].Y - Trim.Y);
    //            }
    //        }
    //        indexOfO = PossibleMoves[correctTVector][4];
    //        indexOfS = PossibleMoves[correctTVector][5];
    //    }
    //    while (!(indexOfS == SIndexOfS && indexOfO == SindexOfO));
    //}

    //gets all possible combinations and trims them;

    //private List<PointClass> NFPOptions(PointClass[] shape, PointClass[] orbiting)
    //{
    //    List<Pair> pairs = new List<Pair>();
    //    for (int i = 0; i < shape.Length; i++)
    //    {
    //        for (int j = 0; j < orbiting.Length; j++)
    //        {
    //            pairs.Add(new Pair(i, j));
    //        }
    //    }
    //    foreach (var item in pairs)
    //    {
    //        if
    //    }
    //    return pairs;
    //}
    //List<PointClass> NFPVectors = new List<PointClass>();
    //VectorClass[] NegOrbiting = new VectorClass[orbiting.Length];
    //int i = 0;
    //foreach (var item in orbiting)
    //{
    //    NegOrbiting[i++] = item * -1;
    //}
    //foreach (var s in shape)
    //{
    //    VectorClass vector = GetVector(s, o);
    //    PointClass point = s + orbiting[0];
    //    NFPVectors.Add(point);
    //}
    //return NFPVectors;
    //}

    private VectorClass GetVector(VectorClass s, VectorClass o)
        {
            PointClass Diff=new PointClass(o.PointA.X-s.PointB.X, o.PointA.Y-s.PointB.Y);
            VectorClass vector = o - Diff;
            return new VectorClass(s.PointA, vector.PointB);

        }
    }
}
//        //Rotates one point around another
//        static Vector RotatePoint(Vector pointToRotate, Vector centerPoint, double angleInDegrees)
//        {
//            double angleInRadians = angleInDegrees * (Math.PI / 180);
//            double cosTheta = Math.Cos(angleInRadians);
//            double sinTheta = Math.Sin(angleInRadians);
//            return new Vector
//            {
//                X =
//                    (int)
//                    (cosTheta * (pointToRotate.X - centerPoint.X) -
//                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
//                Y =
//                    (int)
//                    (sinTheta * (pointToRotate.X - centerPoint.X) +
//                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
//            };
//        }
//        public double AngleBetweenLinesInRadians(Vector line1Start, Vector line1End, Vector line2Start, Vector line2End)
//        {
//            double angle1 = Math.Atan2(line1Start.Y - line1End.Y, line1Start.X - line1End.X);
//            double angle2 = Math.Atan2(line2Start.Y - line2End.Y, line2Start.X - line2End.X);
//            double result = (angle2 - angle1) * 180 / 3.14;
//            if (result < 0)
//            {
//                result += 360;
//            }
//            return result;
//        }
//        //check if a point lies inside a circle sector. 
//        public bool CheckPoint(int radius, double x, double y, float percent, float startAngle)
//        {
//            // calculate endAngle 
//            float endAngle = 360 / percent + startAngle;

//            // Calculate polar co-ordinates 
//            float polarradius = (float)Math.Sqrt(x * x + y * y);

//            float Angle = (float)Math.Atan(y / x);

//            // Check whether polarradius is less then radius of circle or not and Angle is between startAngle and endAngle or not 
//            if (Angle >= startAngle && Angle <= endAngle && polarradius < radius) return true;

//            return false;
//        }
//        private Vector Intersect(Vector CirclePos, double CircleRad, Vector LineStart, Vector LineEnd)
//        {
//            //Calculate terms of the linear and quadratic equations
//            var M = (LineEnd.Y - LineStart.Y) / (LineEnd.X - LineStart.X);
//            var B = LineStart.Y - M * LineStart.X;
//            var a = 1 + M * M;
//            var b = 2 * (M * B - M * CirclePos.Y - CirclePos.X);
//            var c = CirclePos.X * CirclePos.X + B * B + CirclePos.Y * CirclePos.Y -
//                    CircleRad * CircleRad - 2 * B * CirclePos.Y;
//            // solve quadratic equation
//            var sqRtTerm = Math.Sqrt(b * b - 4 * a * c);
//            var x = ((-b) + sqRtTerm) / (2 * a);
//            // make sure we have the correct root for our line segment
//            if ((x < Math.Min(LineStart.X, LineEnd.X) ||
//               (x > Math.Max(LineStart.X, LineEnd.X))))
//            { x = ((-b) - sqRtTerm) / (2 * a); }
//            //solve for the y-component
//            var y = M * x + B;
//            // Intersection Calculated
//            return new Vector(x, y);
//        }

//        public bool GetLineArcIntersection(Vector sPoint, Vector ePoint, double sAngle, double eAngle)
//        {
//            double dx = ePoint.X - sPoint.X;
//            double dy = ePoint.Y - sPoint.Y;
//            double al = Math.Tan(dy / dx);
//            if (al < sAngle || al > eAngle) return false;
//            return true;
//        }

//    }
//}
