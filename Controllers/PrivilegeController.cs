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
using api.Attributes;

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
        }

        [HttpGet]
        [Authorize]
        [Route("api/Privilege")]
        [Privilege("Secondary")]
        public ActionResult GetAllPrivileges() {
            try {
                return Ok(_context.Privileges);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/UserPrivilege/{userId}")]
        [Privilege("Admin", "Secondary")]
        public ActionResult GetAllUserPrivileges(long userId) {
            try {
                return Ok(_context.UserPrivileges.Where(x => x.UserId == userId));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("api/UserPrivilege")]
        [Privilege("Secondary")]
        public ActionResult UpdateUserPrivilege([FromBody] UserPrivilegeUpdateContract privilegeUpdates) {
            try {
                    _context.UserPrivileges.RemoveRange(from privilege in _context.UserPrivileges
                                    where privilege.UserId == privilegeUpdates.UserId
                                    select privilege);

                    foreach(var update in privilegeUpdates.PrivilegeIds){
                        _context.UserPrivileges.Add(new UserPrivilege{
                            UserId = privilegeUpdates.UserId,
                            PrivilegeId = update
                        });
                    }

                    _context.SaveChanges();

                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}