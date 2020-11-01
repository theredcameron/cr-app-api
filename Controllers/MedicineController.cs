using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using api.Models;
using api.Contexts;

namespace api.Controllers
{
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly ILogger<MedicineController> _logger;
        private readonly MedicineDbContext _context;

        public MedicineController(MedicineDbContext context, ILogger<MedicineController> logger)
        {
            _logger = logger;
            _context = context;

            //Add test user
            var users = _context.Users.Select(x => x.UserName == "testuser");
            if(users.Count() <= 0) {
                _context.Users.Add(new User{
                    UserName = "testuser",
                    Password = "12345",
                    FirstName = "test",
                    LastName = "user",
                    CreatedDate = DateTime.Now
                });

                _context.SaveChanges();
            }
            
        }

        // GET: api/Medicine
        [Authorize]
        [HttpGet]
        [Route("api/Medicine")]
        public ActionResult Get()
        {
            try 
            {
                return Ok(_context.Medicines);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Medicine/5
        [HttpGet]
        [Route("api/Medicine/{id}")]
        public ActionResult GetWithId(long id) {
            try 
            {
                return Ok(_context.Medicines.Find(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Medicine
        [HttpPost]
        [Route("api/Medicine")]
        public ActionResult AlterMedicine([FromBody]Medicine medicine) {
            try{
                if(medicine.Id > 0) {
                    _context.Update(medicine);
                } else {
                    _context.Add(medicine);
                }
                _context.SaveChanges();
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            } 
        }

        // DELETE: api/Medicine
        [HttpDelete]
        [Route("api/Medicine")]
        public ActionResult DeleteMedicine([FromBody]Medicine medicine) {
            try {
                _context.Medicines.Remove(medicine);
                _context.SaveChanges();
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
