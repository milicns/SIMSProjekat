using SIMSProjekat.Model;
using SIMSProjekat.Repository;
using SIMSProjekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProjekat.Controller
{
    public class UserController
    {
        private UserService userService = new UserService();

        public UserController()
        {
            //User user = new User("1234567", "milospetrovic@gmail.com","novisad123", "Milos", "Petrovic", "3212313", UserType.Manager);
            

        }

        public List<User> GetAllUsers()
        {
            return userService.GetAllUsers();
        }

        public void RegisterUser(User userForRegistering)
        {
            userService.RegisterUser(userForRegistering);
        }

        public void BlockUser(string jmbg)
        {
            userService.BlockUser(jmbg);
        }

        public void UnblockUser(string jmbg)
        {
            userService.UnblockUser(jmbg);
        }

        public bool CheckIfBlocked(string email, string password)
        {
            return userService.CheckIfBlocked(email, password);
        }

        public User GetActiveUser(string email, string password)
        {
            return userService.GetActiveUser(email, password);
        }


        public User FindByJmbg(String jmbg)
        {
            return userService.FindByJmbg(jmbg);
        }
    }
}
