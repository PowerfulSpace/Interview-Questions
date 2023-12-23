using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Preparation.Models;

namespace Preparation.Data.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {

        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder
                 .HasMany(x => x.Questions)
                 .WithOne(x => x.Subject);

            builder.Property(x => x.Name).IsRequired();
        }
    }
}
