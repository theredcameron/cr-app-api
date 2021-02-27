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

using api.Models;
using api.Contexts;
using api.Contracts;
using api.Utilities;

namespace api.Controllers {
    [ApiController]
    public class UserController : ControllerBase {
        private readonly ILogger<UserController> _logger;
        private readonly IOptions<ConfigContract> _config;
        private readonly MedicineDbContext _context;

        public UserController(MedicineDbContext context, ILogger<UserController> logger, IOptions<ConfigContract> config)
        {
            _logger = logger;
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("api/User/Login")]
        public ActionResult Login([FromBody]UserContract userContract) {
            try {
                //var user = _context.Users.Single(x => x.UserName == userContract.UserName);
                var users = from innerUser in _context.Users
                            where innerUser.UserName == userContract.UserName
                            select innerUser;
                if(users.Count() <= 0) {
                    return BadRequest("Invalid Credentials");
                }

                var user = users.Single();
                
                if(user == null || Crypto.Decrypt(user.Password) != userContract.Password) {
                    return BadRequest("Invalid Credentials");
                }

                var privileges = from privilege in _context.Privileges
                                    join userPrivilege in _context.UserPrivileges
                                    on privilege.Id equals userPrivilege.PrivilegeId
                                    where userPrivilege.UserId == user.UserId
                                    select privilege;

                var key = _config.Value.Key;
                var issuer = _config.Value.Issuer;

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                claims.Add(new Claim("UserId", user.UserId.ToString()));
                claims.Add(new Claim("UserName", user.UserName));

                foreach(var privilege in privileges){
                    claims.Add(new Claim("Privilege", privilege.Name));
                }

                var token = new JwtSecurityToken(issuer,
                                issuer,
                                claims,
                                expires: DateTime.Now.AddHours(12),
                                signingCredentials: credentials);
                            
                var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new {token = jwt_token} );
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/User
        [HttpGet]
        [Authorize]
        [Route("api/User")]
        public ActionResult GetAllUsers() {
            try {
                // Iterate through users to omit their passwords
                var users = new List<User>();
                foreach(var user in _context.Users) {
                    var fullUser = new User{
                        UserId = user.UserId,
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        CreatedDate = user.CreatedDate,
                        Active = user.Active
                    };
                    users.Add(fullUser);
                }
                return Ok(users);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/User/{id}
        [HttpGet]
        [Authorize]
        [Route("api/User/{id}")]
        public ActionResult GetUserById(long id) {
            try {
                var user = (from users in _context.Users
                            where users.UserId == id
                            select users).FirstOrDefault();
                if(user == null) {
                    return NotFound();
                }
                return Ok(user);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/User
        [HttpPost]
        [Authorize]
        [Route("api/User")]
        public ActionResult AlterUser([FromBody]User user) {
            try {
                if(user.UserId > 0) {
                    user = UpdateUser(user);
                } else {
                    user = CreateNewUser(user);
                }
                _context.SaveChanges();
                return Ok(user);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        private User CreateNewUser(User user) {
            user.CreatedDate = DateTime.Now;
            _context.Users.Add(user);
            return user;
        }

        private User UpdateUser(User user) {
            _context.Users.Update(user);
            return user;
        }

        // GET: api/User/{id}
        [HttpDelete]
        [Authorize]
        [Route("api/User/{id}")]
        public ActionResult DeleteUser(long id) {
            try {
                var user = (from users in _context.Users
                            where users.UserId == id
                            select users).FirstOrDefault();
                if(user == null) {
                    return NotFound();
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}