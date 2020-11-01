using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.Contracts {
    public class MedicineContract {
        [JsonPropertyName("MedicineId")]
        public long Id {get;set;}

        [JsonPropertyName("Name")]
        public string Name {get;set;}

        [JsonPropertyName("CurrentQuantity")]
        public int CurrentQuantity {get;set;}
    }
}