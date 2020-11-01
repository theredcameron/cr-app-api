using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using api.Models;
using api.Contexts;
using api.Contracts;

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
        }

        // GET: api/Medicine
        [Authorize]
        [HttpGet]
        [Route("api/Medicine")]
        public ActionResult Get()
        {
            try 
            {
                return Ok(from medicines in _context.Medicines select medicines);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Medicine/5
        [Authorize]
        [HttpGet]
        [Route("api/Medicine/{id}")]
        public ActionResult GetWithId(long id) {
            try 
            {
                var medicine = (from medicines in _context.Medicines
                                where medicines.Id == id
                                select medicines).FirstOrDefault();
                if(medicine == null) {
                    return NotFound();
                }
                return Ok(medicine);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Medicine
        [Authorize]
        [HttpPost]
        [Route("api/Medicine")]
        public ActionResult AlterMedicine([FromBody]MedicineContract medicine) {
            try{
                if(medicine.Id > 0) {
                    UpdateMedicine(medicine);
                } else {
                    AddMedicine(medicine);
                }
                _context.SaveChanges();
                return Ok(medicine);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            } 
        }

        private Medicine AddMedicine(MedicineContract medContract) {
            var medicine = new Medicine{
                Id = 0,
                Name = medContract.Name,
                CurrentQuantity = medContract.CurrentQuantity
            };
            _context.Medicines.Add(medicine);
            return medicine;
        }

        private Medicine UpdateMedicine(MedicineContract medContract) {
            var medicine = _context.Medicines.Find(medContract.Id);
            medicine.Name = medContract.Name;
            medicine.CurrentQuantity = medContract.CurrentQuantity;
            return medicine;
        }

        // POST: api/Medicine/Prescription
        [Authorize]
        [HttpPost]
        [Route("api/Medicine/Prescription")]
        public ActionResult AddPrescription([FromBody]PrescriptionContract prescription) {
            try {
                var ledger = new Ledger{
                    Id = 0,
                    MedicineId = prescription.MedicineId,
                    Quantity = prescription.Quantity,
                    SavedDate = DateTime.Now,
                    Note = prescription.Note
                };

                var medicine = (from medicines in _context.Medicines 
                                where medicines.Id == prescription.MedicineId 
                                select medicines).FirstOrDefault();
                if(medicine == null) {
                    return BadRequest("Medicine cannot be found");
                }
                medicine.CurrentQuantity += prescription.Quantity;
                _context.Ledgers.Add(ledger);
                _context.SaveChanges();
                
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Medicine/{id}
        [Authorize]
        [HttpDelete]
        [Route("api/Medicine/{id}")]
        public ActionResult DeleteMedicine(long id) {
            try {
                var medicine = _context.Medicines.Find(id);
                if(medicine == null) {
                    return NotFound();
                }
                _context.Ledgers.RemoveRange(from ledgers in _context.Ledgers 
                                            where ledgers.MedicineId == id 
                                            select ledgers);
                _context.Medicines.Remove(medicine);
                _context.SaveChanges();
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
