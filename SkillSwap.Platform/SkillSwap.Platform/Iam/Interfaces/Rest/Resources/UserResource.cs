namespace SkillSwap.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     User resource for REST API
/// </summary>
/// <param name="Id">The unique identifier of the user</param>
/// <param name="Username">The username</param>
/// <param name="Email">The institutional email</param>
/// <param name="Role">The user's role: Learner, Tutor or Coordinator</param>
/// <param name="IsVerified">Whether the user's institutional email has been verified</param>
public record UserResource(int Id, string Username, string Email, string Role, bool IsVerified);
