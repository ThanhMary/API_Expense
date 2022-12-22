using SocleRH_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRH_Test.IRepository
{
    public interface ICurrencyRepository
    {
        ICollection<Currency> GetAll();
        Currency GetById(int currencyId);
        Currency GetCurrencyUserByUser(int userId);
        ICollection<User> GetUsersByCurrency(int currencyId);
        bool Exists(int currencyId);
        bool Create(Currency model);
        bool Update(Currency model);
        bool Delete(Currency model);
        bool Save();


   }
}

