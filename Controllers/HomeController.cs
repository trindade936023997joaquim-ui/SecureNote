using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SecureNotes.Models;
using SecureNotes.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SecureNotes.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        int totalNotes = 0;

        if (userId != null)
        {
            totalNotes = await _context.Notes
                .Where(n => n.UserId == userId)
                .CountAsync();
        }

        ViewBag.TotalNotes = totalNotes;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}