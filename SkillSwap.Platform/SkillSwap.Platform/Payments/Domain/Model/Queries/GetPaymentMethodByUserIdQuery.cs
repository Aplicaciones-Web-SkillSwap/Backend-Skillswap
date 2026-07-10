namespace SkillSwap.Platform.Payments.Domain.Model.Queries;

/// <summary>
///     Get Payment Method by User Id Query
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user
/// </param>
public record GetPaymentMethodByUserIdQuery(int UserId);
