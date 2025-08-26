using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._4
{
    internal class StudyingPlatfrom
    {
        public string PlatformName { get; set; }
        private List<User> users;
        private List<Course> courses;
        private List<Certificate> certificates;
        private EnrollmentManager enrollmentManager;

        public StudyingPlatfrom(string platformName)
        {
            PlatformName = platformName;
            users = new List<User>();
            courses = new List<Course>();
            certificates = new List<Certificate>();
            enrollmentManager = new EnrollmentManager();
        }

        public static void Main (string[] args)
        {
            var platform = new StudyingPlatfrom("Cool Name");

            var teacher01 = platform.RegisterNewUser("teacher", "Prof. Jolly") as Teacher;

            var student1 = platform.RegisterNewUser("student", "Anna Mueller") as Student;
            var student2 = platform.RegisterNewUser("student", "Max Weber") as Student;

            var csharpCourse = teacher01.CreateNewCourse("c#", "einführung in die Programmierung");

            csharpCourse.AddTest(new Test("Grundlagen", 1));

            teacher01.PublishCourse(csharpCourse);

            platform.enrollmentManager.EnrollStudent(student1, csharpCourse);
            platform.enrollmentManager.EnrollStudent(student2, csharpCourse);

            student1.TakeTest(csharpCourse.Tests[0]);

            platform.enrollmentManager.UnenrollStudent(student2, csharpCourse);
            Console.Write("\n");
            platform.enrollmentManager.PrintAllEnrollments();

            var certificate01 = platform.IssueCertificate(student1, csharpCourse);
            platform.GetCertificates();

        }

        public User RegisterNewUser(string userType, string name)
        {
            switch (userType.ToLower())
            {
                case "student":
                    return new Student(name, GenerateNewId());

                case "teacher":
                    return new Teacher(name, GenerateNewId());

                default:
                    throw new ArgumentException("Invalid user type");
            }
        }

        public Certificate IssueCertificate(Student student, Course course)
        {
            var certifivate = new Certificate(course.CourseName, student);
            certificates.Add(certifivate);
            Console.WriteLine($"Certificate issued to {student.Name} for completing the course '{course.CourseName}'");
            return certifivate;
        }


        // Hilfmethode
        private int GenerateNewId()
        {
            return new Random().Next(1000, 9999);
        }

        //Hilfmethode
        public List<Certificate> GetCertificates()
        {
            foreach (var cert in certificates)
            {
                Console.WriteLine($"Certificate ID: {cert.CertificateId}");
                Console.WriteLine($"Course: {cert.CourseName}");
                Console.WriteLine($"Issued To: {cert.AssignedTo.Name}");
                Console.WriteLine($"Issue Date: {cert.IssueDate}");
                Console.WriteLine();
            }

            return certificates;
        }
    }
}