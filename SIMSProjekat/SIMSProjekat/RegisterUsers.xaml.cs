using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Interaction logic for RegisterUsers.xaml
    /// </summary>
    public partial class RegisterUsers : Page
    {
        private UserController uc = new UserController();
        private ManagerView mv;
        public RegisterUsers(ManagerView mv)
        {
            InitializeComponent();
            this.mv = mv;
            this.DataContext = this;
            List<string> userType = new List<string>();
            userType.Add("Doctor");
            userType.Add("Pharmacist");
            Box.ItemsSource = userType;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = TextBox1.Text;
            string password = TextBox2.Text;
            string jmbg = TextBox3.Text;
            string name = TextBox4.Text;
            string surname = TextBox5.Text;
            string phone = TextBox6.Text;
            UserType type;
            string tip = Box.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(jmbg) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(tip))
            {
                MessageBox.Show("Fields cannot be empty");
                return;
            }

            if (Box.SelectedIndex == 0)
            {
                type = UserType.Doctor;
            }
            else
            {
                type = UserType.Pharmacist;
            }

            User user = new User
            {
                Email = email, Jmbg = jmbg, Name = name, Password = password, PhoneNumber = phone, Surname = surname,
                TypeOfUser = type, IsBlocked = false
            };
            if (uc.FindByJmbg(jmbg) != null)
            {
                MessageBox.Show("User with same jmbg already exists");
                return;
            }
            uc.RegisterUser(user);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ShowUsers(mv));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainWindow());
        }
    }
}
