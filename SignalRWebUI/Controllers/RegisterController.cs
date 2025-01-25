using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR.EntityLayer.Entities;
using SignalRWebUI.DTOs.IdentityDTOs;

namespace SignalRWebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(RegisterDto registerDto)
        {
            var appUser = new AppUser
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Email=registerDto.Mail,
                UserName=registerDto.Username,
            };
            IdentityResult result = await _userManager.CreateAsync(appUser,registerDto.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("Index","Login");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            
            return View();
        }
    }
}
