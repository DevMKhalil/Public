using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsingIdentityWithApi.Logic;

namespace UsingIdentityWithApi.Data.EFConfigration
{
    //public class IdentityUserConfigration : IEntityTypeConfiguration<IdentityUser>
    //{
    //    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    //    {
    //        builder.ToTable("ApiUsers");
    //        builder.Property(p => p.Id).HasColumnName("Id");
    //        //builder.HasKey(p => p.UserId);

    //        //builder.Property(p => p.Id).IsRequired();
    //        //builder.Property(p => p.Id).HasMaxLength(450);
    //        //builder.Property(p => p.UserName).HasMaxLength(256);
    //        //builder.Property(p => p.NormalizedUserName).HasMaxLength(256);
    //        //builder.Property(p => p.PasswordHash);
    //    }
    //}
}
