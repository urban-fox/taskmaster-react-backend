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
    public class TestCourse
    {
        public static readonly DbContextOptions<TaskMasterApiContext> options
            = new DbContextOptionsBuilder<TaskMasterApiContext>()
            .UseInMemoryDatabase(databaseName: "testDatabase")
            .Options;

        [TestInitialize]
        public void SetupDb()
        {
            // Initialise the test database with 1 course

            using (var context = new TaskMasterApiContext(options))
            {
                Course course1 = new Course
                {
                    CourseId = 1,
                    Title = "Testcourse 1",
                    CourseCode = "TEST 101",
                    Topics = null
                };
                context.Course.Add(course1);
                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void ClearDb()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                context.Course.RemoveRange(context.Course);
                context.SaveChanges();
            };
        }

        // GET Course/
        [TestMethod]
        public void TestGetAll()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var controller = new CoursesController(context);

                // Act
                var retrievedCourses = controller.GetCourse();

                // Assert
                Assert.AreEqual(retrievedCourses.Count(), 1);
                foreach (var course in retrievedCourses)
                {
                    Assert.AreEqual(course.Title, "Testcourse 1");
                }
            }
        }

        // GET Course/{id}
        [TestMethod]
        public async Task TestGetOne()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var controller = new CoursesController(context);

                // Act
                var result = await controller.GetCourse(1);
                var course = (Course)(result as OkObjectResult).Value;

                // Assert
                Assert.AreEqual(course.Title, "Testcourse 1");
            }
        }


        [TestMethod]
        public async Task TestPost()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                Course course2 = new Course
                {
                    CourseId = 2,
                    Title = "Testcourse 2",
                    CourseCode = "TEST 202",
                    Topics = null
                };
                var controller = new CoursesController(context);

                // Act
                await controller.PostCourse(course2);

                // Assert
                Assert.AreEqual(context.Course.Count(), 2);

                var query = (from course in context.Course
                             where course.Title == "Testcourse 2"
                             select course.Title);
                Assert.AreEqual(query.First(), "Testcourse 2");
            }
        }

        [TestMethod]
        public async Task TestPut()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                Course course2 = new Course
                {
                    CourseId = 1,
                    Title = "Testcourse 2",
                    CourseCode = "TEST 202",
                    Topics = null
                };
                var controller = new CoursesController(context);

                // Act
                await controller.PutCourse(1, course2);

                // Assert
                Assert.AreEqual(context.Course.Count(), 1);

                var foundCourse = context.Course.Find(1);
                Assert.AreEqual(foundCourse.Title, course2.Title);
            }
        }

        [TestMethod]
        public async Task TestDelete()
        {
            using (var context = new TaskMasterApiContext(options))
            {
                // Arrange
                var controller = new CoursesController(context);

                // Act
                await controller.DeleteCourse(1);

                // Assert
                Assert.AreEqual(0, context.Course.Count());
            }
        }
    }
}
