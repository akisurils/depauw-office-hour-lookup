using depauw_officer_hour_lookup.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using depauw_officer_hour_lookup.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.IO;
using System;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

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

// Testing scraping logic
using (var scope = app.Services.CreateScope())
{
    var data = File.ReadAllText("./example.asp");

    // var web = new HtmlWeb();
    var doc = new HtmlDocument();
    doc.LoadHtml(data);
    var HtmlTables = doc.DocumentNode.QuerySelectorAll("table");
    // Console.WriteLine(HtmlTables.Count);
    // int count = 0;
    var courses = new List<CourseModel>();
    var HtmlTRs = HtmlTables[2].QuerySelectorAll("tr");
    int countt = 0;
    foreach (var HtmlTR in HtmlTRs) {
        var HtmlTDs = HtmlTR.QuerySelectorAll("td");
        // Console.WriteLine(HtmlTDs.Count);
        if(HtmlTDs.Count > 1) {
            countt++;
            if(countt == 1) {
                continue;
            }
            
            var soc = HtmlEntity.DeEntitize(HtmlTDs[0].QuerySelector("font").InnerText);
            // Console.WriteLine(soc);
            var course = HtmlEntity.DeEntitize(HtmlTDs[1].QuerySelector("font").InnerText);
            // Console.WriteLine(course);
            var description = HtmlEntity.DeEntitize(HtmlTDs[2].QuerySelector("font").InnerText);
            var credit = HtmlEntity.DeEntitize(HtmlTDs[3].QuerySelector("font").InnerText);
            var method = HtmlEntity.DeEntitize(HtmlTDs[4].QuerySelector("font").InnerText);
            var time = HtmlEntity.DeEntitize(HtmlTDs[6].QuerySelector("font").InnerText);
            // time = HtmlEntity.DeEntitize(time);
            // time = time.Replace("&nbsp;", "");
            time = Regex.Replace(time, @"\n+", "");
            // Console.WriteLine(time);
            var area = HtmlEntity.DeEntitize(HtmlTDs[7].QuerySelector("font").InnerText);
            // Console.WriteLine(area);
            var cmp = HtmlEntity.DeEntitize(HtmlTDs[8].QuerySelector("font").InnerText);
            var ip = HtmlEntity.DeEntitize(HtmlTDs[9].QuerySelector("font").InnerText);
            var passfail = HtmlEntity.DeEntitize(HtmlTDs[10].QuerySelector("font").InnerText);
            var instructorElement = HtmlTDs[12].QuerySelector("font");
            var instructor = string.Join("",instructorElement.ChildNodes.
            Where(node => node.NodeType == HtmlNodeType.Text).
            Select(node => node.InnerText));
            var room = HtmlEntity.DeEntitize(HtmlTDs[12].QuerySelector("font font").InnerText);
            // Console.WriteLine(room);
            // instructor.Remove(instructor.Length - room.Length, room.Length);
            // Console.WriteLine(instructor)
            var notes = HtmlTDs.Count == 13 ? "" : HtmlEntity.DeEntitize(HtmlTDs[13].QuerySelector("font").InnerText);
            // notes = Regex.Replace(notes, @"\n+", " ");
            var newcourse = new CourseModel() {SOC = soc, Course = course, Description = description, Credit = credit, Method = method, Time = time, Area = area, Cmp = cmp, IP = ip, PassFail = passfail != "N", Instructor = instructor, Room = room, Notes = notes};
            courses.Add(newcourse);
        }
    }
    foreach(var course in courses) {
        Console.Write("SOC: " + course.SOC + "\n");
        Console.Write("Course: " + course.Course + "\n");
        Console.Write("Description: " + course.Description + "\n");
        Console.Write("Credit: " + course.Credit + "\n");
        Console.Write("Method: " + course.Method + "\n");
        Console.Write("Time: " + course.Time + "\n");
        Console.Write("Area: " + course.Area + "\n");
        Console.Write("Cmp: " + course.Cmp + "\n");
        Console.Write("IP: " + course.IP + "\n");
        Console.Write("PassFail: " + (course.PassFail ? "True" : "False") + "\n");
        Console.Write("Instructor: " + course.Instructor + "\n");
        Console.Write("Room: " + course.Room + "\n");
        Console.Write("Notes: " + course.Notes + "\n");
        Console.WriteLine("--------------------------------------------------------------------------------");
    }

    // Console.WriteLine(courses.ToString());
    // Console.WriteLine(doc.ToString());
}  
// For Controller-based
app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();