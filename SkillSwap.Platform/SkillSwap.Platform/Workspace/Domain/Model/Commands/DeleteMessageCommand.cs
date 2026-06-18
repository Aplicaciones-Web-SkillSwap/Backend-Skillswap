namespace SkillSwap.Platform.Workspace.Domain.Model.Commands;

/// <summary>
///     Delete Message Command
/// </summary>
/// <param name="MessageId">
///     The unique identifier of the message to delete
/// </param>
public record DeleteMessageCommand(int MessageId);