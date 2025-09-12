using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace TCPData
{
    public static class Data
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            Employee employee = new Employee
            {
                ID = 1,
                FirstName = "Bob",
                LastName = "Jones",
                AnnualSalary = 60000.3m,
                IsManager = true,
                DepartmentId = 2,

            };
            employees.Add(employee);

            employee = new Employee
            {
                ID = 2,
                FirstName = "Sarah",
                LastName = "Jameson",
                AnnualSalary = 80000.1m,
                IsManager = false,
                DepartmentId = 2,

            };
            employees.Add(employee);
            employee = new Employee
            {
                ID = 3,
                FirstName = "Douglas",
                LastName = "Roberts",
                AnnualSalary = 45000.2m,
                IsManager = false,
                DepartmentId = 2,
            };
            employees.Add(employee);

            employee = new Employee
            {
                ID = 4,
                FirstName = "Jane",
                LastName = "Stevens",
                AnnualSalary = 30000.2m,
                IsManager = false,
                DepartmentId = 3,
            };
            employees.Add(employee);
            return employees;
        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            Department department = new Department
            {
                Id = 1,
                ShortName = "HR",
                LongName = "Human Resources"
            };
            departments.Add(department);

            department = new Department
            {
                Id = 2,
                ShortName = "FN",
                LongName = "Finance"
            };
            departments.Add(department);

            department = new Department
            {
                Id = 3,
                ShortName = "IT",
                LongName = "Information Technology"
            };
            departments.Add(department);

            return departments;
        }
    }
}
