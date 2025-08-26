using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._4
{
    internal class EnrollmentManager
    {

        private List<(Student student, Course course)> enrollments;


        public EnrollmentManager()
        {
            enrollments = new List<(Student, Course)>();
        }


        public void EnrollStudent(Student student, Course course)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student cannot be null");

            if (course == null)
                throw new ArgumentNullException(nameof(student), "Course cannot be null");

            if (enrollments.Any(e => e.student == student && e.course == course))
            {
                Console.WriteLine($"Student {student.Name} is allready enrolled in '{course.CourseName}'");
                return;
            }

            enrollments.Add((student, course));

            Console.WriteLine($"Student {student.Name} was successfully '{course.CourseName}' enrolled");
        }

        public void UnenrollStudent(Student student, Course course) 
        { 
        
            if (student == null || course == null)
 
                return;

            var enrollmentToRemove = enrollments.FirstOrDefault(e => e.student == student && e.course == course);
            if (enrollmentToRemove != default)
            {
                enrollments.Remove(enrollmentToRemove);
                Console.WriteLine($"Student {student.Name} was removed from this Course: {course.CourseName}");
            }
        }

        public void PrintAllEnrollments()
        {
            if (!enrollments.Any())
            {
                Console.WriteLine("No enrollments found.");
                return;
            }

            foreach (var (studnt,course) in enrollments)
            {
                Console.WriteLine($"Student: {studnt.Name} is enrolled in Course: {course.CourseName}");
            }

        }

    }
}