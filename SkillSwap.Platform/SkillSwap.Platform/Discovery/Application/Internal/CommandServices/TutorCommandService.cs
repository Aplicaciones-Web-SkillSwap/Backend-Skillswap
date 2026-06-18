using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Discovery.Application.CommandServices;
using SkillSwap.Platform.Discovery.Domain.Model;
using SkillSwap.Platform.Discovery.Domain.Model.Aggregates;
using SkillSwap.Platform.Discovery.Domain.Model.Commands;
using SkillSwap.Platform.Discovery.Domain.Repositories;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Domain.Repositories;
using SkillSwap.Platform.Shared.Resources.Errors;

namespace SkillSwap.Platform.Discovery.Application.Internal.CommandServices;

/// <summary>
///     Tutor command service
/// </summary>
/// <param name="tutorRepository">
///     Tutor repository
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
/// <param name="localizer">
///     String localizer for error messages
/// </param>
public class TutorCommandService(
    ITutorRepository tutorRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : ITutorCommandService
{
    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    /// <inheritdoc />
    public async Task<Result<Tutor>> Handle(CreateTutorCommand command, CancellationToken cancellationToken)
    {
        var alreadyExists = await tutorRepository.ExistsByUserIdAsync(command.UserId, cancellationToken);
        if (alreadyExists)
            return Result<Tutor>.Failure(DiscoveryError.TutorAlreadyExists,
                _localizer[nameof(DiscoveryError.TutorAlreadyExists)]);

        var tutor = new Tutor(command);
        try
        {
            await tutorRepository.AddAsync(tutor, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Tutor>.Success(tutor);
        }
        catch (OperationCanceledException)
        {
            return Result<Tutor>.Failure(DiscoveryError.OperationCancelled,
                _localizer[nameof(DiscoveryError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Tutor>.Failure(DiscoveryError.DatabaseError,
                _localizer[nameof(DiscoveryError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Tutor>.Failure(DiscoveryError.InternalServerError,
                _localizer[nameof(DiscoveryError.InternalServerError)]);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Tutor>> Handle(UpdateTutorCommand command, CancellationToken cancellationToken)
    {
        var tutor = await tutorRepository.FindByIdAsync(command.TutorId, cancellationToken);
        if (tutor is null)
            return Result<Tutor>.Failure(DiscoveryError.TutorNotFound,
                _localizer[nameof(DiscoveryError.TutorNotFound)]);

        tutor.Update(command);
        try
        {
            tutorRepository.Update(tutor);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Tutor>.Success(tutor);
        }
        catch (OperationCanceledException)
        {
            return Result<Tutor>.Failure(DiscoveryError.OperationCancelled,
                _localizer[nameof(DiscoveryError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Tutor>.Failure(DiscoveryError.DatabaseError,
                _localizer[nameof(DiscoveryError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Tutor>.Failure(DiscoveryError.InternalServerError,
                _localizer[nameof(DiscoveryError.InternalServerError)]);
        }
    }
}