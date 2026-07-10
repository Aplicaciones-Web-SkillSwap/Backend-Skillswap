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

            // Un usuario que acumula 3 advertencias en el mismo mes calendario recibe
            // automáticamente un ban de 1 mes, además de la advertencia recién creada.
            if (command.Type == "warning")
            {
                var monthStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                var userSanctions = await sanctionRepository.FindBySanctionedUserIdAsync(
                    command.SanctionedUserId, cancellationToken);
                var warningsThisMonth = userSanctions.Count(s => s.Type == "warning" && s.CreatedAt >= monthStart);

                if (warningsThisMonth >= 3)
                {
                    var autoBan = new Sanction(command with
                    {
                        Type = "ban",
                        Description = "Ban automático por acumular 3 advertencias este mes.",
                        DurationDays = 30,
                        IsPermanent = false
                    });
                    await sanctionRepository.AddAsync(autoBan, cancellationToken);
                    await unitOfWork.CompleteAsync(cancellationToken);
                }
            }

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

    /// <inheritdoc />
    public async Task<Result<Sanction>> Handle(AcknowledgeSanctionCommand command, CancellationToken cancellationToken)
    {
        var sanction = await sanctionRepository.FindByIdAsync(command.SanctionId, cancellationToken);
        if (sanction is null)
            return Result<Sanction>.Failure(ModerationError.SanctionNotFound,
                _localizer[nameof(ModerationError.SanctionNotFound)]);

        if (sanction.SanctionedUserId.UserId != command.ActorUserId)
            return Result<Sanction>.Failure(ModerationError.NotSanctionOwner,
                _localizer[nameof(ModerationError.NotSanctionOwner)]);

        sanction.Acknowledge();
        try
        {
            sanctionRepository.Update(sanction);
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
