using SkillSwap.Platform.Workspace.Domain.Model.Commands;
using SkillSwap.Platform.Workspace.Domain.Model.ValueObjects;

namespace SkillSwap.Platform.Workspace.Domain.Model.Aggregates;

/// <summary>
///     Session Aggregate Root
/// </summary>
/// <remarks>
///     This class represents the Session aggregate root.
///     It contains the properties and methods to manage a tutoring session.
/// </remarks>
public partial class Session
{
    public Session()
    {
        SessionLearnerId = new LearnerId();
        SessionTutorId = new TutorId();
        Topic = string.Empty;
        Status = "pending";
        ScheduledAt = DateTime.UtcNow;
        CourseId = 0;
        ProposedByUserId = 0;
    }

    public Session(CreateSessionCommand command)
    {
        SessionLearnerId = new LearnerId(command.LearnerId);
        SessionTutorId = new TutorId(command.TutorId);
        Topic = command.Topic;
        Status = "pending";
        ScheduledAt = command.ScheduledAt;
        CourseId = command.CourseId;
        ProposedByUserId = command.LearnerId;
    }

    public int Id { get; private set; }
    public LearnerId SessionLearnerId { get; private set; }
    public TutorId SessionTutorId { get; private set; }
    public string Topic { get; private set; }
    public string Status { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public int CourseId { get; private set; }
    public int ProposedByUserId { get; private set; }

    public bool IsPending => Status == "pending";
    public bool IsScheduled => Status == "scheduled";
    public bool IsCompleted => Status == "completed";

    public void UpdateStatus(UpdateSessionStatusCommand command)
    {
        Status = command.Status;
    }

    public void Reschedule(RescheduleSessionCommand command)
    {
        ScheduledAt = command.NewScheduledAt;
        ProposedByUserId = command.ActorUserId;
    }
}