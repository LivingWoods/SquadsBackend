using Ardalis.GuardClauses;
using Squads.Domain.Common;
using Squads.Domain.Exercises;

namespace Squads.Domain.PersonalRecords;

public class PersonalRecord : Entity
{
    public DateTime AchievedOn { get; private set; }
    public int Reps { get; private set; }
    public double WeightUsed { get; private set; }
    public Exercise Exercise { get; private set; }

    /// <summary>
    /// EF constructor
    /// </summary>
    protected PersonalRecord() { }
    public PersonalRecord(DateTime achievedOn, int reps, double weightUsed, Exercise exercise)
    {
        AchievedOn = Guard.Against.OutOfRange(achievedOn, nameof(achievedOn), DateTime.UtcNow, DateTime.MaxValue);
        Reps = Guard.Against.NegativeOrZero(reps, nameof(reps));
        WeightUsed = Guard.Against.NegativeOrZero(weightUsed);
        Exercise = Guard.Against.Null(exercise);
    }
}
