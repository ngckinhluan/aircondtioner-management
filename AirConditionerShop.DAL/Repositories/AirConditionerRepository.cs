using AirConditionerShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConditionerShop.DAL.Repositories
{
    public class AirConditionerRepository
    {
        private AirConditionerShop2024DbContext _context;

        public List<AirConditioner> GetAll()
        {
            _context = new();
            return _context.AirConditioners.Include(a => a.Supplier).ToList();
        }

        public AirConditioner GetById(int id)
        {
            _context = new();
            return _context.AirConditioners.Find(id);
        }

        public void Add(AirConditioner airConditioner)
        {
            _context = new();
            _context.AirConditioners.Add(airConditioner);
            _context.SaveChanges();
        }

        public void Delete(AirConditioner airConditioner)
        {
            _context = new();
            _context.AirConditioners.Remove(airConditioner);
            _context.SaveChanges();
        }

        public void Update(AirConditioner airConditioner)
        {
            _context = new();
            _context.AirConditioners.Update(airConditioner);
            _context.SaveChanges();
        }
    }
}
