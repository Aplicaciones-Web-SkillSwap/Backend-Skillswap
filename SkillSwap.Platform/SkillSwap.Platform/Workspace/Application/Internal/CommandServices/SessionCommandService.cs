using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Domain.Repositories;
using SkillSwap.Platform.Shared.Resources.Errors;
using SkillSwap.Platform.Workspace.Application.CommandServices;
using SkillSwap.Platform.Workspace.Domain.Model;
using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Workspace.Domain.Model.Commands;
using SkillSwap.Platform.Workspace.Domain.Repositories;

namespace SkillSwap.Platform.Workspace.Application.Internal.CommandServices;

/// <summary>
///     Session command service
/// </summary>
/// <param name="sessionRepository">
///     Session repository
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
/// <param name="localizer">
///     String localizer for error messages
/// </param>
public class SessionCommandService(
    ISessionRepository sessionRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : ISessionCommandService
{
    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    /// <inheritdoc />
    public async Task<Result<Session>> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
    {
        if (command.LearnerId == command.TutorId)
            return Result<Session>.Failure(WorkspaceError.SelfSessionNotAllowed,
                _localizer[nameof(WorkspaceError.SelfSessionNotAllowed)]);

        var session = new Session(command);
        try
        {
            await sessionRepository.AddAsync(session, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Session>.Success(session);
        }
        catch (OperationCanceledException)
        {
            return Result<Session>.Failure(WorkspaceError.OperationCancelled,
                _localizer[nameof(WorkspaceError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Session>.Failure(WorkspaceError.DatabaseError,
                _localizer[nameof(WorkspaceError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Session>.Failure(WorkspaceError.InternalServerError,
                _localizer[nameof(WorkspaceError.InternalServerError)]);
        }
    }

    /// <inheritdoc />
    public async Task<Result<Session>> Handle(UpdateSessionStatusCommand command, CancellationToken cancellationToken)
    {
        var session = await sessionRepository.FindByIdAsync(command.SessionId, cancellationToken);
        if (session is null)
            return Result<Session>.Failure(WorkspaceError.SessionNotFound,
                _localizer[nameof(WorkspaceError.SessionNotFound)]);

        var isTutor = command.ActorUserId == session.SessionTutorId.UserId;
        var isLearner = command.ActorUserId == session.SessionLearnerId.UserId;
        if (!isTutor && !isLearner)
            return Result<Session>.Failure(WorkspaceError.NotSessionParticipant,
                _localizer[nameof(WorkspaceError.NotSessionParticipant)]);

        var transitionAllowed = (session.Status, command.Status) switch
        {
            ("pending", "scheduled")   => isTutor,
            ("pending", "rejected")    => isTutor,
            ("pending", "cancelled")   => isTutor || isLearner,
            ("scheduled", "cancelled") => isTutor || isLearner,
            ("scheduled", "completed") => isTutor || isLearner,
            _ => false
        };
        if (!transitionAllowed)
            return Result<Session>.Failure(WorkspaceError.InvalidSessionStatus,
                _localizer[nameof(WorkspaceError.InvalidSessionStatus)]);

        session.UpdateStatus(command);
        try
        {
            sessionRepository.Update(session);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Session>.Success(session);
        }
        catch (OperationCanceledException)
        {
            return Result<Session>.Failure(WorkspaceError.OperationCancelled,
                _localizer[nameof(WorkspaceError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Session>.Failure(WorkspaceError.DatabaseError,
                _localizer[nameof(WorkspaceError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Session>.Failure(WorkspaceError.InternalServerError,
                _localizer[nameof(WorkspaceError.InternalServerError)]);
        }
    }
}