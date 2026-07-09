namespace SkillSwap.Platform.Discovery.Interfaces.Rest.Resources;

/// <summary>
///     Tutor resource for REST API
/// </summary>
/// <param name="Id">
///     The unique identifier of the tutor
/// </param>
/// <param name="UserId">
///     The unique identifier of the user who is a tutor
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
/// <param name="Rating">
///     The average rating of the tutor
/// </param>
/// <param name="ReviewCount">
///     The number of reviews the tutor has received
/// </param>
/// <param name="Verified">
///     Whether the tutor has been verified
/// </param>
/// <param name="Online">
///     Whether the tutor is currently online
/// </param>
/// <param name="Visible">
///     Whether the tutor profile is visible in search results
/// </param>
public record TutorResource(
    int Id,
    int UserId,
    string Name,
    string University,
    string Career,
    string Bio,
    IList<string> Skills,
    string AvatarUrl,
    int ExperienceYears,
    string MainSubject,
    double Rating,
    int ReviewCount,
    bool Verified,
    bool Online,
    bool Visible);