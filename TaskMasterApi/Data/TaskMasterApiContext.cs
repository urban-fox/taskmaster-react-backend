using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskMasterApi.Models;

namespace TaskMasterApi.Models
{
    public class TaskMasterApiContext : DbContext
    {
        public TaskMasterApiContext (DbContextOptions<TaskMasterApiContext> options)
            : base(options)
        {
        }

        public DbSet<TaskMasterApi.Models.Course> Course { get; set; }

        public DbSet<TaskMasterApi.Models.Workblock> Workblock { get; set; }
    }
}
