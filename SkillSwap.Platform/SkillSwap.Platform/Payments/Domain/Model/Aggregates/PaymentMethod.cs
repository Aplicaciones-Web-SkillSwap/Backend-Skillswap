using SkillSwap.Platform.Payments.Domain.Model.Commands;

namespace SkillSwap.Platform.Payments.Domain.Model.Aggregates;

/// <summary>
///     Payment Method Aggregate Root
/// </summary>
/// <remarks>
///     This class represents a user's saved, simulated payment method (card, bank account or
///     Yape). Only a masked display label is stored — this app never sets up or processes any
///     real payment mechanism.
/// </remarks>
public partial class PaymentMethod
{
    public PaymentMethod()
    {
        Type = string.Empty;
        DisplayLabel = string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    public PaymentMethod(SavePaymentMethodCommand command)
    {
        UserId = command.UserId;
        Type = command.Type;
        DisplayLabel = command.DisplayLabel;
        CreatedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Type { get; private set; }
    public string DisplayLabel { get; private set; }
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    ///     Replaces this payment method's details, since a user may only have one saved at a time.
    /// </summary>
    public void Replace(SavePaymentMethodCommand command)
    {
        Type = command.Type;
        DisplayLabel = command.DisplayLabel;
        CreatedAt = DateTime.UtcNow;
    }
}
