using System.ComponentModel.DataAnnotations;

namespace CampusFix.Models;

public class Complaint
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Enter a complaint title.")]
    [StringLength(100)]
    [Display(Name = "Complaint title")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Describe the problem.")]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter a building or room.")]
    [StringLength(100)]
    public string Location { get; set; } = string.Empty;

    [Required]
    [StringLength(40)]
    public string Category { get; set; } = "Electrical";

    [Required]
    [StringLength(20)]
    public string Priority { get; set; } = "Medium";

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Pending";

    [StringLength(100)]
    [Display(Name = "Reported by")]
    public string? ReportedBy { get; set; }

    [StringLength(1000)]
    [Display(Name = "Resolution notes")]
    public string? ResolutionNotes { get; set; }

    [Display(Name = "Created at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
