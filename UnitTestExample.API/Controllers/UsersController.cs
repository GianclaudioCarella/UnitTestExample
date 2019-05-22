using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnitTestExample.Business;
using UnitTestExample.Business.Models;
using UnitTestExample.Business.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnitTestExample.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET api/user/5
        [HttpGet("{user}")]
        public IActionResult Get(string user)
        {
            var pobjUser = _userService.Get(user);
            if (pobjUser == null)
                return NotFound();
            else
                return new ObjectResult(pobjUser);
        }

        // POST api/users
        [HttpPost]
        public Result Post([FromBody]User user)
        {
            return _userService.Add(user);
        }
    }
}
