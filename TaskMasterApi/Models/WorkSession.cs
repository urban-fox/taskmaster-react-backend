using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMasterApi.Models
{
    public class WorkSession
    {
        public int Id { get; set; }
        public Topic Topic { get; set; }
        public DateTime ScheduleAfter { get; set; }
        public int Priority { get; set; }
    }
}
