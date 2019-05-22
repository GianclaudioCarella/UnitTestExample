using System;
using System.Linq;
using UnitTestExample.Business.Data;
using UnitTestExample.Business.Models;

namespace UnitTestExample.Business.Services
{
    public class UserService
    {
        private ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add the specified user.
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="user">User.</param>
        public Result Add(User user)
        {
            var result = ValidateUser(user);
            result.Action = "Add new User";

            if(result.Success)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }

            return result;
        }

        /// <summary>
        /// Get the specified name.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="name">Name.</param>
        public User Get(string name)
        {
            return _context.Users
                .Where(u => u.Name.ToLower().Trim() == name.ToLower().Trim()).FirstOrDefault();
        }

        /// <summary>
        /// Gets the total of a user.
        /// </summary>
        /// <returns>The total.</returns>
        /// <param name="user">User.</param>
        public double GetTotal(User user)
        {
            var pobjUser = _context.Users.Where(u => u.UserId.Equals(user.UserId)).FirstOrDefault();
            return pobjUser.Total;
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name="user">User.</param>
        public Result ValidateUser(User user)
        {
            var result = new Result();

            if (String.IsNullOrEmpty(user.Name) && String.IsNullOrWhiteSpace(user.Name))
                result.Validations.Add("The user needs a name");

            if (String.IsNullOrEmpty(user.Email) && String.IsNullOrWhiteSpace(user.Email))
                result.Validations.Add("The user needs a email");

            return result;
        }
    }
}
