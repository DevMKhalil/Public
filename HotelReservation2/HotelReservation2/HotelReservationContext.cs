using CSharpFunctionalExtensions;
using HotelReservation.Logic;
using HotelReservation2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace HotelReservation
{
    public class HotelReservationContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public HotelReservationContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<RoomTypePricing> RoomTypePricings { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<MealPlanPricing> MealPlanPricings { get; set; }

        public async Task<Result> SaveChangesWithValidation(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (DbUpdateException dbExce)
            {
                return Result.Failure(dbExce.Message);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }

        }

        public static readonly ILoggerFactory ConsolLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddFilter(DbLoggerCategory.Database.Transaction.Name, LogLevel.Debug);
            builder.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
        });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(ConsolLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer(Configuration.GetValue<string>("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
