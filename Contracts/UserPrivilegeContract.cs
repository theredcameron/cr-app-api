using System.Text.Json.Serialization;

namespace api.Contracts {
    public class UserPrivilegeController {
        [JsonPropertyName("UserId")]
        public long UserId {get;set;}
        [JsonPropertyName("PrivilegeId")]
        public long PrivilegeId {get;set;}
    }
}