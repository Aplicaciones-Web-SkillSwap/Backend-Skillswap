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
///     Message command service
/// </summary>
/// <param name="messageRepository">
///     Message repository
/// </param>
/// <param name="unitOfWork">
///     Unit of work
/// </param>
/// <param name="localizer">
///     String localizer for error messages
/// </param>
public class MessageCommandService(
    IMessageRepository messageRepository,
    ISessionRepository sessionRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : IMessageCommandService
{
    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    /// <inheritdoc />
    public async Task<Result<Message>> Handle(CreateMessageCommand command, CancellationToken cancellationToken)
    {
        var session = await sessionRepository.FindByIdAsync(command.SessionId, cancellationToken);
        if (session is null)
            return Result<Message>.Failure(WorkspaceError.SessionNotFound,
                _localizer[nameof(WorkspaceError.SessionNotFound)]);

        var isTutor = session.SessionTutorId.UserId == command.SenderId;
        var isLearner = session.SessionLearnerId.UserId == command.SenderId;
        if (!isTutor && !isLearner)
            return Result<Message>.Failure(WorkspaceError.NotSessionParticipant,
                _localizer[nameof(WorkspaceError.NotSessionParticipant)]);

        if (command.QuizId.HasValue && !isTutor)
            return Result<Message>.Failure(WorkspaceError.OnlyTutorCanShareQuiz,
                _localizer[nameof(WorkspaceError.OnlyTutorCanShareQuiz)]);

        var message = new Message(command);
        try
        {
            await messageRepository.AddAsync(message, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Message>.Success(message);
        }
        catch (OperationCanceledException)
        {
            return Result<Message>.Failure(WorkspaceError.OperationCancelled,
                _localizer[nameof(WorkspaceError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Message>.Failure(WorkspaceError.DatabaseError,
                _localizer[nameof(WorkspaceError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Message>.Failure(WorkspaceError.InternalServerError,
                _localizer[nameof(WorkspaceError.InternalServerError)]);
        }
    }

    /// <inheritdoc />
    public async Task<Result> Handle(DeleteMessageCommand command, CancellationToken cancellationToken)
    {
        var message = await messageRepository.FindByIdAsync(command.MessageId, cancellationToken);
        if (message is null)
            return Result.Failure(WorkspaceError.MessageNotFound,
                _localizer[nameof(WorkspaceError.MessageNotFound)]);

        try
        {
            messageRepository.Remove(message);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(WorkspaceError.OperationCancelled,
                _localizer[nameof(WorkspaceError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(WorkspaceError.DatabaseError,
                _localizer[nameof(WorkspaceError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result.Failure(WorkspaceError.InternalServerError,
                _localizer[nameof(WorkspaceError.InternalServerError)]);
        }
    }
}