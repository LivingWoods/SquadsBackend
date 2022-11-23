namespace Squads.Fakers.Common;

public class EntityFaker<TEntity> : Faker<TEntity> where TEntity : Entity
{
    protected EntityFaker(string locale = "nl") : base(locale)
    {
        int id = 1;
        RuleFor(x => x.Id, f => id++);
    }

    public EntityFaker<TEntity> AsTransient()
    {
        RuleFor(x => x.Id, f => default);
        return this;
    }
}
