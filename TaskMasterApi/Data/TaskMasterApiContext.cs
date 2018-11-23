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

        public DbSet<TaskMasterApi.Models.WorkBlock> Workblock { get; set; }

        public DbSet<TaskMasterApi.Models.Dodge> Dodge { get; set; }

        public DbSet<TaskMasterApi.Models.WorkSession> WorkSession { get; set; }

        public DbSet<TaskMasterApi.Models.Topic> Topic { get; set; }
    }
}
