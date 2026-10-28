using CampusFix.Data;
using CampusFix.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusFix.Controllers;

public class ComplaintsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ComplaintsController(ApplicationDbContext context)
    {
        _context = context;
    }

    private void LoadChoices()
    {
        ViewBag.Categories = new[] { "Electrical", "Plumbing", "Furniture", "Internet", "Classroom Equipment", "Other" };
        ViewBag.Priorities = new[] { "Low", "Medium", "High", "Urgent" };
        ViewBag.Statuses = new[] { "Pending", "In Progress", "Resolved" };
    }

    public async Task<IActionResult> Index(string? search, string? status, string? category)
    {
        var query = _context.Complaints.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c => c.Title.Contains(search) || c.Location.Contains(search));

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(c => c.Status == status);

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(c => c.Category == category);

        ViewBag.Search = search;
        ViewBag.SelectedStatus = status;
        ViewBag.SelectedCategory = category;
        ViewBag.Categories = new[] { "Electrical", "Plumbing", "Furniture", "Internet", "Classroom Equipment", "Other" };
        ViewBag.LastComplaintId = HttpContext.Session.GetInt32("LastComplaintId");

        return View(await query.OrderByDescending(c => c.CreatedAt).ToListAsync());
    }

    public IActionResult Create()
    {
        LoadChoices();
        return View(new Complaint());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Complaint complaint)
    {
        if (!ModelState.IsValid)
        {
            LoadChoices();
            return View(complaint);
        }

        complaint.CreatedAt = DateTime.Now;
        _context.Complaints.Add(complaint);
        await _context.SaveChangesAsync();

        HttpContext.Session.SetInt32("LastComplaintId", complaint.Id);
        TempData["SuccessMessage"] = "Complaint submitted successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var complaint = await _context.Complaints.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        if (complaint == null) return NotFound();
        return View(complaint);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var complaint = await _context.Complaints.FindAsync(id);
        if (complaint == null) return NotFound();
        LoadChoices();
        return View(complaint);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Complaint complaint)
    {
        if (id != complaint.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            LoadChoices();
            return View(complaint);
        }

        var existing = await _context.Complaints.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Title = complaint.Title;
        existing.Description = complaint.Description;
        existing.Location = complaint.Location;
        existing.Category = complaint.Category;
        existing.Priority = complaint.Priority;
        existing.Status = complaint.Status;
        existing.ReportedBy = complaint.ReportedBy;
        existing.ResolutionNotes = complaint.ResolutionNotes;

        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Complaint updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var complaint = await _context.Complaints.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        if (complaint == null) return NotFound();
        return View(complaint);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var complaint = await _context.Complaints.FindAsync(id);
        if (complaint != null)
        {
            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync();
        }

        TempData["SuccessMessage"] = "Complaint deleted.";
        return RedirectToAction(nameof(Index));
    }
}
