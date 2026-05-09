using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using UrbanHub.Data;
using UrbanHub.DTO;
using UrbanHub.Entities;
using UrbanHub.shared;
using UrbanHubManagement.repo;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(mapper));

builder.Services.AddDbContext<UrbanHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UrbanHubDB")
    , x => x.UseNetTopologySuite()
    ));

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
    });
builder.Services.AddSession();
builder.Services.AddScoped<Auth>();
builder.Services.AddScoped<ParkinHome>();
builder.Services.AddScoped<LoginDTO>();
builder.Services.AddScoped<ParkinViewDetails>();
builder.Services.AddAuthentication("UrbanAuth").AddCookie("UrbanAuth",
    opt =>
    {
        opt.AccessDeniedPath = "/Denied";
        opt.LoginPath = "/Login";
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(300);
    });
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

