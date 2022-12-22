using SocleRH_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRH_Test.IRepository
{
    public interface IExpenseRepository
    {
        ICollection<Expense> GetAll();
        Expense GetById(int expenseId);
        Currency GetCurrencyExpenseByCurrencyId(int currencyId);
        User GetUserExpenseByUserId(int userId);
        ICollection<Expense> Search(string searchString);
        bool Exists(int expenseId);
        bool Create(Expense expense);
        bool Update(Expense expense);
        bool Delete(Expense expense);
        bool Save();
    }
}
