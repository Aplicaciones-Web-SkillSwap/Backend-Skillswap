namespace SkillSwap.Platform.Iam.Application.Internal.OutboundServices;

/// <summary>
///     Outbound service used to hash and verify passwords
/// </summary>
public interface IHashingService
{
    /// <summary>
    ///     Hash a plain-text password
    /// </summary>
    /// <param name="password">The password to hash</param>
    /// <returns>The hashed password</returns>
    string HashPassword(string password);

    /// <summary>
    ///     Verify a plain-text password against a hash
    /// </summary>
    /// <param name="password">The plain-text password</param>
    /// <param name="passwordHash">The hash to verify against</param>
    /// <returns>True if the password matches the hash; otherwise false</returns>
    bool VerifyPassword(string password, string passwordHash);
}
