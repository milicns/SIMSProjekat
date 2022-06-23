using SIMSProjekat.Service;
using SIMSProjekat.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProjekat.Repository;

namespace SIMSProjekat.Controller
{
    public class MedicineController
    {
        private MedicineService medicineService = new MedicineService();

        public List<Medicine> GetAcceptedMedicines()
        {
            return medicineService.GetAcceptedMedicines();
        }

        public List<Medicine> GetDeclinedMedicines()
        {
            return medicineService.GetDeclinedMedicines();
        }

        public List<Medicine> GetNotAcceptedMedicines()
        {
            return medicineService.GetNotAcceptedMedicines();
        }

        public List<Medicine> GetMedicinesInPriceRange(double min, double max, List<Medicine> searchedListOfMedicines)
        {
            return medicineService.GetMedicinesInPriceRange(min, max, searchedListOfMedicines);
        }


        public void AddMedicineForApproval(Medicine medicine)
        {
            medicineService.AddMedicineForApproval(medicine);
        }

        public void AddMoreMedicines(Medicine medicine, int amount)
        {
            medicineService.AddMoreMedicines(medicine, amount);
        }

        public void DeclineMedicine(Medicine medicine)
        {
            medicineService.DeclineMedicine(medicine);
        }
        public void ApproveMedicine(Medicine medicine)
        {
            medicineService.ApproveMedicine(medicine);
        }

        public void AcceptMedicineByUser(Medicine medicine, User user)
        {
            medicineService.AcceptMedicineByUser(medicine, user);
        }

        public Medicine FindById(String id)
        {
            return medicineService.FindById(id);
        }

    }
}
