namespace SkillSwap.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned after a successful sign-in, including the issued JWT
/// </summary>
/// <param name="Id">The unique identifier of the user</param>
/// <param name="Username">The username</param>
/// <param name="Email">The institutional email</param>
/// <param name="Role">The user's role: Learner, Tutor or Coordinator</param>
/// <param name="IsVerified">Whether the user's institutional email has been verified</param>
/// <param name="Token">The signed JWT to use in the Authorization header</param>
public record AuthenticatedUserResource(
    int Id,
    string Username,
    string Email,
    string Role,
    bool IsVerified,
    string Token);
