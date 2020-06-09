using System;
using System.Collections.Generic;
using System.Text;

namespace Comon
{
    public class VectorClass
    {
        public PointClass PointA { get; set; }
        public PointClass PointB { get; set; }

        public VectorClass()
        {

        }
        public VectorClass(PointClass a, PointClass b)
        {
            PointA = a;
            PointB = b;
        }

        public static VectorClass operator +(VectorClass v, VectorClass w)
        {
            return new VectorClass(v.PointA + w.PointA, v.PointB + w.PointB);
        }

        public static VectorClass operator *(VectorClass v, double mult)
        {
            return new VectorClass(v.PointA * mult, v.PointB * mult);
        }

        public static VectorClass operator -(VectorClass v, PointClass w)
        {
            return new VectorClass(v.PointA - w, v.PointB - w);
        }

    }
}
