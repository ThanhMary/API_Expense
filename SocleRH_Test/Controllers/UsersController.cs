using AutoMapper;
using Microsoft.AspNetCore.Http;
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

namespace SocleRH_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region CONTRUCTOR
        private readonly ICurrencyRepository _currencyRepos;
        private readonly IUserRepository _userRepos;
        private readonly IMapper _mapper;

        public UsersController(ICurrencyRepository currencyRepos, IUserRepository userRepos, IMapper mapper)
        {
            _currencyRepos = currencyRepos;
            _userRepos = userRepos;
            _mapper = mapper;
        }
        #endregion

        #region GET ALL USERS
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {

            var users = _mapper.Map<List<UserDto>>(_userRepos.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        #endregion

        #region GET USER BY ID
        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public ActionResult<User> GetUserById(int userId)
        {
            var user = _mapper.Map<UserDto>(_userRepos.GetById(userId));

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        #endregion

        #region CREATE NEW USER

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromQuery] int currencyId, [FromBody] UserDto user)
        {
            if (user == null)
                return BadRequest(ModelState);

            var users = _userRepos.GetAll()
                .Where(c => c.LastName.Trim().ToUpper() == user.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (users != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(user);

            userMap.Currency = _currencyRepos.GetById(currencyId);

            if (!_userRepos.Create(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        #endregion

        #region UPDATE USER

        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (userId != updatedUser.UserId)
                return BadRequest(ModelState);

            if (!_userRepos.Exists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userRepos.Update(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        #endregion

        #region DELETE USER

        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepos.Exists(userId))
            {
                return NotFound();
            }

            var userToDelete = _userRepos.GetById(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepos.Delete(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }

            return Ok("Successfully deleted");
        }

    }
    #endregion

}
