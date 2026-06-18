namespace SkillSwap.Platform.Discovery.Domain.Model.Queries;

/// <summary>
///     Get tutors by skill query
/// </summary>
/// <param name="Skill">
///     The skill to filter tutors by
/// </param>
public record GetTutorsBySkillQuery(string Skill);