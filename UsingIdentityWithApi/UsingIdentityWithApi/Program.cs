using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UsingIdentityWithApi.Application;
using UsingIdentityWithApi.Data;
using UsingIdentityWithApi.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ILoggerFactory ConsolLoggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddFilter(DbLoggerCategory.Database.Transaction.Name, LogLevel.Debug);
    builder.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
});

builder.Services.AddDbContext<UsingIdentityWithApiContext>(options =>
{
    options
    .UseLoggerFactory(ConsolLoggerFactory)
    .EnableSensitiveDataLogging()
    .UseSqlServer(builder.Configuration.GetValue<string>("DefaultConnection"));

});
builder.Services.AddIdentityCore<ApiUser>(options => { });
builder.Services.AddScoped<IUserStore<ApiUser>, ApiUserStore>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient<IUsingIdentityWithApiContext, UsingIdentityWithApiContext>();

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

app.Run();
