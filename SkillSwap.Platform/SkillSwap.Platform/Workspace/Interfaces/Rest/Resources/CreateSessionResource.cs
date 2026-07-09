namespace SkillSwap.Platform.Workspace.Interfaces.Rest.Resources;

/// <summary>
///     Resource for creating a new session
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
/// <param name="InitialMessage">
///     An optional note from the learner explaining the request, shown to the tutor before they
///     accept or reject the session.
/// </param>
public record CreateSessionResource(
    int LearnerId,
    int TutorId,
    string Topic,
    DateTime ScheduledAt,
    int CourseId,
    string InitialMessage);