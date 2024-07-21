using AirConditionerShop.DAL.Entities;
using AirConditionerShop.DAL.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace AirConditionerShop.BLL.Services
{
    public class AirConditionerService
    {
        private AirConditionerRepository _repo = new();

        public List<AirConditioner> GetAll()
        {
            return _repo.GetAll();
        }

        public void Add(AirConditioner airConditioner)
        {
            _repo.Add(airConditioner);
        }

        public AirConditioner GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Delete(AirConditioner airConditioner)
        {
            _repo.Delete(airConditioner);
        }

        public void Update(AirConditioner airConditioner)
        {
            _repo.Update(airConditioner);
        }

        public List<AirConditioner> Search(string function, int? quantity)
        {
            //Feature AND quantity
            // 1.KO GÕ HAI KEYWORD, THÌ TRẢ VỀ FULL
            List<AirConditioner> result = _repo.GetAll();
            if (function.IsNullOrEmpty() && !quantity.HasValue) return result;
            // 2. NẾU CÓ GÕ FEATURE, SEARCH TRÊN FEATURE TRƯỚC
            if (!function.IsNullOrEmpty())
            {
                result = result.Where(s => s.FeatureFunction.ToLower().Contains(function.ToLower())).ToList();
            }
            // 3. NẾU CÓ GÕ QUANTITY, SEARCH TRÊN QUANTITY
            if (quantity.HasValue)
            {
                result = result.Where(s => s.Quantity == quantity).ToList();
            }
            return result;
        }

        public List<AirConditioner> SearchOr(string function, int? quantity)
        {
            List<AirConditioner> result = _repo.GetAll();
            if (function.IsNullOrEmpty() && !quantity.HasValue) return result;
            if (!function.IsNullOrEmpty() && quantity.HasValue)
            {
                result = result.Where(x => x.FeatureFunction.ToLower().Contains(function.ToLower()) || x.Quantity == quantity).ToList();
            }
            else if (!function.IsNullOrEmpty())
            {
                result = result.Where(x => x.FeatureFunction.ToLower().Contains(function.ToLower())).ToList();
            }
            else
            {
                result = result.Where(x => x.Quantity == quantity).ToList();
            }
            return result;
        }

    }
}
