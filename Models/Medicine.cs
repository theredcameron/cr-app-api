using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    public class Medicine {
        [Column("MedicineId")]
        [Key]
        [JsonPropertyName("MedicineId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get;set;}

        [Required]
        [JsonPropertyName("Name")]
        [MaxLength(500)]
        public string Name {get;set;}
        
        [ForeignKey("MedicineId")]
        [JsonPropertyName("Ledger")]
        public List<Ledger> Ledger {get;set;}
    }
}