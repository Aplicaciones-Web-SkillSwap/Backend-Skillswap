using SkillSwap.Platform.Moderation.Application.CommandServices;
using SkillSwap.Platform.Moderation.Domain.Model;
using SkillSwap.Platform.Moderation.Domain.Model.Aggregates;
using SkillSwap.Platform.Moderation.Domain.Model.Commands;
using SkillSwap.Platform.Moderation.Domain.Repositories;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Domain.Repositories;
using SkillSwap.Platform.Shared.Resources.Errors;
using SkillSwap.Platform.Workspace.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace SkillSwap.Platform.Moderation.Application.Internal.CommandServices;

/// <summary>
///     Report command service
/// </summary>
/// <param name="reportRepository">
///     Report repository
/// </param>
/// <param name="sessionRepository">
///     Workspace session repository, used to validate the report references a real session
///     the reporter (and reported user) actually participated in.
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
public class ReportCommandService(
    IReportRepository reportRepository,
    ISessionRepository sessionRepository,
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

        var session = await sessionRepository.FindByIdAsync(command.SessionId, cancellationToken);
        if (session is null)
            return Result<Report>.Failure(ModerationError.SessionNotFound,
                _localizer[nameof(ModerationError.SessionNotFound)]);

        var reporterIsLearner = session.SessionLearnerId.UserId == command.ReporterUserId;
        var reporterIsTutor = session.SessionTutorId.UserId == command.ReporterUserId;
        if (!reporterIsLearner && !reporterIsTutor)
            return Result<Report>.Failure(ModerationError.ReporterNotSessionParticipant,
                _localizer[nameof(ModerationError.ReporterNotSessionParticipant)]);

        var reportedIsTheOtherParticipant =
            (reporterIsLearner && session.SessionTutorId.UserId == command.ReportedUserId) ||
            (reporterIsTutor && session.SessionLearnerId.UserId == command.ReportedUserId);
        if (!reportedIsTheOtherParticipant)
            return Result<Report>.Failure(ModerationError.ReportedUserNotSessionParticipant,
                _localizer[nameof(ModerationError.ReportedUserNotSessionParticipant)]);

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
