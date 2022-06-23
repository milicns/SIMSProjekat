using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using SIMSProjekat.Controller;
using SIMSProjekat.Model;
using SIMSProjekat.Repository;

namespace SIMSProjekat
{
    /// <summary>
    /// Interaction logic for MedicinesForApproval.xaml
    /// </summary>
    public partial class MedicinesForApproval : Page{
        private MedicineController mc = new MedicineController();
        public PharmacistView pharmacistView { get; set; }
        public DoctorView doctorView { get; set; }
        private User doctorUser;
        private User phUser;
        public ObservableCollection<Medicine> meds { get; set; }
        private List<String> pretraga;
        private List<Medicine> medicines;
        public MedicinesForApproval(DoctorView dv, User user)
        {
            InitializeComponent();
            doctorView = dv;
            doctorUser = user;
            
            
            medicines = mc.GetNotAcceptedMedicines();

            if (medicines == null)
            {
                medicines = new List<Medicine>();
            }

            ime.Text = doctorUser.Name;
            prezime.Text = doctorUser.Surname;
            if (doctorUser.TypeOfUser == UserType.Doctor)
            {
                tip.Text = "Doctor";
            }
            pretraga = new List<String>();
            pretraga.Add("Id");
            pretraga.Add("Name");
            pretraga.Add("Manufactor");
            pretraga.Add("Price");
            pretraga.Add("Quantity");
            pretraga.Add("Components");
            List<String> sortiranje = new List<String>();
            sortiranje.Add("Name");
            sortiranje.Add("Price");
            sortiranje.Add("Remaining quantity in storage");
            sortby.ItemsSource = sortiranje;
            searchby.ItemsSource = pretraga;
            meds = new ObservableCollection<Medicine>();
            foreach (Medicine med in medicines)
            {
                meds.Add(med);
            }
            MedsForAppr.ItemsSource = meds;
        }

        public MedicinesForApproval(PharmacistView pv, User user)
        {
            InitializeComponent();
            pharmacistView = pv;
            phUser = user;
            ime.Text = phUser.Name;
            prezime.Text = phUser.Surname;
            if (phUser.TypeOfUser == UserType.Pharmacist)
            {
                tip.Text = "Pharmacist";
            }
            pretraga = new List<String>();
            pretraga.Add("Id");
            pretraga.Add("Name");
            pretraga.Add("Manufactor");
            pretraga.Add("Price");
            pretraga.Add("Quantity");
            pretraga.Add("Components");
            List<String> sortiranje = new List<String>();
            sortiranje.Add("Name");
            sortiranje.Add("Price");
            sortiranje.Add("Remaining quantity in storage");
            sortby.ItemsSource = sortiranje;
            searchby.ItemsSource = pretraga;
            medicines = mc.GetNotAcceptedMedicines();
            if (medicines == null)
            {
                medicines = new List<Medicine>();
            }
            meds = new ObservableCollection<Medicine>();
            foreach (Medicine med in medicines)
            {
                meds.Add(med);
            }
            MedsForAppr.ItemsSource = meds;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Medicine medicine = MedsForAppr.SelectedItem as Medicine;
            List<Medicine> medicines = mc.GetAcceptedMedicines();
            

            if (pharmacistView != null)
            {
                        mc.AcceptMedicineByUser(medicine, phUser);
                        MessageBox.Show("Pharmacist accepted the medicine");
            }
            else
            {
                mc.AcceptMedicineByUser(medicine, doctorUser);
                MessageBox.Show("Doctor accepted the medicine");
            }
            int doctors = 0;
            int pharmacists = 0;
            List<User> users = medicine.AcceptedByUsers;
            foreach (User user in users)
            {
                if (user.TypeOfUser == UserType.Doctor)
                {
                    doctors += 1;
                }
                else
                {
                    pharmacists += 1;
                }
            }

            if (doctors == 1 && pharmacists == 2)
            {
                MessageBox.Show("Medicine is accepted into system");
                mc.ApproveMedicine(medicine);
                meds.Remove(medicine);
                MedsForAppr.ItemsSource = meds;
            }




        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (pharmacistView != null)
            {
                this.NavigationService.Navigate(new PharmacistView(phUser));
            }
            else
            {
                this.NavigationService.Navigate(new DoctorView(doctorUser));
            }
        }

        private void ButtonBase_OnClick1(object sender, RoutedEventArgs e)
        {
            Medicine medicine = MedsForAppr.SelectedItem as Medicine;
            new ShowComponents(medicine.Components).ShowDialog();
        }

