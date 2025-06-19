using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using OrderService.Services;
using OrderService.Messaing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlite(@"Data Source=Customers.db"));

builder.Services.AddScoped<OrderServices>();
builder.Services.AddSingleton<IRabbitMqUtil, RabbitMqUtil>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var rabbitUtil = scope.ServiceProvider.GetRequiredService<IRabbitMqUtil>();
    _ = Task.Run(() => rabbitUtil.consumeMessageQueue("inventory.product"));
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
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
