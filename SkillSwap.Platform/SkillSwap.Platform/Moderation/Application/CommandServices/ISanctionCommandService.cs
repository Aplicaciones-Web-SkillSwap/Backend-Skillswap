using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Moderation.Domain.Model.Commands;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Moderation.Application.CommandServices;

/// <summary>
///     Sanction command service interface
/// </summary>
public interface ISanctionCommandService
{
    /// <summary>
    ///     Handle create sanction command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateSanctionCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the created <see cref="Sanction" />
    /// </returns>
    Task<Result<Sanction>> Handle(CreateSanctionCommand command, CancellationToken cancellationToken);
}
