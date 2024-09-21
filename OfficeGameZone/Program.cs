using Application;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GameZoneDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"),
     b =>b.MigrationsAssembly("Infrastructure")));

builder.Services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure()
    .AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
