using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PierresTreats.Models
{
  public class PierresTreatsContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Engineer> Treats { get; set; }
    public DbSet<Machine> Flavors { get; set; }
    public DbSet<EngineerMachine> TreatFlavor { get; set; }

    public PierresTreatsContext(DbContextOptions options) : base(options) { }
  }
}