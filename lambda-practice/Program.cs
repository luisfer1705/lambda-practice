using lamda_practice.Data;
using System;
using System.Globalization;
using System.Linq;

namespace lambda_practice{
    class Program{
        static void Main(string[] args){
            using (var ctx = new DatabaseContext()){
                //1. Listar todos los empleados cuyo departamento tenga una sede en Chihuahua
                var employeesWithDepartmentsWithChihuahuaSede = from Employee in ctx.Employees
                                                                where Employee.Department.Cities.Any(c => c.Name == "Chihuahua")
                                                                select Employee.FirstName + " " + Employee.LastName;
                Console.WriteLine("1. \n");
                foreach (var employee in employeesWithDepartmentsWithChihuahuaSede){
                    Console.WriteLine(employee);
                }

                //2. Listar todos los departamentos y el numero de empleados que pertenezcan a cada departamento.
                var departmentsAndEmployeesByDepartment = from Department in ctx.Departments
                                                          join Employee in ctx.Employees
                                                          on Department.Id equals Employee.DepartmentId
                                                          into joinEmployeeAndDepartment
                                                          select new
                                                          {
                                                              departmentName = Department.Name,
                                                              employeesCount = joinEmployeeAndDepartment.Count()
                                                          };
                Console.WriteLine("2.\n");
                foreach (var departmentsAndEmployees in departmentsAndEmployeesByDepartment){

                    Console.WriteLine("El departamento {0} tiene {1} empleados", departmentsAndEmployees.departmentName, departmentsAndEmployees.employeesCount);
                }
                //3. Listar todos los empleados remotos. Estos son los empleados cuya ciudad no se encuentre entre las sedes de su departamento.
                var remoteEmployees = from Employee in ctx.Employees
                                      where Employee.Department.Cities.All(c => c.Name != Employee.City.Name)
                                      select Employee.FirstName + " " + Employee.LastName;

                Console.WriteLine("3.\n");
                foreach (var employee in remoteEmployees){
                    Console.WriteLine(employee);
                }

                //4. Listar todos los empleados cuyo aniversario de contratación sea el próximo mes.
                DateTime compareDate = DateTime.Now.AddMonths(1);
                var nextAniversary = from Employee in ctx.Employees
                                     where Employee.HireDate.Month == compareDate.Month
                                     select Employee.FirstName + " " + Employee.LastName;

                Console.WriteLine("4.\n");
                foreach (var employee in nextAniversary){
                    Console.WriteLine(employee);
                }

                //Listar los 12 meses del año y el numero de empleados contratados por cada mes.
                var employeesByMonth = from Employee in ctx.Employees
                                       group Employee by Employee.HireDate.Month into employeeByHireDate
                                       select new{
                                           month = employeeByHireDate.FirstOrDefault().HireDate.Month,
                                           employeeCount = employeeByHireDate.Count()
                                       };

                Console.WriteLine("5.\n");
                foreach (var employee in employeesByMonth){
                    Console.WriteLine("En el Mes {0}  Se contrataron {1} empleados", employee.month, employee.employeeCount);
                }
            }
            Console.Read();
        }
    }
}
