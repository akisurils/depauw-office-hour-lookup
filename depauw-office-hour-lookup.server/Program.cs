global using depauw_officer_hour_lookup.Data; //folder containing ApplicationDbContext class
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using dotenv.net;
using depauw_officer_hour_lookup.Models;
using Microsoft.AspNetCore.Identity;
using System;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] {"./.env"}));
var dotenv = DotEnv.Read();

var configuration = builder.Configuration;

var username = dotenv["DB_USER"];

var password = dotenv["DB_PASSWORD"];


// Replace placeholders in the connection string with actual environment variables

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")

    .Replace("{USERNAME}", username)

    .Replace("{PASSWORD}", password);

// Console.Out.WriteLine(connectionString);



// builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddIdentity<Users, IdentityRole>(options => {
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // var context = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
    // context.Database.CanConnect();
    // Console.WriteLine("Connection successful!");
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
    dbContext.Database.Migrate();
}  


app.MapGet("/api/testdata", async (ApplicationIdentityDbContext context) =>
{
    return await context.TestInfos.ToListAsync();
});

// For Controller-based
app.UseRouting();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();