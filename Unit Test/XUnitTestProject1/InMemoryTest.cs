using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using XUnitTestProject1.Factory;

namespace XUnitTestProject1
{
    public class InMemoryTest
    {
        [Fact]
        public void Test1()
        {
            // We can add an enrollment that has a coruse that does not exist in the db. Violates referential integrity
            //Same test wud fail wen using Sqlite as it preserves referential integrity
            //Arrange and Act
            var newEnrollment = new Enrollment { CourseID = 1, EnrollmentID = 1, StudentID = 1, Grade = Grade.A };
            var  inMemoryOption = new DbContextOptionsBuilder<SchoolContext>().UseInMemoryDatabase(databaseName: "Test_Database").Options;
            using (var context = new SchoolContext(inMemoryOption))
            {
                context.Enrollment.Add(newEnrollment);
                context.SaveChanges();
            }

           //Assert
            using (var context = new SchoolContext(inMemoryOption))
            {
                var expected =context.Enrollment.FirstOrDefault();
                Assert.True(newEnrollment.StudentID == expected.StudentID);
            }

        }
    }
}
