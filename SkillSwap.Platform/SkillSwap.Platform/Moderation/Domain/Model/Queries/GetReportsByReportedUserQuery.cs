namespace SkillSwap.Platform.Moderation.Domain.Model.Queries;

/// <summary>
///     Get reports by reported user query
/// </summary>
/// <param name="ReportedUserId">
///     The unique identifier of the reported user.
/// </param>
public record GetReportsByReportedUserQuery(int ReportedUserId);
