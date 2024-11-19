using FinalProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.Services
{
    public class ServicesService : IServicesService
    {
        private readonly ApplicationDbContext _context;

        public ServicesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Service> GetAllServices()
        {
            return _context.services.ToList();
        }

        public Service GetServiceById(int id)
        {
            return _context.services.Find(id);
        }

        public void AddService(Service service)
        {
            _context.services.Add(service);
            _context.SaveChanges();
        }

        public void UpdateService(Service service)
        {
            _context.services.Update(service);
            _context.SaveChanges();
        }

        public void DeleteService(int id)
        {
            var service = GetServiceById(id);
            if (service != null)
            {
                _context.services.Remove(service);
                _context.SaveChanges();
            }
        }
    }
}
