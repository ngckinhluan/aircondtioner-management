using AirConditionerShop.DAL.Entities;
using AirConditionerShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop.BLL.Services
{
    public class SupplierService
    {
        private SupplierCompanyRepository _repo = new();
        public List<SupplierCompany> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
