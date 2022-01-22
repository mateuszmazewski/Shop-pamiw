using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.WebApp.Models;
using System.Threading.Tasks;

namespace Shop.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        // Logowanie POST
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) // Dodatkowa walidacja wprowadzanych danych (jeśli ktoś obejdzie walidację w widoku)
            {
                return View(loginVM);
            }

            // Zwraca użytkownika do zalogowania
            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if (user != null)
            {
                // Logowanie
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Niepoprawna nazwa użytkownika lub hasło");
            return View(loginVM);
        }

        public IActionResult Register()
        {
            return View(new LoginVM());
        }

        // Rejestracja POST
        // Niewymagająca podania ConfirmPassword
        [HttpPost]
        public async Task<IActionResult> Register(LoginVM loginVM)
        {
            if (ModelState.IsValid) // Wprowadzone dane przeszły walidację; ModelState - model predefiniowany
            {
                var user = new IdentityUser() { UserName = loginVM.UserName };
                var result = await _userManager.CreateAsync(user, loginVM.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(loginVM);
        }

        // Wylogowanie
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
