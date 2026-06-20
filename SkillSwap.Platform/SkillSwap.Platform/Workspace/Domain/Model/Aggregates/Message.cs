using SkillSwap.Platform.Workspace.Domain.Model.Commands;

namespace SkillSwap.Platform.Workspace.Domain.Model.Aggregates;

/// <summary>
///     Message Aggregate Root
/// </summary>
/// <remarks>
///     This class represents the Message aggregate root.
///     It contains the properties and methods to manage a message in a session.
/// </remarks>
public partial class Message
{
    public Message()
    {
        Content = string.Empty;
        FileUrl = string.Empty;
        FileName = string.Empty;
        SentAt = DateTime.UtcNow;
    }

    public Message(CreateMessageCommand command)
    {
        SessionId = command.SessionId;
        SenderId = command.SenderId;
        Content = command.Content;
        FileUrl = command.FileUrl ?? string.Empty;
        FileName = command.FileName ?? string.Empty;
        SentAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public int SessionId { get; private set; }
    public int SenderId { get; private set; }
    public string Content { get; private set; }
    public string FileUrl { get; private set; }
    public string FileName { get; private set; }
    public DateTime SentAt { get; private set; }
}