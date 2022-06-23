using SIMSProjekat.Model;
using SIMSProjekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProjekat.Repository
{
    public class UserRepository
    {

        private Serializer<User> userSerializer = new Serializer<User>();
        private List<User> users;
        private string filePath = @"..\..\..\SIMSProjekat\Data\users.json";

        public List<User> GetAllUsers()
        {
            users = userSerializer.JsonDeserialize(filePath) as List<User>;
            return users;
        }

        public void SaveUsers(List<User> users)
        {

            userSerializer.JsonSerialize(users, filePath);    

        }
        
        public void AddUser(User user)
        {
            users = GetAllUsers();
            users.Add(user);
            SaveUsers(users);
        }

        public void DeleteUser(User user)
        {
            users = GetAllUsers();
            users.Remove(user);
            SaveUsers(users);
        }

        public User FindByKey(String key)
        {
            users = GetAllUsers();
            foreach (User user in users)
            {
                if (user.Jmbg.Equals(key))
                {
                    return user;
                }
            }
            return null;
        }

        

    }
}
