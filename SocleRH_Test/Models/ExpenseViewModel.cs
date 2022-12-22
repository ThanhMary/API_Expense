using SocleRH_Test.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRH_Test.Models
{
    public class ExpenseViewModel
    {

      

        public int ExpenseId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
 
        public string Nature { get; set; }
      
        public double Amount { get; set; }

        public string Comment { get; set; }
        public string NameUserComplet { get; set;}
        public string NameCurrency{ get; set;}
        public int UserId { get; set; }
        public int CurrencyId { get; set; }

        public ExpenseViewModel()
        {
        }
        public ExpenseViewModel(Expense expense)
        {
            this.ExpenseId = expense.ExpenseId;
            this.Date = expense.Date;
            this.Nature = expense.Nature;
            this.Amount = expense.Amount;
            this.Comment = expense.Comment;
            this.NameCurrency = expense.Currency.CurrencyName;
            this.NameUserComplet = expense.User.FistName + " " + expense.User.LastName;
            this.UserId = expense.User.UserId;
            this.CurrencyId = expense.Currency.CurrencyId;

        }
    }
}
