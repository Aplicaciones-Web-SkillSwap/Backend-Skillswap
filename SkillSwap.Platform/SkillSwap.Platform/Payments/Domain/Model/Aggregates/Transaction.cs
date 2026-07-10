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
        AmountSent = 0m;
        PlatformFee = 0m;
        Type = string.Empty;
        Description = string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    public Transaction(CreateTransactionCommand command)
    {
        WalletId = command.WalletId;
        Amount = command.Amount;
        AmountSent = command.AmountSent;
        PlatformFee = command.PlatformFee;
        Type = command.Type;
        Description = command.Description;
        CreatedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public int WalletId { get; private set; }

    /// <summary>
    ///     The net amount this transaction added to the wallet (e.g. 95% of a donation, after the platform fee).
    /// </summary>
    public decimal Amount { get; private set; }

    /// <summary>
    ///     The original gross amount sent by the payer, before the platform fee. Equal to <see cref="Amount" />
    ///     for transactions that don't carry a fee (e.g. manual/generic transactions).
    /// </summary>
    public decimal AmountSent { get; private set; }

    /// <summary>
    ///     The platform fee deducted from <see cref="AmountSent" /> to produce <see cref="Amount" />.
    /// </summary>
    public decimal PlatformFee { get; private set; }

    public string Type { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
}