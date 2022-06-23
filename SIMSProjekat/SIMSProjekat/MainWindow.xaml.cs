using SIMSProjekat.Repository;
using SIMSProjekat.Service;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        private User activeUser;
        private UserController userController = new UserController();
        private int attemptCounter = 0;

        public MainWindow()
        {
            InitializeComponent();
           
        }

        public void CheckLoginCredentials()
        {
            string email = this.email.Text;
            string password = this.password.Text;
            GetActiveUser(email, password);
            CheckAttempts();
            
        }

        public void GetActiveUser(string email, string password)
        {
            activeUser = userController.GetActiveUser(email, password);

            if (activeUser != null)
            {
                if (userController.CheckIfBlocked(email, password))
                {
                    MessageBox.Show("Blokirani ste sa sistema");
                    return;
                }
                MessageBox.Show("Logovali ste se uspesno");
                
                CheckActiveUserType();

            }
            else
            {
                
                MessageBox.Show("Netacan email ili lozinka");
                attemptCounter++;
            }
        }

        private void CheckAttempts()
        {
            if (attemptCounter == 3)
            {
                login.IsEnabled = false;
                MessageBox.Show("Zabranjeno vam je ponovno logovanje");
            }
        }

        private void CheckActiveUserType()
        {
            if (activeUser.TypeOfUser == UserType.Doctor)
            {
                MainFrame.NavigationService.Navigate(new DoctorView(activeUser));

            }else if (activeUser.TypeOfUser == UserType.Manager)
            {
                MainFrame.NavigationService.Navigate(new ManagerView(activeUser));
            }
            else
            {
                MainFrame.NavigationService.Navigate(new PharmacistView(activeUser));
            }
        }

        private void Login_OnClick(object sender, RoutedEventArgs e)
        {
            CheckLoginCredentials();
            
        }
    }
}
