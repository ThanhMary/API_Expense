using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SocleRH_Test.Controllers;
using SocleRH_Test.Dto;
using SocleRH_Test.IRepository;
using SocleRH_Test.Models;
using System.Collections.Generic;
using Xunit;

namespace SocleRH_TestXUnit.Controller
{
    public class ExpensesControllerTest
    {

        private readonly IExpenseRepository _expenseRepo;
        private readonly IUserRepository _userRepo;
        private readonly ICurrencyRepository _currencyRepo;
        private readonly IMapper _mapper;
   
        public ExpensesControllerTest()
        {
            _expenseRepo = A.Fake<IExpenseRepository>();
            _userRepo = A.Fake<IUserRepository>();
            _currencyRepo = A.Fake<ICurrencyRepository>();
            _mapper = A.Fake<IMapper>();

        }
    
        [Fact]
        public void GetExpenses_ReturnOK()
        {
            //Arrange
            var expenses = A.Fake<ICollection<ExpenseDto>>();
            var expensesList = A.Fake<List<ExpenseDto>>();
            A.CallTo(() => _mapper.Map<List<ExpenseDto>>(expenses)).Returns(expensesList);
            var controller = new ExpensesController(_expenseRepo, _userRepo, _currencyRepo, _mapper);
            //Act
            var result = controller.GetExpenses();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }


        [Fact]
        public void CreateExpenseWithCorrectModel()
        {
            //Arrange
            int userId = 1;
            int currencyId = 1;
            var expenseMap = A.Fake<Expense>();
            var expense = A.Fake<Expense>();
            var expenseCreate = A.Fake<Expense>();
            A.CallTo(() => _mapper.Map<Expense>(expenseCreate)).Returns(expense);
            A.CallTo(() => _expenseRepo.Create(expenseMap)).Returns(true);
            var controller = new ExpensesController(_expenseRepo, _userRepo, _currencyRepo, _mapper);
            //Act
            var result = controller.CreateExpense(userId, currencyId, expenseCreate);
            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void CreateExpenseInCorrectModel_WithOutComment()
        {
            //Arrange
            int userId = 1;
            int currencyId = 1;
            var expenseMap = A.Fake<Expense>();
            var expense = A.Fake<Expense>();
            var expenseCreate = A.Fake<Expense>(); 
            A.CallTo(() => _mapper.Map<Expense>(expenseCreate)).Returns(expense);
            A.CallTo(() => _expenseRepo.Create(expenseMap)).Returns(true);
            var controller = new ExpensesController(_expenseRepo, _userRepo, _currencyRepo, _mapper);
            controller.ModelState.AddModelError("Comment", "Required");

            //Act
            var result = controller.CreateExpense(userId, currencyId, new Expense{Comment = "" });

            //Assert
            Assert.Equal(400, (result.Result as ObjectResult)?.StatusCode);

        }

        [Fact]
        public void CreateExpenseInCorrectModel_WithDateInFutur()
        {
            //Arrange
            int userId = 1;
            int currencyId = 1;
            var expenseMap = A.Fake<Expense>();
            var expense = A.Fake<Expense>();
            var expenseCreate = A.Fake<Expense>();
            A.CallTo(() => _mapper.Map<Expense>(expenseCreate)).Returns(expense);
            A.CallTo(() => _expenseRepo.Create(expenseMap)).Returns(true);
           
            var controller = new ExpensesController(_expenseRepo, _userRepo, _currencyRepo, _mapper);
            controller.ModelState.AddModelError("Date", "Not in futur");

            //Act
            var result = controller.CreateExpense(userId, currencyId, new Expense { Date = new System.DateTime(2023, 5, 1, 8, 30, 52) });

            //Assert
            Assert.Equal(400, (result.Result as ObjectResult)?.StatusCode);

        }

        [Fact]
        public void CreateExpenseInCorrectModel_WithDateInferieur3Months()
        {
            //Arrange
            int userId = 1;
            int currencyId = 1;
            var expenseMap = A.Fake<Expense>();
            var expense = A.Fake<Expense>();
            var expenseCreate = A.Fake<Expense>();
            A.CallTo(() => _mapper.Map<Expense>(expenseCreate)).Returns(expense);
            A.CallTo(() => _expenseRepo.Create(expenseMap)).Returns(true);
            var controller = new ExpensesController(_expenseRepo, _userRepo, _currencyRepo, _mapper);
            controller.ModelState.AddModelError("Date", "Not inferior now for 3 months");

            //Act
            var result = controller.CreateExpense(userId, currencyId, new Expense { Date = new System.DateTime(2022, 5, 1, 8, 30, 52) });

            //Assert
            Assert.Equal(400, (result.Result as ObjectResult)?.StatusCode);

        }

        [Fact]
        public void CreateExpenseInCorrectModel_WithDifferentCurrency()
        {
            //Arrange
      
            var user = Fake.user1;
            var currency = Fake.cur2;
            var expenseMap = A.Fake<Expense>();
            var expense = A.Fake<Expense>();
            var expenseCreate = A.Fake<Expense>();
            A.CallTo(() => _mapper.Map<Expense>(expenseCreate)).Returns(expense);
            A.CallTo(() => _expenseRepo.Create(expenseMap)).Returns(true);
            var controller = new ExpensesController(_expenseRepo, _userRepo, _currencyRepo, _mapper);
            controller.ModelState.AddModelError("Currency", "Not different Currency");

            //Act
            var result = controller.CreateExpense(user.UserId, currency.CurrencyId, new Expense { Currency = currency, User = user });

            //Assert
            Assert.Equal(400, (result.Result as ObjectResult)?.StatusCode);

        }

        [Fact]
        public void CreateExpenseInCorrectModel_WithTheSameDateAndAmount()
        {
            //Arrange
            var user = Fake.user1;
            var currency = Fake.cur1;
            var expenseMap = A.Fake<Expense>();
            var expenseCreate = A.Fake<Expense>();
            var expenses = A.Fake<ICollection<ExpenseDto>>();
            A.CallTo(() => _mapper.Map<Expense>(expenseCreate)).Returns(expenseCreate);
            A.CallTo(() => _expenseRepo.Create(expenseMap)).Returns(true);
            var controller = new ExpensesController(_expenseRepo, _userRepo, _currencyRepo, _mapper);
            controller.ModelState.AddModelError("Date", "Not the same with new expense");

            //Act
            var result = controller.CreateExpense(user.UserId, currency.CurrencyId, expenseCreate);

            //Assert
            Assert.Equal(400, (result.Result as ObjectResult)?.StatusCode);

        }

    }
}
