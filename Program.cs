using RPDSerice.RPDGenerator.Interfaces;
using RPDSerice.RPDGenerator.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddTransient<IRPDGenerator, RPDGenerator>();
builder.Services.AddScoped<IRPDGenerator, RPDGenerator>();
builder.Services.AddSingleton<IRPDGenerator, RPDGenerator>();

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