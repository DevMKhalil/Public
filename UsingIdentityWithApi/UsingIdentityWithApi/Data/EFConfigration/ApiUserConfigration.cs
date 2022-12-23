using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsingIdentityWithApi.Logic;

namespace UsingIdentityWithApi.Data.EFConfigration
{
    public class ApiUserConfigration : IEntityTypeConfiguration<ApiUser>
    {
        public void Configure(EntityTypeBuilder<ApiUser> builder)
        {
            builder.ToTable("ApiUser");
            //builder.HasKey(p => p.UserId);
            builder.Property(p => p.Id).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Id).HasMaxLength(450);
            builder.Property(p => p.UserName).HasMaxLength(256);
            builder.Property(p => p.NormalizedUserName).HasMaxLength(256);
            builder.Property(p => p.PasswordHash);
        }
    }
}
