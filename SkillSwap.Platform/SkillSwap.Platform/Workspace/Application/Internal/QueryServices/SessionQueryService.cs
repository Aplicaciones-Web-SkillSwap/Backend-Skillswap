using SkillSwap.Platform.Workspace.Application.QueryServices;
using SkillSwap.Platform.Workspace.Domain.Model.Aggregates;
using SkillSwap.Platform.Workspace.Domain.Model.Queries;
using SkillSwap.Platform.Workspace.Domain.Repositories;

namespace SkillSwap.Platform.Workspace.Application.Internal.QueryServices;

/// <summary>
///     Session query service
/// </summary>
/// <param name="sessionRepository">
///     Session repository
/// </param>
public class SessionQueryService(ISessionRepository sessionRepository) : ISessionQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Session>> Handle(GetAllSessionsQuery query, CancellationToken cancellationToken)
    {
        return await sessionRepository.ListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Session?> Handle(GetSessionByIdQuery query, CancellationToken cancellationToken)
    {
        return await sessionRepository.FindByIdAsync(query.SessionId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Session>> Handle(GetSessionsByLearnerIdQuery query, CancellationToken cancellationToken)
    {
        return await sessionRepository.FindByLearnerIdAsync(query.LearnerId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Session>> Handle(GetSessionsByTutorIdQuery query, CancellationToken cancellationToken)
    {
        return await sessionRepository.FindByTutorIdAsync(query.TutorId, cancellationToken);
    }
}