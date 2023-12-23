using Login.Models;
using Login.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    public class AccountController(SignInManager<AppUser> inManager, UserManager<AppUser> userManager) : Controller
    {

        

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Reg()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reg(RegistrarVm registrar)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    UserName = registrar.Email,
                    Name = registrar.Name,
                    Email = registrar.Email,
                    Address=registrar.Address,
                    
                };
                var result = await userManager.CreateAsync(appUser, registrar.Password!);
                if (result.Succeeded)
                {
                    await inManager.SignInAsync(appUser, false);
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await inManager.PasswordSignInAsync(loginVm.UserName!, loginVm.Password!, loginVm.RememberMe,
                        false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginVm);
            }
            return View(loginVm);
        }
    }
}
