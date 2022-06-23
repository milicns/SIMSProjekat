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
using SIMSProjekat.Model;

namespace SIMSProjekat
{
    /// <summary>
    /// Interaction logic for ShowComponents.xaml
    /// </summary>
    public partial class ShowComponents : Window
    {
        private ObservableCollection<Component> Components;
        public ShowComponents(IDictionary<string, Component> components)
        {
            InitializeComponent();
            this.DataContext = this;
            if (components == null)
            {
                components = new Dictionary<string, Component>();
            }
            Components = new ObservableCollection<Component>(components.Values);
            componentsDatagrid.ItemsSource = Components;
        }
    }
}
