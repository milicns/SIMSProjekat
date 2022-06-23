using SIMSProjekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProjekat.Repository
{
    public class MedicineRepository
    {
        private Serializer<Medicine> medicineSerializer = new Serializer<Medicine>();
        private List<Medicine> medicines;
        private string filePath = @"..\..\..\SIMSProjekat\Data\medicines.json";


        public List<Medicine> GetAllMedicines()
        {
            medicines = medicineSerializer.JsonDeserialize(filePath) as List<Medicine>;
            return medicines;
        }

        public void SaveMedicines(List<Medicine> medicines)
        {

            medicineSerializer.JsonSerialize(medicines, filePath);

        }

        public void AddMedicine(Medicine medicine)
        {
            medicines = GetAllMedicines();
            medicines.Add(medicine);
            SaveMedicines(medicines);
        }

        public void SaveEditedMedicine(Medicine editedMedicine)
        {
            List<Medicine> medicines = GetAllMedicines();
            for (int i = 0; i < medicines.Count; i++)
            {
                if (medicines[i].MedicineId.Equals(editedMedicine.MedicineId))
                {
                    medicines[i] = editedMedicine;
                }
            }
            SaveMedicines(medicines);
        }


        public void DeleteMedicine(Medicine medicine)
        {
            medicines = GetAllMedicines();
            medicines.Remove(medicine);
            SaveMedicines(medicines);
        }

        public Medicine FindByKey(String key)
        {
            medicines = GetAllMedicines();
            foreach (Medicine medicine in medicines)
            {
                if (medicine.MedicineId.Equals(key))
                {
                    return medicine;
                }
            }
            return null;
        }

    }
}
