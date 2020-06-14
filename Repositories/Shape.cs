using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class Shape
    {
        public int id { get; set; }
        public bool area { get; set; }
        public int unit { get; set; }
        public List<PointMap> points { get; set; }
        public Results result { get; set; }
        public Shape(int id, bool area, int unit, List<PointMap> points, Results result)
        {
            this.id = id;
            this.area = area;
            this.unit = unit;
            this.points = points;
            this.result = result;
        }
    }
    public class PointMap
    {
        public int id { get; set; }
        public decimal x { get; set; }
        public decimal y { get; set; }
        public PointMap(int id, decimal x, decimal y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
        }
    }
    public class Results
    {
        public int Id { get; set; }
        public decimal PointOfShapeX { get; set; }
        public decimal PointOfShapeY { get; set; }
        public decimal PointOnAreaX { get; set; }
        public decimal PointOnAreaY { get; set; }
        public Results(int id, decimal x, decimal y, decimal ax, decimal ay)
        {
           Id = id;
           PointOfShapeX = x;
           PointOfShapeY = y;
           PointOnAreaX = ax;
           PointOnAreaY = ay;
        }

        public Results()
        {
        }
    }
    public class ProjectClass
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime ProjectDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? ProjectStatus { get; set; }

        public ProjectClass()
        {

        }
       
    }
}
