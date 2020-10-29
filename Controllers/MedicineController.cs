using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using api.Models;
using api.Services;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineController : ControllerBase
    {
        private static MedicineService _medicineService;

        private readonly ILogger<MedicineController> _logger;

        public MedicineController(ILogger<MedicineController> logger)
        {
            _logger = logger;
            _medicineService = new MedicineService();
        }

        // GET: api/Medicine
        [HttpGet]
        public ActionResult Get()
        {
            try 
            {
                return Ok(_medicineService.GetMedicines());
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
                return Ok(_medicineService.GetMedicineById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
