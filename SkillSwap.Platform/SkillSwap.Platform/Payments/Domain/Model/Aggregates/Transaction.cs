using SkillSwap.Platform.Payments.Domain.Model.Commands;

namespace SkillSwap.Platform.Payments.Domain.Model.Aggregates;

/// <summary>
///     Transaction Aggregate Root
/// </summary>
/// <remarks>
///     This class represents the Transaction aggregate root.
///     It contains the properties and methods to manage a wallet transaction.
/// </remarks>
public partial class Transaction
{
    public Transaction()
    {
        Amount = 0m;
        Type = string.Empty;
        Description = string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    public Transaction(CreateTransactionCommand command)
    {
        WalletId = command.WalletId;
        Amount = command.Amount;
        Type = command.Type;
        Description = command.Description;
        CreatedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public int WalletId { get; private set; }
    public decimal Amount { get; private set; }
    public string Type { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
}