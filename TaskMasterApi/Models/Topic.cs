using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMasterApi.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public int Confidence { get; set; }

        [ForeignKey("CourseForeignKey")]
        public int CourseId { get; set; }
    }
}
