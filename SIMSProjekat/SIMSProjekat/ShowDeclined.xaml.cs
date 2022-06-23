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

namespace SIMSProjekat
{
    /// <summary>
    /// Interaction logic for ShowDeclined.xaml
    /// </summary>
    public partial class ShowDeclined : Window
    {
        private MedicineController mc = new MedicineController();
        private ObservableCollection<Medicine> lekovi;
        private List<String> pretraga;
        List<Medicine> medicines;
        public ShowDeclined()
        {
            InitializeComponent();
            
            medicines = mc.GetDeclinedMedicines();
            if (medicines == null)
            {
                medicines = new List<Medicine>();
            }
            lekovi = new ObservableCollection<Medicine>();
            foreach (Medicine medicine in medicines)
            {
                lekovi.Add(medicine);
            }

            this.DataContext = this;
            pretraga = new List<string>();
            pretraga.Add("Id");
            pretraga.Add("Name");
            pretraga.Add("Manufactor");
            pretraga.Add("Price");
            pretraga.Add("Quantity");
            pretraga.Add("Components");
            List<String> sortiranje = new List<string>();
            sortiranje.Add("Name");
            sortiranje.Add("Price");
            sortiranje.Add("Remaining quantity in storage");
            sortby.ItemsSource = sortiranje;
            searchby.ItemsSource = pretraga;
            MedicineDataGrid.ItemsSource = lekovi;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Medicine medicine = MedicineDataGrid.SelectedItem as Medicine;
            new ShowDetails(medicine).ShowDialog();
        }

        private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            Medicine medicine = (Medicine)MedicineDataGrid.SelectedItem;
            new ShowComponents(medicine.Components).ShowDialog();
        }

        private void Min_OnKeyUp(object sender, KeyEventArgs e)
        {



            if ((string.IsNullOrWhiteSpace(min.Text) || string.IsNullOrWhiteSpace(max.Text)) && (!Regex.IsMatch(max.Text, @"^\d*\.{0,1}\d+$") || !Regex.IsMatch(min.Text, @"^\d*\.{0,1}\d+$")))
            {
                MedicineDataGrid.ItemsSource = lekovi;
                return;
            }

            if (!string.IsNullOrWhiteSpace(max.Text) && Regex.IsMatch(max.Text, @"^\d*\.{0,1}\d+$") && Regex.IsMatch(min.Text, @"^\d*\.{0,1}\d+$"))
            {
                var filtered =
                    new ObservableCollection<Medicine>(mc.GetMedicinesInPriceRange(Double.Parse(min.Text),
                        Double.Parse(max.Text), mc.GetDeclinedMedicines()));

                MedicineDataGrid.ItemsSource = filtered;
            }

        }

        private void Max_OnKeyUp(object sender, KeyEventArgs e)
        {

            {
                if ((string.IsNullOrWhiteSpace(min.Text) || string.IsNullOrWhiteSpace(max.Text)) && (!Regex.IsMatch(max.Text, @"^\d*\.{0,1}\d+$") || !Regex.IsMatch(min.Text, @"^\d*\.{0,1}\d+$")))
                {
                    MedicineDataGrid.ItemsSource = lekovi;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(min.Text) && Regex.IsMatch(max.Text, @"^\d*\.{0,1}\d+$") && Regex.IsMatch(min.Text, @"^\d*\.{0,1}\d+$"))
                {
                    var filtered =
                        new ObservableCollection<Medicine>(mc.GetMedicinesInPriceRange(Double.Parse(min.Text),
                            Double.Parse(max.Text), mc.GetDeclinedMedicines()));

                    MedicineDataGrid.ItemsSource = filtered;
                }
            }
        }

