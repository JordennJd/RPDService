using RPDSerice.RPDGenerator.Interfaces;
using RPDSerice.RPDGenerator.Implementation;
using RPDSerice;
using Microsoft.EntityFrameworkCore;
using Serilog;
using RPDSerice.Inital;
using RPDSerice.RpdRepository.Implementation;
var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot configuration = new ConfigurationManager();

var configurationBuilder = new ConfigurationBuilder();
IConfigurationRoot MyConfig;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
		policy  =>
		{
			policy.WithOrigins("https://localhost:7223" ,"http://185.192.246.20","http://localhost:3000", "https://saxscalc.ru")
				.AllowAnyHeader() 
				.AllowCredentials();
		});
});
if(Environment.GetEnvironmentVariable("IS_PROD") == "1")
{
	MyConfig = configurationBuilder.AddJsonFile("appsettings.Production.json").Build();
}
else
{
	MyConfig = configurationBuilder.AddJsonFile("appsettings.json").Build();
}
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddTransient<RpdRepository>();
builder.Services.AddTransient<IRPDGenerator, RPDGenerator>();
builder.Services.AddScoped<IRPDGenerator, RPDGenerator>();
builder.Services.AddSingleton<IRPDGenerator, RPDGenerator>();
builder.Services.AddDbContext<ApplicationDbContext>(
	opt => opt.UseSqlServer(MyConfig.GetValue<string>("ConnectionStrings:" + Helper.GetMachineName()), act=>
	{
		act.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
		
	}));

Log.Logger = new LoggerConfiguration()
	.WriteTo.File(@$"{MyConfig.GetValue<string>($"Path:Logging:{Helper.GetMachineName()}")}/logs.txt")
	.WriteTo.Console()
	.MinimumLevel.Debug()
	.CreateLogger();
	
builder.Host.UseSerilog();

Log.Information(@$"{MyConfig.GetValue<string>($"Path:Logging:{Helper.GetMachineName()}")}/logs.txt");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();


Initial.Init(app.Services.CreateScope().ServiceProvider,MyConfig);
app.Run();
