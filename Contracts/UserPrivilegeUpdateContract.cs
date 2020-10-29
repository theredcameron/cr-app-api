using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace api.Contracts {
    public class UserPrivilegeUpdateContract {
        [JsonPropertyName("UserId")]
        public long UserId {get;set;}

        [JsonPropertyName("Privileges")]
        public List<long> PrivilegeIds {get;set;}

    }
}