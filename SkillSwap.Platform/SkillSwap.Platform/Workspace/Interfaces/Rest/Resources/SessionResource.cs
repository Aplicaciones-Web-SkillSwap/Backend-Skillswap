namespace SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;

/// <summary>
///     Session resource for REST API
/// </summary>
/// <param name="Id">
///     The unique identifier of the session
/// </param>
/// <param name="LearnerId">
///     The unique identifier of the learner
/// </param>
/// <param name="TutorId">
///     The unique identifier of the tutor
/// </param>
/// <param name="Topic">
///     The topic of the session
/// </param>
/// <param name="Status">
///     The current status of the session
/// </param>
/// <param name="ScheduledAt">
///     The date and time the session is scheduled for
/// </param>
/// <param name="CourseId">
///     The unique identifier of the course the session is related to
/// </param>
public record SessionResource(
    int Id,
    int LearnerId,
    int TutorId,
    string Topic,
    string Status,
    DateTime ScheduledAt,
    int CourseId);