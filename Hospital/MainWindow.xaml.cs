using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace Hospital
{

    /* The application allows the user to create a collection of Wards and a collection of Patients to populate these Wards. 
     * Functionality is included to display the ward and patient details and Error checking is provided for when entering data to create new wards and patients.
     * The logic behind the application was that a Ward could be created in isolation giving the ward a name and capacity (or default values if no parameters assigned)
     * but a patient must belong to a ward if they have been accepted into the Hospital. It was because of this relationship that the Patient class inherits from Ward class.
     * This means that when creating a Patient object the user must select the Ward that the Patient is to be assigned to (only patients created within the code at
     * startup can be created without a previously defined ward object)
     * Chained contructors are used which iterate through all constructors if a Patient object is created and jsut through the Ward class constructors
     * if a new ward is created.
     * Load and Save buttons have been added to manage the json files which save the Wards and Patients separately.
     */



    public partial class MainWindow : Window
    {

        //observable collections to be able to list the Wards, Patients and Patients within a selected Ward (i.e. the MatchingPatients collection)
        ObservableCollection<Ward> ListWards = new ObservableCollection<Ward>();
        ObservableCollection<Patient> ListPatients = new ObservableCollection<Patient>();
        ObservableCollection<Patient> MatchingPatients = new ObservableCollection<Patient>();


        public MainWindow()
        {
            InitializeComponent();
        }


        //window loaded method that triggers when application starts and calls a 'Populate' method to add some wards and patients to the observable collections
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Populate();
        }



        #region region containing methods triggered by events on xaml page
        //displays the ward details when a new selection is made in the listbox
        private void lbxWard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePatient();
        }

        //displays the patient details if or when a patient is selected
        private void lbxPatient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lbxPatient.SelectedItem != null)
            {
                //set selected item as a patient
                Patient selectedpatient = lbxPatient.SelectedItem as Patient;
                //display patients name
                txbName.Text = selectedpatient.PatientName;
                //show correct bloodtype
                imageBType.Source = new BitmapImage(new Uri(string.Format($"/images/{selectedpatient.BloodGroup}.png"), UriKind.Relative));
            }

        }

        //method to create a new patient object
        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            //get the ward details for the patient
            string wardname = (lbxWard.SelectedItem as Ward).WardName;
            int capacity = (lbxWard.SelectedItem as Ward).Capacity;

            //get the patients details (use the getbloodtype method here)
            string patientname = txbPatient.Text;
            Patient.BloodType type = GetBloodType();
            DateTime dob;

            //make sure a DOB is chosen
            if (dpkDOB.SelectedDate.HasValue == false)
            {
                MessageBox.Show("You must select a Date of Birth");
                return;
            }
            else
            {
                dob = dpkDOB.SelectedDate.Value;
            }

            //make sure there is place for the patient in the selected ward
            if (CountPatients(wardname) >= capacity)
            {
                MessageBox.Show("The number of patients for this Ward will be exceeded, please chose a different ward");
                return;
            }

            //update list, set so as last patient added is selected and disable the add patient button
            ListPatients.Add(new Patient(wardname, capacity, patientname, type, dob));
            lbxPatient.SelectedIndex = MatchingPatients.Count;
            btnAddPatient.IsEnabled = false;
            UpdatePatient();
        }

        //Method to add a new ward
        private void btnAddWard_Click(object sender, RoutedEventArgs e)
        {
            string wardname = txbWard.Text;
            int capacity = Convert.ToInt32(sldCapacity.Value);

            foreach (Ward ward in ListWards)
            {
                //if blank or name already used choose again
                if (ward.WardName == wardname | ward.WardName == null)
                {
                    MessageBox.Show("Enter a ward or choose a different ward name");
                    return;
                }

                //ensure at least one person can be in the ward
                if (capacity < 1)
                {
                    MessageBox.Show("Ward must have capacity of at least one patient");
                    return;
                }


            }

            //update ward details and disable add ward button
            ListWards.Add(new Ward(wardname, capacity));
            lbxWard.ItemsSource = ListWards;
            lbxWard.SelectedIndex = ListWards.Count();
            tblWardHeader.Text = string.Format("Ward ({0})", (Ward.numberOfWards - Patient.numberOfPatients));
            btnAddWard.IsEnabled = false;


        }
       
        //method to get value of slider
        private void sldCapacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txbCapacity.Text = string.Format("{0:F0}", sldCapacity.Value);
        }

        //methods to enable buttons
        private void txbWard_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnAddWard.IsEnabled = true;
        }

        private void txbPatient_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnAddPatient.IsEnabled = true;
        }

        //load ward and patient details from json file
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            /*reset the counters for wards and patients as these are about to be loaded from the json file
            - wards and patients will be counted as they pass through the contructors for the class objects*/
            Ward.numberOfWards = 0;
            Patient.numberOfPatients = 0;

            //clear the lists to load the new data from the json files
            ListWards.Clear();
            ListPatients.Clear();

            //deserialise the wards objects, put in list & set item source for display
            using (StreamReader sr = new StreamReader(@"c:\Temp\Wards.json"))
            {
                string json = sr.ReadToEnd();

                ListWards = JsonConvert.DeserializeObject<ObservableCollection<Ward>>(json);
                lbxWard.ItemsSource = ListWards;
            }

            //deserialise the patient objects, put in list & set item source for display
            using (StreamReader sr = new StreamReader(@"c:\Temp\Patients.json"))
            {
                string json = sr.ReadToEnd();

                ListPatients = JsonConvert.DeserializeObject<ObservableCollection<Patient>>(json);
                lbxPatient.ItemsSource = ListPatients;
            }
        }

        //save ward and patient details to json file
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            //write ward objects to file
            string json = JsonConvert.SerializeObject(ListWards, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(@"c:\Temp\Wards.json"))
            {
                sw.Write(json);
            }

            //write patient objects to file
            json = JsonConvert.SerializeObject(ListPatients, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(@"c:\Temp\Patients.json"))
            {
                sw.Write(json);
            }
        }
        #endregion


        #region Methods for updating, counting, populating and getting bloodtypes of patients
        //method that populates patients and wards when loaded
        public void Populate()

        {
            //create the wards
            Ward W1 = new Ward("St.Marys", 20);
            Ward W2 = new Ward("Lukes", 15);
            Ward W3 = new Ward("St. Josephs", 15);
            Ward W4 = new Ward("st.Attractas", 15);
            Ward W5 = new Ward("Brendans", 12);
            //a blank ward constructor
            Ward W6 = new Ward();


            //add the wards to the collection
            ListWards.Add(W1);
            ListWards.Add(W2);
            ListWards.Add(W3);
            ListWards.Add(W4);
            ListWards.Add(W5);
            ListWards.Add(W6);

            //create patient objects
            Patient P1 = new Patient("St.Marys", 20, "Joe", Patient.BloodType.A, DateTime.Today.AddYears(-34));
            Patient P2 = new Patient("Lukes", 15, "Ann", Patient.BloodType.B, DateTime.Today.AddYears(-26));
            Patient P3 = new Patient("St. Josephs", 15, "Mary", Patient.BloodType.A, DateTime.Today.AddYears(-48));
            Patient P4 = new Patient("Brendans", 12, "Pete", Patient.BloodType.AB, DateTime.Today.AddYears(-22));
            Patient P5 = new Patient("", 15, "Jane", Patient.BloodType.A, DateTime.Today.AddYears(-57));
            Patient P6 = new Patient("St.Marys", 20, "Alice", Patient.BloodType.O, DateTime.Today.AddYears(-45));
            Patient P7 = new Patient("St. Josephs", 15, "John", Patient.BloodType.A, DateTime.Today.AddYears(-78));
            Patient P8 = new Patient("Lukes", 15, "Tom", Patient.BloodType.unknown, DateTime.Today.AddYears(-82));
            Patient P9 = new Patient("", 10, "Tom", Patient.BloodType.unknown, DateTime.Today.AddYears(-82));
            //an empty patient contstuctor
            Patient P10 = new Patient();

            //add patients to the collection
            ListPatients.Add(P1);
            ListPatients.Add(P2);
            ListPatients.Add(P3);
            ListPatients.Add(P4);
            ListPatients.Add(P5);
            ListPatients.Add(P6);
            ListPatients.Add(P7);
            ListPatients.Add(P8);
            ListPatients.Add(P9);
            ListPatients.Add(P10);


            //set the listbox sources to the observable collections and set index to 0 to have the first item in the collection selected when loaded
            lbxWard.ItemsSource = ListWards;
            lbxWard.SelectedIndex = 0;
            lbxPatient.ItemsSource = ListPatients;
            lbxPatient.SelectedIndex = 0;

            // display the number of wards in the ward header
            tblWardHeader.Text = string.Format("Ward ({0})", (Ward.numberOfWards - Patient.numberOfPatients));

            // call the update patient method so that their details are displayed (i.e. bloodgroup and name)
            UpdatePatient();

        }

        //method to update the patient details 
        public void UpdatePatient()
        {
            //empty the matchingpatients collection
            MatchingPatients.Clear();

            //If null select the first item in the list (this can happen when page is loaded)
            if (lbxWard.SelectedItem == null)
            {
                lbxWard.SelectedIndex = 0;
            }

            //set the selected ward as as ward object
            Ward selectedward = lbxWard.SelectedItem as Ward;

            //iterate through the patient list and add the patient to the 'matching patients' list if the name matches the ward
            foreach (var patient in ListPatients)
            {
                if (selectedward == null)
                {
                    return;
                }

                else if (patient.WardName == selectedward.WardName)
                {
                    MatchingPatients.Add(patient);
                }

            }

            //set the patients listbox source to display the patients in the selected ward and set the first patient in that list as selected
            lbxPatient.ItemsSource = MatchingPatients;
            lbxPatient.SelectedIndex = 0;

            //display number of wards (as patients inherit from wards we simply subract the number of patient objects from ward ojects to get number of wards)
            tblWardHeader.Text = string.Format("Ward ({0})", (Ward.numberOfWards - Patient.numberOfPatients));
        }

        //method to count number of patients in a ward
        public int CountPatients(string ward)
        {
            int counter = 0;

            foreach (Patient patient in ListPatients)
            {
                if (patient.WardName == ward)
                {
                    counter++;
                }
            }
            return counter;
        }

        //Method used to get bloodgroup from radio buttons
        private Patient.BloodType GetBloodType()
        {
            //default type O as all patients can receive O (and all people with type O can only receive type O)
            Patient.BloodType type = Patient.BloodType.O;

            if (A.IsChecked == true)
            {
                type = Patient.BloodType.A;
            }
            else if (B.IsChecked == true)
            {
                type = Patient.BloodType.AB;
            }
            else if (AB.IsChecked == true)
            {
                type = Patient.BloodType.AB;
            }

            else if (O.IsChecked == true)
            {
                type = Patient.BloodType.O;
            }

            //message you must chose a bloodgroup
            else
            {
                MessageBox.Show("You must select a Bloodgroup");
            }

            return type;
        }

        #endregion

    }
}
