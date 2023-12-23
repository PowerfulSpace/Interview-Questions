using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Preparation.Models;

namespace Preparation.Data.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder
                 .HasOne(x => x.Subject)
                 .WithMany(x => x.Questions);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Answer).IsRequired();
        }
    }
}
