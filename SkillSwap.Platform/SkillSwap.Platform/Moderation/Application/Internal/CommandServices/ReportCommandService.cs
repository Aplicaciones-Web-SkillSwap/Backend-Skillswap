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
///     Report command service
/// </summary>
/// <param name="reportRepository">
///     Report repository
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
public class ReportCommandService(
    IReportRepository reportRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : IReportCommandService
{
    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    /// <inheritdoc />
    public async Task<Result<Report>> Handle(CreateReportCommand command, CancellationToken cancellationToken)
    {
        if (command.ReporterUserId == command.ReportedUserId)
            return Result<Report>.Failure(ModerationError.SelfReportNotAllowed,
                _localizer[nameof(ModerationError.SelfReportNotAllowed)]);

        var alreadyExists = await reportRepository.ExistsPendingReportAsync(
            command.ReporterUserId, command.ReportedUserId, cancellationToken);
        if (alreadyExists)
            return Result<Report>.Failure(ModerationError.DuplicatePendingReport,
                _localizer[nameof(ModerationError.DuplicatePendingReport)]);

        var report = new Report(command);
        try
        {
            await reportRepository.AddAsync(report, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Report>.Success(report);
        }
        catch (OperationCanceledException)
        {
            return Result<Report>.Failure(ModerationError.OperationCancelled,
                _localizer[nameof(ModerationError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Report>.Failure(ModerationError.DatabaseError,
                _localizer[nameof(ModerationError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Report>.Failure(ModerationError.InternalServerError,
                _localizer[nameof(ModerationError.InternalServerError)]);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Report>> Handle(CloseReportCommand command, CancellationToken cancellationToken)
    {
        var report = await reportRepository.FindByIdAsync(command.ReportId, cancellationToken);
        if (report is null)
            return Result<Report>.Failure(ModerationError.ReportNotFound,
                _localizer[nameof(ModerationError.ReportNotFound)]);

        report.Close();
        try
        {
            reportRepository.Update(report);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Report>.Success(report);
        }
        catch (Exception)
        {
            return Result<Report>.Failure(ModerationError.InternalServerError,
                _localizer[nameof(ModerationError.InternalServerError)]);
        }
    }
}
