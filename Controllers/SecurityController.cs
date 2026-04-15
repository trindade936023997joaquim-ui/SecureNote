using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SecureNotes.Models;
using System.Threading.Tasks;

public class SecurityController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SecurityController(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public IActionResult Lock()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Unlock(string password)
    {
        var user = await _signInManager.UserManager.GetUserAsync(User);

        if (user == null)
            return RedirectToPage("/Account/Login", new { area = "Identity" });

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (result.Succeeded)
            return RedirectToAction("Index", "Notes");

        ViewBag.Error = "Senha incorreta";
        return View("Lock");
    }
}
