using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRH_Test.Dto
{
    public class ExpenseDto
    {
        public int ExpenseId { get; set; }
     
        public DateTime Date { get; set; }
 
        public string Nature { get; set; }
  
        public double Amount { get; set; }
   
        public string Comment { get; set; }



    }
}
