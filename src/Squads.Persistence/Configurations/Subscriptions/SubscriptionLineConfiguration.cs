using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squads.Domain.Subscriptions;
using Squads.Domain.Tokens;

namespace Squads.Persistence.Configurations.Subscriptions;

internal class SubscriptionLineConfiguration : IEntityTypeConfiguration<SubscriptionLine>
{
    public void Configure(EntityTypeBuilder<SubscriptionLine> builder)
    {
        builder.OwnsOne(sl => sl.Payment, payment =>
        {
            payment.Property(p => p.Amount);
            payment.Property(p => p.PaidOn);
        });
    }
}
