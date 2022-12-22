using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocleRH_Test.Data;
using SocleRH_Test.Dto;
using SocleRH_Test.IRepository;
using SocleRH_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SocleRH_Test.Models.ExpenseViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocleRH_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        #region PROPRIETES PRIVATTES
        private readonly IExpenseRepository _expenseRepos;
        private readonly IUserRepository _userRepos;
        private readonly ICurrencyRepository _currencyRepos;
        private readonly IMapper _mapper;
       
        #endregion

        #region CONSTRUCTEUR
        public ExpensesController(IExpenseRepository expenseRepo, 
                                  IUserRepository useRepos, 
                                  ICurrencyRepository currencyRepos,
                                  IMapper mapper)
        {
            _expenseRepos = expenseRepo;
            _userRepos = useRepos;
            _currencyRepos = currencyRepos;
            _mapper = mapper;
        }
        #endregion

        #region GET ALL EXPENSE
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Expense>))]
        public IActionResult GetExpenses()
        {
            List<ExpenseViewModel> listExpense = new List<ExpenseViewModel>();
            var expenses = _mapper.Map<List<Expense>>(_expenseRepos.GetAll())
                .OrderByDescending(e => e.Date);                
  
            foreach (var x in expenses)
            {
                listExpense.Add(new ExpenseViewModel(x));
            }
    
            return Ok(listExpense);

        }
        #endregion

        #region GET EXPENSE BY ID
        [HttpGet("{expenseId}")]
        [ProducesResponseType(200, Type = typeof(Expense))]
        [ProducesResponseType(400)]
        public ActionResult<Expense> GetExpenseById(int expenseId)
        {
            var expense = _mapper.Map<ExpenseDto>(_expenseRepos.GetById(expenseId));
            if (expense == null)
                {
                    return NotFound();
                }
                return Ok(expense);
         
        }
        #endregion

        #region FILTRER EXPENSE
        /**
         * Lister les dépenses pour un utilisateur donné 
         * Lister les dépenses par montant 
         * Lister les dépenses par date
         * Parametre searching est une date ou un montant ou un utilisateur
        */

        [HttpGet("Search/{searching}")]
        [ProducesResponseType(200, Type = typeof(Expense))]
        [ProducesResponseType(400)]
        public IActionResult GetExpensesBy(string searching)
        {

            List<ExpenseViewModel> listExpense = new List<ExpenseViewModel>();
            var expenses = _mapper.Map<List<Expense>>(_expenseRepos.Search(searching))
                .OrderByDescending(e => e.Date);

            foreach (var ex in expenses)
            {
                listExpense.Add(new ExpenseViewModel(ex));
            }
            return Ok(listExpense);

        }
        #endregion

        #region CREATE EXPENSE
        /**
         * Création d'une nouvelle dépense 
         * Une dépense ne peut ps avoir une date dans le futur
         * Une dépense ne peut pas etre datée de plus de 3 mois
         * Le commentaire est obligatoir
         * Un utilisateur ne peut pas déclarer deux fois la meme dépense(meme date et meme montant)
         * la devise de la dépense doit être identique à celle de l'utilisateur
         * code 422 pour les regles non valide
        */
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult<Expense> CreateExpense([FromQuery] int currencyId, [FromQuery] int userId,[FromBody] Expense expense)
        {
            if (expense == null)
                return BadRequest(ModelState);

            // vérification la meme date et même montant 
            var expensesExist = _expenseRepos.GetAll()
             .Where(e => e.Date == expense.Date && e.Amount == expense.Amount)
             .FirstOrDefault();
            if (expensesExist!=null){
                ModelState.AddModelError("", "Expense with the same date and amount already exists");
                return StatusCode(422, ModelState);
            }

            // Vérification de la date qui est égale ou inférieure à la date actuelle et non inférieure à 3 mois
            if (!Validations.Date.DateValidated(Convert.ToDateTime(expense.Date)))
            {
                ModelState.AddModelError("", "Date non valide! Date have equal or be smaller than the date now and not inferior to 3 months");
                return StatusCode(400, ModelState);
            }

            // Vérification de l'identique du devise 
            var currencyUser = _currencyRepos.GetCurrencyUserByUser(userId);
            var currencyExpense = _expenseRepos.GetCurrencyExpenseByCurrencyId(currencyId);
            var cur = _currencyRepos.GetById(currencyId);
            if (currencyUser.CurrencyName.Trim().ToUpper() != currencyExpense.CurrencyName.Trim().ToUpper())
            {
                ModelState.AddModelError("", "Currency non valide! The currency in expense have to be one with the currency of user");
                return StatusCode(422, ModelState);
            }

            // Vérification l'état de model et le commentaire n'est pas vide
            if (!ModelState.IsValid && expense.Comment !=null)
                return BadRequest(ModelState);

            var expenseMap = _mapper.Map<Expense>(expense);
            expenseMap.Currency = _currencyRepos.GetById(currencyId);
            expenseMap.User = _userRepos.GetById(userId);

            // Vérification la creation de la dépense 
            if (!_expenseRepos.Create(expenseMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        #endregion

        #region UPDATE EXPENSE
        [HttpPut("{expenseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateExpense(int expenseId, int userId, int CurrencyId, [FromBody] Expense expense)
        {
            if (expense == null)
                return BadRequest(ModelState);

            if (expenseId != expense.ExpenseId)
                return BadRequest(ModelState);

            if (!_expenseRepos.Exists(expenseId))
                return NotFound();

            if (!ModelState.IsValid)
                return StatusCode(400, "Model n'est pas valisde");

            //   var expenseUpdate = _expenseRepos.GetById(expenseId);

            // Vérification de la date qui est égale ou inférieure à la date actuelle et non inférieure à 3 mois
            if (!Validations.Date.DateValidated(Convert.ToDateTime(expense.Date)))
            {
                ModelState.AddModelError("", "Date non valide! Date have equal or be smaller than the date now and not inferior to 3 months");
                return StatusCode(422, ModelState);
            }
           
            // Vérification de l'identique du devise 
            var currencyUser = _currencyRepos.GetCurrencyUserByUser(userId);
            var currencyExpense = _expenseRepos.GetCurrencyExpenseByCurrencyId(CurrencyId);

            var cur = _currencyRepos.GetById(CurrencyId);
            if (currencyUser.CurrencyName.Trim().ToUpper() != currencyExpense.CurrencyName.Trim().ToUpper())
            {
                ModelState.AddModelError("", "Currency non valide! The currency in expense have to be one with the currency of user");
                return StatusCode(422, ModelState);
            }

            var expenseMap = _mapper.Map<Expense>(expense);

            if (!_expenseRepos.Update(expenseMap))
            {
                ModelState.AddModelError("", "Something went wrong updating expense");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        #endregion

        #region DELETE EXPENSE
        [HttpDelete("{expenseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int expenseId)
        {
            if (!_expenseRepos.Exists(expenseId))
                return NotFound();
            
             var expenseToDelete = _expenseRepos.GetById(expenseId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_expenseRepos.Delete(expenseToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting expense");
            }
            return Ok("Successfully deleted");
        }
        #endregion

    }
}
