using Microsoft.EntityFrameworkCore;
using OrderService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlite(@"Data Source=Customers.db"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
    db.Database.EnsureCreated();

    app.UseSwagger();
    app.UseSwaggerUI();
    Console.WriteLine("Swagger should be available at: http://localhost:5000/swagger");
}

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
