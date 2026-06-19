namespace SkillSwap.Platform.Payments.Domain.Model.Queries;

/// <summary>
///     Get wallet by user id query
/// </summary>
/// <param name="UserId">
///     The unique identifier of the user
/// </param>
public record GetWalletByUserIdQuery(int UserId);