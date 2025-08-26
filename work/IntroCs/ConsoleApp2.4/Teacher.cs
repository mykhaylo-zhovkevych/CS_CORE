using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._4
{
    internal class Teacher : User
    {

        public Teacher(string name, int id) : base(name, id)
        {
            
        }

        public Course CreateNewCourse(string title, string description)
        {
            // Validierung

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description can not be a null");

            // fiktive Id
            int newCourseId =+ 1;

            Course newCourse = new Course(title, newCourseId, description);

            Console.WriteLine($"Teacher {Name} has Course'{title}' created with (ID: {newCourseId})");
            return newCourse;
        }

        public void PublishCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course), "Price cannot be null");

            if (course.IsPublished)
            {
                Console.WriteLine($"Course '{course.CourseName}' is allready published");
                return;
            }

            course.IsPublished = true;
            Console.WriteLine($"Course '{course.CourseName}' had been {Name} published");
        }

    }
}
