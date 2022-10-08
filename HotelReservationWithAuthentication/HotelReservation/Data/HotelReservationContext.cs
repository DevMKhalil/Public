using CSharpFunctionalExtensions;
using HotelReservationWithAuthentication.Application;
using HotelReservationWithAuthentication.Logic;
using HotelReservationWithAuthentication.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HotelReservationWithAuthentication.Data
{
    public class HotelReservationContext : IdentityDbContext<ApplicationUser>, IHotelReservationContext
    {
        public HotelReservationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<RoomType> RoomTypes { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
