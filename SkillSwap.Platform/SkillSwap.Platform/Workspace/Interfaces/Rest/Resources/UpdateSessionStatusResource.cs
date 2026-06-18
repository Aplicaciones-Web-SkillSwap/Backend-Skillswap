namespace SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;

/// <summary>
///     Resource for updating the status of a session
/// </summary>
/// <param name="Status">
///     The new status of the session (pending, scheduled, completed, cancelled)
/// </param>
public record UpdateSessionStatusResource(string Status);