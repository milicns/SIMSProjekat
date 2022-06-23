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
using SIMSProjekat.Model;

namespace SIMSProjekat
{
    /// <summary>
    /// Interaction logic for AddComponent.xaml
    /// </summary>
    public partial class AddComponent : Page
    {
        private IDictionary<string, Component> components = new Dictionary<string, Component>();
        private AddNewMedicine addNew;
        
        public AddComponent(AddNewMedicine addm, IDictionary<string, Component> components)
        {
            InitializeComponent();
            addNew = addm;
           
            if (components != null)
            {
                this.components = components;
            }
        }

        private void ButtonBase_Onclick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(addNew);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = TextBox1.Text;
            string description = TextBox2.Text;
            Component component = new Component { Name = name, Description = description};
            if (components.ContainsKey(name))
            {
                MessageBox.Show("You cant add same component twice");
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name field cannot be empty");
                return;
            }
            components.Add(component.Name, component);
            addNew.Components = components;
            this.NavigationService.Navigate(addNew);
        }
    }
}
