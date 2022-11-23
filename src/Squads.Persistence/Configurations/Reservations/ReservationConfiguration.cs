using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Squads.Domain.Reservations;

namespace Squads.Persistence.Configurations.Reservations;

internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasOne(r => r.User).WithMany(u => u.Reservations).HasForeignKey(r => r.UserId);
        builder.HasOne(r => r.Session).WithMany(s => s.Reservations).HasForeignKey(r => r.SessionId);
        //builder.Navigation(r => r.Session).AutoInclude();
    }
}
