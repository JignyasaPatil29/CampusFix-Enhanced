using CampusFix.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusFix.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Complaint> Complaints => Set<Complaint>();
}
