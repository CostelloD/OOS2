using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA2
{
    class Employee
    {

        public static int countEmployee;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Decimal Salary { get; set; }

        public Employee(string firstName, string lastName, decimal salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
        }

        public Employee(string firstName, string lastName)
            :this(firstName, lastName, 20000m)
        {
        }

        public Employee(string firstName)
            :this(firstName, "Unknown", 20000m)
        {
        }

        public Employee()
            :this("Unknown", "Unknown", 20000m)
        {
        }

        public override string ToString()
        {
            return string.Format("[EMPLOYEE] {0} {1} Salary: {2}", FirstName, LastName, Salary);
        }

    }
}
