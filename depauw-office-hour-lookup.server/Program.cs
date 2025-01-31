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
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
// using NextjsStaticHosting.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add DbContext Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// builder.Services.AddAuthorization();
  

// Step 1: Add Next.js hosting support
// builder.Services.Configure<NextjsStaticHostingOptions>(builder.Configuration.GetSection("NextjsStaticHosting"));
// builder.Services.AddNextjsStaticHosting();

var app = builder.Build();
// app.UseRouting();

// app.UseAuthorization();

// app.MapControllers();

app.UseHttpsRedirection();
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

app.MapPost("/api/signup", async (UserModelClass user, ApplicationDbContext context) => {

    if(await context.Users.Where(u => u.Username == user.Username).AnyAsync()) {
        return Results.BadRequest("Username already exists");
    }

    context.Users.Add(user);
    await context.SaveChangesAsync();

    return Results.Created("$/api/signup", "User created");

  // return user;
});

// var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

// lifetime.ApplicationStarted.Register(() => {
//   using (var scope = app.Services.CreateScope()) {

//     var services = scope.ServiceProvider;
//     var context = services.GetRequiredService<ApplicationDbContext>();

//     if(!context.OfficeHours.Any()) {
//       var officerHour = new OfficeHourModelClass {
//         Name = "Bruh"
//       };
//       context.OfficeHours.Add(officerHour);
//       context.SaveChanges();
//     }
//   }
// });


app.Run();