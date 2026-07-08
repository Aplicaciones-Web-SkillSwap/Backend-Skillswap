namespace SkillSwap.Platform.Iam.Domain.Model.ValueObjects;

/// <summary>
///     The role assigned to a user, controlling which parts of SkillSwap they can access.
/// </summary>
/// <remarks>
///     SkillSwap accounts are hybrid: a <see cref="Student" /> can act as both learner and
///     tutor from the same account depending on context (e.g. which side of a
///     <c>TutoringSession</c> they are on), so there is no separate Learner/Tutor role.
///     Only <see cref="Coordinator" /> (university staff) is a distinct role, with its own
///     moderation/analytics panel and no participation in the tutoring marketplace itself.
/// </remarks>
public enum UserRole
{
    Student,
    Coordinator
}
