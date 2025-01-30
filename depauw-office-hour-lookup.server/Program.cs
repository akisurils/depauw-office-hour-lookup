global using depauw_officer_hour_lookup.Data; //folder containing ApplicationDbContext class
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;
using depauw_officer_hour_lookup.Model;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System;
// using NextjsStaticHosting.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add DbContext Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
  

// Step 1: Add Next.js hosting support
// builder.Services.Configure<NextjsStaticHostingOptions>(builder.Configuration.GetSection("NextjsStaticHosting"));
// builder.Services.AddNextjsStaticHosting();

var app = builder.Build();
app.UseRouting();

// Step 2: Register dynamic endpoints to serve the correct HTML files at the right request paths.
// app.MapNextjsStaticHtmls();

// Step 3: Serve other required files (e.g. js, css files in the exported next.js app).
// app.UseNextjsStaticHosting();


// Use the Migrate method during application startup to ensure the database is created 
// and any pending migrations are applied.
using (var scope = app.Services.CreateScope()) // Create a scope specifically for migration
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    try
    {
        context.Database.Migrate(); // Apply any pending migrations
    }
    catch (Exception ex) 
    {
        // Log the migration error.  Don't let it crash the application startup.
        // Use a proper logging mechanism like ILogger or Serilog here instead of Console.WriteLine.
        Console.WriteLine($"Error applying migrations: {ex.Message}"); 
    }
}

var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

lifetime.ApplicationStarted.Register(() => {
  using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    // var lifetime = services.GetRequiredService<IHostApplicationLifetime>();

    // lifetime.ApplicationStarted.Register(() => {
      if(!context.OfficeHours.Any()) {
        var officerHour = new OfficeHourModelClass {
          Name = "Bruh"
        };
        context.OfficeHours.Add(officerHour);
        context.SaveChanges();
      }
    // });
  }
});
// using (var context = new ApplicationDbContext()){
//   var officerHour = new OfficeHourModelClass {
//     Name = "Bruh"
//   };

//   context.YourModel.Add(officerHour);
//   context.SaveChanges();
// }

app.Run();