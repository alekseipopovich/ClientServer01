using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace TestProject1
{
    public class StudentTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void CreateStudent_ValidData_Success()
        {
            var student = new Student { FirstName = "Name1", LastName = "Name2", Age = 20 };
            Assert.NotNull(student);
        }

        //[Theory]
        //[InlineData("", false)]
        //[InlineData("John", true)]
        //[InlineData(null, false)]
        //public void ValidateStudentName_ReturnsExpectedResult(string name, bool expectedIsValid)
        //{
        //    var student = new Student { FirstName = name };
        //    var isValid = student.ValidateName();
        //    Assert.Equal(expectedIsValid, isValid);
        //}

        //[Theory]
        //[MemberData(nameof(GetTestStudents))]
        //public void ValidateStudent_WithComplexData(Student student, bool expectedIsValid)
        //{
        //    var isValid = student.Validate();
        //    Assert.Equal(expectedIsValid, isValid);
        //}

        public static IEnumerable<object[]> GetTestStudents()
        {
            yield return new object[]
            {
            new Student { FirstName = "John", Age = 20 },
            true
            };
            yield return new object[]
            {
            new Student { FirstName = "", Age = -1 },
            false
            };
        }
    }
}
