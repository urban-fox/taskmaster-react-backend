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
                
                if (context.Course.Any())
                {
                    return;
                }

                // Create topics
                List<Topic> seedTopics = new List<Topic>();

                seedTopics.Add(
                    new Topic
                    {
                        Title = "Basic sorting algorithms",
                        Confidence = 0
                    }
                );

                // add 2 more topics
                seedTopics.Add(
                    new Topic
                    {
                        Title = "Advanced sorting algorithms",
                        Confidence = 2,
                    }
                );
                seedTopics.Add(
                    new Topic
                    {
                        Title = "Efficient searching",
                        Confidence = 1,
                    }
                );

                // Create a course
                Course seedCourse = new Course
                {
                    Title = "Algorithms and Data Structures",
                    CourseCode = "COMPSCI 220",
                    Topics = seedTopics
                };


                foreach (Topic topic in seedTopics)
                {
                    topic.Course = seedCourse;
                }

                context.Course.Add(seedCourse);
                context.Topic.AddRange(seedTopics);
                
                // Seed a WorkBlock
                context.Workblock.AddRange(
                    new WorkBlock
                    {
                        Time = new TimeSpan(14, 0, 0) // 2pm
                    }
                );

                // Seed a Dodge
                context.Dodge.AddRange(
                    new Dodge
                    {
                        Date = new DateTime(2018, 11, 17),
                        Reason = "Felt lazy that day",
                        Topic = seedTopics[0],
                    }
                );

                // Seed a WorkSession
                context.WorkSession.AddRange(
                    new WorkSession
                    {
                        Topic = seedTopics[0],
                        ScheduleAfter = new DateTime(2018, 11, 18),
                        Priority = 0
                    }
                );
                context.SaveChanges();

            }
        }
    }
}
