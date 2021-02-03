using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    public class UserPrivilege {
        [Column("UserId")]
        [JsonPropertyName("UserId")]
        public long UserId {get;set;}
        public User User {get;set;}
        [Column("PrivilegeId")]
        [JsonPropertyName("PrivilegeId")]
        public long PrivilegeId {get;set;}
        public Privilege Privilege {get;set;}
    }
}