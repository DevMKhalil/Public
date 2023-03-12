using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsingIdentityWithApi.Logic.api;

namespace UsingIdentityWithApi.Data.EFConfigration
{
    public class ApiUserTokensConfigration : IEntityTypeConfiguration<ApiUserTokens>
    {
        public void Configure(EntityTypeBuilder<ApiUserTokens> builder)
        {
            builder.ToTable("ApiUserTokens");
        }
    }
}
