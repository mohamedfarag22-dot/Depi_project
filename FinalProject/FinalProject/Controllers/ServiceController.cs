using FinalProject.Dtos;
using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServicesService _servicesService;
        private readonly IFeedbackService _feedbackService;

        public ServicesController(IServicesService servicesService, IFeedbackService feedbackService)
        {
            _servicesService = servicesService;
            _feedbackService = feedbackService;
        }

        public IActionResult Index()
        {
            var services = _servicesService.GetAllServices();
            return View(services);
        }

        public IActionResult Create(int categoryId)
        {
            var serviceProviderId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(serviceProviderId))
            {
                return Unauthorized();
            }

            var serviceDto = new ServiceDto
            {
                CategoryId = categoryId,
                ServiceProviderId = serviceProviderId
            };

            return View(serviceDto);
        }

        [HttpPost]
        public IActionResult Create(ServiceDto serviceDto)
        {
            if (ModelState.IsValid)
            {
                var service = new Service
                {
                    Name = serviceDto.Name,
                    Description = serviceDto.Description,
                    Price = serviceDto.Price,
                    CategoryId = serviceDto.CategoryId,
                    ServiceProviderId = serviceDto.ServiceProviderId
                };

                _servicesService.AddService(service);
                return RedirectToAction("Details", "Categories", new { id = serviceDto.CategoryId });
            }

            return View(serviceDto);
        }

        public IActionResult Edit(int id)
        {
            var service = _servicesService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }

            // Get the logged-in user's ID
            var userId = HttpContext.Session.GetString("UserId");
            if (service.ServiceProviderId != userId)
            {
                return Forbid(); // Prevent unauthorized access
            }

            var serviceDto = new ServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                CategoryId = service.CategoryId,
                ServiceProviderId = service.ServiceProviderId
            };

            return View(serviceDto);
        }

        [HttpPost]
        public IActionResult Edit(ServiceDto serviceDto)
        {
            if (ModelState.IsValid)
            {
                var service = new Service
                {
                    Id = serviceDto.Id,
                    Name = serviceDto.Name,
                    Description = serviceDto.Description,
                    Price = serviceDto.Price,
                    CategoryId = serviceDto.CategoryId,
                    ServiceProviderId = serviceDto.ServiceProviderId
                };

                _servicesService.UpdateService(service);
                return RedirectToAction("Details", "Categories", new { id = serviceDto.CategoryId });
            }
            return View(serviceDto);
        }

        public IActionResult Delete(int id)
        {
            var service = _servicesService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }

            // Get the logged-in user's ID
            var userId = HttpContext.Session.GetString("UserId");
            if (service.ServiceProviderId != userId)
            {
                return Forbid(); // Prevent unauthorized access
            }

            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var service = _servicesService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }

            // Get the logged-in user's ID
            var userId = HttpContext.Session.GetString("UserId");
            if (service.ServiceProviderId != userId)
            {
                return Forbid(); // Prevent unauthorized access
            }

            var categoryId = service.CategoryId;
            _servicesService.DeleteService(id);

            return RedirectToAction("Details", "Categories", new { id = categoryId });
        }

        public IActionResult Details(int id)
        {
            var service = _servicesService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }

            service.Feedbacks = _feedbackService.GetFeedbacksByServiceId(id);
            return View(service);
        }

        [HttpPost]
        public IActionResult SubmitFeedback(int serviceId, string content, int rating)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var feedback = new Feedback
            {
                Content = content,
                Rating = rating,
                ServiceId = serviceId,
                UserId = userId,
            };

            _feedbackService.AddFeedback(feedback); // Assuming you have a method to add feedback
            return RedirectToAction("Details", new { id = serviceId });
        }
    }
}
