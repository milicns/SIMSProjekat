using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SIMSProjekat.Controller;
using SIMSProjekat.Model;

namespace SIMSProjekat
{
    /// <summary>
    /// Interaction logic for AddMoreMedicines.xaml
    /// </summary>
    public partial class AddMoreMedicines : Page
    {
        private ManagerView managerView;
        private Medicine med;
        private MedicineController mc = new MedicineController();
        private List<Medicine> medicines = new List<Medicine>();
        private DispatcherTimer timer = new DispatcherTimer();
        private DateTime dt1;
        private DateTime dt2;
        public AddMoreMedicines(ManagerView mv, Medicine medicine)
        {
            InitializeComponent();
            managerView = mv;
            med = medicine;
            timer.Interval = TimeSpan.FromSeconds(15);
            timer.Tick += timer_Tick;
            medname.Text = med.MedicineName;
            id.Text = med.MedicineId;
           

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainWindow());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ShowUsers(managerView));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ondate.IsEnabled == false)
            {
                int kol = Int32.Parse(amount.Text);
                mc.AddMoreMedicines(med, kol);
                this.NavigationService.Navigate(new ManagerView(managerView.activeUser));
            }
            else if (ondate.IsEnabled == true)
            {
                timer.Start();
                DateTime dt1 = DateTime.Parse(ondate.Text);
                DateTime dt2 = DateTime.Now;
                if (string.IsNullOrWhiteSpace(amount.Text))
                {
                    MessageBox.Show("Enter the amount");
                    return;
                }
                int kol = Int32.Parse(amount.Text);
                if (dt1.Date > dt2.Date)
                {
                    MessageBox.Show("Medicines will arrive on given date");
                    
                }
                
                
                
            }

        }
        private void timer_Tick(object sender, EventArgs e)
        {

            DispatcherTimer _timer = sender as DispatcherTimer;
            int kol = Int32.Parse(amount.Text);
            if (dt1.Date >= dt2.Date)
            {
                mc.AddMoreMedicines(med, kol);
                MessageBox.Show("Medicines arrived");
                
            }

            _timer.Stop();

        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            
            ondate.IsEnabled = true;
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            ondate.IsEnabled = false;
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ManagerView(managerView.activeUser));
        }
    }
}
