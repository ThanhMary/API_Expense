using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRH_Test.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FistName { get; set; }
        [Required]
        public string LastName { get; set; }
    
        public Currency Currency { get; set; }

      

    }
}
