using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Qate3Dashboard.ViewModels;

namespace Qate3Dashboard.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager , SignInManager<IdentityUser> signInManager)
        {
           _userManager = userManager;
            _signInManager = signInManager;
        }

        //Register 
        [Authorize]
        public  IActionResult Register()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user =await _userManager.FindByEmailAsync(model.Email);

                if (user is null)
                {
                user = new IdentityUser()
                {
                    UserName = model.Email.Split('@')[0] ,
                    Email = model.Email
                    
                };

                    var result = await _userManager.CreateAsync(user, model.Password);

                   if (result.Succeeded)  RedirectToAction("Login" );
                    else
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "this Account is exist");
                }
               
                
            }
            return View(model);
           
        }



        //login 

        public IActionResult Login()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Departmenr");
            }

            return View();

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if(ModelState.IsValid) 
            {

                var User = await _userManager.FindByEmailAsync(model.Email);

                if (User is not null)
                {
                  var result = await _userManager.CheckPasswordAsync(User, model.Password);

                    if (result)
                    {
                     var LoginResult =   await _signInManager.PasswordSignInAsync(User, model.Password, true, false);

                        if (LoginResult.Succeeded)
                            return RedirectToAction("index", "Department");
                        
                    
                    }
                    else
                    {
                         ModelState.AddModelError(string.Empty, "Password is not Correct");

                    }
                    

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is not Exist");
                }

            }
            return View(model);

        }




        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        
     

    }
}