        private void Pretrazi_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (searchby.Text.Equals("Id"))
            {
                if (string.IsNullOrWhiteSpace(pretrazi.Text))
                {
                    MedicineDataGrid.ItemsSource = lekovi;
                    return;
                }
                var filtered = lekovi.Where(medicine => medicine.MedicineId.ToLower().Contains(pretrazi.Text.ToLower()));

                MedicineDataGrid.ItemsSource = filtered;
            }
            else if (searchby.Text.Equals("Name"))
            {
                if (string.IsNullOrWhiteSpace(pretrazi.Text))
                {
                    MedicineDataGrid.ItemsSource = lekovi;
                    return;
                }
                var filtered = lekovi.Where(medicine => medicine.MedicineName.ToLower().Contains(pretrazi.Text.ToLower()));

                MedicineDataGrid.ItemsSource = filtered;
            }
            else if (searchby.Text.Equals("Manufactor"))
            {
                if (string.IsNullOrWhiteSpace(pretrazi.Text))
                {
                    MedicineDataGrid.ItemsSource = lekovi;
                    return;
                }
                var filtered = lekovi.Where(medicine => medicine.Manufactor.ToLower().Contains(pretrazi.Text.ToLower()));

                MedicineDataGrid.ItemsSource = filtered;
            }
            else if (searchby.Text.Equals("Quantity"))
            {
                if (string.IsNullOrWhiteSpace(pretrazi.Text))
                {
                    MedicineDataGrid.ItemsSource = lekovi;
                    return;
                }
                var filtered = lekovi.Where(medicine => medicine.Quantity.ToString().Equals(pretrazi.Text));

                MedicineDataGrid.ItemsSource = filtered;
            }
            else if (searchby.Text.Equals("Components"))
            {
                if (string.IsNullOrWhiteSpace(pretrazi.Text))
                {
                    MedicineDataGrid.ItemsSource = lekovi;
                    return;
                }
                if (pretrazi.Text.Contains("&"))
                {
                    string p = Regex.Replace(pretrazi.Text, @"\s+", "");
                    char[] delimiterChars = { '&' };
                    string[] pretraga = new[] { "" };
                    pretraga = p.Split(delimiterChars);
                    var filtered = new ObservableCollection<Medicine>(GetMedicinesWithAndOperator(pretraga));

                    MedicineDataGrid.ItemsSource = filtered;

                }
                else if (pretrazi.Text.Contains("|"))
                {
                    string p = Regex.Replace(pretrazi.Text, @"\s+", "");
                    char[] delimiterChars = { '|' };
                    string[] pretraga = new[] { "" };
                    pretraga = p.Split(delimiterChars);
                    var filtered = new ObservableCollection<Medicine>(GetMedicinesWithOrOperator(pretraga));

                    MedicineDataGrid.ItemsSource = filtered;

                }
                else
                {
                    var filtered = new ObservableCollection<Medicine>(GetMedicinesWithoutOperator(pretrazi.Text));

                    MedicineDataGrid.ItemsSource = filtered;
                }

            }
        }

        public List<Medicine> GetMedicinesWithoutOperator(string s)
        {
            List<Medicine> medicinesWithComponents = new List<Medicine>();
            List<Medicine> medicines = mc.GetDeclinedMedicines();
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
            List<Medicine> medicines = mc.GetDeclinedMedicines();
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
            List<Medicine> medicines = mc.GetDeclinedMedicines();
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
                MedicineDataGrid.ItemsSource = new ObservableCollection<Medicine>(medicines);
            }
            else if (sortby.SelectedItem.ToString().Equals("Price"))
            {
                medicines.Sort((x, y) => x.Price.CompareTo(y.Price));
                MedicineDataGrid.ItemsSource = new ObservableCollection<Medicine>(medicines);
            }
            else if (sortby.SelectedItem.ToString().Equals("Remaining quantity in storage"))
            {
                medicines.Sort((x, y) => x.QuantityInStorage.CompareTo(y.QuantityInStorage));
                MedicineDataGrid.ItemsSource = new ObservableCollection<Medicine>(medicines);
            }
        }
    }
}
