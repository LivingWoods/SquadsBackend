using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squads.Domain.Users;

namespace Squads.Persistence.Configurations.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(u => u.Email).Property(e => e.Value);
        builder.OwnsOne(u => u.PhoneNumber).Property(p => p.Value);
        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.AddressLine1);
            address.Property(a => a.AddressLine2);
            address.Property(a => a.ZipCode);
            address.Property(a => a.City);
        });

        //builder.OwnsMany(u => u.Reservations).HasOne(r => r.User);
    }
}
