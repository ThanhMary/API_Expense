using SocleRH_Test.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace SocleRH_TestXUnit
{
    public static class Fake
    {
        
        public static Currency cur1 = new Currency()
        {
            CurrencyId = 1,
            CurrencyName = "Dollar",
            CurrencyCountry = "Americain"

        };
        public static Currency cur2 = new Currency()
        {
            CurrencyId = 2,
            CurrencyName = "Euro",
            CurrencyCountry = "Europe"
        };
        public static User user1 = new User()
        {
            UserId = 1,
            FistName = "Anthony",
            LastName = "Start",
            Currency = cur1

        };
        public static User user2 = new User()
        {
            UserId = 2,
            FistName = "Thanh",
            LastName = "Nguyen",
            Currency = cur2
        };
        public static Expense ExpenseLineCorrect = new Expense()
        {

            Date = new DateTime(2022, 12, 1, 8, 30, 52),
            Nature = "Restaurant",
            Amount = 25.5,
            Comment = "Lorem itsum",
            Currency = cur1,
            User = user1,
        };

        public static Expense ExpenseLineInCorrectWithOutComment = new Expense() 
        {
                Date = new DateTime(2022, 11, 1, 8, 30, 52),
                Nature = "Restaurant",
                Amount = 25.5,
                Comment = "",
                Currency = cur1,
                User = user1,
        };
        

        public static Expense ExpenseLineInCorrectWithDateInFutur = new Expense()
        {

                Date = new System.DateTime(2023, 12, 1, 8, 30, 52),
                Nature = "Restaurant",
                Amount = 25.5,
                Comment = "Loremm Lorem Lorem",
                Currency = cur1,
                User = user1,
            };
        
        public static Expense ExpenseLineInCorrectWithDateInferiorTreeMonths = new Expense()
        {

            Nature = "Restaurant",
            Amount = 25.5,
            Comment = " Lorem Lorem Lorem Lorem",
            Currency = cur1,
            User = user1,

        };
        public static Expense ExpenseLineInCorrectWithCurrencyUserDiffCurrencyExpense = new Expense()
        {
                Date = Convert.ToDateTime("20/07/2022"),
                Nature = "Restaurant",
                Amount = 25.5,
                Comment = "Lorem Lorem Lorem Lorem",
                Currency = cur2,
                User = user1,
        };
      

        public static List<Expense> ExpenseList = new List<Expense>()
        {
            new Expense() {ExpenseId = 1, Amount= 123.5, Nature="Restaurant",  Comment="Lorem Loremmmmm", User = user2, Currency = cur2 },
            new Expense() { ExpenseId = 2, Amount = 1000, Nature = "Hotel",  Comment = "Lorem Loremmmmm", User = user2, Currency = cur2 },
            new Expense() {ExpenseId = 3, Amount= 300, Nature="Mics", Comment="Lorem Loremmmmm", User = user2, Currency = cur2 }


        };

    }
   
}
