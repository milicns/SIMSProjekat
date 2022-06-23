using SIMSProjekat.Model;
using SIMSProjekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMSProjekat.Service
{
    public class UserService
    {
        private UserRepository userRepository = new UserRepository();


        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public void SaveUsers(List<User> users)
        {
            userRepository.SaveUsers(users);
        }


        public void RegisterUser(User userForRegistering)
        {
            User user = FindByJmbg(userForRegistering.Jmbg);
            if (user == null)
            {
                userRepository.AddUser(userForRegistering);
            }
        }

        public bool CheckIfBlocked(string email,string password)
        {
            User user = GetActiveUser(email, password);
            if (user.IsBlocked == true)
            {
                return true;
            }

            return false;
        }

        public void BlockUser(string jmbg)
        {
            SetBlockStatus(jmbg, true);
        }

        public void UnblockUser(string jmbg)
        {
            SetBlockStatus(jmbg, false);
        }

        public void SetBlockStatus(string jmbg, bool status)
        {
            List<User> users = GetAllUsers();
            foreach (User user in users)
            {
                if (user.Jmbg.Equals(jmbg))
                {
                    user.IsBlocked = status;
                }
            }
            SaveUsers(users);
        }

        public User GetActiveUser(string email, string password)
        {
            List<User> users = GetAllUsers();
            foreach (User user in users)
            {
                if (user.Email.Equals(email) && user.Password.Equals(password))
                {
                    return user;
                }
            }
            return null;
        }

        public void DeleteUser(User user)
        {
            userRepository.DeleteUser(user);
        }

        public User FindByJmbg(String jmbg)
        {
            return userRepository.FindByKey(jmbg);
        }

    }
}
