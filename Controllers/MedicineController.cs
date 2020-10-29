using System;
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
    [Route("[controller]")]
    public class MedicineController : ControllerBase
    {
        private static MedicineService _medicineService;

        private readonly ILogger<MedicineController> _logger;

        public MedicineController(ILogger<MedicineController> logger)
        {
            _logger = logger;
            _medicineService = new MedicineService();
        }

        [HttpGet]
        public IEnumerable<Medicine> Get()
        {
            return _medicineService.GetMedicines();
        }
    }
}
