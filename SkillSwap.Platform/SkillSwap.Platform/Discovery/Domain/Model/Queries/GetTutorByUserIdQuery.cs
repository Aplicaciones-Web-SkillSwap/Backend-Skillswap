namespace SkillSwap.Platform.Discovery.Domain.Model.Queries;

/// <summary>
///     Get tutor by user id query
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user
/// </param>
public record GetTutorByUserIdQuery(int UserId);