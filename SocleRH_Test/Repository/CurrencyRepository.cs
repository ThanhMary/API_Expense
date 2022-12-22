using Microsoft.EntityFrameworkCore;
using SocleRH_Test.Data;
using SocleRH_Test.IRepository;
using SocleRH_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRH_Test.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        #region Context
        private readonly ExpenseApiContext _context;
        public CurrencyRepository(ExpenseApiContext context)
        {
            _context = context;
        }
        #endregion

        #region Méthode Save
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        #endregion

        #region Get All Currencies
        public ICollection<Currency> GetAll()
        {
            return _context.Currencies.OrderBy(c=>c.CurrencyName).ToList();
        }
        #endregion

        #region Get Currency by Id
        public Currency GetById(int id)
        {
            return _context.Currencies.Where(c => c.CurrencyId.Equals(id)).FirstOrDefault();
        }
        #endregion

        #region Get Currency by user
        public Currency GetCurrencyUserByUser(int userId)
        {
            return _context.Users.Where(o => o.UserId == userId).Select(c => c.Currency).FirstOrDefault();
        }

        #endregion
        #region Get all users by currency
        public ICollection<User> GetUsersByCurrency(int currencyId)
        {
            return _context.Users.Where(c => c.Currency.CurrencyId == currencyId).ToList();
        }
        #endregion

        #region Create Currency
        public bool Create(Currency currency)
        {
            _context.Add(currency);
            return Save();
        }
        #endregion

        #region Update Currency
        public bool Update(Currency currency)
        {
            _context.Update(currency);
            return Save();
        }
        #endregion

        #region Delete Currency
        public bool Delete(Currency currency)
        {
            _context.Remove(currency);
            return Save();
        }
        #endregion

        #region Exist Currency
        public bool Exists(int currencyId)
        {
            return _context.Currencies.Any(u => u.CurrencyId == currencyId);
        }
        #endregion
    }
}

