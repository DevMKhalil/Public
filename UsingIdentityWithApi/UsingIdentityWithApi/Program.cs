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

//builder.Services.AddDefaultIdentity<ApiUser>(options => { })
//        .AddEntityFrameworkStores<UsingIdentityWithApiContext>();

//builder.Services.AddAuthentication(o =>
//{
//    o.DefaultScheme = IdentityConstants.ApplicationScheme;
//    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
//})
//.AddIdentityCookies(o => { });

//builder.Services.AddIdentityCore<ApiUser>(o =>
//{
//    o.Stores.MaxLengthForKeys = 128;
//    o.SignIn.RequireConfirmedAccount = true;
//})
//.AddDefaultTokenProviders();
builder.Services.AddAuthentication("cookies").AddCookie("cookies", options => options.LoginPath = "/api/Users/Login");

builder.Services.AddIdentityCore<ApiUser>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<UsingIdentityWithApiContext>()
    .AddDefaultTokenProviders();


//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
//{
//})
//            .AddRoles<IdentityRole>()
//            .AddEntityFrameworkStores<UsingIdentityWithApiContext>()
//            .AddDefaultTokenProviders()
//            .AddSignInManager();


builder.Services.AddScoped<IUserStore<ApiUser>, ApiUserStore>();
builder.Services.AddScoped<ApiUserManager>();
//builder.Services.AddScoped<IUserStore<ApiUser>,UserOnlyStore<ApiUser, UsingIdentityWithApiContext>>();
//builder.Services.AddScoped<IUserStore<ApiUser>, CustomIdentityUserStore>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient<IUsingIdentityWithApiContext, UsingIdentityWithApiContext>();
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
