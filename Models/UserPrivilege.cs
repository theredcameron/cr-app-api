using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    public class UserPrivilege {
        [Column("UserId")]
        [JsonPropertyName("UserId")]
        [ForeignKey("User")]
        public long UserId {get;set;}
        [Column("PrivilegeId")]
        [JsonPropertyName("PrivilegeId")]
        [ForeignKey("Privilege")]
        public long PrivilegeId {get;set;}
    }
}