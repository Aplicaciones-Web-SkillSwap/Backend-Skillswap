using SkillSwap.Platform.Moderation.Domain.Model.Commands;
using SkillSwap.Platform.Moderation.Domain.Model.ValueObjects;

namespace SkillSwap.Platform.Moderation.Domain.Model.Aggregates;

/// <summary>
///     Sanction Aggregate Root
/// </summary>
/// <remarks>
///     This class represents the Sanction aggregate root.
///     It contains the properties and methods to manage a sanction applied to a user as a result of a report.
/// </remarks>
public partial class Sanction
{
    public Sanction()
    {
        SanctionedUserId = new SanctionedUserId();
        Type = "warning";
        Description = string.Empty;
    }

    public Sanction(CreateSanctionCommand command)
    {
        ReportId = command.ReportId;
        SanctionedUserId = new SanctionedUserId(command.SanctionedUserId);
        Type = command.Type;
        Description = command.Description;
        DurationDays = command.DurationDays;
    }

    public int Id { get; private set; }
    public int ReportId { get; private set; }
    public SanctionedUserId SanctionedUserId { get; private set; }
    public string Type { get; private set; }
    public string Description { get; private set; }
    public int DurationDays { get; private set; }

    public bool IsBan => Type == "ban";
}
