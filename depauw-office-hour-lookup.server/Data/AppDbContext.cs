using DePauwOfficeHourLookup.server.Models;
using Microsoft.EntityFrameworkCore;

namespace DePauwOfficeHourLookup.server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TestInfo> TestInfos { get; set; }
    }
}
