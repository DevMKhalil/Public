using HotelReservation.Logic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.EFConfigration
{
    public class SeasonConfigration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.ToTable("Season");
            builder.Property(x => x.SeasonId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50);
        }
    }
}
