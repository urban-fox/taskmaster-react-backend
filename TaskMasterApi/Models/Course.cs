﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMasterApi.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string CourseCode { get; set; }
        public List<Topic> Topics;
    }
}
