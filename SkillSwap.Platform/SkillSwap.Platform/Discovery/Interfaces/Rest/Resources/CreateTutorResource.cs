namespace SkillSwap.Platform.Discovery.Interfaces.Rest.Resources;

/// <summary>
///     Resource for creating a new tutor
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user who registers as tutor
/// </param>
/// <param name="Name">
///     The full name of the tutor
/// </param>
/// <param name="University">
///     The university the tutor attends
/// </param>
/// <param name="Career">
///     The career the tutor is studying
/// </param>
/// <param name="Bio">
///     A short biography of the tutor
/// </param>
/// <param name="Skills">
///     The list of skills the tutor offers
/// </param>
/// <param name="AvatarUrl">
///     The URL of the tutor's avatar image
/// </param>
/// <param name="ExperienceYears">
///     The number of years of experience the tutor has
/// </param>
/// <param name="MainSubject">
///     The main subject the tutor teaches
/// </param>
public record CreateTutorResource(
    int UserId,
    string Name,
    string University,
    string Career,
    string Bio,
    IList<string> Skills,
    string AvatarUrl,
    int ExperienceYears,
    string MainSubject);