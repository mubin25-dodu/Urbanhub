using Microsoft.EntityFrameworkCore;
using UrbanHub.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext
<UrbanHubDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration
        .GetConnectionString(
            "UrbanHubDB"),
        x => x.UseNetTopologySuite()
    ));

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();