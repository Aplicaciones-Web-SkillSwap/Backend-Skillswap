using SkillSwap.Platform.Payments.Domain.Model.Commands;
using SkillSwap.Platform.Payments.Domain.Model.ValueObjects;

namespace SkillSwap.Platform.Payments.Domain.Model.Aggregates;

/// <summary>
///     Wallet Aggregate Root
/// </summary>
/// <remarks>
///     This class represents the Wallet aggregate root.
///     It contains the properties and methods to manage a user's wallet balance.
/// </remarks>
public partial class Wallet
{
    public Wallet()
    {
        WalletOwnerId = new OwnerId();
        Balance = 0m;
    }

    public Wallet(CreateWalletCommand command)
    {
        WalletOwnerId = new OwnerId(command.UserId);
        Balance = 0m;
    }

    public int Id { get; private set; }
    public OwnerId WalletOwnerId { get; private set; }
    public decimal Balance { get; private set; }

    public void AddFunds(AddFundsCommand command)
    {
        Balance += command.Amount;
    }

    public void Deduct(decimal amount)
    {
        Balance -= amount;
    }
}