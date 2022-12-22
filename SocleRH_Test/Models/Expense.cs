using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRH_Test.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        [Required]
         public DateTime Date { get; set; }
        [Required]
        public string Nature { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string Comment { get; set; }
  
        public virtual Currency Currency { get; set; }
           
        public virtual User User { get; set; }

       
        

    }
}
