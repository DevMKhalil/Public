﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HotelReservationWithAuthentication.Logic;

namespace HotelReservationWithAuthentication.Data.EFConfigration
{
    public class RoomTypeConfigration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.ToTable("RoomType");
            builder.Property(x => x.RoomTypeId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50);
        }
    }
}
