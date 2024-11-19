using FinalProject.Models;
using FinalProject.Dtos;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;
using FinalProject.ViewModel;

namespace FinalProject.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IAuthService _authService;

        public IdentityController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login([FromQuery] string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVeiwModel viewModel, string? returnUrl = null) 
        {
            if (ModelState.IsValid)
            {
                var authResult = await _authService.LoginAsync(new LoginModel
                {
                    Email = viewModel.Email,
                    Password = viewModel.Password
                }, HttpContext);

                if (authResult.IsAuthenticated)
                {
                    
                    HttpContext.Session.SetString("UserRole", authResult.Roles.FirstOrDefault());
                    HttpContext.Session.SetString("UserId", authResult.UserId);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        
                        return RedirectToAction("Index", "Categories");
                    }
                }

                
                ModelState.AddModelError("", authResult.Message);
            }

            ViewData["ReturnUrl"] = returnUrl; 
            return View(viewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVeiwModel registerView) 
        {
            if (ModelState.IsValid)
            {
                var authResult = await _authService.RegisterAsync(new RegisterModel
                {
                    UserName = registerView.UserName,
                    Email = registerView.Email,
                    Password = registerView.Password,
                    Role = registerView.Role,
                    Name = registerView.Name,
                    Phone = registerView.Phone,
                    Address = registerView.Address,
                    Job = registerView.Job
                });

                if (authResult.IsAuthenticated)
                {
                    
                    TempData["SuccessMessage"] = "Registration successful! Please log in.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("", authResult.Message);
            }

            return View(registerView);
        }
    }
}
