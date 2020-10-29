using System;

namespace api.Models {
    public class Ledger {
        public long Id {get;set;}
        public long MedicineId {get;set;}
        public long Quantity {get;set;}
        public DateTime SavedDate {get;set;}
    }
}