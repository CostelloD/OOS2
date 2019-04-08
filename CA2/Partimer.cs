using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA2
{
    class Partimer : Employee
    {

        public decimal Rate { get; set; }
        public int Hours { get; set; }

        public Partimer(string firstName, string lastName, decimal rate, int hours)
            : base (firstName, lastName)
        {
            Rate = rate;
            Hours = hours;
        }

        public Partimer(string firstName, string lastName, decimal rate)
            : this(firstName, lastName, rate, 10)
        {
        }

        public Partimer(string firstName, string lastName)
            : this(firstName, lastName, 10, 10)
        {
        }

        public override string ToString()
        {
            return  string.Format("[PARTIMER] {0} {1} Weekly Wages: {2}", FirstName, LastName, (Rate*Hours));
        }






    }
}
