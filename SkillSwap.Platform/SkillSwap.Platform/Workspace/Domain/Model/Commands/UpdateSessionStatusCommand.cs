namespace SkillSwap.Platform.Workspace.Domain.Model.Commands;

/// <summary>
///     Update Session Status Command
/// </summary>
/// <param name="SessionId">
///     The unique identifier of the session to update
/// </param>
/// <param name="Status">
///     The new status of the session
/// </param>
public record UpdateSessionStatusCommand(
    int SessionId,
    string Status);