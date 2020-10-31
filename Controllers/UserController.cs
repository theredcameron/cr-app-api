using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Linq;

using api.Models;
using api.Contexts;
using api.Contracts;

namespace api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase {
        private readonly ILogger<MedicineController> _logger;
        private readonly MedicineDbContext _context;

        public UserController(MedicineDbContext context, ILogger<MedicineController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Route("api/[controller]/Login")]
        public ActionResult Login(UserContract userContract) {
            try {
                var user = _context.Users.Single(x => x.UserName == userContract.UserName);

                if(user.Password == userContract.Password) {
                    throw new Exception("Invalid Credentials");
                }

                var key = Configuration
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}