using Microsoft.EntityFrameworkCore;
using UrbanHub.Data;
using UrbanHubManagement.repo;
using UrbanHub.shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UrbanHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UrbanHubDB")
        , x => x.UseNetTopologySuite()
    ));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Register dependencies
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserCard>();
builder.Services.AddScoped<AdminUserManagement>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
