////using Comon;
////using System;
////using System.Collections.Generic;
////using System.Text;

////namespace Services
////{
////    class BuildTables
////    {
////        private void BuildTable(ref int[][] PossibleMoves /*matrix*/, MyShapes shap /*orbiting shape*/, int indexOfO /*vertex on orbiting shape where now holding*/, MyShapes s/*stationary shape*/, int indexOfS /*vertex on stationary shape where now holding*/, PointClass SE /*if statinary translation was trimmed in the last move*/, PointClass OE/*if orbiting translation was trimmed in the last move*/)
////        {
////            int i = 0;
////            PointClass[] ShapMovedVector = GetPoints(shap, s.PlacedPoints[indexOfS], indexOfO);
////            int previousO, nextO, previousS, nextS;
////            previousS = indexOfS == 0 ? s.Points.Length - 1 : indexOfS - 1;
////            nextS = indexOfS == s.Points.Length - 1 ? 0 : indexOfS + 1;
////            previousO = indexOfO == 0 ? shap.Points.Length - 1 : indexOfO - 1;
////            nextO = indexOfO == shap.Points.Length - 1 ? 0 : indexOfO + 1;
////            if (SE.X == -1 && OE.X == -1) //get translation options
////            {

////                PossibleMoves[0][0] = PossibleMoves[0][1] = 1;//e, e
////                PossibleMoves[0][2] = ShapMovedVector[previousO].X <= s.PlacedPoints[indexOfS].X ? 1 : 0;
////                PossibleMoves[1][0] = 1;//e
////                PossibleMoves[1][1] = 0;//s
////                PossibleMoves[1][2] = ShapMovedVector[nextO].X <= s.PlacedPoints[indexOfS].X ? 1 : 0;
////                PossibleMoves[2][0] = PossibleMoves[2][1] = 0; //s, s
////                PossibleMoves[2][2] = ShapMovedVector[nextO].X <= s.PlacedPoints[indexOfS].X ? 1 : 0;
////                PossibleMoves[3][0] = 0;//s
////                PossibleMoves[3][1] = 1;//e
////                PossibleMoves[3][2] = ShapMovedVector[previousO].X <= s.PlacedPoints[indexOfS].X ? 1 : 0;
////                for (i = 0; i < 4; i++)//get vector derivision and eliminate Points
////                {
////                    if ((PossibleMoves[i][0] == 1 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 0) ||
////                        (PossibleMoves[i][0] == 1 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 1))
////                    {
////                        PossibleMoves[i][3] = 1;
////                        PossibleMoves[i][7] = indexOfS == 0 ? s.LinearEquation.Length - 1 : indexOfS - 1;
////                        PossibleMoves[i][8] = indexOfO == shap.LinearEquation.Length - 1 ? 0 : indexOfO + 1;
////                    }
////                    else if (PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 1)
////                    {
////                        PossibleMoves[i][3] = 1;
////                        PossibleMoves[i][7] = indexOfS == s.LinearEquation.Length - 1 ? 0 : indexOfS + 1;
////                        PossibleMoves[i][8] = indexOfO == shap.LinearEquation.Length - 1 ? 0 : indexOfO + 1;
////                    }
////                    else if (PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 0)
////                    {
////                        PossibleMoves[i][3] = 0;
////                        PossibleMoves[i][7] = indexOfS == s.LinearEquation.Length - 1 ? 0 : indexOfS + 1;
////                        PossibleMoves[i][8] = indexOfO == shap.LinearEquation.Length - 1 ? 0 : indexOfO + 1;
////                    }
////                    else if ((PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 1 && PossibleMoves[i][2] == 0) ||
////                        (PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 1 && PossibleMoves[i][2] == 1))
////                    {
////                        PossibleMoves[i][3] = 0;
////                        PossibleMoves[i][7] = indexOfS == s.LinearEquation.Length - 1 ? 0 : indexOfS + 1;
////                        PossibleMoves[i][8] = indexOfO == 0 ? shap.LinearEquation.Length - 1 : indexOfO - 1;
////                    }
////                    else PossibleMoves[i][3] = PossibleMoves[i][7] = PossibleMoves[i][8] = -1;
////                }
////                for (i = 0; i < 4; i++)//eliminate incorrect translation vector
////                {
////                    if (PossibleMoves[i][3] == -1) PossibleMoves[i][4] = PossibleMoves[i][5] = -1;
////                    else if ((PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 1) ||
////                        (PossibleMoves[i][0] == 1 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 0) ||
////                        (PossibleMoves[i][0] == 1 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 1))
////                    {
////                        PossibleMoves[i][4] = nextO;
////                        PossibleMoves[i][5] = indexOfS;
////                        PossibleMoves[i][6] = indexOfO;
////                    }
////                    else if ((PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 0 && PossibleMoves[i][2] == 0) ||
////                         (PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 1 && PossibleMoves[i][2] == 0) ||
////                         (PossibleMoves[i][0] == 0 && PossibleMoves[i][1] == 1 && PossibleMoves[i][2] == 1))
////                    {
////                        PossibleMoves[i][4] = indexOfO;
////                        PossibleMoves[i][5] = nextS;
////                        PossibleMoves[i][6] = indexOfS;
////                    }
////                }
////            }
////            else if (SE.X != -1)
////            {
////                for (int j = 0; j < 4; j++)
////                {
////                    for (int j2 = 0; j2 < 7; j2++)
////                    {
////                        PossibleMoves[j][j2] = -1;
////                    }
////                }
////                PossibleMoves[0][4] = indexOfO;
////                PossibleMoves[0][5] = nextS;
////            }
////            else
////            {
////                for (int j = 0; j < 4; j++)
////                {
////                    for (int j2 = 0; j2 < 6; j2++)
////                    {
////                        PossibleMoves[j][j2] = -1;
////                    }
////                }
////                PossibleMoves[0][4] = nextO;
////                PossibleMoves[0][5] = indexOfS;
////            }
////        }
////        //moves every vertex of shape to location on which will be placed
////        public PointClass[] GetPoints(MyShapes s, PointClass indexOnA, int indexOfF, PointClass trim = null)
////        {
////            if (trim == null) trim = new PointClass(0, 0);
////            double difx = indexOnA.X - (s.Points[indexOfF].X + trim.X), diffy = indexOnA.Y - (s.Points[indexOfF].Y + trim.Y);
////            PointClass[] vector = new PointClass[s.Points.Length];
////            for (int i = 0; i < vector.Length; i++) vector[i] = new PointClass(s.Points[i].X + difx, s.Points[i].Y + diffy);
////            return vector;
////        }
////    }
////}
//public bool PointCollectionsOverlap_Fast(PointClass[] area1, PointClass[] area2)
//{
//    for (int i = 0; i < area1.Length; i++)
//    {
//        for (int j = 0; j < area2.Length; j++)
//        {
//            if (lineSegmentsIntersect(area1[i], area1[(i + 1) % area1.Length], area2[j], area2[(j + 1) % area2.Length]))
//            {
//                return true;
//            }
//        }
//    }
//    if (IsInside(area1, area1.Length, area2[0], Length, null, false) ||
//        IsInside(area2, area2.Length, area1[0], Length, null, false))
//    {
//        return true;
//    }
//    return false;
//}

//public static bool PointCollectionContainsPoint(PointClass[] area, PointClass point)
//{
//    PointClass start = new PointClass(-100, -100);
//    int intersections = 0;

//    for (int i = 0; i < area.Length; i++)
//    {
//        if (lineSegmentsIntersect(area[i], area[(i + 1) % area.Length], start, point))
//        {
//            intersections++;
//        }
//    }

//    return (intersections % 2) == 1;
//}

//private static double determinant(PointClass vector1, PointClass vector2)
//{
//    return vector1.X * vector2.Y - vector1.Y * vector2.X;
//}



//private static bool lineSegmentsIntersect(PointClass _segment1_Start, PointClass _segment1_End, PointClass _segment2_Start, PointClass _segment2_End)
//{
//    double det = determinant(_segment1_End - _segment1_Start, _segment2_Start - _segment2_End);
//    double t = determinant(_segment2_Start - _segment1_Start, _segment2_Start - _segment2_End) / det;
//    double u = determinant(_segment1_End - _segment1_Start, _segment2_Start - _segment1_Start) / det;
//    return (t >= 0) && (u >= 0) && (t <= 1) && (u <= 1);
//}
