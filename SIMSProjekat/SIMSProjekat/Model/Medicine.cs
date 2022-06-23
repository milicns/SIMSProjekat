using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProjekat.Model
{
    public class Medicine
    {
        
        public string MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string Manufactor { get; set; }
        public int Quantity { get; set; }
        public IDictionary<string,Component> Components { get; set; }
        public bool Accepted { get; set; }
        public bool Deleted  { get; set; }
        public List<User> AcceptedByUsers { get; set; }
        public bool Declined { get; set; }
        public User DeclinedByUser { get; set; }
        public string DeclinedReason { get; set; }
        public double Price { get; set; }
        public int QuantityInStorage { get; set; }

    }
}