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
using SIMSProjekat.Controller;
using SIMSProjekat.Model;

namespace SIMSProjekat
{
    /// <summary>
    /// Interaction logic for ShowApproved.xaml
    /// </summary>
    public partial class ShowApproved : Window
    {
        private MedicineController mc = new MedicineController();
        public ShowApproved()
        {
            InitializeComponent();
            List<Medicine> medicines;
            medicines = mc.GetAcceptedMedicines();
            ObservableCollection<Medicine> lekovi = new ObservableCollection<Medicine>();
            foreach (Medicine medicine in medicines)
            {
                lekovi.Add(medicine);
            }

            this.DataContext = this;
            MedicineDataGrid.ItemsSource = lekovi;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Medicine medicine = (Medicine)MedicineDataGrid.SelectedItem;
            new ShowComponents(medicine.Components).ShowDialog();
        }
    }
}
