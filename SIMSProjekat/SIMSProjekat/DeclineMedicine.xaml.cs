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
using SIMSProjekat.Controller;
using SIMSProjekat.Model;

namespace SIMSProjekat
{
    /// <summary>
    /// Interaction logic for DeclineMedicine.xaml
    /// </summary>
    public partial class DeclineMedicine : Page
    {
        private User activeUser;
        private Medicine medicine;
        private MedicineController mc = new MedicineController();
        private MedicinesForApproval ma;
        private PharmacistView pv;
        private DoctorView dv;
        public DeclineMedicine(User user, Medicine medicine, MedicinesForApproval ma)
        {
            InitializeComponent();
            activeUser = user;
            this.medicine = medicine;
            this.ma = ma;
            pv = ma.pharmacistView;
            dv = ma.doctorView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            medicine.DeclinedByUser = activeUser;
            medicine.DeclinedReason = reason.Text;
            mc.DeclineMedicine(medicine);
            if (pv != null)
            {   
                
                this.NavigationService.Navigate(new MedicinesForApproval(pv, activeUser));
            }
            else
            {
                this.NavigationService.Navigate(new MedicinesForApproval(dv, activeUser));
            }
            
        }
    }
}
