using TaskMasterApi.Controllers;
using TaskMasterApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;

namespace UnitTestTaskMasterApi
{
    [TestClass]
    public class TestWorkSession
    {
        public static readonly DbContextOptions<TaskMasterApiContext> options
            = new DbContextOptionsBuilder<TaskMasterApiContext>()
            .UseInMemoryDatabase(databaseName: "testDatabase")
            .Options;

        [TestInitialize]
        public void SetupDb()
        {
            // Initialise the test database with 6 worksessions:
                // 1 november
                // 5 november
                // 10 november
                // 1 december
                // 5 december
                // 10 december

            using (var context = new TaskMasterApiContext(options))
            {
                // Create a common and topic
                var commonTopic = new Topic()
                {
                    TopicId = 1,
                    Course = null,
                    Title = "Dummy topic",
                    Confidence = 2
                };

                context.Topic.Add(commonTopic);

                List<WorkSession> workSessions = new List<WorkSession>();

                workSessions.Add(new WorkSession
                {
                    WorkSessionId = 1,
                    Topic = commonTopic,
                    ScheduleAfter = new System.DateTime(2018, 11, 1),
                    Priority = 0
                });
                workSessions.Add(new WorkSession
                {
                    WorkSessionId = 2,
                    Topic = commonTopic,
                    ScheduleAfter = new System.DateTime(2018, 11, 5),
                    Priority = 0
                });
                workSessions.Add(new WorkSession
                {
                    WorkSessionId = 3,
                    Topic = commonTopic,
                    ScheduleAfter = new System.DateTime(2018, 11, 10),
                    Priority = 0
                });
                workSessions.Add(new WorkSession
                {
                    WorkSessionId = 4,
                    Topic = commonTopic,
                    ScheduleAfter = new System.DateTime(2018, 12, 1),
                    Priority = 0
                });
                workSessions.Add(new WorkSession
                {
                    WorkSessionId = 5,
                    Topic = commonTopic,
                    ScheduleAfter = new System.DateTime(2018, 12, 5),
                    Priority = 0
                });
                workSessions.Add(new WorkSession
                {
                    WorkSessionId = 6,
                    Topic = commonTopic,
                    ScheduleAfter = new System.DateTime(2018, 12, 10),
                    Priority = 0
                });

                context.SaveChanges();
            }
        }

        private void PrioritySetup()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Create a common and topic
                var commonTopic = new Topic()
                {
                    TopicId = 1,
                    Course = null,
                    Title = "Dummy topic",
                    Confidence = 2
                };

                context.Topic.Add(commonTopic);

                List<WorkSession> workSessions = new List<WorkSession>();

                workSessions.Add(new WorkSession
                {
                    WorkSessionId = 1,
                    Topic = commonTopic,
                    ScheduleAfter = new System.DateTime(2018, 11, 1),
                    Priority = 1
                });
                workSessions.Add(new WorkSession
                {
                    WorkSessionId = 2,
                    Topic = commonTopic,
                    ScheduleAfter = new System.DateTime(2018, 11, 5),
                    Priority = 2
                });
                workSessions.Add(new WorkSession
                {
                    WorkSessionId = 3,
                    Topic = commonTopic,
                    ScheduleAfter = new System.DateTime(2018, 11, 10),
                    Priority = 0
                });
                workSessions.Add(new WorkSession
                {
                    WorkSessionId = 4,
                    Topic = commonTopic,
                    ScheduleAfter = new System.DateTime(2018, 12, 1),
                    Priority = 0
                });
                workSessions.Add(new WorkSession
                {
                    WorkSessionId = 5,
                    Topic = commonTopic,
                    ScheduleAfter = new System.DateTime(2018, 12, 5),
                    Priority = 1
                });
                workSessions.Add(new WorkSession
                {
                    WorkSessionId = 6,
                    Topic = commonTopic,
                    ScheduleAfter = new System.DateTime(2018, 12, 10),
                    Priority = 2
                });

                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void ClearDb()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                context.Topic.RemoveRange(context.Topic);
                context.WorkSession.RemoveRange(context.WorkSession);
                context.SaveChanges();
            };
        }

        // GET WorkSession/{date}
        // Gets the next 4 work sessions by date and priority
        // This test sets date to after the scheduleafter dates of all sessions
        [TestMethod]
        public void TestGetAllSchedulable()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var controller = new WorkBlocksController(context);
                var today = new DateTime(2019, 1, 1);

                // Act
                var sessions = (IEnumerable<WorkSession>)controller.GetNext(today);

                // Assert - should have all worksessions ordered by scheduleAfter
                Assert.AreEqual(sessions.Count(), 3);

                var resultList = sessions.ToList();
                for (int i = 0; i < 3; i++)
                {
                    Assert.AreEqual(resultList[i].WorkSessionId, i + 1);
                }

            }
        }

        // GET WorkSession/{date}
        // Gets the next 4 work sessions by date and priority
        // This test sets date to 1 December
        [TestMethod]
        public void TestGetOnlySchedulable()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var controller = new WorkBlocksController(context);
                var today = new DateTime(2018, 12, 1);

                // Act
                var sessions = (IEnumerable<WorkSession>)controller.GetNext(today);

                // Assert - should have all worksessions ordered by scheduleAfter
                Assert.AreEqual(sessions.Count(), 3);

                var resultList = sessions.ToList();
                for (int i = 0; i < 3; i++)
                {
                    Assert.AreEqual(resultList[i].WorkSessionId, i + 4);
                }

            }
        }


        // GET WorkSession/{date}
        // Gets the next 4 work sessions by date and priority
        // This test sets date to after all scheduleAfter, but sets priorities
        [TestMethod]
        public void TestGetByPriority()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var controller = new WorkBlocksController(context);
                var today = new DateTime(2019, 1, 1);

                // remove existing priority 0 setup
                context.Topic.RemoveRange(context.Topic);
                context.WorkSession.RemoveRange(context.WorkSession);
                context.SaveChanges();

                this.PrioritySetup();

                // Act
                var sessions = (IEnumerable<WorkSession>)controller.GetNext(today);

                // Assert
                // Should be 4
                Assert.AreEqual(sessions.Count(), 4);

                // Should have all worksessions ordered by priority and scheduleAfter
                // order should be 3, 4, 1, 5
                var resultList = sessions.ToList();
                Assert.AreEqual(resultList[0].WorkSessionId, 3);
                Assert.AreEqual(resultList[1].WorkSessionId, 4);
                Assert.AreEqual(resultList[2].WorkSessionId, 1);
                Assert.AreEqual(resultList[3].WorkSessionId, 5);

            }
        }



        [TestMethod]
        public async Task TestPost()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var ws = new WorkSession
                {
                    WorkSessionId = 7,
                    Topic = null,
                    ScheduleAfter = new System.DateTime(2018, 12, 15),
                    Priority = 0
                };
                var controller = new WorkSessionsController(context);

                // Act
                await controller.PostWorkSession(ws);

                // Assert
                Assert.IsTrue(context.WorkSession.Any(w => w.WorkSessionId == 7));
            }
        }

        [TestMethod]
        public async Task TestDelete()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var controller = new WorkBlocksController(context);

                // Act
                await controller.DeleteWorkBlock(1);

                // Assert
                Assert.AreEqual(1, context.Workblock.Count());
                Assert.AreEqual(15, context.Workblock.First().Time.Hours);
            }
        }
    }
}
