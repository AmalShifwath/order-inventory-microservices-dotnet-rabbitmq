using Microsoft.EntityFrameworkCore;
using InventoryService.Models;
using InventoryService.Services;
using InventoryService.Messaing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlite(@"Data Source=Products.db"));

builder.Services.AddScoped<InventoryServices>();
builder.Services.AddSingleton<IRabbitMqUtil, RabbitMqUtil>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    // db.Database.EnsureCreated();
    db.Database.Migrate(); // applies all pending migrations

    app.UseSwagger();
    app.UseSwaggerUI();
    Console.WriteLine("Swagger should be available at: https://localhost:5000/swagger");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.Hosting;

// public class Program
// {
//     public static void Main(string[] args)
//     {
//         CreateHostBuilder(args).Build().Run();
//     }

//     public static IHostBuilder CreateHostBuilder(string[] args) =>
//         Host.CreateDefaultBuilder(args)
//             .ConfigureWebHostDefaults(webBuilder =>
//             {
//                 webBuilder.UseStartup<Startup>();
//             });
