using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._4
{
    internal class Course
    {

        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public List<Test> Tests { get; private set; }

        public Course(string courseName, int courseId, string description)
        {
            CourseName = courseName;
            CourseId = courseId;
            Description = description;
            IsPublished = false;
            Tests = new List<Test>();
        }

        public void AddTest(Test test)
        {
            if (test == null)
                throw new ArgumentNullException(nameof(test), "Test must not be null");

            if (Tests.Any(t => t.Id == test.Id))
                throw new InvalidOperationException($"Test with this {test.Id} allready exists");

            Tests.Add(test);
            Console.WriteLine($"Test '{test.TestName}' was added to this Course:'{CourseName}'");
        }

        public void RemoveTest(Test test)
        {
            if (test != null && Tests.Contains(test))
            {
                Tests.Remove(test);
                Console.WriteLine($"Test '{test.TestName}' was removed from this Course: '{CourseName}' ");
            }
        }

        public override string ToString()
        {
            return $"Course: {CourseName} (ID: {CourseId}) - {Tests.Count} Tests";
        }
    }
}
