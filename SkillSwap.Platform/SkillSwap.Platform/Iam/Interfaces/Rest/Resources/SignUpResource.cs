namespace SkillSwap.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource for signing up a new user
/// </summary>
/// <param name="Username">The desired username</param>
/// <param name="Email">The institutional email (must belong to the .edu.pe domain)</param>
/// <param name="Password">The plain-text password</param>
/// <param name="Role">The role the user is registering as: Learner, Tutor or Coordinator</param>
public record SignUpResource(string Username, string Email, string Password, string Role);
