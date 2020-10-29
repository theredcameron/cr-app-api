using System.Text.Json.Serialization;

namespace api.Contracts {
    public class PrescriptionContract {
        [JsonPropertyName("MedicineId")]
        public long MedicineId {get;set;}

        [JsonPropertyName("Quantity")]
        public int Quantity {get;set;}

        [JsonPropertyName("Note")]
        public string Note {get;set;}
    }
}