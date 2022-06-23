using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProjekat.Model
{
    public class User
    {

       
        public string Jmbg { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public UserType TypeOfUser { get; set; }
        public bool IsBlocked { get; set; }
 


    }
}
