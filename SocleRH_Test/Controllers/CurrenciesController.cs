using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using SocleRH_Test.Models;
using SocleRH_Test.IRepository;
using AutoMapper;
using SocleRH_Test.Dto;

namespace SocleRH_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        #region CONTRUCTOR
        private readonly ICurrencyRepository _currencyRepos;
        private readonly IMapper _mapper;
        public CurrenciesController(ICurrencyRepository currencyRepo,  IMapper mapper)
        {
            _currencyRepos = currencyRepo;
            _mapper = mapper;
        }
        #endregion

        #region GET ALL CURRENCIES
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Currency>))]
        public IActionResult GetCurrencies()
        {
            var currencies = _mapper.Map<List<CurrencyDto>>(_currencyRepos.GetAll());
        
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(currencies);
        }
        #endregion

        #region GET CURRENCYBY ID
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Currency))]
        [ProducesResponseType(400)]
        public ActionResult<Currency> GetCurrency(int id)
        {
            // verify the currency exist
            if (!_currencyRepos.Exists(id))
                return NotFound();

            var currency = _mapper.Map<CurrencyDto>(_currencyRepos.GetById(id));
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }
        #endregion

        #region CREATE NEW CURRENCY
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult<Currency> Create([FromBody] Currency currency)
        {
            // verification if object currency is null
            if (currency == null)
            {
                return BadRequest(ModelState);
            }

            // get all currencies with currency name 
            var currencies = _currencyRepos.GetAll()
                .Where(c => c.CurrencyName.Trim().ToUpper() == currency.CurrencyName.TrimEnd().ToUpper())
                .FirstOrDefault();

            // if there is one currency with the same name, not request 
            if (currencies != null)
            {
                ModelState.AddModelError("", "Currency already exists");
                return StatusCode(422, ModelState);
            }

            // verify the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // mappe data 
            var currencyMap = _mapper.Map<Currency>(currency);

            // alert if there are the problemes during the create
            if (!_currencyRepos.Create(currencyMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            // alert the succes of create
            return Ok("Successfully created");

        }
        #endregion

        #region UPDATE CURRENCY

        [HttpPut("{currencyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateCurrency(int id, [FromBody] Currency currency)
        {

            if (currency == null)
                return BadRequest(ModelState);
            // verify if id done equal id of currency to update 
            if (id != currency.CurrencyId)
                return BadRequest(ModelState);

            // vérify if currency exist
            if (!_currencyRepos.Exists(id))
                return NotFound();

            // vefify the correction of model state 
            if (!ModelState.IsValid)
                return BadRequest();

            // mapping data to currency objet
            var currencyMap = _mapper.Map<Currency>(currency);

            // alert if there are the problem during the updating
            if (!_currencyRepos.Update(currencyMap))
            {
                ModelState.AddModelError("", "Something went wrong updating currency");
                return StatusCode(500, ModelState);
            }
            // alert the succes of update
            return Ok("Successfully updated");
        }
        #endregion

        #region DELETE CURRENCY

        [HttpDelete("{currencyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int currencyId)
        {
            // vérify if currency exist
            if (!_currencyRepos.Exists(currencyId))
                return NotFound();//4045

            // Get currency to delete
            var currencyDelete = _currencyRepos.GetById(currencyId);

            // verification of correction of model state 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // alert if delete have problem 
            if (!_currencyRepos.Delete(currencyDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting currency");
            }
            return Ok("Successfully deleted");
        }

    }
    #endregion

}
