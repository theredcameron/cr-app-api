using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    public class User {
        [Column("UserId")]
        [JsonPropertyName("UserId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId {get;set;}

        [Required]
        [JsonPropertyName("UserName")]
        [MaxLength(50)]
        public string UserName {get;set;}

        [Required]
        [JsonPropertyName("Password")]
        public string Password {get;set;}

        [Required]
        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate {get;set;}

        [Required]
        [JsonPropertyName("FirstName")]
        public string FirstName {get;set;}

        [Required]
        [JsonPropertyName("LastName")]
        public string LastName {get;set;}

        [Required]
        [JsonPropertyName("Active")]
        public bool Active {get;set;}
        [ForeignKey("PrivilegeId")]
        public IList<UserPrivilege> UserPrivileges {get;set;}
    }
}