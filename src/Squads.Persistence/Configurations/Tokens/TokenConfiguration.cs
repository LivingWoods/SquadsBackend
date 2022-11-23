using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squads.Domain.Tokens;

namespace Squads.Persistence.Configurations.Tokens;

internal class TokenConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.OwnsOne(t => t.Payment, payment =>
        {
            payment.Property(p => p.Amount);
            payment.Property(p => p.PaidOn);
        });
    }
}
