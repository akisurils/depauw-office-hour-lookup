using depauw_officer_hour_lookup.Model;
using Microsoft.EntityFrameworkCore;

namespace depauw_officer_hour_lookup.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
     {
     }
    public DbSet <OfficeHourModelClass> YourModel{ get; set; }
  }
}
