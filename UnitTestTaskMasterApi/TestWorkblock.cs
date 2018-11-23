using TaskMasterApi.Controllers;
using TaskMasterApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace UnitTestTaskMasterApi
{
    [TestClass]
    public class TestWorkBlock
    {
        public static readonly DbContextOptions<TaskMasterApiContext> options
            = new DbContextOptionsBuilder<TaskMasterApiContext>()
            .UseInMemoryDatabase(databaseName: "testDatabase")
            .Options;

        [TestInitialize]
        public void SetupDb()
        {
            // Initialise the test database with 2 workblocks at 2pm and 3pm

            using (var context = new TaskMasterApiContext(options))
            {
                var workblock1 = new WorkBlock
                {
                    WorkBlockId = 1,
                    Time = new System.TimeSpan(14,0,0)
                };
                context.Workblock.Add(workblock1);

                var workblock2 = new WorkBlock
                {
                    WorkBlockId = 2,
                    Time = new System.TimeSpan(15, 0, 0)
                };
                context.Workblock.Add(workblock2);

                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void ClearDb()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                context.Workblock.RemoveRange(context.Workblock);
                context.SaveChanges();
            };
        }

        // GET Workblock/
        [TestMethod]
        public void TestGetAll()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var controller = new WorkBlocksController(context);

                // Act
                var wb = controller.GetWorkblock();

                // Assert
                Assert.AreEqual(wb.Count(), 2);
                Assert.AreEqual(wb.First().Time, new System.TimeSpan(14,0,0));
                Assert.AreEqual(wb.Last().Time, new System.TimeSpan(15, 0, 0));
            }
        }


        [TestMethod]
        public async Task TestPost()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var wb3 = new WorkBlock
                {
                    WorkBlockId = 3,
                    Time = new System.TimeSpan(18, 0, 0)
                };
                var controller = new WorkBlocksController(context);

                // Act
                await controller.PostWorkBlock(wb3);

                // Assert
                Assert.AreEqual(context.Workblock.Count(), 3);

                var query = (from wb in context.Workblock
                             where wb.Time.Hours == 18
                             select wb.WorkBlockId);
                Assert.AreEqual(query.First(), 3);
            }
        }

        [TestMethod]
        public async Task TestPut()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var wb = new WorkBlock
                {
                    WorkBlockId = 1,
                    Time = new System.TimeSpan(10,0,0)
                };
                var controller = new WorkBlocksController(context);

                // Act
                await controller.PutWorkBlock(1, wb);

                // Assert
                Assert.AreEqual(context.Workblock.Count(), 2);

                var foundWb = context.Workblock.Find(1);
                Assert.AreEqual(foundWb.Time.Hours, 10);
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
