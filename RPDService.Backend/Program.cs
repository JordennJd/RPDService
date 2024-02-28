using RPDSerice.RPDGenerator.Interfaces;
using RPDSerice.RPDGenerator.Implementation;
using RPDSerice;
using Microsoft.EntityFrameworkCore;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot configuration = new ConfigurationManager();
var MyConfig = new ConfigurationBuilder()
.AddJsonFile("appsettings.json").Build();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddTransient<IRPDGenerator, RPDGenerator>();
builder.Services.AddScoped<IRPDGenerator, RPDGenerator>();
builder.Services.AddSingleton<IRPDGenerator, RPDGenerator>();
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("database"));

Log.Logger = new LoggerConfiguration()
	.WriteTo.File(@$"{MyConfig.GetValue<string>($"Path:Logging:{Environment.MachineName}" )}/logs.txt")
	.WriteTo.Console()
	.MinimumLevel.Debug()
	.CreateLogger();
	
builder.Host.UseSerilog();

Log.Information(@$"{MyConfig.GetValue<string>($"Path:Logging:{Environment.MachineName}")}/logs.txt");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();



app.Run();
