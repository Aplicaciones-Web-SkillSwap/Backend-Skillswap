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
        CreatedAt = DateTime.UtcNow;
    }

    public Sanction(CreateSanctionCommand command)
    {
        ReportId = command.ReportId;
        SanctionedUserId = new SanctionedUserId(command.SanctionedUserId);
        Type = command.Type;
        Description = command.Description;
        DurationDays = command.DurationDays;
        IsPermanent = command.IsPermanent;
        CreatedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public int ReportId { get; private set; }
    public SanctionedUserId SanctionedUserId { get; private set; }
    public string Type { get; private set; }
    public string Description { get; private set; }
    public int DurationDays { get; private set; }
    public bool IsPermanent { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? AcknowledgedAt { get; private set; }

    public bool IsBan => Type == "ban";

    public bool IsActiveBanAt(DateTime nowUtc) => IsBan && (IsPermanent || CreatedAt.AddDays(DurationDays) > nowUtc);

    public void Acknowledge() => AcknowledgedAt = DateTime.UtcNow;
}
