namespace SkillSwap.Platform.Discovery.Interfaces.Rest.Resources;

/// <summary>
///     Resource for updating an existing tutor
/// </summary>
/// <param name="Bio">
///     The updated biography of the tutor
/// </param>
/// <param name="Skills">
///     The updated list of skills the tutor offers
/// </param>
/// <param name="AvatarUrl">
///     The updated URL of the tutor's avatar image
/// </param>
/// <param name="MainSubject">
///     The updated main subject the tutor teaches
/// </param>
/// <param name="Visible">
///     Whether the tutor profile should be visible in search results
/// </param>
public record UpdateTutorResource(
    string Bio,
    IList<string> Skills,
    string AvatarUrl,
    string MainSubject,
    bool Visible);