using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using api.Models;
using api.Contexts;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineController : ControllerBase
    {
        private readonly ILogger<MedicineController> _logger;
        private readonly MedicineDbContext _context;

        public MedicineController(MedicineDbContext context, ILogger<MedicineController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/Medicine
        [HttpGet]
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
        [HttpGet("{id}")]
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
        public ActionResult AlterMedicine(Medicine medicine) {
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
        public ActionResult DeleteMedicine(Medicine medicine) {
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
