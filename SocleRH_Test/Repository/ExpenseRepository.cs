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
    public class ExpenseRepository : IExpenseRepository
    {
        #region Context 
        private readonly ExpenseApiContext _context;
        public ExpenseRepository(ExpenseApiContext context)
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

        #region Get All Expenses
        public ICollection<Expense> GetAll()
        {
            return _context.Expenses.Include(e=>e.User).Include(e=>e.Currency).OrderByDescending(e => e.Date).ToList();
        }
        #endregion

        #region Search by User, Amount, Date
        public ICollection<Expense> Search(string searching)
        {
            return _context.Expenses.Where(e => e.Amount.ToString() == searching
                                    || e.User.LastName.Trim().ToUpper() == searching.TrimEnd().ToUpper()
                                    || e.User.FistName.Trim().ToUpper() == searching.TrimEnd().ToUpper()
                                    || e.User.UserId.ToString() == searching
                                    || e.Date.ToString() == searching)
                .Include(e=>e.Currency)
                .Include(e=>e.User)
                .ToList();

        }
        #endregion

        #region Get Currency by ExpenseId

        public Currency GetCurrencyExpenseByCurrencyId(int currencyId)
        {
            return _context.Expenses.Where(c => c.Currency.CurrencyId.Equals(currencyId)).Select(c=>c.Currency).FirstOrDefault();
        }
        #endregion

        #region Get User by ExpenseId

        public User GetUserExpenseByUserId(int userId)
        {
            return _context.Expenses.Where(c => c.User.UserId.Equals(userId)).Select(c => c.User).FirstOrDefault();
        }
        #endregion

        #region Get Expense by Id
        public Expense GetById(int expenseId)
        {
            return _context.Expenses.Where(c => c.ExpenseId.Equals(expenseId)).FirstOrDefault();
        }
        #endregion

        #region Create Expense
        public bool Create(Expense expense)
        {
            _context.Add(expense);
            return Save();
        }
        #endregion

        #region Update Expense
        public bool Update(Expense expense)
        {
            _context.Update(expense);
            return Save();
        }
        #endregion

        #region Delete Expense
        public bool Delete(Expense expense)
        {
            _context.Remove(expense);
            return Save();
        }
        #endregion

        #region Exist Expense
        public bool Exists(int expenseId)
        {
            return _context.Expenses.Any(u => u.ExpenseId == expenseId);
        }
        #endregion
    }
}

