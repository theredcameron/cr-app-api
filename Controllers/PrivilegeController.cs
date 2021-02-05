using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using api.Contexts;
using api.Contracts;
using api.Models;

namespace api.Controllers {
    [ApiController]
    public class PrivilegeController : ControllerBase {
        private readonly ILogger<UserController> _logger;
        private readonly IOptions<ConfigContract> _config;
        private readonly MedicineDbContext _context;

        public PrivilegeController(MedicineDbContext context, ILogger<UserController> logger, IOptions<ConfigContract> config) {
            _logger = logger;
            _context = context;
            _config = config;

            var privileges = _context.Privileges.Select(x => x.Name == "test privilege");
            if(privileges.Count() <= 0) {
                _context.Privileges.Add(new Privilege{
                    Name = "test privilege"
                });

                _context.SaveChanges();
            }

            var userPrivilege = _context.UserPrivileges.Select(x => x.UserId == 1 && x.PrivilegeId == 1);
            if(userPrivilege.Count() <= 0) {
                _context.UserPrivileges.Add(new UserPrivilege{
                    UserId = 1,
                    PrivilegeId = 1
                });

                _context.SaveChanges();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/Privilege")]
        public ActionResult GetAllPrivileges() {
            try {
                return Ok(_context.Privileges);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/UserPrivilege")]
        public ActionResult GetAllUserPrivileges() {
            try {
                return Ok(_context.UserPrivileges);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}