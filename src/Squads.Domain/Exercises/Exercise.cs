using Ardalis.GuardClauses;
using Squads.Domain.Common;

namespace Squads.Domain.Exercises;

public class Exercise : Entity
{
    public string Name { get; set; }
    public string Explanation { get; set; }
    public string ImageUrl { get; set; }
                                                                                                   
    public Exercise(string name, string explanation, string imageUrl)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
        Explanation = Guard.Against.NullOrEmpty(explanation, nameof(explanation));
        ImageUrl = Guard.Against.NullOrEmpty(imageUrl, nameof(imageUrl));
    }
}
