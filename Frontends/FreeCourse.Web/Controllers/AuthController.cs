using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService ıdentityService)
        {
            _identityService = ıdentityService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SigninInput signinInput)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _identityService.SignIn(signinInput);

            if (!response.IsSuccessful)
            {
                response.Errors.ForEach(error =>
                {
                    ModelState.AddModelError(String.Empty, error);
                });


                return View();
            }

            return RedirectToAction(nameof(Index), "Home");


        }


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            await _identityService.RevokeRefreshToken();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
