using SkillSwap.Platform.Payments.Domain.Model.Aggregates;
using SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

namespace SkillSwap.Platform.Payments.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="PaymentMethod" /> aggregate into a
///     <see cref="PaymentMethodResource" />.
/// </summary>
public static class PaymentMethodResourceFromEntityAssembler
{
    public static PaymentMethodResource ToResourceFromEntity(PaymentMethod entity)
    {
        return new PaymentMethodResource(
            entity.Id,
            entity.UserId,
            entity.Type,
            entity.DisplayLabel,
            entity.CreatedAt);
    }
}
