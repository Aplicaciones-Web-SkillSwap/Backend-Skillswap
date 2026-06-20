namespace SkillSwap.Platform.Workspace.Domain.Model.Commands;

public record CreateMessageCommand(
    int SessionId,
    int SenderId,
    string Content,
    string? FileUrl,
    string? FileName);