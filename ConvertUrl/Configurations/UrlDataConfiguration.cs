using ConvertUrlRepository.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConvertUrlRepository.Configurations
{
    public class UrlDataConfiguration : BaseEntityConfiguration<UrlData>
    {
        public override void Configure(EntityTypeBuilder<UrlData> builder)
        {
            base.Configure(builder);
            builder?.Property(x => x.LongUrl).HasMaxLength(300).IsRequired();
            builder?.Property(x => x.ShortUrl).HasMaxLength(8).IsRequired();
        }
    }
}
