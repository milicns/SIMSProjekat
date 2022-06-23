using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddNewMedicine.xaml
    /// </summary>
    public partial class AddNewMedicine : Page
    {
        public IDictionary<string, Component> Components { get; set; }
        private MedicineController mc = new MedicineController();
        private ManagerView mv;
        public AddNewMedicine(ManagerView managerView)
        {
            InitializeComponent();
            mv = managerView;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(mv);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox1.Text) || string.IsNullOrWhiteSpace(TextBox2.Text) ||
                string.IsNullOrWhiteSpace(TextBox3.Text) || string.IsNullOrWhiteSpace(quantity.Text) ||
                string.IsNullOrWhiteSpace(this.price.Text))
            {
                MessageBox.Show("Fields cannot be empty");
                return;
            }
            string id = TextBox1.Text;
            string name = TextBox2.Text;
            string manufactor = TextBox3.Text;
            int amount = Int32.Parse(quantity.Text);
            double price = Double.Parse(this.price.Text);
            Medicine medicine = new Medicine {MedicineId = id, MedicineName = name, Manufactor = manufactor,Quantity = amount,Components = this.Components, Accepted = false, Deleted = false, AcceptedByUsers = new List<User>(),Price = price};
            

            if (mc.FindById(medicine.MedicineId) != null)
            {
                MessageBox.Show("Medicine is already added for approval");
                return;
            }
            mc.AddMedicineForApproval(medicine);
            this.NavigationService.Navigate(mv);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddComponent(this, Components));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Components == null)
            {
                Components = new Dictionary<string, Component>();
            }
            new ShowComponents(Components).ShowDialog();
        }
    }
}
