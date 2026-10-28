using CampusFix.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusFix.Controllers;

public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Total = await _context.Complaints.CountAsync();
        ViewBag.Pending = await _context.Complaints.CountAsync(c => c.Status == "Pending");
        ViewBag.InProgress = await _context.Complaints.CountAsync(c => c.Status == "In Progress");
        ViewBag.Resolved = await _context.Complaints.CountAsync(c => c.Status == "Resolved");
        return View();
    }
}
