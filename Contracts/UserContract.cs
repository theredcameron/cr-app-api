using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.Contracts {
    public class UserContract {
        [JsonPropertyName("UserName")]
        public string UserName {get;set;}

        [JsonPropertyName("Password")]
        public string Password {get;set;}
    }
}