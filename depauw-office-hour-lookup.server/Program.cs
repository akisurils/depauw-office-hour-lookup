using DePauwOfficeHourLookup.server.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database context
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration["postgres:ConnectionString"]));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseRouting();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Use the Migrate method during application startup to ensure the database is created 
// and any pending migrations are applied.
//using (var scope = app.Services.CreateScope()) // Create a scope specifically for migration
//{
//    var services = scope.ServiceProvider;
//    var context = services.GetRequiredService<AppDbContext>();

//    try
//    {
//        context.Database.Migrate(); // Apply any pending migrations
//    }
//    catch (Exception ex)
//    {
//        // Log the migration error.  Don't let it crash the application startup.
//        // Use a proper logging mechanism like ILogger or Serilog here instead of Console.WriteLine.
//        Console.WriteLine($"Error applying migrations: {ex.Message}");
//    }
//}

//app.MapPost("/api/signup", async (UserModelClass user, ApplicationDbContext context) =>
//{

//    if (await context.Users.Where(u => u.Username == user.Username).AnyAsync())
//    {
//        return Results.BadRequest("Username already exists");
//    }

//    context.Users.Add(user);
//    await context.SaveChangesAsync();

//    return Results.Created("$/api/signup", "User created");

//    // return user;
//});

//app.MapGet("/api/testdata", async (AppDbContext context) =>
//{
//    return await context.TestInfos.ToListAsync();
//});

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