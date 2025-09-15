using System.Linq;
using static Part02.Program;

namespace Part02
{

    public static class EnumerableCollectionExtendsionMethods
    {
        internal static IEnumerable<Employee> GetHighSalarieEmployees(this IEnumerable<Employee> employees)
        {
            foreach (Employee emp in employees)
            {
                Console.WriteLine($"Accessing employee: {emp.FirstName + " " + emp.LastName}");
                if (emp.AnnualSalary >= 50000)
                    yield return emp;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            //// Method Syntax
            //// Returns enumerable collection of objects 
            //var results = employeeList.Select(e => new
            //{
            //    FullName = e.FirstName + "" + e.LastName,
            //    AnnualSalary = e.AnnualSalary

            //    // With Where I can chain the methods together,
            //    // because each Method returns a object allowing calls to be changed toghether in a single statment
            //    // without requiring varibel to store the intermediate data  
            //}).Where(e => e.AnnualSalary >= 50000);

            //foreach(var item in results)
            //{
            //    Console.WriteLine($"{item.FullName, -20}{item.AnnualSalary, 10}");
            //}

            var results = from emp in employeeList
                          where emp.AnnualSalary >= 50000
                          select new
                          {
                              FullName = emp.FirstName + " " + emp.LastName,
                              AnnualSalary = emp.AnnualSalary
                          };


            employeeList.Add(new Employee
            {
                ID = 5,
                FirstName = "Sam",
                LastName = "Davis",
                AnnualSalary = 100000.20m,
                IsManager = true,
                DepartmentId = 5,
            });

            // The execution of the query is deferred until the results are traversed within the relevant for each loop 
            foreach (var item in results)
            {
                Console.WriteLine($"{item.FullName,-20}{item.AnnualSalary,10}");
            }

            Console.Write("\n:)\n");

            // Deferred Execution Example
            var results02 = from emp in employeeList.GetHighSalarieEmployees()
                            select new
                            {
                                FullName = emp.FirstName + " " + emp.LastName,
                                AnnualSalary = emp.AnnualSalary
                            };

            employeeList.Add(new Employee
            {
                ID = 6,
                FirstName = "Tody",
                LastName = "Davis",
                AnnualSalary = 200000.20m,
                IsManager = false,
                DepartmentId = 5,
            });

            // The deferred execution re-evaluates on each execution which is know as lazy evaluation
            foreach (var item in results02)
            {
                Console.WriteLine($"{item.FullName,-20}{item.AnnualSalary,10}");
            }

            Console.Write("\n:)\n");

            // Immediate Execution Example 
            var results03 = (from emp in employeeList.GetHighSalarieEmployees()
                            select new
                            {
                                FullName = emp.FirstName + " " + emp.LastName,
                                AnnualSalary = emp.AnnualSalary
                            }).ToList();

            employeeList.Add(new Employee
            {
                ID = 7,
                FirstName = "Tody",
                LastName = "Davis",
                AnnualSalary = 200000.20m,
                IsManager = false,
                DepartmentId = 5,
            });

            Console.Write("\n:)\n");

            // Join Operation Example - Method Syntax
            //foreach (var item in results03)
            //{
            //    Console.WriteLine($"{item.FullName,-20}{item.AnnualSalary,10}");
            //}


            //var results04 = departmentList.Join(employeeList,
            //    department => department.Id,
            //    employee => employee.DepartmentId,
            //    ( department, employee) => new { 
            //        FullName = employee.FirstName + " " + employee.LastName,
            //        AnnualSalary = employee.AnnualSalary,
            //        DepartmentName = department.LongName,
            //        }
            //    );

            //foreach (var item in results04 )
            //{
            //    Console.WriteLine($"{item.FullName, -20}{item.AnnualSalary, 10}\t{item.DepartmentName}");
            //}

            // Join Operation Example that is far easier to read 
            var result05 = from dept in departmentList
                           join emp in employeeList
                           on dept.Id equals emp.DepartmentId
                           select new
                           {
                               FullName = emp.FirstName + " " + emp.LastName,
                               AnnualSalary = emp.AnnualSalary,
                               DepartmentName = dept.LongName,
                           };


            foreach (var item in result05)
            {
                Console.WriteLine($"{item.FullName,-20}{item.AnnualSalary,10}\t{item.DepartmentName}");
            }

            Console.ReadKey();
        }


        public class Employee
        {

            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal AnnualSalary { get; set; }
            public bool IsManager { get; set; }
            public int DepartmentId { get; set; }

        }


        public class Department
        {

            public int Id { get; set; }
            public string ShortName { get; set; }
            public string LongName { get; set; }

        }

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
}
