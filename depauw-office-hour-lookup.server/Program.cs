using depauw_officer_hour_lookup.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using depauw_officer_hour_lookup.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Replace placeholders in the connection string with actual environment variables

// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")

//     .Replace("{USERNAME}", username)

//     .Replace("{PASSWORD}", password);

// Console.Out.WriteLine(connectionString);



// builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
// builder.Services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseNpgsql(builder.Configuration["postgres:ConnectionString"]));
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// For Controller-based
app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();