namespace SkillSwap.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;

/// <summary>
///     JWT settings bound from the "TokenSettings" configuration section
/// </summary>
public class TokenSettings
{
    public required string Secret { get; set; }
}
