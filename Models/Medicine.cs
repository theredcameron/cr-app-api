using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    public class Medicine {
        [Column("MedicineId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get;set;}

        [Required]
        [MaxLength(500)]
        public string Name {get;set;}
        
        [ForeignKey("MedicineId")]
        public List<Ledger> Ledger {get;set;}
    }
}