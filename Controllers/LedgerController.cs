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
    public class LedgerController : ControllerBase
    {
        private readonly ILogger<LedgerController> _logger;
        private readonly MedicineDbContext _context;

        public LedgerController(MedicineDbContext context, ILogger<LedgerController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/Ledger
        [HttpGet]
        public ActionResult GetLedgers() {
            try {
                return Ok(_context.Ledgers);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Ledger/5
        [HttpGet]
        public ActionResult GetLedgerById(long id) {
            try {
                return Ok(_context.Ledgers.Find(id));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Ledger?medicineId=5
        [HttpGet]
        [Route("api/[controller]?medicineId={medId}")]
        public ActionResult GetLedgersByMedicineId(long medId) {
            try {
                return Ok(_context.Ledgers.Select(x => x.MedicineId == medId));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Ledger
        [HttpPost]
        public ActionResult AlterLedger(Ledger ledger) {
            try {
                if(ledger.Id > 0){
                    _context.Ledgers.Update(ledger);
                } else {
                    _context.Ledgers.Add(ledger);
                }
                _context.SaveChanges();
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Ledger
        [HttpDelete]
        public ActionResult DeleteLedger(Ledger ledger) {
            try {
                _context.Ledgers.Remove(ledger);
                _context.SaveChanges();
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
