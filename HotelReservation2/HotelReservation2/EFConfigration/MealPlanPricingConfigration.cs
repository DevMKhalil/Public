using HotelReservation.Logic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.EFConfigration
{
    public class MealPlanPricingConfigration : IEntityTypeConfiguration<MealPlanPricing>
    {
        public void Configure(EntityTypeBuilder<MealPlanPricing> builder)
        {
            builder.ToTable("MealPlanPricing");
            builder.Property(x => x.MealPlanPricingId).UseIdentityColumn().IsRequired();
            builder.Property(x => x.Price).HasColumnType("Money");
            builder.HasOne(x => x.MealPlan).WithMany().HasForeignKey(x => x.MealPlanId);
            builder.HasOne(x => x.Season).WithMany().HasForeignKey(x => x.SeasonId);
        }
    }
}
