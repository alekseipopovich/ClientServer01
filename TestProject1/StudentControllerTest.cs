using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;
using WebAPI.Controllers;
using WebAPI.Entities;

namespace TestProject1
{
    public class StudentsControllerTests
    {
        private ApplicationContext GetTestDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite($"Data Source=:memory:") // Используем in-memory SQLite
                .Options;

            var context = new TestApplicationContext(options);
            context.Database.OpenConnection(); // Открываем соединение для in-memory базы
            context.Database.EnsureCreated(); // Создаем схему базы данных
            return context;
        }

        private class TestApplicationContext : ApplicationContext
        {
            public TestApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options)
            {
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                // Пустой метод для предотвращения конфликта настроек
            }
        }

        [Fact]
        public async Task GetStudents_ReturnsAllStudents()
        {
            // Arrange
            using var context = GetTestDbContext();
            var controller = new StudentsController(context);
            var testStudents = new List<Student>
            {
                new Student { FirstName = "Test1", LastName = "User1" },
                new Student { FirstName = "Test2", LastName = "User2" }
            };
            await context.Students.AddRangeAsync(testStudents);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.GetStudents();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Student>>>(result);
            var students = Assert.IsAssignableFrom<IEnumerable<Student>>(actionResult.Value);
            Assert.Equal(2, students.Count());
        }

        [Fact]
        public async Task GetStudent_ReturnsStudent_WhenStudentExists()
        {
            // Arrange
            using var context = GetTestDbContext();
            var controller = new StudentsController(context);
            var testStudent = new Student
            {
                FirstName = "Test",
                LastName = "User"
            };
            context.Students.Add(testStudent);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.GetStudent(testStudent.StudentId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Student>>(result);
            var student = Assert.IsType<Student>(actionResult.Value);
            Assert.Equal(testStudent.StudentId, student.StudentId);
        }

        [Fact]
        public async Task GetStudent_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            using var context = GetTestDbContext();
            var controller = new StudentsController(context);

            // Act
            var result = await controller.GetStudent(999); // Несуществующий ID

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateStudent_ReturnsCreatedAtAction()
        {
            // Arrange
            using var context = GetTestDbContext();
            var controller = new StudentsController(context);
            var newStudent = new Student
            {
                FirstName = "New",
                LastName = "Student"
            };

            // Act
            var result = await controller.CreateStudent(newStudent);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Student>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<Student>(createdAtActionResult.Value);
            Assert.Equal(newStudent.FirstName, returnValue.FirstName);
        }

        [Fact]
        public async Task UpdateStudent_ReturnsNoContent_WhenStudentExists()
        {
            // Arrange
            using var context = GetTestDbContext();
            var controller = new StudentsController(context);
            var student = new Student
            {
                FirstName = "Original",
                LastName = "Name"
            };
            context.Students.Add(student);
            await context.SaveChangesAsync();

            var studentToUpdate = new Student
            {
                StudentId = student.StudentId,
                FirstName = "Updated",
                LastName = student.LastName
            };

            // Act
            var result = await controller.UpdateStudent(student.StudentId, studentToUpdate);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var updatedStudent = await context.Students.FindAsync(student.StudentId);
            Assert.Equal("Updated", updatedStudent.FirstName);
        }

        [Fact]
        public async Task DeleteStudent_ReturnsNoContent_WhenStudentExists()
        {
            // Arrange
            using var context = GetTestDbContext();
            var controller = new StudentsController(context);
            var student = new Student
            {
                FirstName = "Test",
                LastName = "User"
            };
            context.Students.Add(student);
            await context.SaveChangesAsync();

            var studentId = student.StudentId; // Сохраняем ID после добавления

            // Act
            var result = await controller.DeleteStudent(studentId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Null(await context.Students.FindAsync(studentId));
        }

        [Fact]
        public async Task DeleteStudent_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange
            using var context = GetTestDbContext();
            var controller = new StudentsController(context);

            // Act
            var result = await controller.DeleteStudent(999); // Несуществующий ID

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [MemberData(nameof(GetTestStudentData))]
        public async Task CreateStudent_WithVariousData_ReturnsExpectedResults(Student student, bool shouldSucceed)
        {
            // Arrange
            using var context = GetTestDbContext();
            var controller = new StudentsController(context);

            if (!shouldSucceed)
            {
                // Добавляем ошибку в ModelState перед вызовом метода
                controller.ModelState.AddModelError("FirstName", "First Name is required");
            }

            // Act
            var result = await controller.CreateStudent(student);

            // Assert
            if (shouldSucceed)
            {
                var actionResult = Assert.IsType<ActionResult<Student>>(result);
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
                Assert.NotNull(createdAtActionResult.Value);
            }
            else
            {
                // Для невалидных данных ожидаем BadRequest
                var actionResult = Assert.IsType<ActionResult<Student>>(result);
                Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            }
        }

        public static IEnumerable<object[]> GetTestStudentData()
        {
            yield return new object[]
            {
                new Student
                {
                    FirstName = "Valid",
                    LastName = "Student",
                    Age = 20,
                    Address = "Test Address"
                },
                true
            };
            yield return new object[]
            {
                new Student
                {
                    FirstName = null,
                    LastName = null
                },
                false
            };
        }
    }
}
