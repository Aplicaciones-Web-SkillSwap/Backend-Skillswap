namespace SkillSwap.Platform.Workspace.Domain.Model.Queries;

/// <summary>
///     Get session by id query
/// </summary>
/// <param name="SessionId">
///     The unique identifier of the session
/// </param>
public record GetSessionByIdQuery(int SessionId);