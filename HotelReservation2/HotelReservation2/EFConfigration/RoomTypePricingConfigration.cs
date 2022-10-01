using HotelReservation.Logic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.EFConfigration
{
    public class RoomTypePricingConfigration : IEntityTypeConfiguration<RoomTypePricing>
    {
        public void Configure(EntityTypeBuilder<RoomTypePricing> builder)
        {
            builder.ToTable("RoomTypePricing");
            builder.Property(x => x.RoomTypePricingId).UseIdentityColumn().IsRequired();
            builder.Property(x => x.Price).HasColumnType("Money");
            builder.HasOne(x => x.RoomType).WithMany().HasForeignKey(x => x.RoomTypeId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Season).WithMany().HasForeignKey(x => x.SeasonId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
