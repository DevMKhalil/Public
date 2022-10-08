using CSharpFunctionalExtensions;
using HotelReservation.Logic;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Application
{
    public interface IHotelReservationContext
    {
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RoomTypePricing> RoomTypePricings { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<MealPlanPricing> MealPlanPricings { get; set; }
        Task<Result> SaveChangesWithValidation(CancellationToken cancellationToken = default(CancellationToken));
    }
}
