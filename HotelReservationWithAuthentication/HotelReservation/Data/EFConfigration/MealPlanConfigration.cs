using HotelReservationWithAuthentication.Logic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationWithAuthentication.Data.EFConfigration
{
    public class MealPlanConfigration : IEntityTypeConfiguration<MealPlan>
    {
        public void Configure(EntityTypeBuilder<MealPlan> builder)
        {
            builder.ToTable("MealPlan");
            builder.Property(x => x.MealPlanId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50);
        }
    }
}
