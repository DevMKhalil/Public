using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UsingIdentityWithApi.Application;
using UsingIdentityWithApi.Data;
using UsingIdentityWithApi.Logic;
using UsingIdentityWithApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Context Services
builder.Services.AddContext(builder.Configuration.GetValue<string>("DefaultConnection"));

//builder.Services.AddAuthentication("cookies").AddCookie("cookies", options => options.LoginPath = "/api/Users/Login");

// Add Identity Services
builder.Services.AddIdentityForIdentityUser();
builder.Services.AddIdentityForAspNetIdentityUser();

// Add Mesc
builder.Services.AddMisc();

// Add Jwt Services
builder.Services.AddJwtBearerAuthentication(builder.Configuration["Jwt:Issuer"], builder.Configuration["Jwt:Audience"], builder.Configuration["Jwt:Key"]);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
