using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using api.Models;
using api.Contexts;
using api.Contracts;

namespace api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase {
        private readonly ILogger<MedicineController> _logger;
        private readonly IOptions<ConfigContract> _config;
        private readonly MedicineDbContext _context;

        public UserController(MedicineDbContext context, ILogger<MedicineController> logger, IOptions<ConfigContract> config)
        {
            _logger = logger;
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("api/[controller]/Login")]
        public ActionResult Login([FromBody]UserContract userContract) {
            try {
                var user = _context.Users.Single(x => x.UserName == userContract.UserName);

                if(user == null || user.Password == userContract.Password) {
                    throw new Exception("Invalid Credentials");
                }

                var key = _config.Value.Key;
                var issuer = _config.Value.Issuer;

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                claims.Add(new Claim("UserId", user.UserId.ToString()));
                claims.Add(new Claim("UserName", user.UserName));

                var token = new JwtSecurityToken(issuer,
                                issuer,
                                claims,
                                expires: DateTime.Now.AddMinutes(5),
                                signingCredentials: credentials);
                            
                var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(jwt_token);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}