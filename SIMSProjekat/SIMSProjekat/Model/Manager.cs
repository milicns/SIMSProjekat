using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProjekat.Model
{
    public class Manager : User
    {

        public Manager()
        {
            TypeOfUser = UserType.Manager;
        }

    }
}
