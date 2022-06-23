using SIMSProjekat.Model;
using SIMSProjekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProjekat.Service
{
    public class MedicineService
    {
        private MedicineRepository medicineRepository = new MedicineRepository();

        public List<Medicine> GetAllMedicines()
        {
            return medicineRepository.GetAllMedicines();
        }

        public List<Medicine> GetAcceptedMedicines()
        {
            List<Medicine> acceptedMedicines = new List<Medicine>();
            List<Medicine> medicines = GetAllMedicines();
            foreach (Medicine medicine in medicines)
            {
                if (medicine.Accepted == true)
                {
                    acceptedMedicines.Add(medicine);
                }
            }

            return acceptedMedicines;
        }

        public List<Medicine> GetNotAcceptedMedicines()
        {
            List<Medicine> notAcceptedMedicines = new List<Medicine>();
            List<Medicine> medicines = GetAllMedicines();
            foreach (Medicine medicine in medicines)
            {
                if (medicine.Accepted == false && medicine.Declined == false)
                {
                    notAcceptedMedicines.Add(medicine);
                }
            }

            return notAcceptedMedicines;
        }

        public List<Medicine> GetDeclinedMedicines()
        {
            List<Medicine> declinedMedicines = new List<Medicine>();
            List<Medicine> medicines = GetAllMedicines();
            foreach (Medicine medicine in medicines)
            {
                if (medicine.Declined == true)
                {
                    declinedMedicines.Add(medicine);
                }
            }

            return declinedMedicines;
        }


        public void ApproveMedicine(Medicine medicine)
        {
            medicine.Accepted = true;
            SaveEditedMedicine(medicine);
        }

        public void DeclineMedicine(Medicine medicine)
        {
            medicine.Declined = true;
            SaveEditedMedicine(medicine);
        }

        public void AddMedicineForApproval(Medicine medicineForApproval)
        {
            Medicine medicine = FindById(medicineForApproval.MedicineId);
            if (medicine == null)
            {
                medicineRepository.AddMedicine(medicineForApproval);
            }
        }

        public List<Medicine> GetMedicinesInPriceRange(double min, double max, List<Medicine> searchedListOfMedicines)
        {
            List<Medicine> medicinesInPriceRange = new List<Medicine>();
            List<Medicine> medicines = searchedListOfMedicines;
            foreach (Medicine medicine in medicines)
            {
                if (medicine.Price >= min && medicine.Price <= max)
                {
                    medicinesInPriceRange.Add(medicine);
                }
            }
            return medicinesInPriceRange;

        }

        public void AddMoreMedicines(Medicine medicine, int amount)
        {
            medicine.Quantity = medicine.Quantity + amount;
            SaveEditedMedicine(medicine);
        }

        public void AcceptMedicineByUser(Medicine medicine, User user)
        {
            Medicine medicineForApproval = medicine;
            if (!CheckIfAcceptedByUser(medicineForApproval, user))
            {
                medicineForApproval.AcceptedByUsers.Add(user);
                SaveEditedMedicine(medicineForApproval);
            }

        }

        public void SaveEditedMedicine(Medicine medicine)
        {
            medicineRepository.SaveEditedMedicine(medicine);
        }

        public bool CheckIfAcceptedByUser(Medicine medicine, User user)
        {
            foreach (User u in medicine.AcceptedByUsers)
            {
                if (u.Jmbg.Equals(user.Jmbg))
                {
                    return true;
                }
            }

            return false;
        }


        public Medicine FindById(String id)
        {
            return medicineRepository.FindByKey(id);
        }
    }
}
