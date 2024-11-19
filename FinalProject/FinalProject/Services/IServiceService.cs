using FinalProject.Models;
using System.Collections.Generic;

namespace FinalProject.Services
{
    public interface IServicesService
    {
        IEnumerable<Service> GetAllServices();
        Service GetServiceById(int id);
        void AddService(Service service);
        void UpdateService(Service service);
        void DeleteService(int id);
    }
}
