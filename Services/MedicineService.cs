using System.Collections.Generic;

using api.Models;

namespace api.Services {
    public class MedicineService {
        private List<Medicine> _medicines = null;

        public MedicineService() {
            _medicines = new List<Medicine>{
                new Medicine{
                    Id = 1,
                    Name = "First medicine"
                },
                new Medicine{
                    Id = 2,
                    Name = "Second medicine"
                },
                new Medicine{
                    Id = 3,
                    Name = "Third medicine"
                }
            };
        }

        public List<Medicine> GetMedicines() {
            return _medicines;
        }

        public Medicine GetMedicineById(long id) {
            return _medicines.Find(x => x.Id == id);
        }
    }
}