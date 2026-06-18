namespace SkillSwap.Platform.Discovery.Domain.Model.ValueObjects;

/// <summary>
///     Tutor skills value object
/// </summary>
/// <param name="Skills">
///     The list of skills the tutor offers
/// </param>
public record TutorSkills(IList<string> Skills)
{
    public TutorSkills() : this(new List<string>())
    {
    }
}