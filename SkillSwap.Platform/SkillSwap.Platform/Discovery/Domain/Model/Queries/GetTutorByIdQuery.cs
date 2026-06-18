namespace SkillSwap.Platform.Discovery.Domain.Model.Queries;

/// <summary>
///     Get tutor by id query
/// </summary>
/// <param name="TutorId">
///     The unique identifier of the tutor
/// </param>
public record GetTutorByIdQuery(int TutorId);