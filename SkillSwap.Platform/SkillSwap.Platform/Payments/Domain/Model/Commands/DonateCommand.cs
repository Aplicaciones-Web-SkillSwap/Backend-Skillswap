namespace SkillSwap.Platform.Payments.Domain.Model.Commands;

/// <summary>
///     Donate Command
/// </summary>
/// <param name="FromUserId">
///     The unique identifier of the user (student) sending the donation
/// </param>
/// <param name="ToUserId">
///     The unique identifier of the user (tutor) receiving the donation
/// </param>
/// <param name="Amount">
///     The total amount sent by the student
/// </param>
/// <param name="Description">
///     The description of the donation
/// </param>
public record DonateCommand(
    int FromUserId,
    int ToUserId,
    decimal Amount,
    string Description);