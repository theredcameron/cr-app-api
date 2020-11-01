using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    public class User {
        [Column("UserId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId {get;set;}

        [Required]
        [MaxLength(50)]
        public string UserName {get;set;}

        [Required]
        public string Password {get;set;}

        [Required]
        public DateTime CreatedDate {get;set;}

        [Required]
        public string FirstName {get;set;}

        [Required]
        public string LastName {get;set;}

        [Required]
        public bool Active {get;set;}
    }
}