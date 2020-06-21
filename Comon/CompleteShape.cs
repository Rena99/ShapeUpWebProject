using System;
using System.Collections.Generic;
using System.Text;

namespace Comon
{
    public class CompleteShape
    {
        public ShapesDTO shape { get; set; }
        public List<PointDTO> points { get; set; }
        public ResultsDTO result { get; set; }

        public CompleteShape()
        {
                
        }
        public CompleteShape(ShapesDTO s, List<PointDTO> p, ResultsDTO r)
        {
            shape = s;
            points = p;
            result = r;
        }
    }
}
