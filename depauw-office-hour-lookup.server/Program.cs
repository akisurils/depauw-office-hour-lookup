global using depauw_officer_hour_lookup.Data; //folder containing ApplicationDbContext class
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
// using NextjsStaticHosting.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add DbContext Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));


// Step 1: Add Next.js hosting support
// builder.Services.Configure<NextjsStaticHostingOptions>(builder.Configuration.GetSection("NextjsStaticHosting"));
// builder.Services.AddNextjsStaticHosting();

var app = builder.Build();
app.UseRouting();

// Step 2: Register dynamic endpoints to serve the correct HTML files at the right request paths.
// app.MapNextjsStaticHtmls();

// Step 3: Serve other required files (e.g. js, css files in the exported next.js app).
// app.UseNextjsStaticHosting();

app.Run();