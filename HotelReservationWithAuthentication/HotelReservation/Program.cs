using HotelReservationWithAuthentication.Application;
using HotelReservationWithAuthentication.Logic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Reflection;
using HotelReservationWithAuthentication.Data;

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

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<HotelReservationContext>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();