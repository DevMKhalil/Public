using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsingIdentityWithApi.Logic;

namespace UsingIdentityWithApi.Data.EFConfigration
{
    public class AspUserConfigration : IEntityTypeConfiguration<AspUser>
    {
        public void Configure(EntityTypeBuilder<AspUser> builder)
        {
            builder.HasIndex(p => p.Locale).IsUnique(false);
        }
    }
}
