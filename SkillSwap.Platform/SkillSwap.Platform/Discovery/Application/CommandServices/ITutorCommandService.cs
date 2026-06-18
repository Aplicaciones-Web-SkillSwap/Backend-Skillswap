using SkillSwap.Platform.Discovery.Domain.Model.Aggregates;
using SkillSwap.Platform.Discovery.Domain.Model.Commands;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Discovery.Application.CommandServices;

/// <summary>
///     Tutor command service interface
/// </summary>
public interface ITutorCommandService
{
    /// <summary>
    ///     Handle create tutor command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateTutorCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the created <see cref="Tutor" />
    /// </returns>
    Task<Result<Tutor>> Handle(CreateTutorCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle update tutor command
    /// </summary>
    /// <param name="command">
    ///     The <see cref="UpdateTutorCommand" /> command
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The <see cref="Result{T}" /> wrapping the updated <see cref="Tutor" />
    /// </returns>
    Task<Result<Tutor>> Handle(UpdateTutorCommand command, CancellationToken cancellationToken);
}