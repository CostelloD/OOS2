namespace Hospital
{
    public class Ward
    {

        // variable to count the number of wards (public to make accessible to xaml code)
        public static int numberOfWards = 0;

        // two properties for the Ward class
        public string WardName { get; set; }
        public int Capacity { get; set; }


        //full contructor including a counter for number of wards
        public Ward(string wardName, int capacity)
        {
            WardName = wardName;
            Capacity = capacity;
            numberOfWards++;
        }


        // chained contructors to allow completion of object depending on what information is available
        public Ward(string wardName)
            : this(wardName, 20) { }


        public Ward(int capacity)
            : this("UnNamed", capacity) { }


        public Ward()
            : this("UnNamed", 10) {}


        //Overidden ToString method to display ward details where required
        public override string ToString()
        {
            return string.Format("{0} Ward" + "\t\t\t\t(Limit: {1})",WardName,Capacity );
        }

     
    }
}
