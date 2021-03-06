using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    public class Privilege {
        [Column("PrivilegeId")]
        [Key]
        [JsonPropertyName("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get;set;}

        [Required]
        [JsonPropertyName("Name")]
        [MaxLength(100)]
        public string Name {get;set;}

        [JsonPropertyName("Description")]
        [MaxLength(500)]
        public string Description {get;set;}
    }
}