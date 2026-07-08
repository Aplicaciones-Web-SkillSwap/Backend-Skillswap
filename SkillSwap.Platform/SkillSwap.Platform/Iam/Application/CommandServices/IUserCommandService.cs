using SkillSwap.Platform.Iam.Domain.Model.Aggregates;
using SkillSwap.Platform.Iam.Domain.Model.Commands;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Iam.Application.CommandServices;

/// <summary>
///     User command service interface
/// </summary>
public interface IUserCommandService
{
    /// <summary>
    ///     Handle sign up command
    /// </summary>
    /// <param name="command">The <see cref="SignUpCommand" /> command</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="Result" /> of the sign-up operation</returns>
    Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle sign in command
    /// </summary>
    /// <param name="command">The <see cref="SignInCommand" /> command</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The <see cref="Result{T}" /> wrapping the authenticated user and its JWT</returns>
    Task<Result<(User User, string Token)>> Handle(SignInCommand command, CancellationToken cancellationToken);
}
