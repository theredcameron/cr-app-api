using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using api.Models;
using api.Contexts;

namespace api.Controllers
{
    [ApiController]
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
        [Authorize]
        [Route("api/Ledger")]
        public ActionResult GetLedgers() {
            try {
                return Ok(from ledgers in _context.Ledgers select ledgers);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Ledger/5
        [Authorize]
        [HttpGet]
        [Route("api/Ledger/{id}")]
        public ActionResult GetLedgerById(long id) {
            try {
                var ledger = (from ledgers in _context.Ledgers
                                where ledgers.Id == id
                                select ledgers).FirstOrDefault();
                if(ledger == null) {
                    return NotFound();
                }
                return Ok(ledger);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Ledger/Medicine/5
        [Authorize]
        [HttpGet]
        [Route("api/Ledger/Medicine/{medId}")]
        public ActionResult GetLedgersByMedicineId(long medId) {
            try {
                return Ok(from ledgers in _context.Ledgers
                            where ledgers.MedicineId == medId
                            select ledgers);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Ledger
        [Authorize]
        [HttpPost]
        [Route("api/Ledger")]
        public ActionResult AlterLedger([FromBody]Ledger ledger) {
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
        [Authorize]
        [HttpDelete]
        [Route("api/Ledger")]
        public ActionResult DeleteLedger(long id) {
            try {
                var ledger = (from ledgers in _context.Ledgers
                                where ledgers.Id == id
                                select ledgers).FirstOrDefault();
                if(ledger == null) {
                    return NotFound();
                }
                _context.Ledgers.Remove(ledger);
                _context.SaveChanges();
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
