namespace SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;

/// <summary>
///     Message resource for REST API
/// </summary>
/// <param name="Id">
///     The unique identifier of the message
/// </param>
/// <param name="SessionId">
///     The unique identifier of the session the message belongs to
/// </param>
/// <param name="SenderId">
///     The unique identifier of the user who sent the message
/// </param>
/// <param name="Content">
///     The content of the message
/// </param>
/// <param name="FileUrl">
///     The URL of the file attached to the message
/// </param>
/// <param name="FileName">
///     The name of the file attached to the message
/// </param>
/// <param name="SentAt">
///     The date and time the message was sent
/// </param>
public record MessageResource(
    int Id,
    int SessionId,
    int SenderId,
    string Content,
    string FileUrl,
    string FileName,
    DateTime SentAt);