using AuthenticationAndAuthorization.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationAndAuthorization.Data.EFConfigration
{
    public class UserModelConfigration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("UserModel");
            //builder.HasKey(p => p.UserId);
            builder.Property(p => p.UserId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.UserName).HasMaxLength(50);
            builder.Property(p => p.Password).HasMaxLength(50);
            builder.Property(p => p.EmailAddress).HasMaxLength(50);
            builder.Property(p => p.Role).HasMaxLength(50);
            builder.Property(p => p.Surname).HasMaxLength(50);
            builder.Property(p => p.GivenName).HasMaxLength(50);
        }
    }
}
