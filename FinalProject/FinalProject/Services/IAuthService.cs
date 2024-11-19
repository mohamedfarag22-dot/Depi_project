using FinalProject.Dtos;

namespace FinalProject.Services
{
    public interface IAuthService
    {
        Task<AuthModel>RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model, HttpContext httpContext);
        //Task<string> AddRoleAsync(AddRoleModel model);
    }
}
