using Ardalis.GuardClauses;
using Squads.Domain.Common;

namespace Squads.Domain.Measurements;

public class Measurement : Entity
{
    public double Weight { get; }
    public double FatPercentage { get; }
    public double MusclePercentage { get; }
    public double WaistCircumference { get; }

    /// <summary>
    /// EF Constructor
    /// </summary>
    protected Measurement() { }
    /// <summary>
    /// Validates and creates a meausurement
    /// </summary>
    /// <param name="weight"></param>
    /// <param name="fatPercentage"></param>
    /// <param name="musclePercentage"></param>
    /// <param name="waistCircumference"></param>
    public Measurement(double weight, double fatPercentage, double musclePercentage, double waistCircumference)
    {
        Weight = Guard.Against.NegativeOrZero(weight, nameof(weight));
        FatPercentage = Guard.Against.NegativeOrZero(fatPercentage, nameof(fatPercentage));
        MusclePercentage = Guard.Against.NegativeOrZero(musclePercentage, nameof(musclePercentage));
        WaistCircumference = Guard.Against.NegativeOrZero(waistCircumference, nameof(waistCircumference));
    }

    //public double CalculateBmi(int length)
    //{
    //    return Weight / (length * length);
    //}
}
