using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA2
{
    class Contractor : Partimer
    {
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set;}


        public Contractor(string firstName, string lastName,  decimal rate, int hours, DateTime endDate, DateTime startDate) 
            : base(firstName, lastName, rate, hours)
        {
            EndDate = endDate;
            StartDate = startDate;
        }

        public Contractor(string firstName, string lastName, decimal rate, int hours, DateTime endDate)
            : this (firstName, lastName, rate, hours, endDate, DateTime.Today.AddMonths(-12))
        {
        }

        public Contractor(string firstName, string lastName,  decimal rate, int hours)
            : this (firstName, lastName,  rate, hours, DateTime.Today, DateTime.Today.AddMonths(-12))
        {
        }

        public override string ToString()
        {
            return string.Format("[CONTRACTOR] {0} {1} Weekly Wages: {2} StartDate: {3} EndDate: {4}", FirstName, LastName, (Rate * Hours), StartDate.ToShortDateString(), EndDate.ToShortDateString());
        }

    }
}
