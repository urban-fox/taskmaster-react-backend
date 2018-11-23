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
    public class TestDodge
    {
        public static readonly DbContextOptions<TaskMasterApiContext> options
            = new DbContextOptionsBuilder<TaskMasterApiContext>()
            .UseInMemoryDatabase(databaseName: "testDatabase")
            .Options;

        [TestInitialize]
        public void SetupDb()
        {
            // Initialise the test database

            using (var context = new TaskMasterApiContext(options))
            {
                var dodge1 = new Dodge
                {
                    DodgeId = 1,
                    Topic = null,
                    Date = new DateTime(2018, 11, 1),
                    Reason = "I was too tired"
                };
                var dodge2 = new Dodge
                {
                    DodgeId = 2,
                    Topic = null,
                    Date = new DateTime(2018, 11, 1),
                    Reason = "I was too lazy"
                };
                context.Dodge.Add(dodge1);
                context.Dodge.Add(dodge2);

                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void ClearDb()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                context.Dodge.RemoveRange(context.Dodge);
                context.SaveChanges();
            };
        }

        // GET Dodge/
        [TestMethod]
        public void TestGetAll()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var controller = new DodgesController(context);

                // Act
                var wb = controller.GetDodge();

                // Assert
                Assert.AreEqual(context.Dodge.Count(), 2);
            }
        }


        [TestMethod]
        public async Task TestPost()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var dodge3 = new Dodge
                {
                    DodgeId = 3,
                    Topic = null,
                    Date = new DateTime(2018, 11, 1),
                    Reason = "I was sleeping"
                };
                var controller = new DodgesController(context);

                // Act
                await controller.PostDodge(dodge3);

                // Assert
                Assert.AreEqual(context.Dodge.Count(), 3);
                Assert.IsTrue(context.Dodge.Any(d => d.Reason == "I was sleeping"));
            }
        }
    }
}
