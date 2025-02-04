using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using depauw_officer_hour_lookup.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UsersApp.Controllers
{
    [Route("/api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;

        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        // public IActionResult Login()
        // {
        //     return View();
        // }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return Ok();
                    // return RedirectToAction("Index", "Home");
                }
                else
                {
                    return BadRequest("Email or password is incorrect.");
                    // ModelState.AddModelError("", "Email or password is incorrect.");
                    // return View(model);
                }
            }
            return BadRequest();
            // return View(model);
        }

        // public IActionResult Register()
        // {
        //     return View();
        // }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users users = new Users
                {
                    FullName = model.Name,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await userManager.CreateAsync(users, model.Password);

                if (result.Succeeded)
                {
                    return Ok();
                    // return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return BadRequest();
                    // return View(model);
                }
            }
            return BadRequest();
            // return View(model);
        }

        // public IActionResult VerifyEmail()
        // {
        //     return View();
        // }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);

                if(user == null)
                {
                    ModelState.AddModelError("", "Something is wrong!");
                    return BadRequest("Something is wrong!");
                    // return View(model);
                }
                else
                {
                    return CreatedAtAction("ChangePassword", new {username = user.UserName});
                    // return RedirectToAction("ChangePassword","Account", new {username = user.UserName});
                }
            }
            return BadRequest();
            // return View(model);
        }

        // public IActionResult ChangePassword(string username)
        // {
        //     if (string.IsNullOrEmpty(username))
        //     {
        //         return RedirectToAction("VerifyEmail", "Account");
        //     }
        //     return CreatedAtAction("ChangePassword", new ChangePasswordViewModel { Email = username });
        //     // return View(})
        //     // return View(new ChangePasswordViewModel { Email = username });
        // }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if(user != null)
                {
                    var result = await userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddPasswordAsync(user, model.NewPassword);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return BadRequest();

                        // return View(model);
                    }
                }
                else
                {
                    return BadRequest("Email not found!");
                    // ModelState.AddModelError("", "Email not found!");
                    // return View(model);
                }
            }
            else
            {
                return BadRequest("Something went wrong. try again.");
                // ModelState.AddModelError("", "Something went wrong. try again.");
                // return View(model);
            }
        }
        
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
            // return RedirectToAction("Index", "Home");
        }
    }
}