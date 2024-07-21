using AirConditionerShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop.DAL.Repositories
{
    public class StaffMemberRepository
    {
        private AirConditionerShop2024DbContext _context;

        public List<StaffMember> GetAll()
        {
            _context = new();
            return _context.StaffMembers.ToList();
        }

        public StaffMember GetOne(string email, string password)
        {
            _context = new();
            return _context.StaffMembers.FirstOrDefault(s => s.EmailAddress == email && s.Password == password && s.Role != 3);
        }
    }
}