        private void DeclineClick(object sender, RoutedEventArgs e)
        {
            Medicine medicine = MedsForAppr.SelectedItem as Medicine;
            if (pharmacistView != null)
            {
                this.NavigationService.Navigate(new DeclineMedicine(phUser, medicine, this));
            }
            else
            {
                this.NavigationService.Navigate(new DeclineMedicine(doctorUser, medicine, this));
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainWindow());
        }

        private void Min_OnKeyUp(object sender, KeyEventArgs e)
        {



            if ((string.IsNullOrWhiteSpace(min.Text) || string.IsNullOrWhiteSpace(max.Text)) && (!Regex.IsMatch(max.Text, @"^\d*\.{0,1}\d+$") || !Regex.IsMatch(min.Text, @"^\d*\.{0,1}\d+$")))
            {
                MedsForAppr.ItemsSource = meds;
                return;
            }

            if (!string.IsNullOrWhiteSpace(max.Text) && Regex.IsMatch(max.Text, @"^\d*\.{0,1}\d+$") && Regex.IsMatch(min.Text, @"^\d*\.{0,1}\d+$"))
            {
                var filtered =
                    new ObservableCollection<Medicine>(mc.GetMedicinesInPriceRange(Double.Parse(min.Text),
                        Double.Parse(max.Text), mc.GetNotAcceptedMedicines()));

                MedsForAppr.ItemsSource = filtered;
            }

        }

        private void Max_OnKeyUp(object sender, KeyEventArgs e)
        {

            {
                if ((string.IsNullOrWhiteSpace(min.Text) || string.IsNullOrWhiteSpace(max.Text)) && (!Regex.IsMatch(max.Text, @"^\d*\.{0,1}\d+$") || !Regex.IsMatch(min.Text, @"^\d*\.{0,1}\d+$")))
                {
                    MedsForAppr.ItemsSource = meds;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(min.Text) && Regex.IsMatch(max.Text, @"^\d*\.{0,1}\d+$") && Regex.IsMatch(min.Text, @"^\d*\.{0,1}\d+$"))
                {
                    var filtered =
                        new ObservableCollection<Medicine>(mc.GetMedicinesInPriceRange(Double.Parse(min.Text),
                            Double.Parse(max.Text), mc.GetNotAcceptedMedicines()));

                    MedsForAppr.ItemsSource = filtered;
                }
            }
        }

        private void Pretrazi_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (searchby.Text.Equals("Id"))
            {
                if (string.IsNullOrWhiteSpace(pretrazi.Text))
                {
                    MedsForAppr.ItemsSource = meds;
                    return;
                }
                var filtered = meds.Where(medicine => medicine.MedicineId.ToLower().Contains(pretrazi.Text.ToLower()));

                MedsForAppr.ItemsSource = filtered;
            }
            else if (searchby.Text.Equals("Name"))
            {
                if (string.IsNullOrWhiteSpace(pretrazi.Text))
                {
                    MedsForAppr.ItemsSource = meds;
                    return;
                }
                var filtered = meds.Where(medicine => medicine.MedicineName.ToLower().Contains(pretrazi.Text.ToLower()));

                MedsForAppr.ItemsSource = filtered;
            }
            else if (searchby.Text.Equals("Manufactor"))
            {
                if (string.IsNullOrWhiteSpace(pretrazi.Text))
                {
                    MedsForAppr.ItemsSource = meds;
                    return;
                }
                var filtered = meds.Where(medicine => medicine.Manufactor.ToLower().Contains(pretrazi.Text.ToLower()));

                MedsForAppr.ItemsSource = filtered;
            }
            else if (searchby.Text.Equals("Quantity"))
            {
                if (string.IsNullOrWhiteSpace(pretrazi.Text))
                {
                    MedsForAppr.ItemsSource = meds;
                    return;
                }
                var filtered = meds.Where(medicine => medicine.Quantity.ToString().Equals(pretrazi.Text));

                MedsForAppr.ItemsSource = filtered;
            }
            else if (searchby.Text.Equals("Components"))
            {
                if (string.IsNullOrWhiteSpace(pretrazi.Text))
                {
                    MedsForAppr.ItemsSource = meds;
                    return;
                }
                if (pretrazi.Text.Contains("&"))
                {
                    string p = Regex.Replace(pretrazi.Text, @"\s+", "");
                    char[] delimiterChars = { '&' };
                    string[] pretraga = new[] { "" };
                    pretraga = p.Split(delimiterChars);
                    var filtered = new ObservableCollection<Medicine>(GetMedicinesWithAndOperator(pretraga));

                    MedsForAppr.ItemsSource = filtered;

                }
                else if (pretrazi.Text.Contains("|"))
                {
                    string p = Regex.Replace(pretrazi.Text, @"\s+", "");
                    char[] delimiterChars = { '|' };
                    string[] pretraga = new[] { "" };
                    pretraga = p.Split(delimiterChars);
                    var filtered = new ObservableCollection<Medicine>(GetMedicinesWithOrOperator(pretraga));

                    MedsForAppr.ItemsSource = filtered;

                }
                else
                {
                    var filtered = new ObservableCollection<Medicine>(GetMedicinesWithoutOperator(pretrazi.Text));

                    MedsForAppr.ItemsSource = filtered;
                }

            }
        }

