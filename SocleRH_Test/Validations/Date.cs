using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRH_Test.Validations
{
    public class Date
    {
        public static bool DateValidated(DateTime dateInput)
        {
            //Date qui tient en compte de fuseaux d'horaire
            DateTime dateMax = DateTime.Now;

            //une date ne peut pas etre date de plus de 3 mois;
            DateTime dateMin = dateMax.AddMonths(-3);

            if (dateInput > dateMin && dateInput <= DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
    

}
