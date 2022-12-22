using SocleRH_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRH_Test.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetAll();
        User GetById(int userId);
        bool Exists(int userId);
        bool Create(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();
    }
}
