using FinalProject.Dtos;
using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FinalProject.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFeedbackService _feedbackService;
        private readonly IServicesService _servicesService;
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IFeedbackService feedbackService, IServicesService servicesService)
        {
            _feedbackService = feedbackService;
            _servicesService = servicesService;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index(int serviceId)
        {
            var feedbacks = _feedbackService.GetAllFeedback().Where(f => f.ServiceId == serviceId);
            ViewData["ServiceId"] = serviceId;
            return View(feedbacks);
        }

        public IActionResult Details(int id)
        {
            var feedback = _feedbackService.GetFeedbackById(id);
            if (feedback == null)
            {
                return NotFound();
            }

            var service = _context.services.FirstOrDefault(s => s.Id == feedback.ServiceId);
            ViewBag.ServiceName = service?.Name;

            return View(feedback);
        }

        public IActionResult Create(int serviceId)
        {
            var service = _servicesService.GetServiceById(serviceId);
            if (service == null)
            {
                return NotFound();
            }

            var feedbackDto = new FeedbackDto
            {
                ServiceId = serviceId,
                UserId = _userManager.GetUserId(User)
            };

            return View(feedbackDto);
        }

        [HttpPost]
        public IActionResult Create(FeedbackDto feedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return View(feedbackDto);
            }

            var feedback = new Feedback
            {
                Content = feedbackDto.Content,
                Rating = feedbackDto.Rating,
                ServiceId = feedbackDto.ServiceId,
                UserId = feedbackDto.UserId
            };

            _feedbackService.AddFeedback(feedback);
            return RedirectToAction("Index", new { serviceId = feedback.ServiceId });
        }

        public IActionResult Edit(int id)
        {
            var feedback = _feedbackService.GetFeedbackById(id);
            if (feedback == null)
            {
                return NotFound();
            }

            var feedbackDto = new FeedbackDto
            {
                
                Content = feedback.Content,
                Rating = feedback.Rating,
                ServiceId = feedback.ServiceId,
                UserId = feedback.UserId
            };

            return View(feedbackDto);
        }

        [HttpPost]
        public IActionResult Edit(FeedbackDto feedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return View(feedbackDto);
            }

            var feedback = new Feedback
            {
                
                Content = feedbackDto.Content,
                Rating=feedbackDto.Rating,
                ServiceId = feedbackDto.ServiceId,
                UserId = feedbackDto.UserId
            };

            _feedbackService.EditFeedback(feedback);
            return RedirectToAction("Details", new { id = feedback.Id });
        }

        public IActionResult Delete(int id)
        {
            var feedback = _feedbackService.GetFeedbackById(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var feedback = _feedbackService.GetFeedbackById(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _feedbackService.DeleteFeedback(id);
            return RedirectToAction("Index", new { serviceId = feedback.ServiceId });
        }
    }
}
