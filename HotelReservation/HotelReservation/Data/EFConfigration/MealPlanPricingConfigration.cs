using HotelReservation.Logic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Data.EFConfigration
{
    public class MealPlanPricingConfigration : IEntityTypeConfiguration<MealPlanPricing>
    {
        public void Configure(EntityTypeBuilder<MealPlanPricing> builder)
        {
            builder.ToTable("MealPlanPricing");
            builder.Property(x => x.MealPlanPricingId).UseIdentityColumn().IsRequired();
            builder.Property(x => x.Price).HasColumnType("Money");
        }
    }
}
