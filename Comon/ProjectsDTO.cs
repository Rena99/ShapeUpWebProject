using System;
using System.Collections.Generic;
using System.Text;

namespace Comon
{
    public class ProjectsDTO
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime ProjectDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? ProjectStatus { get; set; }
    }
}
