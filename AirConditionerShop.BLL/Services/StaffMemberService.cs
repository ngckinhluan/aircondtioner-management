using AirConditionerShop.DAL.Entities;
using AirConditionerShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop.BLL.Services
{
    public class StaffMemberService
    {
        private StaffMemberRepository _repo = new();
        public StaffMember authorize(string email, string password)
        {
            return _repo.GetOne(email, password);
        }
    }
}
