using FinalProject.Models;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string GetUserAddressById(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            return user?.Address;
        }
    }

}
