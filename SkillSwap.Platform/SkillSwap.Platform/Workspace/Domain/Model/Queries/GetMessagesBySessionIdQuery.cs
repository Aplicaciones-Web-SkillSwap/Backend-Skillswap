namespace SkillSwap.Platform.Workspace.Domain.Model.Queries;

/// <summary>
///     Get messages by session id query
/// </summary>
/// <param name="SessionId">
///     The unique identifier of the session
/// </param>
public record GetMessagesBySessionIdQuery(int SessionId);