using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CA2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<Employee> ListAll = new ObservableCollection<Employee>();
        ObservableCollection<Employee> employeeList = new ObservableCollection<Employee>();
        ObservableCollection<Partimer> partimeList = new ObservableCollection<Partimer>();
        ObservableCollection<Contractor> contractorList = new ObservableCollection<Contractor>();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < 10; i++)
            {
                Employee emp = new Employee(GetFirstNAme(i), GetLastNAme(i), GetSalary(i));
                ListAll.Add(emp);


            }

            for (int i = 0; i < 10; i++)
            {
                Partimer part = new Partimer(GetFirstNAme(i), GetLastNAme(i), GetRate(i), GetHours(i));
                ListAll.Add(part);
            }

            for (int i = 0; i < 10; i++)
            {
                Contractor contr = new Contractor(GetFirstNAme(i), GetLastNAme(i), GetRate(i), GetHours(i), GetStartdate(i), GetEndDate(i));
                ListAll.Add(contr);
            }

            CreateLists();

            lbxAll.ItemsSource = ListAll;

            txbEmployee.Text = employeeList.Count.ToString();
            txbPartime.Text = partimeList.Count.ToString();
            txbContractor.Text = contractorList.Count.ToString();

        }


        public void CreateLists()
        {


            foreach (Employee emp in ListAll)
            {
                Type type = emp.GetType();

                if (type.Name == "Employee")
                {
                    employeeList.Add(emp);
                }
                else if (type.Name == "Partimer")
                {
                    partimeList.Add(emp as Partimer);
                }
                else contractorList.Add(emp as Contractor);

            }


        }




        #region Methods for random object generation

        public string GetFirstNAme(int i)
        {
            List<string> firstnames = new List<string> { "MARY", "PATRICIA", "LINDA", "BARBARA", "ELIZABETH", "JENNIFER", "MARIA", "SUSAN", "MARGARET",
                "DOROTHY", "LISA", "JAMES", "JOHN", "ROBERT", "MICHAEL", "WILLIAM", "DAVID", "RICHARD", "CHARLES", "JOSEPH", "THOMAS" };

            Random rnd = new Random(i);
            int name = rnd.Next(1, firstnames.Count);
            return firstnames[name];



        }

        public string GetLastNAme(int i)
        {
            List<string> lastnames = new List<string> { "SMITH", "JOHNSON", "WILLIAMS", "JONES", "BROWN", "DAVIS", "MILLER", "WILSON", "MOORE", "TAYLOR",
                "ANDERSON", "THOMAS", "JACKSON", "WHITE", "HARRIS", "MARTIN", "THOMPSON", "ROBINSON", "CLARK", "LEWIS", "LEE" };

            Random rnd = new Random(i);
            int name = rnd.Next(1, lastnames.Count);

            return lastnames[name];

        }

        public decimal GetSalary(int i)
        {
            Random rnd = new Random(i);
            decimal salary = rnd.Next(20000, 40000);
            return salary;

        }


        public int GetRate(int i)
        {
            Random rand = new Random(i);
            int rate = rand.Next(10, 40);
            return (rate);
        }

        public int GetHours(int i)
        {
            Random rand = new Random(i);
            int hours = rand.Next(10, 20);
            return (hours);
        }

        public DateTime GetStartdate(int i)
        {
            Random rand = new Random(i);
            int days = rand.Next(1, 364);
            DateTime startdate = DateTime.Today.AddDays(days *-1);
            return (startdate);
        }

        public DateTime GetEndDate(int i)
        {
            Random rand = new Random(i);
            int days = rand.Next(1, 364);
            DateTime endate = DateTime.Today.AddDays(days);
            return (endate);
        }

        private void rbtAll_Checked(object sender, RoutedEventArgs e)
        {
            lbxAll.ItemsSource = ListAll;
        }

        private void rbtEmployees_Checked(object sender, RoutedEventArgs e)
        {
            lbxAll.ItemsSource = employeeList;
        }

        private void rbtPartTime_Checked(object sender, RoutedEventArgs e)
        {
            lbxAll.ItemsSource = partimeList;
        }

        private void rbtContractors_Checked(object sender, RoutedEventArgs e)
        {
            lbxAll.ItemsSource = contractorList;
        }

        #endregion



        //private string FirstName()
        //{

        //    List<string> firstnames = new List<string> { "MARY", "PATRICIA", "LINDA", "BARBARA", "ELIZABETH", "JENNIFER", "MARIA", "SUSAN", "MARGARET",
        //        "DOROTHY", "LISA", "JAMES", "JOHN", "ROBERT", "MICHAEL", "WILLIAM", "DAVID", "RICHARD", "CHARLES", "JOSEPH", "THOMAS" };

        //    List<string> lastnames = new List<string> { "SMITH", "JOHNSON", "WILLIAMS", "JONES", "BROWN", "DAVIS", "MILLER", "WILSON", "MOORE", "TAYLOR",
        //        "ANDERSON", "THOMAS", "JACKSON", "WHITE", "HARRIS", "MARTIN", "THOMPSON", "ROBINSON", "CLARK", "LEWIS", "LEE" };



        //    Random rnd = new Random();
        //    // Generate random indexes for names.
        //    int firstIndex = rnd.Next(firstnames.Count);
        //    int lastIndex = rnd.Next(lastnames.Count);

        //    for (int i = 0; i < firstnames.Count; i++)
        //    {
        //        firstIndex = rnd.Next(firstnames.Count);
        //        firstnames.RemoveAt(firstIndex);
        //    }


        //}
        //private string GetlastName()
        //{

        //    List<string> lastnames = new List<string> { "SMITH", "JOHNSON", "WILLIAMS", "JONES", "BROWN", "DAVIS", "MILLER", "WILSON", "MOORE", "TAYLOR",
        //        "ANDERSON", "THOMAS", "JACKSON", "WHITE", "HARRIS", "MARTIN", "THOMPSON", "ROBINSON", "CLARK", "LEWIS", "LEE" };



        //    Random rnd = new Random();
        //    // Generate random indexes for names.
        //    int firstIndex = rnd.Next(lastnames.Count);
        //    int lastIndex = rnd.Next(lastnames.Count);

        //    for (int i = 0; i < lastnames.Count; i++)
        //    {
        //        firstIndex = rnd.Next(lastnames.Count);
        //        lastnames.RemoveAt(firstIndex);

        //    }


        //}



    }


}
