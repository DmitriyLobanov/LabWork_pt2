using Microsoft.AspNetCore.Mvc;
using LabWork_pt2.DTO.Request;
using LabWork_pt2.Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Build.Framework;

namespace LabWork_pt2.Controllers
{
  
    public class AccountController : Controller
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private DataBaseContext _dataBaseContext;
        public AccountController(DataBaseContext dataBaseContext, ILogger<AccountController> logger)
        {
            _dataBaseContext = dataBaseContext;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
        public IActionResult Start()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
         
            var user = await _dataBaseContext.Users.Include(m => m.Role).FirstOrDefaultAsync(x => x.UserName == model.UserName && x.Password == model.Password);
         
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username/password");
                _logger.LogError("Invalid username/password. Username: {username}", model.UserName);
                return View(model);
            }
            await Authenticate(user);
            if (user.Role.role == Enum.RoleEnum.ROLE_ADMIN)
            {
                return Redirect("/Admin/Index");
            }    
            return Redirect("/Service/Index");
        }

        
        public IActionResult Registration()
        {
            return View("Registration");
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _dataBaseContext.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);
            if (user == null)
            {
                user = new User(model.UserName, model.Password, new Role(Enum.RoleEnum.ROLE_USER));
                _dataBaseContext.Add(user);
                _dataBaseContext.SaveChanges();
                await Authenticate(user);
                _logger.LogInformation("User: {username} registered", user.UserName);
            }
            else
            {
                ModelState.AddModelError("", "User with this name already exists");
                return View("Registration");
            }

            return Redirect("/Service/Index");
        }

        [Authorize(Roles ="ROLE_USER, ROLE_ADMIN")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Start");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.role.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(
                claims,
                "Cookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
                );
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            _logger.LogInformation("User " + user.UserName + " authenticated and authorizated");
        }
    }
}
