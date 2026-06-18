namespace SkillSwap.Platform.Workspace.Domain.Model.Commands;

/// <summary>
///     Create Message Command
/// </summary>
/// <param name="SessionId">
///     The unique identifier of the session the message belongs to
/// </param>
/// <param name="SenderId">
///     The unique identifier of the user who sends the message
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
public record CreateMessageCommand(
    int SessionId,
    int SenderId,
    string Content,
    string FileUrl,
    string FileName);