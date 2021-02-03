using System.Text.Json.Serialization;

namespace api.Contracts {
    public class PrivilegeContract {
        [JsonPropertyName("PrivilegeId")]
        public long Id {get;set;}

        [JsonPropertyName("Name")]
        public string Name {get;set;}
    }
}