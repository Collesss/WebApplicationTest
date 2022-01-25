using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAppDbContext;
using WebApplicationTest.ViewsModels.Account;
using WebAppDbModels.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace WebApplicationTest.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly WebAppDataDbContext _context;
        private readonly IMapper _mapper;

        public AccountController(WebAppDataDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if(ModelState.IsValid)
            {
                loginModel.Password = Encoding.Unicode.GetString(SHA512.Create().ComputeHash(Encoding.Unicode.GetBytes(loginModel.Password))); 
                Debug.WriteLine($"{loginModel.Name}: {loginModel.Password}");

                User user = await _context.Users.FirstOrDefaultAsync(u => u.Name == loginModel.Name && u.HashPassword == loginModel.Password);

                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Неправильный логин и(или) пароль.");
            }

            return View(loginModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                registerModel.Password = Encoding.Unicode.GetString(SHA512.Create().ComputeHash(Encoding.Unicode.GetBytes(registerModel.Password))); 
                Debug.WriteLine($"{registerModel.Name}: {registerModel.Password}");

                if(!_context.Users.Any(u => u.Name == registerModel.Name))
                {
                    User user = _mapper.Map<User>(registerModel);
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Пользователь с таким логином уже существует.");
            }

            return View(registerModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {   
            return View();
        }


        private async Task Authenticate(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
