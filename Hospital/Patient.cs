using System;

namespace Hospital
{

    //patient inherits Ward class as each patient must belong to a ward
    public class Patient : Ward
    {

        //enum to store bloodtypes
        public enum BloodType
        {
            A,
            B,
            AB,
            O,
            unknown
        }

        //static int to count the number of patients in a ward
        public static int numberOfPatients = 0;

        //Properties of the patient class
        public string PatientName { get; set; }
        public BloodType BloodGroup{ get; set; }
        public DateTime DOB { get; set; }


        //contructor for patient which uses the Ward Base class - counter for number of patients included
        public Patient(string wardName, int capacity, string patientName, BloodType type, DateTime dob)
            : base(wardName, capacity)
        {
            
            PatientName = patientName;
            BloodGroup = type;
            DOB = dob;
            numberOfPatients++;
        }


        //chained contructors to allow all combinations of properties to be catered for.
        public Patient(string wardName, int capacity, string patientName, BloodType type)
            : this(wardName, capacity, patientName, type, DateTime.Now) { }

        public Patient(string wardName, int capacity, string patientName)
            : this(wardName, capacity, patientName, BloodType.unknown, DateTime.Now) { }

        public Patient(string wardName, int capacity)
            : this(wardName, capacity, "Unknown", BloodType.unknown, DateTime.Now) { }

        public Patient(string wardName)
            : this(wardName, 10, "Unknown", BloodType.unknown, DateTime.Now) { }

        public Patient()
            : this("UnNamed", 10, "Unknown", BloodType.unknown, DateTime.Now) { }



        //Short method to allow date of birth to be displayed as patient age
        private int GetAge(DateTime dob)
        {
            int age = Convert.ToInt32((DateTime.Today.Date.Subtract(DOB).TotalDays)/365);
            return age;
        }



        //ToString method to display patient details where needed
        public override string ToString()
        {
            int age = GetAge(DOB);
            return string.Format("{0}" + " ({1} years)" + "\t\t\tType: {2}", PatientName, age, BloodGroup);
        }


    }
}
