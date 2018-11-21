using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMasterApi.Models
{
    public class Workblock
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
    }
}
