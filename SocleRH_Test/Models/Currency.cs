using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SocleRH_Test.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }

        [Required]
        public string CurrencyName { get; set; }
        [Required]
        public string CurrencyCountry { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
