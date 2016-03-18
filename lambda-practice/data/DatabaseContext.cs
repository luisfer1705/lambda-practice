using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lamda_practice.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext()
            : base("Name=EmployeesDB") 
            {
                Database.SetInitializer<DatabaseContext>(new DatabaseContextInitializer<DatabaseContext>());
            }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}
