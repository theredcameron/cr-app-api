using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    public class Ledger {
        [Column("LedgerId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get;set;}

        [Column("MedicineId")]
        [Required]
        public long MedicineId {get;set;}

        [Required]
        public long Quantity {get;set;}

        [Required]
        public DateTime SavedDate {get;set;}
    }
}