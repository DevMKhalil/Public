using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsingIdentityWithApi.Logic.api;

namespace UsingIdentityWithApi.Data.EFConfigration
{
    public class ApiUserConfigration : IEntityTypeConfiguration<ApiUser>
    {
        public void Configure(EntityTypeBuilder<ApiUser> builder)
        {
            builder.ToTable("ApiUsers");
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Locale).IsUnique(false);
        }
    }
}
