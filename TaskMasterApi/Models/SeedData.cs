using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMasterApi.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TaskMasterApiContext(
                serviceProvider.GetRequiredService<DbContextOptions<TaskMasterApiContext>>()))
            {
                
                if (context.Course.Count() > 0)
                {
                    return;
                }

                // Seed a Course with one topic
                List<Topic> seedTopics = new List<Topic>();
                Topic seedTopic = new Topic
                {
                    Title = "Basic sorting algorithms",
                    Confidence = 0
                };

                seedTopics.Add(seedTopic);
                
                context.Course.AddRange(
                    new Course
                    {
                        Title = "Algorithms and Data Structures",
                        CourseCode = "COMPSCI 220",
                        Topics = new List<Topic>(seedTopics)
                    }
                );

                // Seed a WorkBlock
                context.Workblock.AddRange(
                    new WorkBlock
                    {
                        Time = new TimeSpan(14, 0, 0) // 2pm
                    }
                );
                
                // See a Dodge
                context.Dodge.AddRange(
                    new Dodge
                    {
                        Date = new DateTime(2018, 11, 17),
                        Reason = "Felt lazy that day",
                        Topic = seedTopic
                    }
                );

                // Seed a WorkSession
                context.WorkSession.AddRange(
                    new WorkSession
                    {
                        Topic = seedTopic,
                        ScheduleAfter = new DateTime(2018, 11, 18),
                        Priority = 0
                    }
                );
                context.SaveChanges();

            }
        }
    }
}
