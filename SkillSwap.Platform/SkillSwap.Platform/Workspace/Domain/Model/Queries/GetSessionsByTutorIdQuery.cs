namespace SkillSwap.Platform.Workspace.Domain.Model.Queries;

/// <summary>
///     Get sessions by tutor id query
/// </summary>
/// <param name="TutorId">
///     The unique identifier of the tutor
/// </param>
public record GetSessionsByTutorIdQuery(int TutorId);