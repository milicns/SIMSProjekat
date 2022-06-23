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
    /// Interaction logic for ShowUsers.xaml
    /// </summary>
    public partial class ShowUsers : Page
    {
        private UserController uc = new UserController();
        private ManagerView mv;
        private List<User> users;

        public ShowUsers(ManagerView mv)
        {
            InitializeComponent();
            this.mv = mv;
            ime.Text = mv.activeUser.Name;
            prezime.Text = mv.activeUser.Surname;
            if (mv.activeUser.TypeOfUser == UserType.Manager)
            {
                tip.Text = "Manager";
            }

            users = uc.GetAllUsers();
            if (users == null)
            {
                users = new List<User>();
            }

            ObservableCollection<User> usersObservable = new ObservableCollection<User>();
            foreach (User user in users)
            {
                if (user.TypeOfUser != UserType.Manager)
                {
                    usersObservable.Add(user);
                }

            }

            this.DataContext = this;
            List<String> sortiranje = new List<string>();
            sortiranje.Add("Name");
            sortiranje.Add("Surname");
            List<String> tips = new List<string>();
            tips.Add("Asc");
            tips.Add("Desc");
            List<string> filtering = new List<string>();
            filtering.Add("Doctor");
            filtering.Add("Pharmacist");
            filtriranje.ItemsSource = filtering;
            sorttype.ItemsSource = tips;
            sortby.ItemsSource = sortiranje;
            UsersDataGrid.ItemsSource = usersObservable;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterUsers(mv));
        }

        private void BlockUser(object sender, RoutedEventArgs e)
        {
            User u = (User)UsersDataGrid.SelectedItem;
            uc.BlockUser(u.Jmbg);
            MessageBox.Show("User is blocked");

        }

        private void Unblock(object sender, RoutedEventArgs e)
        {
            User u = (User)UsersDataGrid.SelectedItem;
            uc.UnblockUser(u.Jmbg);
            MessageBox.Show("User is unblocked");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(mv);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainWindow());
        }

        private void Sortby_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sorttype.SelectedItem == null)
            {
                MessageBox.Show("Please select sort type");
                return;
            }

            if (sortby.SelectedItem.ToString().Equals("Name") && sorttype.SelectedItem.ToString().Equals("Asc"))
            {
                users.Sort((x, y) => x.Name.CompareTo(y.Name));
                ObservableCollection<User> usersObservable = new ObservableCollection<User>();
                foreach (User user in users)
                {
                    if (user.TypeOfUser != UserType.Manager)
                    {
                        usersObservable.Add(user);
                    }

                }

                UsersDataGrid.ItemsSource = usersObservable;
            }
            else if (sortby.SelectedItem.ToString().Equals("Name") && sorttype.SelectedItem.ToString().Equals("Desc"))
            {
                users.Sort((x, y) => y.Name.CompareTo(x.Name));
                ObservableCollection<User> usersObservable = new ObservableCollection<User>();
                foreach (User user in users)
                {
                    if (user.TypeOfUser != UserType.Manager)
                    {
                        usersObservable.Add(user);
                    }

                }

                UsersDataGrid.ItemsSource = usersObservable;
            }
            else if (sortby.SelectedItem.ToString().Equals("Surname") && sorttype.SelectedItem.ToString().Equals("Asc"))
            {
                users.Sort((x, y) => x.Surname.CompareTo(y.Surname));
                ObservableCollection<User> usersObservable = new ObservableCollection<User>();
                foreach (User user in users)
                {
                    if (user.TypeOfUser != UserType.Manager)
                    {
                        usersObservable.Add(user);
                    }

                }

                UsersDataGrid.ItemsSource = usersObservable;
            }
            else if (sortby.SelectedItem.ToString().Equals("Surname") &&
                     sorttype.SelectedItem.ToString().Equals("Desc"))
            {
                users.Sort((x, y) => y.Surname.CompareTo(x.Surname));
                ObservableCollection<User> usersObservable = new ObservableCollection<User>();
                foreach (User user in users)
                {
                    if (user.TypeOfUser != UserType.Manager)
                    {
                        usersObservable.Add(user);
                    }

                }

                UsersDataGrid.ItemsSource = usersObservable;
            }
        }

        private void Sorttype_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sortby.SelectedItem == null)
            {
                MessageBox.Show("Please select sort criteria");

                return;
            }

            if (sortby.SelectedItem.ToString().Equals("Name") && sorttype.SelectedItem.ToString().Equals("Asc"))
            {
                users.Sort((x, y) => x.Name.CompareTo(y.Name));
                ObservableCollection<User> usersObservable = new ObservableCollection<User>();
                foreach (User user in users)
                {
                    if (user.TypeOfUser != UserType.Manager)
                    {
                        usersObservable.Add(user);
                    }

                }

                UsersDataGrid.ItemsSource = usersObservable;
            }
            else if (sortby.SelectedItem.ToString().Equals("Name") && sorttype.SelectedItem.ToString().Equals("Desc"))
            {
                users.Sort((x, y) => y.Name.CompareTo(x.Name));
                ObservableCollection<User> usersObservable = new ObservableCollection<User>();
                foreach (User user in users)
                {
                    if (user.TypeOfUser != UserType.Manager)
                    {
                        usersObservable.Add(user);
                    }

                }

                UsersDataGrid.ItemsSource = usersObservable;
            }
            else if (sortby.SelectedItem.ToString().Equals("Surname") && sorttype.SelectedItem.ToString().Equals("Asc"))
            {
                users.Sort((x, y) => x.Surname.CompareTo(y.Surname));
                ObservableCollection<User> usersObservable = new ObservableCollection<User>();
                foreach (User user in users)
                {
                    if (user.TypeOfUser != UserType.Manager)
                    {
                        usersObservable.Add(user);
                    }

                }

                UsersDataGrid.ItemsSource = usersObservable;
            }
            else if (sortby.SelectedItem.ToString().Equals("Surname") &&
                     sorttype.SelectedItem.ToString().Equals("Desc"))
            {
                users.Sort((x, y) => y.Surname.CompareTo(x.Surname));
                ObservableCollection<User> usersObservable = new ObservableCollection<User>();
                foreach (User user in users)
                {
                    if (user.TypeOfUser != UserType.Manager)
                    {
                        usersObservable.Add(user);
                    }

                }

                UsersDataGrid.ItemsSource = usersObservable;
            }
        }

    

        private void Filtriranje_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filtriranje.SelectedItem.ToString().Equals("Pharmacist"))
            {
                var filtered = users.Where(user => user.TypeOfUser == UserType.Pharmacist);

                UsersDataGrid.ItemsSource = filtered;
            }
            else if (filtriranje.SelectedItem.ToString().Equals("Doctor"))
            {
                var filtered = users.Where(user => user.TypeOfUser == UserType.Doctor);

                UsersDataGrid.ItemsSource = filtered;
            }
        }
    }
}
