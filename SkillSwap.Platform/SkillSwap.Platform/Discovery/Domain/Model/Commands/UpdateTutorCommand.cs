namespace SkillSwap.Platform.Discovery.Domain.Model.Commands;

/// <summary>
///     Update Tutor Command
/// </summary>
/// <param name="TutorId">
///     The unique identifier of the tutor to update
/// </param>
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
public record UpdateTutorCommand(
    int TutorId,
    string Bio,
    IList<string> Skills,
    string AvatarUrl,
    string MainSubject);