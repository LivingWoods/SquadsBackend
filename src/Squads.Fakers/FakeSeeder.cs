using Squads.Fakers.Data.Sessions;
using Squads.Fakers.Sessions;
using Squads.Domain.Sessions;
using Squads.Fakers.Users;
using Squads.Persistence;

namespace Squads.Fakers;

public class FakeSeeder
{
    private readonly SquadsDbContext _dbContext;

	public FakeSeeder(SquadsDbContext context)
	{
		this._dbContext = context;
	}

	public void Seed()
	{
		SeedUsers();
		SeedSessions();
	}

	public void SeedUsers()
	{
        _dbContext.Users.Add(new UserFaker.TraineeFaker().AsTransient());
		_dbContext.Users.Add(new UserFaker.TrainerFaker().AsTransient());
		_dbContext.SaveChanges();
    }

	public void SeedSessions()
	{
        foreach (Session session in new Week2().Sessions) 
		{
            session.ChangeTrainer(_dbContext.Users.Where(u => u.IsTrainer).First());
			_dbContext.Sessions.Add(new SessionFaker(session).AsTransient());
		}
        foreach (Session session in new Week3().Sessions)
        {
            session.ChangeTrainer(_dbContext.Users.Where(u => u.IsTrainer).First());
            _dbContext.Sessions.Add(new SessionFaker(session).AsTransient());
        }
		_dbContext.SaveChanges();
	}
}
