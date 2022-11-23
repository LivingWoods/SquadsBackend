using Microsoft.EntityFrameworkCore;
using Squads.Persistence.Triggers;
using Squads.Domain.Reservations;
using Squads.Domain.Sessions;
using Squads.Domain.Users;
using System.Reflection;
using Squads.Domain.Tokens;

namespace Squads.Persistence;

public class SquadsDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Token> Tokens => Set<Token>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseInMemoryDatabase(databaseName: "SquadsDB");
        optionsBuilder.UseTriggers(options =>
        {
            options.AddTrigger<EntityBeforeSaveTrigger>();
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
