using HotelReservation.Logic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Data.EFConfigration
{
    public class RoomTypePricingConfigration : IEntityTypeConfiguration<RoomTypePricing>
    {
        public void Configure(EntityTypeBuilder<RoomTypePricing> builder)
        {
            builder.ToTable("RoomTypePricing");
            builder.Property(x => x.RoomTypePricingId).UseIdentityColumn().IsRequired();
            builder.Property(x => x.Price).HasColumnType("Money");
        }
    }
}
