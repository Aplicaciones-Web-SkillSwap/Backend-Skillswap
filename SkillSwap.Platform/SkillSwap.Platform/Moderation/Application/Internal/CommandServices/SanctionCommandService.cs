using SkillSwap.Platform.Moderation.Application.CommandServices;
using SkillSwap.Platform.Moderation.Domain.Model;
using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Moderation.Domain.Model.Commands;
using SkillSwap.Platform.Moderation.Domain.Repositories;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Domain.Repositories;
using SkillSwap.Platform.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace SkillSwap.Platform.Moderation.Application.Internal.CommandServices;

/// <summary>
///     Sanction command service
/// </summary>
/// <param name="sanctionRepository">
///     Sanction repository
/// </param>
/// <param name="reportRepository">
///     Report repository, used to validate that the related report exists
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
public class SanctionCommandService(
    ISanctionRepository sanctionRepository,
    IReportRepository reportRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : ISanctionCommandService
{
    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    /// <inheritdoc />
    public async Task<Result<Sanction>> Handle(CreateSanctionCommand command, CancellationToken cancellationToken)
    {
        var report = await reportRepository.FindByIdAsync(command.ReportId, cancellationToken);
        if (report is null)
            return Result<Sanction>.Failure(ModerationError.ReportNotFound,
                _localizer[nameof(ModerationError.ReportNotFound)]);

        if (command.DurationDays < 0)
            return Result<Sanction>.Failure(ModerationError.InvalidSanctionDuration,
                _localizer[nameof(ModerationError.InvalidSanctionDuration)]);

        var sanction = new Sanction(command);
        try
        {
            await sanctionRepository.AddAsync(sanction, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Sanction>.Success(sanction);
        }
        catch (OperationCanceledException)
        {
            return Result<Sanction>.Failure(ModerationError.OperationCancelled,
                _localizer[nameof(ModerationError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Sanction>.Failure(ModerationError.DatabaseError,
                _localizer[nameof(ModerationError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Sanction>.Failure(ModerationError.InternalServerError,
                _localizer[nameof(ModerationError.InternalServerError)]);
        }
    }
}
