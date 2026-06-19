namespace SkillSwap.Platform.Payments.Interfaces.Rest.Resources;

/// <summary>
///     Resource for making a donation from a student to a tutor
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
public record DonateResource(
    int FromUserId,
    int ToUserId,
    decimal Amount,
    string Description);