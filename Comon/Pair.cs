using System;
using System.Collections.Generic;
using System.Text;

namespace Comon
{
    public class Pair
    {
        public int shape { get; set; }
        public int Orbit { get; set; }
        public Points PointOnA { get; set; }
        public Pair()
        {

        }
        public Pair(int a, int b)
        {
            shape = a;
            Orbit = b;
        }
    }
}
