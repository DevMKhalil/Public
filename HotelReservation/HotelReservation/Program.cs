using HotelReservation.Application;
using HotelReservation.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ILoggerFactory ConsolLoggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddFilter(DbLoggerCategory.Database.Transaction.Name, LogLevel.Debug);
    builder.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
});

builder.Services.AddDbContext<HotelReservationContext>(options =>
{
    options
    .UseLoggerFactory(ConsolLoggerFactory)
    .EnableSensitiveDataLogging()
    .UseSqlServer(builder.Configuration.GetValue<string>("DefaultConnection"));

});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient<IHotelReservationContext,HotelReservationContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();