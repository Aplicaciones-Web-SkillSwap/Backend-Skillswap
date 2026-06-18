namespace SkillSwap.Platform.Workspace.Domain.Model.Commands;

/// <summary>
///     Create Session Command
/// </summary>
/// <param name="LearnerId">
///     The unique identifier of the learner who requests the session
/// </param>
/// <param name="TutorId">
///     The unique identifier of the tutor who will teach the session
/// </param>
/// <param name="Topic">
///     The topic of the session
/// </param>
/// <param name="ScheduledAt">
///     The date and time the session is scheduled for
/// </param>
/// <param name="CourseId">
///     The unique identifier of the course the session is related to
/// </param>
public record CreateSessionCommand(
    int LearnerId,
    int TutorId,
    string Topic,
    DateTime ScheduledAt,
    int CourseId);