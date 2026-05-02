using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SecureNotes.Models;
using SecureNotes.ViewModels;

namespace SecureNotes.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // Perfil do utilizador
        public IActionResult Index()
        {
            ViewBag.Name = User.Identity?.Name;
            ViewBag.Email = User.FindFirstValue(ClaimTypes.Email);

            return View();
        }

        [HttpGet]
        public IActionResult Security()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Index");

            var result = await _userManager.ChangePasswordAsync(
                user,
                model.CurrentPassword,
                model.NewPassword
            );

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            await _userManager.UpdateSecurityStampAsync(user);

            ViewBag.Success = "Palavra-passe alterada com sucesso!";
            return View();
        }
    }
}