        public List<Medicine> GetMedicinesWithoutOperator(string s)
        {
            List<Medicine> medicinesWithComponents = new List<Medicine>();
            List<Medicine> medicines = mc.GetNotAcceptedMedicines();
            foreach (Medicine medicine in medicines)
            {

                if (medicine.Components.ContainsKey(s))
                {
                    medicinesWithComponents.Add(medicine);
                }


            }

            return medicinesWithComponents;
        }

        public List<Medicine> GetMedicinesWithOrOperator(string[] search)
        {
            List<Medicine> medicinesWithComponents = new List<Medicine>();
            List<Medicine> medicines = mc.GetNotAcceptedMedicines();
            foreach (Medicine medicine in medicines)
            {
                int j = 0;
                for (int i = 0; i < search.Length; i++)
                {
                    if (medicine.Components.ContainsKey(search[i]))
                    {
                        j += 1;
                        if (!medicinesWithComponents.Contains(medicine))
                        {
                            medicinesWithComponents.Add(medicine);
                        }
                    }
                    if (j == search.Length)
                    {
                        if (!medicinesWithComponents.Contains(medicine))
                        {
                            medicinesWithComponents.Add(medicine);
                        }
                    }

                }
            }

            return medicinesWithComponents;
        }
        public List<Medicine> GetMedicinesWithAndOperator(string[] search)
        {
            List<Medicine> medicinesWithComponents = new List<Medicine>();
            List<Medicine> medicines = mc.GetNotAcceptedMedicines();
            foreach (Medicine medicine in medicines)
            {
                int j = 0;
                for (int i = 0; i < search.Length; i++)
                {
                    if (medicine.Components.ContainsKey(search[i]))
                    {
                        j += 1;
                    }
                    if (j == search.Length)
                    {
                        medicinesWithComponents.Add(medicine);
                    }
                }
            }

            return medicinesWithComponents;
        }



        private void Searchby_OnSelected(object sender, RoutedEventArgs e)
        {
            if (searchby.SelectedItem.ToString().Equals("Price"))
            {
                pretrazi.Visibility = Visibility.Hidden;
                min.Visibility = Visibility.Visible;
                max.Visibility = Visibility.Visible;
                minprice.Visibility = Visibility.Visible;
                maxprice.Visibility = Visibility.Visible;
            }
            else
            {
                pretrazi.Visibility = Visibility.Visible;
                min.Visibility = Visibility.Hidden;
                max.Visibility = Visibility.Hidden;
                minprice.Visibility = Visibility.Hidden;
                maxprice.Visibility = Visibility.Hidden;
            }
        }

        private void Sortby_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sortby.SelectedItem.ToString().Equals("Name"))
            {
                medicines.Sort((x, y) => y.MedicineName.CompareTo(x.MedicineName));
                MedsForAppr.ItemsSource = new ObservableCollection<Medicine>(medicines);
            }
            else if (sortby.SelectedItem.ToString().Equals("Price"))
            {
                medicines.Sort((x, y) => x.Price.CompareTo(y.Price));
                MedsForAppr.ItemsSource = new ObservableCollection<Medicine>(medicines);
            }
            else if (sortby.SelectedItem.ToString().Equals("Remaining quantity in storage"))
            {
                medicines.Sort((x, y) => x.QuantityInStorage.CompareTo(y.QuantityInStorage));
                MedsForAppr.ItemsSource = new ObservableCollection<Medicine>(medicines);
            }
        }
    }
}
