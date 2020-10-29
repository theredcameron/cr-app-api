using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    public class Ledger {
        [Column("LedgerId")]
        [Key]
        [JsonPropertyName("LedgerId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get;set;}

        [Column("MedicineId")]
        [JsonPropertyName("MedicineId")]
        [Required]
        public long MedicineId {get;set;}

        [Required]
        [JsonPropertyName("Quantity")]
        public long Quantity {get;set;}

        [Required]
        [JsonPropertyName("SavedDate")]
        public DateTime SavedDate {get;set;}

        [JsonPropertyName("Note")]
        public string Note {get;set;}
    }
}