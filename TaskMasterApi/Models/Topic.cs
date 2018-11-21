using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMasterApi.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Confidence { get; set; }
    }
}
