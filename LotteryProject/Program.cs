using LotteryProject.EFCore.Services;
using Microsoft.EntityFrameworkCore;
using static LotteryProject.EFCore.EntityDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("LotteryProject.EFCore")));

builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IPresentService, PresentService>();
builder.Services.AddScoped<ILotteryService, LotteryService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "BlazorApp",
					  policy =>
					  {
						  policy.WithOrigins("https://localhost:7169")
						  .AllowAnyHeader()
						  .AllowAnyMethod();
					  });
});
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
app.UseCors("BlazorApp");

app.Run();
