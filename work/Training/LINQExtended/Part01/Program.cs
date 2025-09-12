using System;
using System.Collections.Generic;
using TCPData;
using TCPExtensions;
using System.Linq;
using System.Data.Common;



namespace Part01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employeesList = Data.GetEmployees();

            //// I dont need to pass the first paramater, because it is handelted in the method itself
            //// And reason for that is by default the first parameter is the object on which the relevent extension is invoked 
            //var filteredEmployeeList = Extension.Filter(employeesList, emp => emp.AnnualSalary < 50000 );


            //foreach (var employee in filteredEmployeeList)
            //{
            //    Console.WriteLine($"First Name: {employee.FirstName}");
            //    Console.WriteLine($"Last Name: {employee.LastName}");
            //    Console.WriteLine($"Annual Salary: {employee.AnnualSalary}");
            //    Console.WriteLine($"Is Manager: {employee.IsManager}");
            //}

            List<Department> departmentList = Data.GetDepartments();

            // As here demonstrated the flexibility that the combination of extension methods and lambra expressions for client code is amazing,
            // It makes the client code clean and concise


            // var filteredDepartments = Extension.Filter(departmentList, dept => dept.ShortName == "HR" || dept.ShortName == "FN");
            var filteredDepartments = departmentList.Where(dept => dept.ShortName == "HR" || dept.ShortName == "FN");

            foreach (var department in filteredDepartments)
            {
                Console.WriteLine($"ID: {department.Id}");
                Console.WriteLine($"Short Name: {department.ShortName}");
                Console.WriteLine($"Long Name: {department.LongName}");
            }

            Console.WriteLine();

            var resultList = from emp in employeesList
                             join dept in departmentList
                             on emp.DepartmentId equals dept.Id
                             select new
                             {
                                 FirstName = emp.FirstName,
                                 LastName = emp.LastName,
                                 AnnualSalary = emp.AnnualSalary,
                                 Manager = emp.IsManager,
                                 Department = dept.LongName
                             };

            foreach (var employee in resultList)
            {
                Console.WriteLine($"First Name: {employee.FirstName}");
                Console.WriteLine($"Last Name: {employee.LastName}");
                Console.WriteLine($"Annual Salary: {employee.AnnualSalary}");
                Console.WriteLine($"Manager: {employee.Manager}");
                Console.WriteLine($"Department: {employee.Department}");
                Console.WriteLine();
            }


            Console.ReadKey();
        }
    }
}
