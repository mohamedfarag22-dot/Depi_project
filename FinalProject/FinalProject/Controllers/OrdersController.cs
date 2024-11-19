using FinalProject.Dtos;
using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FinalProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly IServicesService _servicesService;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context , UserManager<ApplicationUser> userManager, IOrderService orderService, IServicesService servicesService, IUserService userService)
        {
            _orderService = orderService;
            _servicesService = servicesService;
            _userManager = userManager;
            _userService = userService;
            _context = context;

        }


        public IActionResult Index(int categoryId)
        {
            var userId = HttpContext.Session.GetString("UserId"); // Get the logged-in user's ID
            var userRole = HttpContext.Session.GetString("UserRole"); // Get the user's role

            IEnumerable<Order> orders;

            if (userRole == "ServiceProvider")
            {
                // If the user is a Service Provider, get all orders for the specified category
                orders = _orderService.GetAllOrders()
             .Where(o => o.Service.CategoryId == categoryId && o.Service.ServiceProviderId == userId);

            }
            else
            {
               // If the user is a regular user, filter orders by the user's ID
        
                orders = _orderService.GetAllOrders().Where(o => o.UserId == userId);
            }

            ViewData["CategoryId"] = categoryId;
            return View(orders.ToList()); // Convert to List and pass to the view
        }








        public IActionResult Details(int id)
        {
            var order = _context.orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

           
            var service = _context.services.FirstOrDefault(s => s.Id == order.ServiceId);

            ViewBag.CategoryId = order.Service.CategoryId;
            ViewBag.ServiceName = service?.Name; 

            return View(order);
        }


        public IActionResult Create(int serviceId, int catagoryId)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var service = _servicesService.GetServiceById(serviceId);

            if (service == null)
            {
                return NotFound();
            }

            var userAddress = _userService.GetUserAddressById(userId);

            var orderDto = new OrderDto
            {
                ServiceId = serviceId,
                UserId = userId,
                CreatedDate = DateTime.Now,
                CategoryId = catagoryId
            };

            return View(orderDto);
        }


        [HttpPost]
        public IActionResult Create(OrderDto orderDto)
        {
            if (orderDto.AvailableTo <= orderDto.AvailableFrom)
            {
                ModelState.AddModelError("AvailableTo", "The 'Available To' date must be later than the 'Available From' date.");
            }

            if (!ModelState.IsValid)
            {
                return View(orderDto);
            }

            var order = new Order
            {
                ServiceId = orderDto.ServiceId,
                UserId = orderDto.UserId,
                CreatedDate = DateTime.Now,
                AvailableFrom = orderDto.AvailableFrom,
                AvailableTo = orderDto.AvailableTo,
                Address = orderDto.Address
            };

            _orderService.AddOrder(order);

            
            return RedirectToAction("Index", "Orders", new { categoryId = orderDto.CategoryId });
        }



        public IActionResult Edit(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderDto = new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                ServiceId = order.ServiceId,
                CreatedDate = order.CreatedDate,
                Address = order.Address,
                AvailableFrom = order.AvailableFrom,
                AvailableTo = order.AvailableTo,
            };

            return View(orderDto);
        }

        [HttpPost]
        public IActionResult Edit(OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return View(orderDto);
            }

            var order = new Order
            {
                Id = orderDto.Id,
                UserId = orderDto.UserId,
                ServiceId = orderDto.ServiceId,
                CreatedDate = orderDto.CreatedDate,
                Address = orderDto.Address,
                AvailableFrom = orderDto.AvailableFrom,
                AvailableTo = orderDto.AvailableTo,
            };

            _orderService.EditOrder(order);
            return RedirectToAction("Details", "Orders", new { id = order.Id });
        }


        public IActionResult Delete(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order); 
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            _orderService.DeleteOrder(id);
            return RedirectToAction(nameof(Index)); 
        }

    }
}
