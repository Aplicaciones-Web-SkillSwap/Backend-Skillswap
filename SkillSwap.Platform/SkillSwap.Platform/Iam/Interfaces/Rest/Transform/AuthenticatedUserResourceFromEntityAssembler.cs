using SkillSwap.Platform.Iam.Domain.Model.Aggregates;
using SkillSwap.Platform.Iam.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="User" /> aggregate and its JWT into an
///     <see cref="AuthenticatedUserResource" />.
/// </summary>
public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User entity, string token)
    {
        return new AuthenticatedUserResource(
            entity.Id,
            entity.Username,
            entity.Email,
            entity.Role.ToString(),
            entity.IsVerified,
            token);
    }
}
