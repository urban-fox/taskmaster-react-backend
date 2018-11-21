using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMasterApi.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public List<Course> Courses {get; set;}
        public List<Workblock> Workblocks { get; set; }
        public List<Dodge> Dodges { get; set; }
    }
}
