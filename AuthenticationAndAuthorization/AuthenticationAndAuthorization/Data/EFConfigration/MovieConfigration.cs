using AuthenticationAndAuthorization.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationAndAuthorization.Data.EFConfigration
{
    public class MovieConfigration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");
            builder.Property(p => p.MovieId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Title).HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(50);
        }
    }
}
