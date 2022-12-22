using Microsoft.EntityFrameworkCore;
using SocleRH_Test.Data;
using SocleRH_Test.IRepository;
using SocleRH_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRH_Test.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Context
        private readonly ExpenseApiContext _context;
        public UserRepository(ExpenseApiContext context)
        {
            _context = context;
        }
        #endregion

        #region Methode Save
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        #endregion

        #region Get all user
        public ICollection<User> GetAll()
        {
            return _context.Users.Include(u=>u.Currency).ToList();
        }
        #endregion

        #region Get User by Id
        public User GetById(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId).FirstOrDefault();
        }
        #endregion

        #region Create User
        public bool Create(User user)
        {
            _context.Add(user);
            return Save();
        }
        #endregion

        #region Update User
        public bool Update(User user)
        {
            _context.Update(user);
            return Save();
        }
        #endregion

        #region Delete User
        public bool Delete(User user)
        {
            _context.Remove(user);
            return Save();
        }
        #endregion

        #region Exist user
        public bool Exists(int userId)
        {
            return _context.Users.Any(u=> u.UserId == userId);
        }
        #endregion
    }
}
