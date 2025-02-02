using depauw_officer_hour_lookup.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace depauw_officer_hour_lookup.Data
{
  public class ApplicationDbContext : DbContext
  {
        // public ApplicationDbContext()
        // {
        // }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {
    }
    public DbSet<OfficeHourModel> OfficeHours{ get; set; }
    // public DbSet<UserModelClass> Users{ get; set; }
  }
  public class ApplicationIdentityDbContext : IdentityDbContext<Users> {
    public ApplicationIdentityDbContext(DbContextOptions options) : base(options) {

    }
  }
}
