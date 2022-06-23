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
using System.Windows.Shapes;
using SIMSProjekat.Model;

namespace SIMSProjekat
{
    /// <summary>
    /// Interaction logic for ShowDetails.xaml
    /// </summary>
    public partial class ShowDetails : Window
    {
        private Medicine med;
        public ShowDetails(Medicine medicine)
        {
            InitializeComponent();
            med = medicine;
            if (med.DeclinedByUser.TypeOfUser == UserType.Doctor)
            {
                tip.Text = "Doctor";
            }else if (med.DeclinedByUser.TypeOfUser == UserType.Pharmacist)
            {
                tip.Text = "Pharmacist";
            }

            ime.Text = medicine.DeclinedByUser.Name;
            prezime.Text = medicine.DeclinedByUser.Surname;
            razlog.Text = medicine.DeclinedReason;
        }
    }
}
