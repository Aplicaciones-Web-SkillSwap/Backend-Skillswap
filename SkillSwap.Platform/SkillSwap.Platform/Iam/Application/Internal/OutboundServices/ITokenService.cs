using SkillSwap.Platform.Iam.Domain.Model.Aggregates;

namespace SkillSwap.Platform.Iam.Application.Internal.OutboundServices;

/// <summary>
///     Outbound service used to generate and validate JWTs
/// </summary>
public interface ITokenService
{
    /// <summary>
    ///     Generate a signed JWT for the given user
    /// </summary>
    /// <param name="user">The user to generate the token for</param>
    /// <returns>The generated token</returns>
    string GenerateToken(User user);

    /// <summary>
    ///     Validate a JWT and extract the user id it was issued for
    /// </summary>
    /// <param name="token">The token to validate</param>
    /// <returns>The user id if the token is valid; otherwise null</returns>
    Task<int?> ValidateToken(string token);
}
