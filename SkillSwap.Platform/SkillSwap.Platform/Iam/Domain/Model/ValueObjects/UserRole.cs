namespace SkillSwap.Platform.Iam.Domain.Model.ValueObjects;

/// <summary>
///     The role assigned to a user, controlling which parts of SkillSwap they can access
/// </summary>
public enum UserRole
{
    Learner,
    Tutor,
    Coordinator
}
