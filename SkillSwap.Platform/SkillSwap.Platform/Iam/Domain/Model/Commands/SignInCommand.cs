namespace SkillSwap.Platform.Iam.Domain.Model.Commands;

/// <summary>
///     Sign in command
/// </summary>
/// <param name="Username">The username to authenticate</param>
/// <param name="Password">The plain-text password to verify</param>
public record SignInCommand(string Username, string Password);
