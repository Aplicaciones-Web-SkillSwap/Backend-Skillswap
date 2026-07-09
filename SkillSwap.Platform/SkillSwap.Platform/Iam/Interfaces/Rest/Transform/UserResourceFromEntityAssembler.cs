using SkillSwap.Platform.Iam.Domain.Model.Aggregates;
using SkillSwap.Platform.Iam.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="User" /> aggregate into a <see cref="UserResource" />.
/// </summary>
public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(entity.Id, entity.Username, entity.Email, entity.Role.ToString(), entity.IsVerified, entity.Bio);
    }
}
