using Squads.Domain.Sessions;

namespace Squads.Fakers.Sessions;

public class SessionFaker : EntityFaker<Session>
{
	public SessionFaker(Session session, string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new Session(
			session.StartDate,
			session.EndDate,
			session.SessionType,
			session.Trainer
		));
	}
}