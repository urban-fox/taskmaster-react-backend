using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMasterApi.Models
{
    public class Dodge
    {
        public int DodgeId { get; set; }

        public Topic Topic { get; set; }

        public DateTime Date { get; set; }
        public string Reason { get; set; }

    }
}
